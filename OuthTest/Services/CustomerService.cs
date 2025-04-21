using OAuthTest.Dtos;
using OAuthTest.Dtos.Form;
using OAuthTest.Exceptions;
using OAuthTest.Interfaces;
using OAuthTest.Models;
using Mapster;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace OAuthTest.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICustomerRepository _customerRepository;
        private readonly ITokenService _tokenService;
        private readonly IPasswordEncoder passwordEncoder;

        public CustomerService(IHttpContextAccessor httpContextAccessor, ICustomerRepository customerRepository, ITokenService tokenService, IPasswordEncoder passwordEncoder)
        {
            _httpContextAccessor = httpContextAccessor;
            _customerRepository = customerRepository;
            _tokenService = tokenService;
            this.passwordEncoder = passwordEncoder;
        }

        public async Task<CustomerDto> GetProfile()
        {
            var userId = _httpContextAccessor.HttpContext!.User!.FindFirstValue(Constants.ClaimTypes.Id);
            var customer = await _customerRepository.GetById(userId!);
            if (customer == null)
            {
                throw new NotFoundException("User not found");
            }
            return customer.Adapt<CustomerDto>();
        }

        public async Task<AuthResultDto> Login(LoginForm form)
        {
            var customer = _customerRepository.FindByName(form.Name);
            
            if (customer == null)
            {
                throw new UnauthorizedException("Bad Credentials");
            }

            if (!passwordEncoder.Matches(form.Password, customer.PasswordHash))
            {
                throw new UnauthorizedException("Bad Credentials");
            }

            string accessToken = _tokenService.GenerateToken(customer);

            SecurityToken token = new JwtSecurityToken();
            ClaimsPrincipal princ = new JwtSecurityTokenHandler().ValidateToken
            (
                accessToken, 
                new TokenValidationParameters() 
                {
                    ValidateAudience = false,
                    ValidIssuer = _tokenService.JwtConfig.ValidIssuer,
                    IssuerSigningKey = _tokenService.SymmetricKey
                },
                out token
            );

            if(_httpContextAccessor.HttpContext != null)
                await _httpContextAccessor.HttpContext.SignInAsync(princ);

            return new AuthResultDto(
                accessToken: accessToken
            );
        }

        public async Task<AuthResultDto> Register(RegisterForm form)
        {
            var existingCustomer = _customerRepository.FindByName(form.Name);
            if (existingCustomer != null)
            {
                throw new ConflictException("User already exists");
            }

            var cust = new Customer();
            cust.Id = (Guid.NewGuid().ToString());
            cust.UserName = form.Name;
            cust.PasswordHash = passwordEncoder.Encode(form.Password);

            try
            {
                _customerRepository.Save(cust);
                await _customerRepository.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                throw ex;
            }

            string accessToken = _tokenService.GenerateToken(cust);

            SecurityToken token = new JwtSecurityToken();
            ClaimsPrincipal princ = new JwtSecurityTokenHandler().ValidateToken
            (
                accessToken,
                new TokenValidationParameters()
                {
                    ValidateAudience = false,
                    ValidIssuer = _tokenService.JwtConfig.ValidIssuer,
                    IssuerSigningKey = _tokenService.SymmetricKey
                },
                out token
            );

            if (_httpContextAccessor.HttpContext != null)
                await _httpContextAccessor.HttpContext.SignInAsync(princ);

            return new AuthResultDto(
                accessToken: accessToken
            );
        }
    }
}
