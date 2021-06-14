using Easy.SMTP.BusinessLogic;
using Easy.SMTP.Client.Utilities;
using Easy.SMTP.Client.ViewModels.Controls;
using Easy.SMTP.Client.ViewModels.Modules;
using Easy.SMTP.Client.Views;
using Easy.SMTP.Core;
using Easy.SMTP.Models;
using Easy.SMTP.ViewModels.Controls;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.IconPacks;
using NLog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy.SMTP.Client.ViewModels
{
    public class MainViewModel : PropertyChangedBase
    {
        private ObservableCollection<MenuItemViewModel> _menuItems;
        private readonly Logger logger = LogManager.GetCurrentClassLogger();
        public event Action CloseApplication;
        public event Action<ActionMessage> ShowMessageApplication;

        public MainViewModel(MainWindow mainWindow)
        {
         
            logger.Info($"MainViewModel(mainWindow={mainWindow})");
            _mainWindow = mainWindow;

            SettingsViewModelElement = new SettingsObjectModel(this);

            HomeViewModelElement = new Modules.HomeViewModel(this);
            OnLoadedAsync();
            this.CreateMenuItems();
        }

        public void OnLoadedAsync()
        {
            TitleApplication = "Application";
            StatusBarItemTitle = "";
        }

        #region Fields
        private MainWindow _mainWindow;
        public MainWindow MainWindowElement
        {
            get { return _mainWindow; }
            set
            {
                if (_mainWindow != value)
                {
                    _mainWindow = value;
                    NotifyOfPropertyChange(() => MainWindowElement);
                }
            }
        }

        private string _statusBarItemTitle;
        public string StatusBarItemTitle
        {
            get { return _statusBarItemTitle; }
            set
            {
                if (_statusBarItemTitle != value)
                {
                    _statusBarItemTitle = value;
                    NotifyOfPropertyChange(() => StatusBarItemTitle);
                }
            }
        }

        private string _titleApplication;
        public string TitleApplication
        {
            get { return _titleApplication; }
            set
            {
                if (_titleApplication != value)
                {
                    _titleApplication = value;
                    NotifyOfPropertyChange(() => TitleApplication);
                }
            }
        }

        private HomeViewModel _homeViewModelElement;
        public HomeViewModel HomeViewModelElement
        {
            get { return _homeViewModelElement; }
            set
            {
                if (_homeViewModelElement != value)
                {
                    _homeViewModelElement = value;
                    NotifyOfPropertyChange(() => HomeViewModelElement);
                }
            }
        }

        private SettingsObjectModel _settingsViewModelElement;
        public SettingsObjectModel SettingsViewModelElement
        {
            get { return _settingsViewModelElement; }
            set
            {
                if (_settingsViewModelElement != value)
                {
                    _settingsViewModelElement = value;
                    NotifyOfPropertyChange(() => SettingsViewModelElement);
                }
            }
        }

        #endregion Fields

        #region Events CloseApp
        public void CloseApp()
        {
            if (CloseApplication != null)
            {
                CloseApplication();
            }
        }
        #endregion

        #region Commands
   

        public RelayCommand SendEmailCommand
        {
            get
            {
                return new RelayCommand(_ => OnSendEmailCommandAsync());
            }
        }

        private async Task OnSendEmailCommandAsync()
        {
            logger.Info($"OnSendEmailCommandAsync()");

            try
            {
                var element = DialogParticipation.GetRegister(MainWindowElement);
                DialogParticipation.SetRegister(MainWindowElement, this);
                var settings = new MetroDialogSettings()
                {
                    ColorScheme = MetroDialogColorScheme.Accented,
                };

                await Task.Run(async () =>
                {
                    var dialogCoordinator = DialogCoordinator.Instance;
                    var controller = await dialogCoordinator.ShowProgressAsync(this, "Please wait...", "Try to send email !", true);
                    controller.SetIndeterminate();
                    await Task.Delay(1000);

                    var sendEmailLogic = new SendEmailLogic();
                    var responseOperation = sendEmailLogic.SendMessage(SettingsViewModelElement.SettingsModelObject.MailMessageModelObject, SettingsViewModelElement.SettingsModelObject.SmtpClientModelObject);
                    if (responseOperation.OperationStatus)
                    {
                        string messageOk = $"Email has been sent corectly.";
                        await dialogCoordinator.ShowMessageAsync(this, TitleApplication, messageOk, MessageDialogStyle.Affirmative, settings);
                        StatusBarItemTitle = DateTimeToStringUtility.AddDateTime(messageOk);
                    }
                    else
                    {
                        string messageError = $"Email has not been sent.";
                        await dialogCoordinator.ShowMessageAsync(this, TitleApplication, messageError + $"\nError message: {responseOperation.Exception}", MessageDialogStyle.Affirmative, settings);
                        StatusBarItemTitle = DateTimeToStringUtility.AddDateTime(messageError);
                    }

                    await controller.CloseAsync();

                });
            }
            catch (Exception ex)
            {
                logger.Error($"OnSendEmailCommandAsync(ex='{ex.ToString()}')");
                var dialogCoordinator = DialogCoordinator.Instance;
                var settings = new MetroDialogSettings()
                {
                    ColorScheme = MetroDialogColorScheme.Accented,
                };
                await dialogCoordinator.ShowMessageAsync(this, TitleApplication, $"Email has not been sent.\nError message: {ex.Message}", MessageDialogStyle.Affirmative, settings);
            }
        }

        #endregion Commands

        #region ToString
        public override string ToString()
        {
            return $"StatusBarItemTitle='{StatusBarItemTitle}' | TitleApplication='{TitleApplication}' | HomeViewModelElement='{HomeViewModelElement} | SettingsViewModelElement='{SettingsViewModelElement}";
        }
        #endregion

        public void CreateMenuItems()
        {
            MenuItems = new ObservableCollection<MenuItemViewModel>
            {
                new HomeViewModel(this)
                {
                    Icon = new PackIconMaterial() {Kind = PackIconMaterialKind.Home},
                    Label = "Message",
                    ToolTip = "Message",
                    
                },
                new SettingsViewModel(this)
                {
                    Icon = new PackIconMaterial() {Kind = PackIconMaterialKind.AccountCircle},
                    Label = "Settings",
                    ToolTip = "Settings",
                }
            };
        }

        public ObservableCollection<MenuItemViewModel> MenuItems
        {
            get => _menuItems;
            set => SetProperty(ref _menuItems, value);
        }
    }
}
