namespace GestionCustomers.Domain.Models;

public class Order
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public string Status { get; set; } = string.Empty;
    
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }

    public List<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}