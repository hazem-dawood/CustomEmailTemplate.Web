namespace CustomEmailTemplate.Application.Interfaces;

public interface IOrderService
{
    public Task<ResultDto<GetOrderDto>> Add(OrderDto model);
}