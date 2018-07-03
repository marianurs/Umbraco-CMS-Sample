using PolRegio.Domain.Models.Database;
using PolRegio.Domain.Models.View.Account;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PolRegio.Domain.Services.Account
{
    public interface IUserRepository
    {
        bool DoesUserExist(string email);

        UserDB GetUserBy(Expression<Func<UserDB, bool>> predicate);
        List<UserDB> GetUsersBy(Expression<Func<UserDB, bool>> predicate);
        List<UserDB> GetAllUsers();

        List<KeyValuePair<string, string>> GetAllRegions();

        List<RegionDB> GetRegionsForUser(UserDB user);

        List<AgreementViewModel> GetAllActiveAgreements();

        List<AgreementViewModel> GetAgreementsViewsForUser(UserDB user);

        List<AgreementDB> GetAgreedAgreementsForUser(UserDB user);

        void Insert(UserDB user, string[] selectedRegions, List<AgreementViewModel> agreements);

        void Update(UserDB user);

        void Update(UserDB user, string[] selectedRegions, List<AgreementViewModel> agreements);

        void Delete(UserDB user);
    }
}
