namespace CafeDuCoinAPI.Models
{
    public class Loan
    {
        public int ID { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime? LoanReturnDate { get; set; }

        public string userID { get; set; }
        public ApplicationUser User { get; set; }

        public int gameID { get; set; }
        public Game Game { get; set; }
    }
}
