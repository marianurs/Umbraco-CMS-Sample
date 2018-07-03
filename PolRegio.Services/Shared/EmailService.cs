using log4net;
using Newtonsoft.Json;
using PolRegio.Domain.Models.Components.BipPage;
using PolRegio.Domain.Models.Database;
using PolRegio.Domain.Models.UmbracoCreate;
using PolRegio.Domain.Models.View.BipPage;
using PolRegio.Domain.Services.Config;
using PolRegio.Domain.Services.Shared;
using RazorEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Reflection;
using System.Web;
using PolRegio.Domain.Models.View.SalesManago;
using Umbraco.Web;

namespace PolRegio.Services.Shared
{
    /// <summary>
    /// Klasa implementująca interfejs IEmailService
    /// </summary>
    public class EmailService : IEmailService
    {
        private static readonly ILog Log =
            LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// Obiekt typu UmbracoHelper
        /// </summary>
        private UmbracoHelper _umbracoHelper;
        /// <summary>
        /// Obiekt typu IConfigService
        /// </summary>
        private readonly IConfigService _configService;
        /// <summary>
        /// Konstruktor klasy
        /// </summary>
        public EmailService(IConfigService configService)
        {
            _umbracoHelper = new UmbracoHelper(UmbracoContext.Current);
            _configService = configService;
        }
        /// <summary>
        /// Metoda służąca do wysyłki emaila z formularza o udostępnienie
        /// informacji publicznej
        /// </summary>
        /// <param name="model">Model BIPFormViewModel</param>
        /// <returns>true/false</returns>
        public BIPFormViewModel SendBIPEmail(BIPFormViewModel model)
        {
            var bipFormNode = _umbracoHelper.TypedContent(model.CurrentUmbracoPageId);
            var bipForm = new BIpform(bipFormNode);

            model.SharingInformation = _umbracoHelper.GetDictionaryValue(
                string.Format("BIP.Placeholder.SharingInformation.{0}", model.SharingInformationType));

            var email = new Email<BIPFormViewModel>
            {
                Model = model,
                Template = "/Views/Mails/BIP.cshtml",
                Subject = "Wniosek o udostępnienie informacji publicznej",
                FromConfigKey = "smtp_email_from_bip",
                To = bipForm.EmailAdress.ToString(),
                DW = bipForm.EmailAdressUdw.ToString()
            };

            try
            {
                SendEmail(email);
                Log.Info(string.Format("Email wysłany poprawnie - model: {0}", JsonConvert.SerializeObject(model)));

                var _model = new BIPFormViewModel();
                _model.SendResponse = new BipSendEmailResponse()
                {
                    Display = true,
                    IsError = false,
                    Message = _umbracoHelper.GetDictionaryValue("BIP.SendEmail.Success")
                };

                _model.CurrentPageCulture = model.CurrentPageCulture;
                _model.CurrentUmbracoPageId = model.CurrentUmbracoPageId;
                model = _model;
            }
            catch (Exception exception)
            {
                Log.Error(string.Format("{0} -|- model: {1}", exception.Message, JsonConvert.SerializeObject(model)),
                    exception);
                model.SendResponse = new BipSendEmailResponse()
                {
                    Display = true,
                    IsError = true,
                    Message = _umbracoHelper.GetDictionaryValue("BIP.SendEmail.Error")
                };
            }

            return model;
        }

        public void SendRegisterEmail(UserDB model)
        {
            var email = new Email<UserDB>
            {
                Model = model,
                Template = "/Views/Mails/Register.cshtml",
                Subject = "Rejestracja",
                FromConfigKey = "smtp_email_from_register",
                To = model.UserEmail
            };

            SendEmail(email);
        }

        public void SendArticleToSalesManago(SalesManagoSendArticleViewModel model, string salesManagoMail)
        {
            var email = new Email<SalesManagoSendArticleViewModel>
            {
                Model = model,
                Template = "/Views/Mails/SalesManago.cshtml",
                Subject = string.Format("Zlecenie wysłania powiadomienia: {0}", model.ArticleTitle),
                FromConfigKey = "smtp_email_from_register",
                To = salesManagoMail,
            };
            SendEmail(email);
        }

        public void SendForgotPassEmail(UserDB model)
        {
            var email = new Email<UserDB>
            {
                Model = model,
                Template = "/Views/Mails/ForgotPass.cshtml",
                Subject = "Przypomnienie hasła",
                FromConfigKey = "smtp_email_from_register",
                To = model.UserEmail
            };

            SendEmail(email);
        }

        private void SendEmail<T>(Email<T> email)
        {
            var smtpServer = string.Empty;
            var smtpPortString = string.Empty;
            var smtpPort = 0;
            var smtpUserName = string.Empty;
            var smtpUserPassword = string.Empty;
            var smtpEnableSsl = string.Empty;
            var smtpEmailFrom = string.Empty;

            if (!(_configService.Custom.TryGetValue("smtp_server", out smtpServer)
                    && _configService.Custom.TryGetValue("smtp_port", out smtpPortString)
                    && int.TryParse(smtpPortString, out smtpPort)))
                return;

            using (var client = new SmtpClient(smtpServer, smtpPort))
            {
                _configService.Custom.TryGetValue("smtp_user_name", out smtpUserName);
                _configService.Custom.TryGetValue("smtp_user_password", out smtpUserPassword);
                if (!string.IsNullOrEmpty(smtpUserName) && !string.IsNullOrEmpty(smtpUserPassword))
                {
                    client.Credentials = new System.Net.NetworkCredential(smtpUserName, smtpUserPassword);
                }

                _configService.Custom.TryGetValue("smtp_enable_ssl", out smtpEnableSsl);
                if (!string.IsNullOrEmpty(smtpEnableSsl) && bool.Parse(smtpEnableSsl))
                {
                    client.EnableSsl = bool.Parse(smtpEnableSsl);
                }

                var mailMessage = new MailMessage();

                mailMessage.BodyEncoding = System.Text.Encoding.GetEncoding("utf-8");
                mailMessage.SubjectEncoding = System.Text.Encoding.GetEncoding("utf-8");

                mailMessage.To.Add(email.To);

                if (!string.IsNullOrWhiteSpace(email.DW))
                    mailMessage.Bcc.Add(email.DW);

                mailMessage.Subject = email.Subject;

                _configService.Custom.TryGetValue(email.FromConfigKey, out smtpEmailFrom);
                mailMessage.From = new MailAddress(smtpEmailFrom);

                mailMessage.IsBodyHtml = true;
                mailMessage.Body = GetHtmlFromTemplate(email.Template, email.Model);

                if (email.AttachmentUrls.Any())
                {
                    foreach (var attachment in email.AttachmentUrls)
                    {
                        mailMessage.Attachments.Add(attachment);
                    }
                }


                client.Send(mailMessage);
            }
        }

        private string GetHtmlFromTemplate<T>(string template, T model)
        {
            var templatePath = AppDomain.CurrentDomain.BaseDirectory + template;
            var templateSource = System.IO.File.ReadAllText(templatePath);
            var templateGeneratedHtml = Razor.Parse(templateSource, model);
            return templateGeneratedHtml;
        }

        private class Email<T>
        {
            public Email()
            {
                AttachmentUrls = new List<Attachment>();
            }

            public T Model { get; set; }
            public string Template { get; set; }
            public string Subject { get; set; }
            public string FromConfigKey { get; set; }
            public string To { get; set; }
            public string DW { get; set; }

            public List<Attachment> AttachmentUrls { get; set; }
        }
    }
}