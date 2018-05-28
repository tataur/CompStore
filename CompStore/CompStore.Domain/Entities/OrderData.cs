namespace CompStore.Domain.Entities
{
    public class OrderData
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int Amount { get; set; }
        public int CustomerId { get; set; }
    }
}