using PolRegio.Domain.Models.View.Account;

namespace PolRegio.Domain.Services.Account
{
    public interface ISocialMediaService
    {
        bool ValidateToken(SocialMediaFormViewModel model);
    }
}
