using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthenticationSample
{
    public class ApplicationUserClaimsPrincipalFactory: UserClaimsPrincipalFactory<ApplicationUser>
    {
        public ApplicationUserClaimsPrincipalFactory(UserManager<ApplicationUser> userManager,
            IOptions<IdentityOptions> optionAccessor): base(userManager, optionAccessor)
        {
        }

        public async override Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
        {
            var principal = await base.CreateAsync(user);

            // Add custom claims to the user's identity
            ((ClaimsIdentity)principal.Identity).AddClaim(new Claim("CompanyStartedDate", user.CompanyStartedDate.ToShortDateString()));
            ((ClaimsIdentity)principal.Identity).AddClaim(new Claim("Address", user.Address));
            return principal;
        }
    }
}
