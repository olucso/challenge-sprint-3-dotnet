using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace motorcycle_rental_api.Models
{
    [Table("mr_motorcycle")]
    public class MotorcycleEntity
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Brand { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Model { get; set; } = string.Empty;

        [Required]
        [StringLength(7, ErrorMessage = "A placa deve conter 7 caracteres.")]
        public string Plate { get; set; } = string.Empty;

        [Required]
        public int ManufacturingYear { get; set; }

        [Required]
        public decimal DailyValue { get; set; }

        [Required]
        public bool Availability { get; set; }

        public ICollection<RentalEntity> Rentals { get; set; } = new List<RentalEntity>();
    }
}
