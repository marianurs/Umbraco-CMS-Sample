using System.Collections.Generic;

namespace PolRegio.Domain.Services.SalesManago
{
    public interface ISalesManagoRecommendedArticleService
    {
        List<int> GetOffersIdForCurrentUser();
    }
}