namespace GameOfChance.Api.Api.Models
{
    public class BetResponse
    {
        public int Account { get; set; }
        public int Points { get; set; }
        public string Status { get; set; }
    }

    public static class BetResponseExtensions
    {
        public static BetResponse ToBetResponse(this Client.Bet bet)
        {
            return new BetResponse
            {
                Account = bet?.Player?.AccountBalance ?? 0,
                Points = bet.BetPoints,
                Status = bet.Status ? "Won" : "Lost"
            };
        }
    }
}
