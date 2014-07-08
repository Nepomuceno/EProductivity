using System.Data.Entity;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using EProductivity.Core.Model;
using EProductivity.Core.Model.Data;
using EProductivity.Core.Model.Data.EF;
using EProductivity.Core.Service;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.Provider;
using Owin;
using System;
using EProductivity.Web.Models;
using SimpleInjector;
using SimpleInjector.Integration.Web.Mvc;

namespace EProductivity.Web
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<EProductivityUserManager, EProductivityUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });
            
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Create the container as usual.
            var container = new Container();

            // Register your types, for instance:
            container.RegisterPerWebRequest<IOrganizationService, OrganizationService>();
            container.RegisterPerWebRequest<IModelContext, EProductivityContext>();
            container.RegisterPerWebRequest<IUserStore<EProductivityUser>>(() => new UserStore<EProductivityUser>((DbContext) container.GetInstance<IModelContext>()));
            container.RegisterPerWebRequest<EProductivityUserManager>();


            // This is an extension method from the integration package.
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            // This is an extension method from the integration package as well.
            container.RegisterMvcIntegratedFilterProvider();

            container.Verify();

            DependencyResolver.SetResolver(
                new SimpleInjectorDependencyResolver(container));


        }
    }
}