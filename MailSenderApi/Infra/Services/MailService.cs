﻿using System.Net;
using System.Net.Mail;

namespace MailSenderApi.Infra.Services;

public class MailService : IMailService
{
    private string smtpAddress => "smtp.gmail.com";
    private int portNumber => 587;
    private string emailFromAddress => "sendmail.dotnetnapratica@gmail.com";
    private string password => "dotnet100";

    public void AddEmailsToMailMessage(MailMessage mailMessage, string[] emails)
    {
        foreach (var email in emails)
        {
            mailMessage.To.Add(email);
        }
    }

    public void SendMail(string[] emails, string subject, string body, bool isHtml = false)
    {
        using (var mailMessage = new MailMessage())
        {
            mailMessage.From = new MailAddress(emailFromAddress);
            AddEmailsToMailMessage(mailMessage, emails);
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = isHtml;
            using (var smtp = new SmtpClient(smtpAddress, portNumber))
            {
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(emailFromAddress, password);
                smtp.SendMailAsync(mailMessage);
            }
        }
    }
}
