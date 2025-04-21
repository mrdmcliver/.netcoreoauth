using Microsoft.AspNetCore.Identity;

namespace OAuthTest.Models
{
    public class Customer
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public List<ProductOrder> ProductOrders { get; set; } = new();
        
    }
}
