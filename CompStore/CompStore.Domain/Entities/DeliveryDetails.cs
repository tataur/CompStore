using System.ComponentModel.DataAnnotations;

namespace CompStore.Domain.Entities
{
    public class DeliveryDetails : CommonEntity
    {
        [Required(ErrorMessage = "Введите имя")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Введите фамилию")]
        public string SecondName { get; set; }

        [Required(ErrorMessage = "Введите электронный адрес")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите номар телефона")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Введите адрес")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Введите страну")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Введите город")]
        public string City { get; set; }
    }
}