using CafeDuCoinAPI.Models;

namespace CafeDuCoinAPI.DTOs
{
    public class GameCard
    {
        public GameCard() { }
        public GameCard(Game game, bool available)
        {
            this.Name = game.Name;
            this.Description = game.Description;
            this.Available = available;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public bool Available { get; set; }
    }
}
