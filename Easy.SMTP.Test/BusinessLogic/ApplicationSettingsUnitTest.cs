using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Easy.SMTP.BusinessLogic;
using Easy.SMTP.Models;
using Easy.SMTP.Client.Utilities;

namespace Easy.SMTP.Test.BusinessLogic
{
    /// <summary>
    /// Summary description for ApplicationSettingsUnitTest
    /// </summary>
    [TestClass]
    public class ApplicationSettingsUnitTest
    {
        public ApplicationSettingsUnitTest()
        {
       
        }

        [TestMethod]
        public void SettingsDeserializeLoadTestMethod()
        {
            var applicationSettingsLogic = new ApplicationSettingsLogic();
            var responseOperationTuple = applicationSettingsLogic.SettingsDeserializeSmtpLoad();
            Assert.IsTrue(responseOperationTuple.Item1.OperationStatus);
            Assert.IsNotNull(responseOperationTuple.Item2);
        }


        [TestMethod]
        public void SettingsSerializeSaveTestMethod()
        {
            MailMessageModel mailMessageModel = new MailMessageModel();
            var applicationSettingsLogic = new ApplicationSettingsLogic();
            var responseOperation = applicationSettingsLogic.SettingsSerializeSaveMessage(mailMessageModel);
            Assert.IsTrue(responseOperation.OperationStatus);
        }

        [TestMethod]
        public void DateTimeToStringTestMethod()
        {
            string messageOk = "test";
            string result = DateTimeToStringUtility.AddDateTime(messageOk);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Contains(messageOk));
        }

        [TestMethod]
        public void ApplicationEncrypterDecryptTestMethod()
        {
            string message = "test";
            string resultEncrypt = ApplicationEncrypterDecrypt.Encrypt(message);
            string resultDescrypt = ApplicationEncrypterDecrypt.Decrypt(resultEncrypt);
            Assert.IsNotNull(resultDescrypt);
            Assert.IsTrue(resultDescrypt.Equals(message));
        }
    }
}
