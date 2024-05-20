using CafeDuCoinAPI.Models;

namespace CafeDuCoinAPI.DTOs
{
    // Define the GameHistory class to represent a DTO (Data Transfer Object) for displaying game loan history
    public class GameHistory
    {
        // Constructor with a loan parameter to initialize properties
        public GameHistory(Loan loan) {
            this.LoanDate = loan.LoanDate;
            this.LoanReturnDate = loan.LoanReturnDate;
            this.User = new UserCard(loan.User);
        }

        public UserCard User { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime? LoanReturnDate { get; set; }
    }
}
