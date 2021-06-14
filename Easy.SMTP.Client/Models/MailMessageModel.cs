using Easy.SMTP.Core;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy.SMTP.Models
{
    public class MailMessageModel : PropertyChangedBase
    {
        private readonly Logger logger = LogManager.GetCurrentClassLogger();
        public MailMessageModel()
        {
            logger.Info("MailMessageModel()");
        }

        private string fromMailAddress;
        public string FromMailAddress
        {
            get
            {
                return fromMailAddress;
            }
            set
            {
                if (fromMailAddress != value)
                {
                    fromMailAddress = value;
                    NotifyOfPropertyChange(() => FromMailAddress);
                }
            }
        }

        private string toMailAddress;
        public string ToMailAddress
        {
            get
            {
                return toMailAddress;
            }
            set
            {
                if (toMailAddress != value)
                {
                    toMailAddress = value;
                    NotifyOfPropertyChange(() => ToMailAddress);
                }
            }
        }

        private string subjectMessage;
        public string SubjectMessage
        {
            get
            {
                return subjectMessage;
            }
            set
            {
                if (subjectMessage != value)
                {
                    subjectMessage = value;
                    NotifyOfPropertyChange(() => SubjectMessage);
                }
            }
        }


        private string bodyMessage;
        public string BodyMessage
        {
            get
            {
                return bodyMessage;
            }
            set
            {
                if (bodyMessage != value)
                {
                    bodyMessage = value;
                    NotifyOfPropertyChange(() => BodyMessage);
                }
            }
        }

        #region ToString
        public override string ToString()
        {
            return $"FromMailAddress='{FromMailAddress}', ToMailAddress='{ToMailAddress}', SubjectMessage='{SubjectMessage}'," +
                $"BodyMessage='{BodyMessage}'";
        }
        #endregion
    }
}
