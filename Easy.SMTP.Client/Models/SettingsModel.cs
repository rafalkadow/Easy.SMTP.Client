using Easy.SMTP.Core;
using NLog;

namespace Easy.SMTP.Models
{
    public class SettingsModel : PropertyChangedBase
    {
        private readonly Logger logger = LogManager.GetCurrentClassLogger();
        public SettingsModel()
        {
            logger.Info("SettingsModel()");
        }

        private MailMessageModel _mailMessageModelObject = new MailMessageModel();
        public MailMessageModel MailMessageModelObject
        {
            get
            {
                return _mailMessageModelObject;
            }
            set
            {
                if (_mailMessageModelObject != value)
                {
                    _mailMessageModelObject = value;
                    NotifyOfPropertyChange(() => MailMessageModelObject);
                }
            }
        }

        private SmtpClientModel _smtpClientModelObject = new SmtpClientModel();
        public SmtpClientModel SmtpClientModelObject
        {
            get
            {
                return _smtpClientModelObject;
            }
            set
            {
                if (_smtpClientModelObject != value)
                {
                    _smtpClientModelObject = value;
                    NotifyOfPropertyChange(() => SmtpClientModelObject);
                }
            }
        }


        #region ToString
        public override string ToString()
        {
            return $"MailMessageModelObject='{MailMessageModelObject}' | SmtpClientModelObject='{SmtpClientModelObject}'";
        }
        #endregion

    }
}
