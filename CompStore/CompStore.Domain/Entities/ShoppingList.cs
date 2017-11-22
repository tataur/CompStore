using System.Collections.Generic;
using System.Linq;

namespace CompStore.Domain.Entities
{
    public class ShoppingListLine
    {
        public Comp Comp { get; set; }
        public int Quantity { get; set; }
    }

    public class ShoppingList
    {
        private List<ShoppingListLine> lineCollection = new List<ShoppingListLine>();

        public void AddItem(Comp comp, int quantity)
        {
            ShoppingListLine line = lineCollection.Where(c => c.Comp.CompId == comp.CompId).FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new ShoppingListLine
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
            lineCollection.RemoveAll(l => l.Comp.CompId == comp.CompId);
        }

        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(e => e.Comp.Price * e.Quantity);

        }
        public void Clear()
        {
            lineCollection.Clear();
        }

        public IEnumerable<ShoppingListLine> Lines
        {
            get { return lineCollection; }
        }
    }
}