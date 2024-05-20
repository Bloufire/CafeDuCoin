using CafeDuCoinAPI.Models;

namespace CafeDuCoinAPI.DTOs
{
    // Define the UserHistory class to represent a DTO (Data Transfer Object) for displaying user loan history
    public class UserHistory
    {
        // Constructor with a loan parameter to initialize properties
        public UserHistory(Loan loan)
        {
            this.Game = loan.Game.Name;
            this.GameDescription = loan.Game.Description;
            this.LoanDate = loan.LoanDate;
            this.LoanReturnDate = loan.LoanReturnDate;
        }

        public string Game { get; set; }
        public string GameDescription { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime? LoanReturnDate { get; set; }
    }
}
