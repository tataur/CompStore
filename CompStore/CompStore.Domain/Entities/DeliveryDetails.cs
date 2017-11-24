using System;
using System.ComponentModel.DataAnnotations;

namespace CompStore.Domain.Entities
{
    public class DeliveryDetails
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Введите имя")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Введите фамилию")]
        public string SecondName { get; set; }

        [Required(ErrorMessage = "Введите электронный адрес")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите номар телефона")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Введите адрес")]
        public string Line { get; set; }

        [Required(ErrorMessage = "Введите страну")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Введите город")]
        public string City { get; set; }
    }

    public class OrderLine
    {
        public Guid Id { get; set; }
        public Guid DeliveryDetailsId { get; set; }
        public Guid CompId { get; set; }
        public int Quantity { get; set; }
        public OrderStatus Status { get; set; }
    }

    public enum OrderStatus
    {
        Wait, Work, WorkDone, Delivery, Done
    }
}
