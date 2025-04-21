using OAuthTest.Dtos;
using OAuthTest.Dtos.Form;

namespace OAuthTest.Interfaces
{
    public interface ICustomerService
    {
        public Task<AuthResultDto> Register(RegisterForm form);
        public Task<AuthResultDto> Login(LoginForm form);
        public Task<CustomerDto> GetProfile();
    }
}
