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

        private bool _isCheckedVeryTerrible;
        public bool IsCheckedVeryTerrible
        {
            get => _isCheckedVeryTerrible;
            set => SetProperty(ref _isCheckedVeryTerrible, value);
        }

        private bool _isCheckedVeryTerribleNoneDeal;
        public bool IsCheckedVeryTerribleNoneDeal
        {
            get => _isCheckedVeryTerribleNoneDeal;
            set => SetProperty(ref _isCheckedVeryTerribleNoneDeal, value);
        }

        private bool _isCheckedVeryTerribleTerribleService;
        public bool IsCheckedVeryTerribleTerribleService
        {
            get => _isCheckedVeryTerribleTerribleService;
            set => SetProperty(ref _isCheckedVeryTerribleTerribleService, value);
        }

        private bool _isCheckedVeryTerribleNoneDealTerribleService;
        public bool IsCheckedVeryTerribleNoneDealTerribleService
        {
            get => _isCheckedVeryTerribleNoneDealTerribleService;
            set => SetProperty(ref _isCheckedVeryTerribleNoneDealTerribleService, value);
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
                this.IsCheckedVeryTerribleNoneDeal = false;
                this.IsCheckedVeryTerribleTerribleService = false;
                this.IsCheckedVeryTerribleNoneDealTerribleService = false;
            }
            else if (parameter == "1")
            {
                if (this.IsCheckedVeryTerribleNone)
                    this.IsCheckedVeryTerribleNone = this.IsCheckedVeryTerribleTerribleService == false &&
                    this.IsCheckedVeryTerribleNoneDealTerribleService == false;
            }
            else if (parameter == "2")
            {
                if (this.IsCheckedVeryTerribleNone)
                    this.IsCheckedVeryTerribleNone = this.IsCheckedVeryTerribleNoneDeal == false &&
                    this.IsCheckedVeryTerribleNoneDealTerribleService == false;
            }
            else if (parameter == "3")
            {
                if (this.IsCheckedVeryTerribleNone)
                    this.IsCheckedVeryTerribleNone = this.IsCheckedVeryTerribleNoneDeal == false &&
                    this.IsCheckedVeryTerribleTerribleService == false;
            }
        }

        private void ExecuteVeryTerribleCheckedCommand(string parameter)
        {
            if (string.IsNullOrEmpty(parameter))
            {
                this.IsCheckedVeryTerribleNone = true;
                this.IsCheckedVeryTerribleNoneDeal = false;
                this.IsCheckedVeryTerribleTerribleService = false;
                this.IsCheckedVeryTerribleNoneDealTerribleService = false;
            }
            else if (parameter == "1")
            {
                this.IsCheckedVeryTerribleNone = false;
                this.IsCheckedVeryTerribleNoneDeal = true;
                this.IsCheckedVeryTerribleTerribleService = false;
                this.IsCheckedVeryTerribleNoneDealTerribleService = false;
            }
            else if (parameter == "2")
            {
                this.IsCheckedVeryTerribleNone = false;
                this.IsCheckedVeryTerribleNoneDeal = false;
                this.IsCheckedVeryTerribleTerribleService = true;
                this.IsCheckedVeryTerribleNoneDealTerribleService = false;
            }
            else if (parameter == "3")
            {
                this.IsCheckedVeryTerribleNone = false;
                this.IsCheckedVeryTerribleNoneDeal = false;
                this.IsCheckedVeryTerribleTerribleService = false;
                this.IsCheckedVeryTerribleNoneDealTerribleService = true;
            }
        }

        private async Task ExecuteSubmitCommand()
        {

            if (!this.IsCheckedButtonA && !this.IsCheckedButtonB &&
                !this.IsCheckedVeryTerribleNone && !IsCheckedVeryTerribleNoneDeal &&
                !this.IsCheckedVeryTerribleTerribleService && !this.IsCheckedVeryTerribleNoneDealTerribleService)
            {
                MessageBox.Show("You must make a selection for Science or Technology or Medicine or all.", "Required.");
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
                reqEntity.Button1 = "Science";
            }

            if (this.IsCheckedButtonB)
            {
                reqEntity.Button2 = "Technology";
            }

            reqEntity.Button3 = string.Empty;

            if (this.IsCheckedVeryTerribleNone)
            {
                reqEntity.Button3 = "Other Medicine";
            }
            else if (this.IsCheckedVeryTerribleNoneDeal)
            {
                reqEntity.Button3 = "No Deals";
            }
            else if (this.IsCheckedVeryTerribleTerribleService)
            {
                reqEntity.Button3 = "Terrible Service";
            }
            else if (this.IsCheckedVeryTerribleNoneDealTerribleService)
            {
                reqEntity.Button3 = "No Deal Terrible";
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

            if (!string.IsNullOrWhiteSpace(SelectedStoreUser.Btn3))
            {
                this.IsCheckedVeryTerrible = true;
                if ("Other Medicine".Equals(SelectedStoreUser.Btn3))
                {
                    this.IsCheckedVeryTerribleNone = true;
                }
                else if ("No Deals".Equals(SelectedStoreUser.Btn3))
                {
                    this.IsCheckedVeryTerribleNoneDeal = true;
                }
                else if ("Terrible Service".Equals(SelectedStoreUser.Btn3))
                {
                    this.IsCheckedVeryTerribleTerribleService = true;
                }
                else if ("No Deal Terrible".Equals(SelectedStoreUser.Btn3))
                {
                    this.IsCheckedVeryTerribleNoneDealTerribleService = true;
                }
            }
        }

        private void SetUnsetProperties()
        {
            this.IsCheckedButtonA = this.IsCheckedButtonB = 
                this.IsCheckedVeryTerribleNone = this.IsCheckedVeryTerrible = 
                this.IsCheckedVeryTerribleNoneDeal = this.IsCheckedVeryTerribleNoneDealTerribleService = 
                IsCheckedVeryTerribleTerribleService = false;
            this.IsUserTypeMobile = false;
            this.IsUserTypeNonMobile = false;
            this.IsSelectedStoreUser = false;
        }
    }
}
