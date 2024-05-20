using CafeDuCoinAPI.Models;

namespace CafeDuCoinAPI.DTOs
{
    public class UserHistory
    {
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
