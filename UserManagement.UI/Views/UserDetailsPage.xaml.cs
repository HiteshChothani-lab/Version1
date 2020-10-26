using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using UserManagement.Common.Constants;
using UserManagement.Entity;

namespace UserManagement.UI.Views
{
    /// <summary>
    /// Interaction logic for UserDetailsPage.xaml
    /// </summary>
    public partial class UserDetailsPage : Window, INotifyPropertyChanged
    {
        public StoreUserEntity StoreUser { get; }
        public ObservableCollection<ItemsCollection> ItemCollections { get; set; }

        public string StoreAddress { get => $"{Config.MasterStore.Street}, {Config.MasterStore.Address}, {Config.MasterStore.Country}"; }
        public string EmergencyContact { get; set; }
        public string EmergencyNumber { get; set; }
        public string HealthCardNumber { get; set; }
        public Uri Url { get; set; }

        public UserDetailsPage(StoreUserEntity storeUserEntity)
        {
            InitializeComponent();
            StoreUser = storeUserEntity;
        }

        private void UserDetailsWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var desktopWorkingArea = SystemParameters.WorkArea;
            Top = 0;
            //this.Left = desktopWorkingArea.Right - this.Width; (Display window on right side of screen)
            this.Left = desktopWorkingArea.Left; //(Display window on left side of screen)
            Height = desktopWorkingArea.Height;

            PostalCodeText.Text = StoreUser.IsZipCode ? "Zip :" : "Postal :";

            ItemCollections = new ObservableCollection<ItemsCollection>();

            foreach (var item in StoreUser.VersionForm)
            {
                if (item.Title.Contains("?"))
                {
                    ItemsCollection qs = new ItemsCollection
                    {
                        Question = item.Title
                    };

                    var ans = item.Answers.FirstOrDefault()?.AnsweredText;
                    if (ans != null)
                    {
                        qs.Answer = ans.ToLower().Contains("true");
                        qs.YesForeground = ans.ToLower().Contains("true") ? "White" : "Black";
                        qs.YesBackground = ans.ToLower().Contains("true") ? "#FFA500" : "White";
                        qs.NoForeground = ans.ToLower().Contains("false") ? "White" : "Black";
                        qs.NoBackground = ans.ToLower().Contains("false") ? "#709443" : "White";
                    }
                    else
                    {
                        qs.Answer = false;
                        qs.YesForeground = "Black";
                        qs.YesBackground = "White";
                        qs.NoForeground = "White";
                        qs.NoBackground = "#A3D069";
                    }

                    ItemCollections.Add(qs);
                }
                else
                {
                    if (item.Title.Equals("First Name"))
                        EmergencyContact = item.Answers.FirstOrDefault()?.AnsweredText;
                    else if(item.Title.Equals("Last Name"))
                        EmergencyContact += $" {item.Answers.FirstOrDefault()?.AnsweredText}";
                    else if(item.Title.Equals("Phone Number"))
                        EmergencyNumber = item.Answers.FirstOrDefault()?.AnsweredText;
                    else if (item.Title.Equals("Card number"))
                        HealthCardNumber = item.Answers.FirstOrDefault()?.AnsweredText;
                }
            }

            StoreUser.Gender = StoreUser.Gender.Trim().First().ToString().ToUpper();

            DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyRaised(string propertyname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }

        private void Image_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }
    }

    public class ItemsCollection
    {
        public string Question { get; set; }
        public bool Answer { get; set; }
        public string NoForeground { get; set; }
        public string NoBackground { get; set; }
        public string YesForeground { get; set; }
        public string YesBackground { get; set; }
    }
}
