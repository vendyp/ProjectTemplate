namespace BoilerPlate.Core.Abstractions;

public interface IEmailService
{
    Task SendEmailAsync(string from, string to, string subject, string htmlContent,
        CancellationToken cancellationToken);
}