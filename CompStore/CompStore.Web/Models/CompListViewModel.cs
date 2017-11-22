using System.Collections.Generic;
using CompStore.Domain.Entities;

namespace CompStore.Web.Models
{
    public class CompListViewModel
    {
        public IEnumerable<Comp> Computers { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
    }
}