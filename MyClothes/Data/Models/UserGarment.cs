namespace MyClothes.Data.Models
{
    public class UserGarment
    {
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public string GarmentId { get; set; }
        public Garment Garment { get; set; }
      
    }
}
