namespace empty2802core.Entities
{
    public class Basket
    {
        public Guid Id { get; set; }
        public Products Product { get; set; }
        public Users Users { get; set; }
        public int Count { get; set; }
    }
}
