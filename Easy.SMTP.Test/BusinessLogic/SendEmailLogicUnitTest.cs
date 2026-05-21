using System.Net.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Easy.SMTP.Client.BusinessLogic;
using Easy.SMTP.Client.Models;

namespace Easy.SMTP.Test.BusinessLogic
{
    [TestClass]
    public class SendEmailLogicUnitTest
    {
        [TestMethod]
        public void RemoteServerCertificateValidationCallback_SslPolicyErrorsNone_ReturnsTrue()
        {
            bool result = SendEmailLogic.RemoteServerCertificateValidationCallback(null, null, null, SslPolicyErrors.None);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void RemoteServerCertificateValidationCallback_NotNameMismatch_ReturnsFalse()
        {
            bool result = SendEmailLogic.RemoteServerCertificateValidationCallback(
                null, null, null, SslPolicyErrors.RemoteCertificateChainErrors);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void SendMessage_InvalidHost_ReturnsOperationStatusFalse()
        {
            var logic = new SendEmailLogic();
            var mail = new MailMessageModel
            {
                FromMailAddress = "from@test.local",
                ToMailAddress = "to@test.local",
                SubjectMessage = "Test",
                BodyMessage = "Body"
            };
            var smtp = new SmtpClientModel
            {
                HostSmtp = "invalid.invalid",
                PortSmtp = 25,
                UserNameSmtp = "u",
                PasswordSmtp = "p",
                EnableSslSmtp = false
            };

            var response = logic.SendMessage(mail, smtp);

            Assert.IsFalse(response.OperationStatus);
            Assert.IsNotNull(response.Exception);
        }
    }
}
