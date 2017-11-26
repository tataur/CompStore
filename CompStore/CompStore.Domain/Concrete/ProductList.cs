using System.Linq;
using System.Collections.Generic;
using CompStore.Domain.Entities;

namespace CompStore.Domain.Concrete
{
    public class ProductListLine
    {
        public Comp Comp { get; set; }
        public int Quantity { get; set; }
    }

    public class ProductList
    {
        private List<ProductListLine> lineCollection = new List<ProductListLine>();

        public void AddItem(Comp comp, int quantity)
        {
            ProductListLine line = lineCollection.Where(c => c.Comp.Id == comp.Id).FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new ProductListLine
                {
                    Comp = comp,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveLine(Comp comp)
        {
            lineCollection.RemoveAll(l => l.Comp.Id == comp.Id);
        }

        public decimal TotalValue()
        {
            return lineCollection.Sum(e => e.Comp.Price * e.Quantity);

        }
        public void Clear()
        {
            lineCollection.Clear();
        }

        public IEnumerable<ProductListLine> Lines
        {
            get { return lineCollection; }
        }

        public void AddToDB(OrderLine order, DeliveryDetails delivery)
        {

        }
    }
}