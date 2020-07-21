using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using System;
using System.Threading.Tasks;
using System.Windows;
using UserManagement.Common.Constants;
using UserManagement.Common.Enums;
using UserManagement.Entity;
using UserManagement.Manager;
using UserManagement.UI.Events;
using UserManagement.UI.ItemModels;

namespace UserManagement.UI.ViewModels
{
    public class EditButtonsPopupPageViewModel : ViewModelBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IWindowsManager _windowsManager;

        public EditButtonsPopupPageViewModel(IRegionManager regionManager, IEventAggregator eventAggregator, IWindowsManager windowsManager) : base(regionManager)
        {
            _eventAggregator = eventAggregator;
            _windowsManager = windowsManager;

            this.CancelCommand = new DelegateCommand(() => ExecuteCancelCommand());
            this.SubmitCommand = new DelegateCommand(async () => await ExecuteSubmitCommand());
            this.VeryTerribleCheckedCommand = new DelegateCommand<string>((user) => ExecuteVeryTerribleCheckedCommand(user));
            this.VeryTerribleUncheckedCommand = new DelegateCommand<string>((user) => ExecuteVeryTerribleUncheckedCommand(user));
        }

        private StoreUserEntity _selectedStoreUser;
        public StoreUserEntity SelectedStoreUser
        {
            get => _selectedStoreUser;
            set => SetProperty(ref _selectedStoreUser, value);
        }

        private bool IsSelectedStoreUser = false;

        private bool _isUserTypeMobile = true;
        public bool IsUserTypeMobile
        {
            get => _isUserTypeMobile;
            set => SetProperty(ref _isUserTypeMobile, value);
        }

        private bool _isUserTypeNonMobile;
        public bool IsUserTypeNonMobile
        {
            get => _isUserTypeNonMobile;
            set => SetProperty(ref _isUserTypeNonMobile, value);
        }

        private bool _isCheckedButtonA;
        public bool IsCheckedButtonA
        {
            get => _isCheckedButtonA;
            set => SetProperty(ref _isCheckedButtonA, value);
        }

        private bool _isCheckedButtonB;
        public bool IsCheckedButtonB
        {
            get => _isCheckedButtonB;
            set => SetProperty(ref _isCheckedButtonB, value);
        }

        private bool _isCheckedButtonC;
        public bool IsCheckedButtonC
        {
            get => _isCheckedButtonC;
            set => SetProperty(ref _isCheckedButtonC, value);
        }

        private bool _isCheckedOther;
        public bool IsCheckedOther
        {
            get => _isCheckedOther;
            set => SetProperty(ref _isCheckedOther, value);
        }

        private bool _isCheckedOtherVaccines;
        public bool IsCheckedOtherVaccines
        {
            get => _isCheckedOtherVaccines;
            set => SetProperty(ref _isCheckedOtherVaccines, value);
        }

        private bool _isCheckedSubButton1;
        public bool IsCheckedSubButton1
        {
            get => _isCheckedSubButton1;
            set => SetProperty(ref _isCheckedSubButton1, value);
        }

        private bool _isCheckedSubButton2;
        public bool IsCheckedSubButton2
        {
            get => _isCheckedSubButton2;
            set => SetProperty(ref _isCheckedSubButton2, value);
        }

        private bool _isCheckedCovid19;
        public bool IsCheckedCovid19
        {
            get => _isCheckedCovid19;
            set => SetProperty(ref _isCheckedCovid19, value);
        }

        private bool _isCheckedVeryTerribleNone;
        public bool IsCheckedVeryTerribleNone
        {
            get => _isCheckedVeryTerribleNone;
            set => SetProperty(ref _isCheckedVeryTerribleNone, value);
        }

        public DelegateCommand CancelCommand { get; private set; }
        public DelegateCommand SubmitCommand { get; private set; }
        public DelegateCommand<string> VeryTerribleCheckedCommand { get; private set; }
        public DelegateCommand<string> VeryTerribleUncheckedCommand { get; private set; }

        private void ExecuteCancelCommand()
        {
            this.RegionNavigationService.Journal.Clear();
            _eventAggregator.GetEvent<EditButtonsSubmitEvent>().Publish(null);
            SetUnsetProperties();
        }

        private void ExecuteVeryTerribleUncheckedCommand(string parameter)
        {
            if (string.IsNullOrEmpty(parameter))
            {
                this.IsCheckedVeryTerribleNone = false;
                this.IsCheckedSubButton1 = false;
                this.IsCheckedSubButton2 = false;
                this.IsCheckedCovid19 = false;
                this.IsCheckedOtherVaccines = false;
            }
            else if (parameter == "1")
            {
                if (this.IsCheckedVeryTerribleNone)
                    this.IsCheckedVeryTerribleNone = this.IsCheckedSubButton2 == false &&
                    this.IsCheckedCovid19 == false && this.IsCheckedOtherVaccines == false;
            }
            else if (parameter == "2")
            {
                if (this.IsCheckedVeryTerribleNone)
                    this.IsCheckedVeryTerribleNone = this.IsCheckedSubButton1 == false &&
                    this.IsCheckedCovid19 == false && this.IsCheckedOtherVaccines == false;
            }
            else if (parameter == "3")
            {
                if (this.IsCheckedVeryTerribleNone)
                    this.IsCheckedVeryTerribleNone = this.IsCheckedSubButton1 == false &&
                    this.IsCheckedSubButton2 == false && this.IsCheckedOtherVaccines == false;
            }
            else if (parameter == "4")
            {
                if (this.IsCheckedVeryTerribleNone)
                    this.IsCheckedVeryTerribleNone = this.IsCheckedSubButton1 == false &&
                    this.IsCheckedSubButton2 == false && this.IsCheckedCovid19 == false;
            }
        }

        private void ExecuteVeryTerribleCheckedCommand(string parameter)
        {
            if (string.IsNullOrEmpty(parameter))
            {
                this.IsCheckedVeryTerribleNone = true;
                this.IsCheckedOtherVaccines = true;
                this.IsCheckedSubButton1 = false;
                this.IsCheckedSubButton2 = false;
                this.IsCheckedCovid19 = false;
            }
            else if (parameter == "1")
            {
                this.IsCheckedVeryTerribleNone = false;
                this.IsCheckedSubButton2 = false;
                this.IsCheckedCovid19 = false;
                this.IsCheckedOtherVaccines = false;
                this.IsCheckedSubButton1 = true;
            }
            else if (parameter == "2")
            {
                this.IsCheckedVeryTerribleNone = false;
                this.IsCheckedSubButton1 = false;
                this.IsCheckedCovid19 = false;
                this.IsCheckedOtherVaccines = false;
                this.IsCheckedSubButton2 = true;
            }
            else if (parameter == "3")
            {
                this.IsCheckedVeryTerribleNone = false;
                this.IsCheckedSubButton1 = false;
                this.IsCheckedSubButton2 = false;
                this.IsCheckedOtherVaccines = false;
                this.IsCheckedCovid19 = true;
            }
            else if (parameter == "4")
            {
                this.IsCheckedVeryTerribleNone = false;
                this.IsCheckedSubButton1 = false;
                this.IsCheckedSubButton2 = false;
                this.IsCheckedCovid19 = false;
                this.IsCheckedOtherVaccines = true;
            }
        }

        private async Task ExecuteSubmitCommand()
        {
            if (IsCheckedCovid19)
            {
                MessageBox.Show("Sorry, COVID-19 is not available at moment.", "Warning", MessageBoxButton.OK);
                return;
            }

            if (!this.IsCheckedButtonA && !this.IsCheckedButtonB && !this.IsCheckedButtonC &&
                !this.IsCheckedVeryTerribleNone && !IsCheckedSubButton1 &&
                !this.IsCheckedSubButton2)
            {
                MessageBox.Show("You must make a selection for Prescriptions or Devices or Vaccines or all.", "Required.");
                return;
            }

            var reqEntity = new UpdateButtonsRequestEntity
            {
                Id = this.SelectedStoreUser.Id,
                UserId = Convert.ToInt32(this.SelectedStoreUser.UserId),
                SuperMasterId = Config.MasterStore.UserId,
                Action = this.IsSelectedStoreUser ? "update_buttons" : "update_buttons_archive"
            };

            reqEntity.Button1 = string.Empty;
            reqEntity.Button2 = string.Empty;
            reqEntity.Button3 = string.Empty;

            if (this.IsCheckedButtonA)
            {
                reqEntity.Button1 = "Prescriptions";
            }

            if (this.IsCheckedButtonB)
            {
                reqEntity.Button2 = "Devices";
            }

            if (this.IsCheckedButtonC)
            {
                reqEntity.Button3 = "Flu Shot";
            }

            reqEntity.Button4 = string.Empty;

            if (this.IsCheckedVeryTerribleNone || IsCheckedOtherVaccines)
            {
                reqEntity.Button4 = "Other Vaccines";
            }
            else if (this.IsCheckedSubButton1)
            {
                reqEntity.Button4 = "Shingles";
            }
            else if (this.IsCheckedSubButton2)
            {
                reqEntity.Button4 = "Pneumococcus";
            }

            var result = await _windowsManager.UpdateButtons(reqEntity);

            if (result.StatusCode == (int)GenericStatusValue.Success)
            {
                if (Convert.ToBoolean(result.Status))
                {
                    this.RegionNavigationService.Journal.Clear();
                    _eventAggregator.GetEvent<EditButtonsSubmitEvent>().Publish(new EditButtonsItemModel());
                    SetUnsetProperties();
                }
                else
                {
                    MessageBox.Show(result.Messagee, "Unsuccessful");
                }
            }
            else if (result.StatusCode == (int)GenericStatusValue.NoInternetConnection)
            {
                MessageBox.Show(MessageBoxMessage.NoInternetConnection, "Unsuccessful");
            }
            else if (result.StatusCode == (int)GenericStatusValue.HasErrorMessage)
            {
                MessageBox.Show(result.Message, "Unsuccessful");
            }
            else
            {
                MessageBox.Show(MessageBoxMessage.UnknownErorr, "Unsuccessful");
            }
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            SetUnsetProperties();

            if (navigationContext.Parameters[NavigationConstants.SelectedStoreUser] is StoreUserEntity selectedStoreUser)
            {
                SelectedStoreUser = selectedStoreUser;
            }

            if (navigationContext.Parameters[NavigationConstants.IsSelectedStoreUser] is bool isSelectedStoreUser)
            {
                IsSelectedStoreUser = isSelectedStoreUser;
            }

            this.IsCheckedButtonA = !string.IsNullOrWhiteSpace(SelectedStoreUser.Btn1);
            this.IsCheckedButtonB = !string.IsNullOrWhiteSpace(SelectedStoreUser.Btn2);
            this.IsCheckedButtonC = !string.IsNullOrWhiteSpace(SelectedStoreUser.Btn3);

            if (!string.IsNullOrWhiteSpace(SelectedStoreUser.Btn4))
            {
                this.IsCheckedOther = true;
                if ("Other Vaccines".Equals(SelectedStoreUser.Btn4))
                {
                    this.IsCheckedVeryTerribleNone = true;
                    this.IsCheckedOtherVaccines = true;
                }
                else if ("Shingles".Equals(SelectedStoreUser.Btn4))
                {
                    this.IsCheckedSubButton1 = true;
                }
                else if ("Pneumococcus".Equals(SelectedStoreUser.Btn4))
                {
                    this.IsCheckedSubButton2 = true;
                }
            }
        }

        private void SetUnsetProperties()
        {
            this.IsCheckedButtonA = this.IsCheckedButtonB = this.IsCheckedButtonC =
                this.IsCheckedVeryTerribleNone = this.IsCheckedOtherVaccines = IsCheckedOther =
                this.IsCheckedSubButton1 = this.IsCheckedCovid19 =
                IsCheckedSubButton2 = false;
            this.IsUserTypeMobile = false;
            this.IsUserTypeNonMobile = false;
            this.IsSelectedStoreUser = false;
        }
    }
}
