
namespace Order
{
    public class Customer : EntityBase
    {
        public Person Person { get; set; }
        public bool Active { get; set; }
    }
}
