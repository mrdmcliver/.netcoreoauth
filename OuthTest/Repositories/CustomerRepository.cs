using OAuthTest.Interfaces;
using OAuthTest.Models;

namespace OAuthTest.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(AppDbContext context) : base(context)
        {
        }

        public Customer? FindByName(string name)
        {
            return base._context.Customer.Where(c => c.UserName == name).FirstOrDefault();
        }
    }
}
