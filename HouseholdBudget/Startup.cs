using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HouseholdBudget.Startup))]
namespace HouseholdBudget
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
