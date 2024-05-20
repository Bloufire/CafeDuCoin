using CafeDuCoinAPI.Models;

namespace CafeDuCoinAPI.DTOs
{
    // Define the GameCard class to represent a DTO (Data Transfer Object) for displaying game information
    public class GameCard
    {
        public GameCard() { }
        // Constructor with parameters to initialize properties
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
