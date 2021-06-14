using Easy.SMTP.Client.Models.DTO;
using Easy.SMTP.Client.Utilities;
using Easy.SMTP.Core;
using NLog;
using System;

namespace Easy.SMTP.Models
{
    public class SmtpClientModel : PropertyChangedBase
    {
        public SmtpClientModel()
        {
        }

        private string userNameSmtp;
        public string UserNameSmtp
        {
            get
            {
                return userNameSmtp;
            }
            set
            {
                if (userNameSmtp != value)
                {
                    userNameSmtp = value;
                    NotifyOfPropertyChange(() => UserNameSmtp);
                }
            }
        }

        private string passwordSmtp;
        public string PasswordSmtp
        {
            get
            {
                return passwordSmtp;
            }
            set
            {
                if (passwordSmtp != value)
                {
                    passwordSmtp = value;
                    NotifyOfPropertyChange(() => PasswordSmtp);
                }
            }
        }

        private int portSmtp;
        public int PortSmtp
        {
            get
            {
                return portSmtp;
            }
            set
            {
                if (portSmtp != value)
                {
                    portSmtp = value;
                    NotifyOfPropertyChange(() => PortSmtp);
                }
            }
        }

        private string hostSmtp;
        public string HostSmtp
        {
            get
            {
                return hostSmtp;
            }
            set
            {
                if (hostSmtp != value)
                {
                    hostSmtp = value;
                    NotifyOfPropertyChange(() => HostSmtp);
                }
            }
        }

        private bool enableSslSmtp;

        public bool EnableSslSmtp
        {
            get
            {
                return enableSslSmtp;
            }
            set
            {
                if (enableSslSmtp != value)
                {
                    enableSslSmtp = value;
                    NotifyOfPropertyChange(() => EnableSslSmtp);
                }
            }
        }

        public SmtpClientDto ToObjectSmtpDto()
        {
            var returnObject = new SmtpClientDto
            {
                HostSmtp = HostSmtp,
                UserNameSmtp = UserNameSmtp,
                PasswordSmtp = ApplicationEncrypterDecrypt.Encrypt(PasswordSmtp),
                PortSmtp = PortSmtp,
                EnableSslSmtp = EnableSslSmtp,
            };
            return returnObject;
        }


        public SmtpClientModel ToObjectSmtpClientModel(SmtpClientDto smtpClientDto)
        {
            var returnObject = new SmtpClientModel
            {
                HostSmtp = smtpClientDto.HostSmtp,
                UserNameSmtp = smtpClientDto.UserNameSmtp,
                PasswordSmtp = ApplicationEncrypterDecrypt.Decrypt(smtpClientDto.PasswordSmtp),
                PortSmtp = smtpClientDto.PortSmtp,
                EnableSslSmtp = smtpClientDto.EnableSslSmtp,
            };
            return returnObject;
        }


        #region ToString
        public override string ToString()
        {
            return $"UserNameSmtp='{PortSmtp}', PasswordSmtp='{PasswordSmtp}', PortSmtp='{PortSmtp}', HostSmtp='{HostSmtp}', EnableSslSmtp='{EnableSslSmtp}'";
        }
        #endregion
    }
}
