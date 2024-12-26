namespace CustomEmailTemplate.Application.Implementations;

public class OrderService(IEmailSenderService emailSenderService,
    IStringLocalizer<OrderService> localizer,
    IEmailTemplateService emailTemplateService, ILogger<OrderService> logger) : IOrderService
{
    public async Task<ResultDto<GetOrderDto>> Add(OrderDto model)
    {
        try
        {
            //added to database
            //send email

            var html = await emailTemplateService.HtmlNotifyUserWithNewOrder(model);
            var pdfAttachment = await emailTemplateService.PdfOrderDetail(model);

            await emailSenderService.Send();
            return new ResultDto<GetOrderDto>
            {
                Data = new GetOrderDto
                {
                    Details = model.Details,
                    Id = model.Id,
                    ReferenceNumber = model.ReferenceNumber,
                    CreatedDate = model.CreatedDate,
                    EmailHtml = html.Data!,
                    Pdf = Convert.ToBase64String(pdfAttachment.Data!)
                },
                IsSuccess = true,
                Messages = [localizer["Added and email sent successfully"]]
            };
        }
        catch (Exception ex)
        {
            logger.LogError(ex, nameof(Add));
        }

        return new ResultDto<GetOrderDto>
        {
            IsSuccess = false
        };
    }
}