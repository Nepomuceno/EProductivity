using System.Threading.Tasks;
using EProductivity.Core.Model;
using EProductivity.Core.Model.Data;
using EProductivity.Core.Model.Data.EF;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using EProductivity.Web.Models;
using Microsoft.Owin.Security.DataProtection;

namespace EProductivity.Web
{
    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.

    public class EProductivityUserManager : UserManager<EProductivityUser>
    {
        public EProductivityUserManager(IUserStore<EProductivityUser> store)
            : base(store)
        {
            store = new UserStore<EProductivityUser>();
            this.UserValidator = new UserValidator<EProductivityUser>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            // Configure validation logic for passwords
            this.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };
            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug in here.
            this.RegisterTwoFactorProvider("PhoneCode", new PhoneNumberTokenProvider<EProductivityUser>
            {
                MessageFormat = "Your security code is: {0}"
            });
            this.RegisterTwoFactorProvider("EmailCode", new EmailTokenProvider<EProductivityUser>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is: {0}"
            });
            this.EmailService = new EmailService();
            this.SmsService = new SmsService();
            //if (dataProtectionProvider != null)
            //{
            //    this.UserTokenProvider = new DataProtectorTokenProvider<EProductivityUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            //}
        }
   }

    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your sms service here to send a text message.
            return Task.FromResult(0);
        }
    }
}
