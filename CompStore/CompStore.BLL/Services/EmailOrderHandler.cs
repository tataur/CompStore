using System.Net;
using System.Net.Mail;
using System.Text;
using CompStore.Domain.Entities;
using CompStore.Domain.Interfaces;
using CompStore.Domain.Models;

namespace CompStore.BLL.Services
{
    public class EmailOrderHandler : IOldOrderHandler
    {
        private readonly EmailSettings _emailSettings;

        public EmailOrderHandler(EmailSettings settings)
        {
            _emailSettings = settings;
        }

        public void SendMessage(ProductList productList, DeliveryDetails deliveryDetails)
        {
            using (var mClient = new SmtpClient())
            {
                mClient.EnableSsl = _emailSettings.UseSsl;
                mClient.Host = _emailSettings.ServerName;
                mClient.Port = _emailSettings.ServerPort;
                mClient.UseDefaultCredentials = false;
                mClient.Credentials = new NetworkCredential(_emailSettings.Username, _emailSettings.Password);

                if (_emailSettings.WriteAsFile)
                {
                    mClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    mClient.PickupDirectoryLocation = _emailSettings.FileLocation;
                    mClient.EnableSsl = false;
                }

                StringBuilder body = new StringBuilder()
                    .AppendLine("Заказ обработан")
                    .AppendLine(" ")
                    .AppendLine("Товары:");

                foreach (var line in productList.Lines)
                {
                    var subtotal = line.Comp.Price * line.Quantity;
                    body.AppendFormat("{0} x {1} (Итого: {2:c})", line.Quantity, line.Comp.Name, subtotal);
                }

                body.AppendFormat("Общая стоимость: {0:c}", productList.TotalValue())
                    .AppendLine(" ")
                    .AppendLine("Доставка:")
                    .AppendLine(deliveryDetails.FirstName)
                    .AppendLine(deliveryDetails.SecondName)
                    .AppendLine(deliveryDetails.Street)
                    .AppendLine(deliveryDetails.Country)
                    .AppendLine(deliveryDetails.City)
                    .AppendLine(" ");

                MailMessage mailMessage = new MailMessage(
                    _emailSettings.MailFromAddress,
                    _emailSettings.MailToAddress,
                    "Заказ отправлен",
                    body.ToString());

                if (_emailSettings.WriteAsFile)
                {
                    mailMessage.BodyEncoding = Encoding.UTF8;
                }

                mClient.Send(mailMessage);
            }
        }

        public bool Process(OrderData orderData)
        {
            throw new System.NotImplementedException();
        }
    }

    public class EmailSettings
    {
        public string MailToAddress = "newtestuser2017@yandex.ru";
        public string MailFromAddress = "CompStore@gmail.com";
        public bool UseSsl = true;
        public string Username = "MySmtpUsername";
        public string Password = "MySmtpPassword";
        public string ServerName = "CompStore.com";
        public int ServerPort = 587;
        public bool WriteAsFile = true;
        public string FileLocation = @"c:\comp_store_emails";
    }
}