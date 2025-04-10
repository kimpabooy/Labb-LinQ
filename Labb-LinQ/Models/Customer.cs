using System.ComponentModel.DataAnnotations;

namespace Labb_LinQ.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        
        [EmailAddress]
        public string Email { get; set; } = null!;
        
        [Phone]
        public string Phone { get; set; } = null!;
        public string Address { get; set; } = null!;

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}