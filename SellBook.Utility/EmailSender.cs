using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellBook.Utility
{
	public class EmailSender : IEmailSender
	{
		public string SendGridSecret { get; set; }

		public EmailSender(IConfiguration configuration)
		{
			SendGridSecret = configuration.GetValue<String>("SendGrid:SecretKey");
		}
		public Task SendEmailAsync(string email, string subject, string htmlMessage)
		{

			var client = new SendGridClient(SendGridSecret);

			var from = new EmailAddress("huyhoang11202@gmail.com");
			var to = new EmailAddress(email);
			var message = MailHelper.CreateSingleEmail(from, to, subject, "", htmlMessage);

			return client.SendEmailAsync(message);
		}
	}
}
