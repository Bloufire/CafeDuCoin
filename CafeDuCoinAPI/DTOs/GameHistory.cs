using CafeDuCoinAPI.Models;

namespace CafeDuCoinAPI.DTOs
{
    public class GameHistory
    {
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
