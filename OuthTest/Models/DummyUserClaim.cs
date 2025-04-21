using Microsoft.AspNetCore.Identity;

namespace OAuthTest.Models
{
    public class DummyUserClaim : IdentityUserClaim<string> 
    {
    }
    public class DummyRole: IdentityRole
    {

    }
    public class DummyUserRole : IdentityUserRole<string> {
    }

    public class DummyUserLogin : IdentityUserLogin<string> {
    }

    public class DummyRoleClaim : IdentityRoleClaim<string> {
    }
    
    public class DummyUserToken : IdentityUserToken<string>
    {

    }
}
