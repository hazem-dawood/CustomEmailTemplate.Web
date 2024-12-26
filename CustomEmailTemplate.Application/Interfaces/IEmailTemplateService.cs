namespace CustomEmailTemplate.Application.Interfaces;

public interface IEmailTemplateService
{
    public Task<ResultDto<string>> HtmlNotifyUserWithNewOrder(OrderDto model);

    public Task<ResultDto<byte[]>> PdfOrderDetail(OrderDto model);
}