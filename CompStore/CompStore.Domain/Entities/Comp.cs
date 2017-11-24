using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CompStore.Domain.Entities
{
    public class Comp
    {
        [HiddenInput(DisplayValue = false)]
        public Guid CompId { get; set; }

        [Display(Name = "Название")]
        [Required(ErrorMessage = "Пожалуйста, введите название")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Описание")]
        [Required(ErrorMessage = "Пожалуйста, введите описание")]
        public string Description { get; set; }

        [Display(Name = "Категория")]
        [Required(ErrorMessage = "Пожалуйста, укажите категорию")]
        public string Category { get; set; }

        [Display(Name = "Цена (руб)")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Пожалуйста, введите положительное значение для цены")]
        public decimal Price { get; set; }

        [Display(Name = "Количество")]
        [HiddenInput(DisplayValue = false)]
        public int Quantity { get; set; }
    }
}
