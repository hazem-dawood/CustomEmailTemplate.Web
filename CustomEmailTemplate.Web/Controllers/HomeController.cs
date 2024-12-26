namespace CustomEmailTemplate.Web.Controllers;

public class HomeController(IOrderService orderService) : Controller
{
    public IActionResult Index() => View();

    public IActionResult Privacy() => View();

    #region Ajax

    [HttpPost]
    public async Task<JsonResult> AddOrder(OrderDto model) => Json(await orderService.Add(model));

    #endregion
}