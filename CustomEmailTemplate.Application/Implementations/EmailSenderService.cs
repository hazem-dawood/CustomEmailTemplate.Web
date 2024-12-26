namespace CustomEmailTemplate.Application.Implementations;

internal class EmailSenderService : IEmailSenderService
{
    public Task Send()
    {
        return Task.CompletedTask;
    }
}