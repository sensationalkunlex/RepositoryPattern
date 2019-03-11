using ApplicationCore.Repository;
using ApplicationCore.Services;
using ApplicationCore.UnitofWork;
using Data.Context;
using Data.Database;
using Data.EntityFrameWork;
using Data.EntityFrameWork.UnitofWork;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using SimpleInjector.Lifestyles;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewProject.App_Start
{
    public class SimpleInjectorConfig
    {



        public static void RegisterComponents()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = Lifestyle.CreateHybrid(new WebRequestLifestyle(), new AsyncScopedLifestyle());
            container.Register<IDbContext>(() =>
            {
                return new DatabaseContext();
            }, Lifestyle.Scoped);

            container.Register(() =>
            {
                return container.GetInstance<IDbContext>() as DbContext;
            }, Lifestyle.Scoped);

            container.Register(() =>
            {
                return new HttpContextWrapper(HttpContext.Current) as HttpContextBase;
            }, Lifestyle.Scoped);

           






            container.Register<IUnitOfWork, UnitOfWork>(Lifestyle.Scoped);
            container.Register(typeof(IRepository<>), typeof(Repository<>), Lifestyle.Scoped);





         
            var coreAssembly = typeof(TestService).Assembly;

            var factories = from type in coreAssembly.GetExportedTypes()
                            where type.Namespace == "ApplicationCore.Services"
                            select new { Implementation = type };

            foreach (var reg in factories)
                container.Register(reg.Implementation, reg.Implementation, Lifestyle.Scoped);



            container.Verify();
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));


        }
    }
}