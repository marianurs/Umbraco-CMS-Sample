using PolRegio.Domain.Models.Database;
using PolRegio.Domain.Models.View.BipPage;
using PolRegio.Domain.Models.View.SalesManago;

namespace PolRegio.Domain.Services.Shared
{
    /// <summary>
    /// Interfejs zawierający obsługę wysyłki e-maili
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Metoda służąca do wysyłki emaila z formularza o udostępnienie
        /// informacji publicznej
        /// </summary>
        /// <param name="model">Model BIPFormViewModel</param>
        /// <returns>true/false</returns>
        BIPFormViewModel SendBIPEmail(BIPFormViewModel model);

        void SendRegisterEmail(UserDB model);

        void SendForgotPassEmail(UserDB model);

        void SendArticleToSalesManago(SalesManagoSendArticleViewModel model, string salesManagoMail);
    }
}
