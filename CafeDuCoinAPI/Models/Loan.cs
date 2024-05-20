namespace CafeDuCoinAPI.Models
{
    // Define the Loan class to represent a loan entity
    public class Loan
    {
        public int ID { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime? LoanReturnDate { get; set; }

        // userID property to store the ID of the user who took the loan
        public string userID { get; set; }
        // User navigation property to represent the user who took the loan
        public ApplicationUser User { get; set; }

        // gameID property to store the ID of the game that was loaned
        public int gameID { get; set; }
        // Game navigation property to represent the game that was loaned
        public Game Game { get; set; }
    }
}
