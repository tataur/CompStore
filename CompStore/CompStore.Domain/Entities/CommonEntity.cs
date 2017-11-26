using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CompStore.Domain.Entities
{
    public class CommonEntity
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public Guid Id { get; set; }

        public DateTime CreationDate { get; set; }

        public void FillCommonFields()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.Now;
        }
    }
}
