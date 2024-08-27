namespace Contracts.Configuarations
{
    public interface IEmailSMTPSettings
    {
        string DisplayName { get; set; }
        bool EnableVerfication { get; set; }
        string From { get; set; }
        string SMTPServer { get; set; }
        bool UseSsl { get; set; }
        int Port { get; set; }
        string Username { get; set; }
        string Password { get; set; }
    }
}