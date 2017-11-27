using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CompStore.Domain.Entities
{
    public class Employee : CommonEntity
    {
        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Пожалуйста, введите имя")]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        [Required(ErrorMessage = "Пожалуйста, введите фамилию")]
        public string SecondName { get; set; }

        [Display(Name = "Категория")]
        [Required(ErrorMessage = "Пожалуйста, укажите категорию")]
        public Category Category { get; set; }

        [Display(Name = "Зарплата (руб)")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Пожалуйста, введите положительное значение")]
        public decimal Salary { get; set; }

        [Display(Name = "Статус")]
        public Status Status { get; set; }

        public string GetFullName()
        {
            string fullName = SecondName + " " + FirstName;
            return fullName;
        }
    }

    public enum Status
    {
        Wait, Work
    }

    public enum Category
    {
        Worker, Deliveryman
    }
}
