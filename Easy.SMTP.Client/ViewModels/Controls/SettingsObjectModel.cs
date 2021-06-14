using Easy.SMTP.BusinessLogic;
using Easy.SMTP.Client.ViewModels;
using Easy.SMTP.Core;
using Easy.SMTP.Models;
using NLog;

namespace Easy.SMTP.ViewModels.Controls
{
    public class SettingsObjectModel : PropertyChangedBase
    {
        private readonly Logger logger = LogManager.GetCurrentClassLogger();
        private MainViewModel _mainViewModel;
        public SettingsObjectModel(MainViewModel mainViewModel)
        {
            logger.Info($"SettingsViewModel(mainViewModel={mainViewModel.ToString()})");
            _mainViewModel = mainViewModel;
            var applicationSettingsLogic = new ApplicationSettingsLogic();
            var responseOperationTupleSmtp = applicationSettingsLogic.SettingsDeserializeSmtpLoad();
            if(responseOperationTupleSmtp.Item1.OperationStatus)
            {
                SettingsModelObject.SmtpClientModelObject = responseOperationTupleSmtp.Item2;
            }

            var responseOperationTupleMessage = applicationSettingsLogic.SettingsDeserializeMessageLoad();
            if (responseOperationTupleMessage.Item1.OperationStatus)
            {
                SettingsModelObject.MailMessageModelObject = responseOperationTupleMessage.Item2;
            }
        }

        private SettingsModel _settingsModelObject = new SettingsModel();
        public SettingsModel SettingsModelObject
        {
            get
            {
                return _settingsModelObject;
            }
            set
            {
                if (_settingsModelObject != value)
                {
                    _settingsModelObject = value;
                    NotifyOfPropertyChange(() => SettingsModelObject);
                }
            }
        }

        #region ToString
        public override string ToString()
        {
            return $"SettingsModelObject='{SettingsModelObject}'";
        }
        #endregion

    }
}
