using OAuthTest.Models;

namespace OAuthTest.Interfaces
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        Customer? FindByName(string name);
    }
}
