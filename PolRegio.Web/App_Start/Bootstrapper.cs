using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using AutoMapper;
using PolRegio.Cms.Trees;
using PolRegio.Domain.Models.Database;
using PolRegio.Domain.Models.View.Account;
using PolRegio.Domain.Services.Accordions;
using PolRegio.Domain.Services.Account;
using PolRegio.Domain.Services.AdvertisingOfSalesModel;
using PolRegio.Domain.Services.Article;
using PolRegio.Domain.Services.BipModels;
using PolRegio.Domain.Services.CacheService;
using PolRegio.Domain.Services.Config;
using PolRegio.Domain.Services.Database;
using PolRegio.Domain.Services.DisabilitiesSupport;
using PolRegio.Domain.Services.Home;
using PolRegio.Domain.Services.ISS;
using PolRegio.Domain.Services.Layout;
using PolRegio.Domain.Services.News;
using PolRegio.Domain.Services.Notice;
using PolRegio.Domain.Services.Offers;
using PolRegio.Domain.Services.Search;
using PolRegio.Domain.Services.Shared;
using PolRegio.Domain.Services.Tags;
using PolRegio.Domain.Services.Tickets;
using PolRegio.Domain.Services.Wiremaps;
using PolRegio.Services.Accordions;
using PolRegio.Services.Account;
using PolRegio.Services.AdvertisingOfSalesModel;
using PolRegio.Services.Article;
using PolRegio.Services.BipModels;
using PolRegio.Services.CacheService;
using PolRegio.Services.Config;
using PolRegio.Services.Database;
using PolRegio.Services.DisabilitiesSupport;
using PolRegio.Services.Home;
using PolRegio.Services.ISS;
using PolRegio.Services.Layout;
using PolRegio.Services.News;
using PolRegio.Services.Notice;
using PolRegio.Services.Offers;
using PolRegio.Services.Search;
using PolRegio.Services.Shared;
using PolRegio.Services.Tags;
using PolRegio.Services.Ticket;
using PolRegio.Services.Wiremaps;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using PolRegio.Domain.Models.Events;
using PolRegio.Domain.Services.EPodroznik;
using PolRegio.Domain.Services.EventBus;
using PolRegio.Domain.Services.Koleo;
using PolRegio.Services.EventBus;
using PolRegio.Domain.Services.SalesManago;
using PolRegio.Services.EPodroznik;
using PolRegio.Services.Koleo;
using PolRegio.Services.SalesManago;
using Umbraco.Core;
using Umbraco.Core.Logging;
using Umbraco.Core.Persistence;
using Umbraco.Core.Services;
using Umbraco.Web;

namespace PolRegio.Web.App_Start
{
    /// <summary>
    /// Klasa bootstrap zawierająca rejestrację interfejsów poprzez Autofac
    /// </summary>
    public class Bootstrapper : IApplicationEventHandler
    {
        /// <summary>
        /// Metoda OnApplicationInitialized
        /// </summary>
        /// <param name="umbracoApplication">obiekt UmbracoApplicationBase</param>
        /// <param name="applicationContext">obiekt ApplicationContext</param>
        public void OnApplicationInitialized(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            ConfigurationReader.ReadConfiguration();
        }
        /// <summary>
        /// Metoda OnApplicationStarted
        /// </summary>
        /// <param name="umbracoApplication">obiekt UmbracoApplicationBase</param>
        /// <param name="applicationContext">obiekt ApplicationContext</param>
        public void OnApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FiltersConfig.RegisterFilters(GlobalFilters.Filters);

            GlobalConfiguration.Configuration.Formatters.XmlFormatter.UseXmlSerializer = true;
            GlobalConfiguration.Configuration.Formatters.XmlFormatter.WriterSettings.OmitXmlDeclaration = false;

            SetupAutoMapper();
            SetupCustomTables(applicationContext);

            var config = GlobalConfiguration.Configuration;

            //Dependency injection autofac builder
            var builder = new ContainerBuilder();
            //builder.RegisterInstance(ApplicationContext.Current).AsSelf();
            builder.RegisterApiControllers(typeof(UmbracoApplication).Assembly);
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterApiControllers(typeof(ArticleTypeDBTreeController).Assembly);

            //Rejestracja interfejsów
            builder.RegisterType<ConfigService>().As<IConfigService>();
            builder.RegisterType<LayoutService>().As<ILayoutService>();
            builder.RegisterType<HomeService>().As<IHomeService>();
            builder.RegisterType<NewsService>().As<INewsService>();
            builder.RegisterType<DBService>().As<IDBService>();
            builder.RegisterType<UmbracoHelperService>().As<IUmbracoHelperService>();
            builder.RegisterType<ISSApiService>().As<IISSApiService>();
            builder.RegisterType<RegioApi>().As<IRegioApi>();
            builder.RegisterType<ArticleService>().As<IArticleService>();
            builder.RegisterType<CacheService>().As<ICacheService>();
            builder.RegisterType<BoxListService>().As<IBoxListService>();
            builder.RegisterType<AdvertisingOfSalesService>().As<IAdvertisingOfSalesService>();
            builder.RegisterType<ContractNoticesService>().As<IContractNoticesService>();
            builder.RegisterType<OffersService>().As<IOffersService>();
            builder.RegisterType<AccordionService>().As<IAccordionService>();
            builder.RegisterType<WiremapService>().As<IWiremapService>();
            builder.RegisterType<SearchService>().As<ISearchService>();
            builder.RegisterType<TicketOfficesService>().As<ITicketOfficesService>();
            builder.RegisterType<BIPService>().As<IBIPService>();
            builder.RegisterType<EmailService>().As<IEmailService>();
            builder.RegisterType<DisabilitiesSupportService>().As<IDisabilitiesSupportService>();
            builder.RegisterType<TagsService>().As<ITagsService>();
            builder.RegisterType<AccountService>().As<IAccountService>();
            builder.RegisterType<UserRepository>().As<IUserRepository>();
            builder.RegisterType<MappingService>().As<IMappingService>();
            builder.RegisterType<HashingService>().As<IHashingService>();
            builder.RegisterType<SocialMediaService>().As<ISocialMediaService>();
            builder.RegisterType<SalesManagoService>().As<ISalesManagoService>();
            builder.RegisterType<SalesManagoRecommendedArticleService>().As<ISalesManagoRecommendedArticleService>();
            builder.RegisterType<EPodroznikService>().As<IEPodroznikService>();
            builder.RegisterType<KoleoService>().As<IKoleoService>();
            builder.RegisterApiControllers(typeof(Umbraco.Forms.Web.Controllers.UmbracoFormsController).Assembly);


            //register event bus
            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .Where(x => x.IsAssignableTo<IEventHandler>())
                .AsImplementedInterfaces();
            builder.Register<Func<Type, IEnumerable<IEventHandler>>>(c =>
            {
                var ctx = c.Resolve<IComponentContext>();
                return t =>
                {
                    var handlersCollectionType = typeof(IEnumerable<>).MakeGenericType(t);
                    return (IEnumerable<IEventHandler>) ctx.Resolve(handlersCollectionType);
                };
            });
            builder.RegisterType<EventBus>().As<IEventBus>();
            
            var container = builder.Build();

            var resolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = resolver;
            //setup the mvc dependency resolver to user autofac
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            RegisterUmbracoEvent(container.Resolve<IEventBus>());
        }

        /// <summary>
        /// Metoda OnApplicationStarting
        /// </summary>
        /// <param name="umbracoApplication">obiekt UmbracoApplicationBase</param>
        /// <param name="applicationContext">obiekt ApplicationContext</param>
        public void OnApplicationStarting(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext) { }

        private void SetupCustomTables(ApplicationContext applicationContext)
        {
            var logger = LoggerResolver.Current.Logger;
            var dbContext = applicationContext.DatabaseContext;
            var schemaHelper = new DatabaseSchemaHelper(dbContext.Database, logger, dbContext.SqlSyntax);

            if (!schemaHelper.TableExist("PolRegioUser"))
                schemaHelper.CreateTable<UserDB>(overwrite: false);

            if (!schemaHelper.TableExist("PolRegioUserRegion"))
                schemaHelper.CreateTable<UserRegionDB>(overwrite: false);

            if (!schemaHelper.TableExist("PolRegioAgreement"))
                schemaHelper.CreateTable<AgreementDB>(overwrite: false);

            if (!schemaHelper.TableExist("PolRegioUserAgreement"))
                schemaHelper.CreateTable<UserAgreementDB>(overwrite: false);

            if (!schemaHelper.TableExist("PolRegioStations"))
                schemaHelper.CreateTable<StationDB>(overwrite: false);
        }

        private void SetupAutoMapper()
        {
            Mapper.CreateMap<string, string>().ConvertUsing(x => x ?? string.Empty);

            Mapper.CreateMap<RegisterFormViewModel, UserDB>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => Guid.NewGuid().GetHashCode()))
                .ForMember(x => x.IsActive, opt => opt.UseValue(false));

            Mapper.CreateMap<UserDB,UserInfo>();

            Mapper.CreateMap<SocialMediaFormViewModel, UserDB>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => Guid.NewGuid().GetHashCode()))
                .ForMember(x => x.ActivationToken, opt => opt.UseValue(string.Empty))
                .ForMember(x => x.IsActive, opt => opt.UseValue(true));

            Mapper.CreateMap<UserDB, ProfileViewModel>()
                .ForMember(x => x.UserPassword, opt => opt.Ignore())
                .ForMember(x => x.UserConfirmPassword, opt => opt.Ignore());

            Mapper.CreateMap<ProfileViewModel, UserDB>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.UserEmail, opt => opt.Ignore())
                .ForMember(x => x.UserPassword, opt => opt.Ignore());
        }

        private void RegisterUmbracoEvent(IEventBus bus)
        {
            ContentService.Saved += (sender, args) => bus.Send(new UmbracoContentSavedEvent(sender, args));
        }
    }
}