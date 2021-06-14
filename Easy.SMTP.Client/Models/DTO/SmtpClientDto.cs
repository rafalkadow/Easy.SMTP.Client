using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Easy.SMTP.Client.Models.DTO
{
    [Serializable, XmlRoot("StreamingData")]
    public class SmtpClientDto
    {
        private readonly Logger logger = LogManager.GetCurrentClassLogger();
        public SmtpClientDto()
        {
            logger.Info("SmtpClientDto()");
        }
        [XmlElement]
        public string UserNameSmtp
        {
            get; set;
        }
        [XmlElement]
        public string PasswordSmtp
        {
            get; set;
        }
        [XmlElement]
        public int PortSmtp
        {
            get; set;
        }
        [XmlElement]
        public string HostSmtp
        {
            get; set;
        }
        [XmlElement]
        public bool EnableSslSmtp
        {
            get; set;
        }


        #region ToString
        public override string ToString()
        {
            return $"UserNameSmtp='{PortSmtp}', PasswordSmtp='{PasswordSmtp}', PortSmtp='{PortSmtp}', HostSmtp='{HostSmtp}', EnableSslSmtp='{EnableSslSmtp}'";
        }
        #endregion
    }
}
