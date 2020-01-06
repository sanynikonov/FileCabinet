using Autofac;
using Autofac.Integration.WebApi;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using BLL;

namespace API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.DependencyResolver = ConfigService();

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        public static AutofacWebApiDependencyResolver ConfigService()
        {
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperBLLConfig>();
            });
            builder.Register(x => new Mapper(mapperConfig)).As<IMapper>();
            builder.RegisterModule<UnitAutofacConfig>();

            builder.RegisterType<UserService>().AsImplementedInterfaces();
            builder.RegisterType<SongService>().AsImplementedInterfaces();
            builder.RegisterType<CommentaryService>().AsImplementedInterfaces();
            builder.RegisterType<SongsContainerService>().AsImplementedInterfaces();


            var container = builder.Build();

            AutofacWebApiDependencyResolver autofacWebApiDependencyResolver = new AutofacWebApiDependencyResolver(container);

            return autofacWebApiDependencyResolver;
        }
    }
}
