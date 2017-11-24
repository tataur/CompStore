using CompStore.Domain.Abstract;
using CompStore.Domain.Entities;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace CompStore.Domain.Concrete
{
    public class EmailOrderHandler : IOrderHandler
    {
        private EmailSettings emailSettings;

        public EmailOrderHandler(EmailSettings settings)
        {
            emailSettings = settings;
        }

        public void HandleOrder(ShoppingList shoppingList, DeliveryDetails deliveryDetails)
        {
            throw new System.NotImplementedException();
        }

        // найти незанятого сборщика Worker (статус заказа Wait, сборщика Wait)

        // передать заказ сборщику, изменить статус заказа и сборщика (статус заказа Work, сборщика Work)

        // после сборки заказа изменить статус заказа и сборщика (статус заказа WorkDone, сборщика Wait)

        // передать его в доставку Deliveryman (статус заказа Delivery, доставщика Work)

        // после доставки изменить статус заказа и доставщика, (статус заказа Done, доставщика Wait) 
        
        // отправить сообщение заказчику
        public void SendMessage(ShoppingList shoppingList, DeliveryDetails deliveryDetails)
        {
            using (var mClient = new SmtpClient())
            {
                mClient.EnableSsl = emailSettings.UseSsl;
                mClient.Host = emailSettings.ServerName;
                mClient.Port = emailSettings.ServerPort;
                mClient.UseDefaultCredentials = false;
                mClient.Credentials = new NetworkCredential(emailSettings.Username, emailSettings.Password);

                if (emailSettings.WriteAsFile)
                {
                    mClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    mClient.PickupDirectoryLocation = emailSettings.FileLocation;
                    mClient.EnableSsl = false;
                }

                StringBuilder body = new StringBuilder()
                    .AppendLine("Заказ обработан")
                    .AppendLine(" ")
                    .AppendLine("Товары:");

                foreach (var line in shoppingList.Lines)
                {
                    var subtotal = line.Comp.Price * line.Quantity;
                    body.AppendFormat("{0} x {1} (Итого: {2:c})", line.Quantity, line.Comp.Name, subtotal);
                }

                body.AppendFormat("Общая стоимость: {0:c}", shoppingList.ComputeTotalValue())
                    .AppendLine(" ")
                    .AppendLine("Доставка:")
                    .AppendLine(deliveryDetails.FirstName)
                    .AppendLine(deliveryDetails.SecondName)
                    .AppendLine(deliveryDetails.Line)
                    .AppendLine(deliveryDetails.Country)
                    .AppendLine(deliveryDetails.City)
                    .AppendLine(" ");

                MailMessage mailMessage = new MailMessage(
                    emailSettings.MailFromAddress,
                    emailSettings.MailToAddress,
                    "Заказ отправлен",
                    body.ToString());

                if (emailSettings.WriteAsFile)
                {
                    mailMessage.BodyEncoding = Encoding.UTF8;
                }

                mClient.Send(mailMessage);
            }
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
