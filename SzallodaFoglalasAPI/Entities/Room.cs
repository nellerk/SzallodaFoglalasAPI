namespace SzallodaFoglalasAPI.Entities
{
    public class Room
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; }
        public string Type { get; set; } // Például: Egyágyas, Kétágyas, Lakosztály
        public decimal PricePerNight { get; set; }
        public bool IsAvailable { get; set; }
    }

}
