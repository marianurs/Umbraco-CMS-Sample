using PolRegio.Domain.Models.Database;
using PolRegio.Domain.Models.View.Account;
using PolRegio.Domain.Services.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Umbraco.Core;
using Umbraco.Core.Persistence;
using Umbraco.Web;

namespace PolRegio.Services.Account
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _dbContext;
        private readonly UmbracoHelper _umbracoHelper;

        public UserRepository()
        {
            _umbracoHelper = new UmbracoHelper(UmbracoContext.Current);
            _dbContext = UmbracoContext.Current.Application.DatabaseContext;
        }

        public bool DoesUserExist(string email)
        {
            var query = new Sql()
                .Select("UserEmail")
                .From<UserDB>(_dbContext.SqlSyntax)
                .Where<UserDB>(x => x.UserEmail == email);
            var results = _dbContext.Database.Fetch<UserDB>(query);
            return results.Count != 0;
        }

        public UserDB GetUserBy(Expression<Func<UserDB, bool>> predicate)
        {
            var query = new Sql()
                .Select("*")
                .From<UserDB>(_dbContext.SqlSyntax)
                .Where<UserDB>(predicate);

            return _dbContext.Database
                .SingleOrDefault<UserDB>(query);
        }

        public List<UserDB> GetUsersBy(Expression<Func<UserDB, bool>> predicate)
        {
            var query = new Sql()
                .Select("*")
                .From<UserDB>(_dbContext.SqlSyntax)
                .Where<UserDB>(predicate);

            return _dbContext.Database.Query<UserDB>(query).ToList();
        }

        public List<UserDB> GetAllUsers()
        {
            var query = new Sql()
                .Select("*")
                .From<UserDB>(_dbContext.SqlSyntax);

            return _dbContext
                .Database
                .Query<UserDB>(query)
                .ToList();
        }

        public List<AgreementViewModel> GetAllActiveAgreements()
        {
            var query = new Sql()
                .Select("*")
                .From<AgreementDB>(_dbContext.SqlSyntax)
                .Where<AgreementDB>(x => x.IsActive);

            var agreements = _dbContext.Database
                .Fetch<AgreementDB>(query);

            return agreements
                .Select(x => new AgreementViewModel
                {
                    Id = x.Id,
                    Text = x.Text,
                    Required = x.IsRequired,
                    Preference = x.IsPreference
                })
                .ToList();
        }

        public List<AgreementViewModel> GetAgreementsViewsForUser(UserDB user)
        {
            var query = new Sql()
                .Select("*")
                .From<UserAgreementDB>(_dbContext.SqlSyntax)
                .LeftJoin<AgreementDB>(_dbContext.SqlSyntax)
                .On<UserAgreementDB, AgreementDB>(_dbContext.SqlSyntax, x => x.AgreementId, x => x.Id)
                .Where<UserAgreementDB>(x => x.UserId == user.Id);

            var result = _dbContext.Database
                .Fetch<UserAgreementDB, AgreementDB, AgreementViewModel>((userAgreement, agreement) => new AgreementViewModel
                {
                    Id = userAgreement.Id,
                    Text = agreement.Text,
                    Value = userAgreement.Value,
                    Required = agreement.IsRequired,
                    Preference = agreement.IsPreference
                }, query);

            return result;
        }

        public List<AgreementDB> GetAgreedAgreementsForUser(UserDB user)
        {
            var query = new Sql()
                .Select("*")
                .From<UserAgreementDB>(_dbContext.SqlSyntax)
                .LeftJoin<AgreementDB>(_dbContext.SqlSyntax)
                .On<UserAgreementDB, AgreementDB>(_dbContext.SqlSyntax, x => x.AgreementId, x => x.Id)
                .Where<UserAgreementDB>(x => x.UserId == user.Id && x.Value);

            var result = _dbContext.Database
                .Fetch<UserAgreementDB, AgreementDB, AgreementDB>((userAgreement, agreement) => agreement, query);

            return result;
        }

        public List<KeyValuePair<string, string>> GetAllRegions()
        {
            var query = new Sql().Select("*").From("polregioregion");
            var regionsFromDb = _dbContext.Database.Fetch<RegionDB>(query);
            var regionsDictionary = regionsFromDb.ToDictionary(
                x => x.Id.ToString(),
                x => _umbracoHelper.GetDictionaryValue(x.DictionaryKey));

            var regions = regionsDictionary
                .Where(x => !string.IsNullOrWhiteSpace(x.Value))
                .OrderBy(x => x.Key)
                .ToList();

            return regions;
        }

        public List<RegionDB> GetRegionsForUser(UserDB user)
        {
            var query = new Sql()
                .Select("*")
                .From<UserRegionDB>(_dbContext.SqlSyntax)
                .LeftJoin<RegionDB>(_dbContext.SqlSyntax)
                .On<UserRegionDB, RegionDB>(_dbContext.SqlSyntax, x => x.RegionId, x => x.Id)
                .Where<UserRegionDB>(x => x.UserId == user.Id);

            var result = _dbContext.Database
                .Fetch<UserRegionDB, RegionDB, RegionDB>((userRegion, region) => region, query);

            return result;
        }
        public void Insert(UserDB user, string[] selectedRegions, List<AgreementViewModel> agreements)
        {
            _dbContext.Database.Insert(user);

            var nonEmptyRegions = selectedRegions.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
            foreach (var region in nonEmptyRegions)
            {
                _dbContext.Database.Insert(new UserRegionDB
                {
                    RegionId = int.Parse(region),
                    UserId = user.Id
                });
            }

            var activeAgreements = GetAllActiveAgreements();
            foreach (var agreement in agreements)
            {
                var userAgreement = activeAgreements.Single(x => x.Text == agreement.Text);
                _dbContext.Database.Insert(new UserAgreementDB
                {
                    AgreementId = userAgreement.Id,
                    UserId = user.Id,
                    Value = agreement.Value
                });
            }
        }

        public void Update(UserDB user)
        {
            _dbContext.Database.Update(user);
        }

        public void Update(UserDB user, string[] selectedRegions, List<AgreementViewModel> agreements)
        {
            UpdateRegionsForUser(user, selectedRegions);
            UpdateAgreements(user, agreements);

            _dbContext.Database.Update(user);
        }

        public void Delete(UserDB user)
        {
            var deleteAllConnectedRegionsQuery = new Sql()
                .Where<UserRegionDB>(x => x.UserId == user.Id);

            var deleteAllConnectedAgreementsQuery = new Sql()
                .Where<UserAgreementDB>(x => x.UserId == user.Id);

            _dbContext.Database.Delete<UserRegionDB>(deleteAllConnectedRegionsQuery);
            _dbContext.Database.Delete<UserAgreementDB>(deleteAllConnectedAgreementsQuery);
            _dbContext.Database.Delete(user);
        }

        private void UpdateAgreements(UserDB user, List<AgreementViewModel> agreements)
        {
            var userAgreementsQuery = new Sql()
                .Select("*")
                .From<UserAgreementDB>(_dbContext.SqlSyntax)
                .Where<UserAgreementDB>(x => x.UserId == user.Id);

            var userAgreements = _dbContext.Database
                .Fetch<UserAgreementDB>(userAgreementsQuery);

            foreach (var userAgreement in userAgreements)
            {
                var inputAgreement = agreements.Single(x => x.Id == userAgreement.Id);
                userAgreement.Value = inputAgreement.Value;
                _dbContext.Database.Update(userAgreement);
            }
        }

        private void UpdateRegionsForUser(UserDB user, string[] regions)
        {
            var deleteAllConnectedRegionsQuery = new Sql()
                .Select("*")
                .From<UserRegionDB>(_dbContext.SqlSyntax)
                .Where<UserRegionDB>(x => x.UserId == user.Id);

            _dbContext.Database.Delete<UserRegionDB>(deleteAllConnectedRegionsQuery);

            foreach (var region in regions)
            {
                _dbContext.Database.Insert(new UserRegionDB
                {
                    RegionId = int.Parse(region),
                    UserId = user.Id
                });
            }
        }
    }
}
