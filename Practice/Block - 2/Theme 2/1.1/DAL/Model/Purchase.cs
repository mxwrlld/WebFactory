
namespace _1._1.DAL.Model
{
    class Purchase
    {
        public long Id { get; set; }
        public long? CarId { get; set; }
        public PersonalCard Card { get; set; }
        public decimal? Sum { get; set; }
    }
}
