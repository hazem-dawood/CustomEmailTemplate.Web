namespace CustomEmailTemplate.Application.Models;

public record GetOrderDto : OrderDto
{
    public string EmailHtml { get; set; } = string.Empty;

    public string Pdf { get; set; } = string.Empty;
}
public record OrderDto
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string ReferenceNumber { get; set; } = Guid.NewGuid().ToString();

    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public double Total => Details.Sum(a => a.Total);

    public List<OrderDetailDto> Details { get; set; } = [];

}

public record OrderDetailDto
{
    public string ProductName { get; set; } = string.Empty;

    public double Price { get; set; } = 0;

    public int Quantity { get; set; }

    public double Total => Price * Quantity;
}