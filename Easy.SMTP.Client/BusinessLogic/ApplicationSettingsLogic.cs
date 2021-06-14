using Easy.SMTP.Client.Models.DTO;
using Easy.SMTP.Client.Utilities;
using Easy.SMTP.Models;
using Newtonsoft.Json;
using NLog;
using System;
using System.IO;
using System.Xml.Serialization;

namespace Easy.SMTP.BusinessLogic
{
    public class ApplicationSettingsLogic
    {
        private readonly Logger logger = LogManager.GetCurrentClassLogger();
        private string fileNameSettingsMessage = "settings_message.json";
        private string fileNameSettingsSMTP = "settings_smtp.json";
        public ApplicationSettingsLogic()
        {
            logger.Info("ApplicationSettingsLogic()");
        }

        public Tuple<ResponseOperation, SmtpClientModel> SettingsDeserializeSmtpLoad()
        {
            logger.Info($"SettingsDeserializeSmtpLoad()");

            var responseOperation = new ResponseOperation();
            var smtpClientModel = new SmtpClientModel();
            try
            {
                //XmlSerializer serializer = new XmlSerializer(typeof(SmtpClientModel));

                string basePath = AppDomain.CurrentDomain.BaseDirectory;
                if(!basePath.EndsWith("\\"))
                {
                    basePath = AppDomain.CurrentDomain.BaseDirectory + "\\";
                }
                string filePath = Path.Combine(basePath + Path.GetFileName(fileNameSettingsSMTP));
                if (!File.Exists(filePath))
                {
                    var myFile = File.Create(filePath);
                    myFile.Close();
                }

                if (File.Exists(filePath))
                {
                    if (new FileInfo(filePath).Length == 0)
                    {
                        smtpClientModel = new SmtpClientModel();
                    }
                    else
                    {
                        using (StreamReader file = File.OpenText(filePath))
                        {
                            var serializer = new Newtonsoft.Json.JsonSerializer();
                            smtpClientModel = new SmtpClientModel().ToObjectSmtpClientModel((SmtpClientDto)serializer.Deserialize(file, typeof(SmtpClientDto)));
                            file.Close();
                        }
                    }
                }
                responseOperation.OperationStatus = true;
            }
            catch (Exception ex) {
                logger.Error($"SettingsDeserializeSmtpLoad(ex='{ex.StackTrace}')");
                responseOperation.Exception = ex.ToString();
                return new Tuple<ResponseOperation, SmtpClientModel>(responseOperation, smtpClientModel);
            }
            return new Tuple<ResponseOperation, SmtpClientModel>(responseOperation, smtpClientModel);
        }

        public Tuple<ResponseOperation, MailMessageModel> SettingsDeserializeMessageLoad()
        {
            logger.Info($"SettingsDeserializeMessageLoad()");

            var responseOperation = new ResponseOperation();
            var mailMessageModel = new MailMessageModel();
            try
            {
       
                string basePath = AppDomain.CurrentDomain.BaseDirectory;
                if (!basePath.EndsWith("\\"))
                {
                    basePath = AppDomain.CurrentDomain.BaseDirectory + "\\";
                }
                string filePath = Path.Combine(basePath + Path.GetFileName(fileNameSettingsMessage));
                if (!File.Exists(filePath))
                {
                    var myFile = File.Create(filePath);
                    myFile.Close();
                }

                if (File.Exists(filePath))
                {
                    if (new FileInfo(filePath).Length == 0)
                    {
                        mailMessageModel = new MailMessageModel();
                    }
                    else
                    {
                        using (StreamReader file = File.OpenText(filePath))
                        {
                            var serializer = new Newtonsoft.Json.JsonSerializer();
                            mailMessageModel = (MailMessageModel)serializer.Deserialize(file, typeof(MailMessageModel));
                            file.Close();
                        }
                    }
                }
                responseOperation.OperationStatus = true;
            }
            catch (Exception ex)
            {
                logger.Error($"SettingsDeserializeMessageLoad(ex='{ex.StackTrace}')");
                responseOperation.Exception = ex.ToString();
                return new Tuple<ResponseOperation, MailMessageModel>(responseOperation, mailMessageModel);
            }
            return new Tuple<ResponseOperation, MailMessageModel>(responseOperation, mailMessageModel);
        }

        public ResponseOperation SettingsSerializeSaveSMTP(SmtpClientModel smtpClientModel)
        {
            logger.Info($"SettingsSerializeSaveSMTP(smtpClientModel=({smtpClientModel}))");

            var responseOperation = new ResponseOperation();
            try
            {
                string basePath = AppDomain.CurrentDomain.BaseDirectory;
                if (!basePath.EndsWith("\\"))
                {
                    basePath = AppDomain.CurrentDomain.BaseDirectory + "\\";
                }

                string filePath = Path.Combine(basePath + Path.GetFileName(fileNameSettingsSMTP));
                if (!File.Exists(filePath))
                {
                    var myFile = File.Create(filePath);
                    myFile.Close();
                }
                string jsonString = JsonConvert.SerializeObject(smtpClientModel.ToObjectSmtpDto(), Formatting.Indented);
                File.WriteAllText(filePath, jsonString);
       
                responseOperation.OperationStatus = true;
            }
            catch (Exception ex)
            {
                logger.Error($"SettingsSerializeSaveSMTP(ex='{ex.StackTrace}')");
                responseOperation.Exception = ex.ToString();
                return responseOperation;
            }
            return responseOperation;
        }

        public ResponseOperation SettingsSerializeSaveMessage(MailMessageModel mailMessageModel)
        {
            logger.Info($"SettingsSerializeSaveMessage(mailMessageModel=({mailMessageModel}))");

            var responseOperation = new ResponseOperation();
            try
            {
                string basePath = AppDomain.CurrentDomain.BaseDirectory;
                if (!basePath.EndsWith("\\"))
                {
                    basePath = AppDomain.CurrentDomain.BaseDirectory + "\\";
                }

                string filePath = Path.Combine(basePath + Path.GetFileName(fileNameSettingsMessage));
                if (!File.Exists(filePath))
                {
                    var myFile = File.Create(filePath);
                    myFile.Close();
                }
                string jsonString = JsonConvert.SerializeObject(mailMessageModel, Formatting.Indented);
                File.WriteAllText(filePath, jsonString);

                responseOperation.OperationStatus = true;
            }
            catch (Exception ex)
            {
                logger.Error($"SettingsSerializeSaveMessage(ex='{ex.StackTrace}')");
                responseOperation.Exception = ex.ToString();
                return responseOperation;
            }
            return responseOperation;
        }

    }
}
