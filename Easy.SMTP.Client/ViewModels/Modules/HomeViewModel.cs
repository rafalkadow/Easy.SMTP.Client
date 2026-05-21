using Easy.SMTP.Client.BusinessLogic;
using Easy.SMTP.Client.Utilities;
using Easy.SMTP.Client.ViewModels.Controls;
using Easy.SMTP.Client.Core;
using MahApps.Metro.Controls.Dialogs;
using System.Windows.Input;
using NLog;
using System;
using System.Threading.Tasks;

namespace Easy.SMTP.Client.ViewModels.Modules
{
    public class HomeViewModel : MenuItemViewModel
    {
        private readonly Logger logger = LogManager.GetCurrentClassLogger();
        public HomeViewModel(MainViewModel mainViewModel) : base(mainViewModel)
        {
        }

        #region Commands

        public ICommand SaveMessageCommand
        {
            get
            {
                return new AsyncRelayCommand(_ => OnSaveMessageCommandAsync());
            }
        }

        public async Task OnSaveMessageCommandAsync()
        {
            logger.Info($"OnSaveMessageCommandAsync()");

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
                    var controller = await dialogCoordinator.ShowProgressAsync(this, "Please wait...", "Try to saving message settings!", true, settings);
                    controller.SetIndeterminate();
                    await Task.Delay(1000);

                    var applicationSettingsLogic = new ApplicationSettingsLogic();
                    var responseOperation = applicationSettingsLogic.SettingsSerializeSaveMessage(MainViewModel.SettingsViewModelElement.SettingsModelObject.MailMessageModelObject);
                    if (responseOperation.OperationStatus)
                    {
                        string messageOk = $"Message has been saved correctly.";
                        await dialogCoordinator.ShowMessageAsync(this, MainViewModel.TitleApplication, messageOk, MessageDialogStyle.Affirmative, settings);
                        MainViewModel.StatusBarItemTitle = DateTimeToStringUtility.AddDateTime(messageOk);
                    }
                    else
                    {
                        string messageError = $"Message has not been saved.";
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
                await dialogCoordinator.ShowMessageAsync(this, MainViewModel.TitleApplication, $"Message has not been saved.\nError message: {ex.Message}", MessageDialogStyle.Affirmative, settings);
            }
        }
        #endregion Commands

    }
}
