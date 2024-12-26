namespace CustomEmailTemplate.Application.Interfaces;

public interface IEmailSenderService
{
    public Task Send();
}