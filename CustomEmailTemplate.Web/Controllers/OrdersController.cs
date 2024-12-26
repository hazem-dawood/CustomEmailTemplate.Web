namespace CustomEmailTemplate.Web.Controllers;

public class OrdersController : Controller
{
    public IActionResult OrderDetail()
    {
        return new ViewAsPdf(new OrderDto
        {
            Details = [
                new OrderDetailDto
              {
                Price = 10.5,
                Quantity = 14,
                ProductName = "Test 1"
              },
                new OrderDetailDto
              {
                Price = 25.4,
                Quantity = 8,
                ProductName = "Test 2"
              }
            ]
        });
    }

    public IActionResult NotifyUserWithNewOrder()
    {
        return View(new OrderDto
        {
            Details = [
                new OrderDetailDto
              {
                Price = 10.5,
                Quantity = 14,
                ProductName = "Test 1"
              },
                new OrderDetailDto
              {
                Price = 25.4,
                Quantity = 8,
                ProductName = "Test 2"
              }
            ]
        });
    }
}

