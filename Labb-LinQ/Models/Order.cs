using System.ComponentModel.DataAnnotations.Schema;

namespace Labb_LinQ.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = null!;

        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }

}
