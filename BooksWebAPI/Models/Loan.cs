namespace BooksWebAPI.Models
{
    public class Loan
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public DateTime StartDate { get; set; }
    }
}
