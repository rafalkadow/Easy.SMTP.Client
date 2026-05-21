using Easy.SMTP.Client.Models;
using NLog;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace Easy.SMTP.Client.BusinessLogic
{
    public class SendEmailLogic
    {
        private readonly Logger logger = LogManager.GetCurrentClassLogger();
        public SendEmailLogic()
        {
            logger.Info("SendEmailLogic()");

        }

        public ResponseOperation SendMessage(MailMessageModel mailMessageModel, SmtpClientModel smtpClientModel)
        {
            logger.Info($"SendMessage(mailMessageModel=({mailMessageModel.ToString()}, smtpClientModel={smtpClientModel.ToString()})");
            ResponseOperation responseOperation = new ResponseOperation();
            try
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(RemoteServerCertificateValidationCallback);
                using (MailMessage message = new MailMessage())
                {
                    message.From = new MailAddress(mailMessageModel.FromMailAddress);
                    message.To.Add(new MailAddress(mailMessageModel.ToMailAddress));
                    message.Subject = mailMessageModel.SubjectMessage;
                    message.IsBodyHtml = true; //to make message body as html  
                    message.Body = mailMessageModel.BodyMessage;
                    using (SmtpClient smtp = new SmtpClient())
                    {
                        smtp.Port = smtpClientModel.PortSmtp;
                        smtp.Host = smtpClientModel.HostSmtp; //for gmail host  
                        smtp.EnableSsl = smtpClientModel.EnableSslSmtp;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential(smtpClientModel.UserNameSmtp, smtpClientModel.PasswordSmtp);
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.Timeout = 10000;
                        smtp.Send(message);
                    }
                }
                responseOperation.OperationStatus = true;
            }
            catch (Exception ex) {
                logger.Error($"SendMessage(ex='{ex.ToString()}')");
                responseOperation.Exception = ex.ToString();
                return responseOperation;
            }

            return responseOperation;
        }

        public static bool RemoteServerCertificateValidationCallback(Object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors == SslPolicyErrors.None)
                return true;

            // if got an cert auth error
            if (sslPolicyErrors != SslPolicyErrors.RemoteCertificateNameMismatch) return false;
            const string certFileName = "smtphost.cer";

            // check if cert file exists
            if (File.Exists(certFileName))
            {
                var actualCertificate = X509Certificate.CreateFromCertFile(certFileName);
                return certificate.Equals(actualCertificate);
            }

            // export and check if cert not exists
            using (var file = File.Create(certFileName))
            {
                var cert = certificate.Export(X509ContentType.Cert);
                file.Write(cert, 0, cert.Length);
            }
            var createdCertificate = X509Certificate.CreateFromCertFile(certFileName);
            return certificate.Equals(createdCertificate);
        }


    }
}
