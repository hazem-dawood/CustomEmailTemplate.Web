namespace CustomEmailTemplate.Application.Implementations;

internal class EmailTemplateService(IExecuteViewAsPdfService executeViewAsPdfService) : IEmailTemplateService
{
    public async Task<ResultDto<string>> HtmlNotifyUserWithNewOrder(OrderDto model)
    {
        var viewModel = new ExecuteViewAsPdfDto<OrderDto>
        {
            Model = model,
            FullViewPath = "Views/Orders/NotifyUserWithNewOrder.cshtml",
        };
        return await executeViewAsPdfService.AsHtml(viewModel);
    }

    public async Task<ResultDto<byte[]>> PdfOrderDetail(OrderDto model)
    {
        var viewModel = new ExecuteViewAsPdfDto<OrderDto>
        {
            Model = model,
            FullViewPath = "Views/Orders/OrderDetail.cshtml",
        };
        return await executeViewAsPdfService.AsPdf(viewModel);
    }
}