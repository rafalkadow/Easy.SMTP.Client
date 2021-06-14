using Easy.SMTP.BusinessLogic;
using Easy.SMTP.Client.Utilities;
using Easy.SMTP.Client.ViewModels.Controls;
using Easy.SMTP.Core;
using MahApps.Metro.Controls.Dialogs;
using NLog;
using System;
using System.Threading.Tasks;

namespace Easy.SMTP.Client.ViewModels.Modules
{
    public class SettingsViewModel : MenuItemViewModel
    {

        private readonly Logger logger = LogManager.GetCurrentClassLogger();
        public SettingsViewModel(MainViewModel mainViewModel) : base(mainViewModel)
        {
        }


        #region Commands
        public RelayCommand SaveSettingsCommand
        {
            get
            {
                return new RelayCommand(_ => OnSaveSettingsCommandAsync());
            }
        }

        public async void OnSaveSettingsCommandAsync()
        {
            logger.Info($"OnSaveSettingsCommandAsync()");

            try
            {
                var element = DialogParticipation.GetRegister(MainViewModel.MainWindowElement);
                DialogParticipation.SetRegister(MainViewModel.MainWindowElement, this);
                var settings = new MetroDialogSettings()
                {
                    ColorScheme = MetroDialogColorScheme.Accented,
                };

                await Task.Run(async () =>
                {
                    var dialogCoordinator = DialogCoordinator.Instance;
                    var controller = await dialogCoordinator.ShowProgressAsync(this, "Please wait...", "Try to saving settings!", true);
                    controller.SetIndeterminate();
                    await Task.Delay(1000);

                    var applicationSettingsLogic = new ApplicationSettingsLogic();
                    var responseOperation = applicationSettingsLogic.SettingsSerializeSaveSMTP(MainViewModel.SettingsViewModelElement.SettingsModelObject.SmtpClientModelObject);
                    if (responseOperation.OperationStatus)
                    {
                        string messageOk = $"Settings have been saved corectly.";
                        await dialogCoordinator.ShowMessageAsync(this, MainViewModel.TitleApplication, messageOk, MessageDialogStyle.Affirmative, settings);
                        MainViewModel.StatusBarItemTitle = DateTimeToStringUtility.AddDateTime(messageOk);
                    }
                    else
                    {
                        string messageError = $"Settings have not been saved corectly.";
                        await dialogCoordinator.ShowMessageAsync(this, MainViewModel.TitleApplication, messageError + $"\nError message: {responseOperation.Exception}", MessageDialogStyle.Affirmative, settings);
                        MainViewModel.StatusBarItemTitle = DateTimeToStringUtility.AddDateTime(messageError);
                    }
                    await controller.CloseAsync();
                });
            }
            catch (Exception ex)
            {
                logger.Error($"SaveSettingsAsync(ex='{ex.ToString()}')");
                var dialogCoordinator = DialogCoordinator.Instance;
                var settings = new MetroDialogSettings()
                {
                    ColorScheme = MetroDialogColorScheme.Accented,
                };
                await dialogCoordinator.ShowMessageAsync(this, MainViewModel.TitleApplication, $"Settings have been saved.\nError message: {ex.Message}", MessageDialogStyle.Affirmative, settings);
            }
        }

        #endregion Commands

    }
}
