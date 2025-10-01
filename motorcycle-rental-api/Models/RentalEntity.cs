using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace motorcycle_rental_api.Models
{
    [Table("mr_rental")]
    public class RentalEntity
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int ClientId { get; set; }

        [ForeignKey("ClientId")]
        public ClientEntity Client { get; set; } = null!;

        [Required]
        public int MotorcycleId { get; set; }

        [ForeignKey("MotorcycleId")]
        public MotorcycleEntity Motorcycle { get; set; } = null!;

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Required]
        public decimal TotalValue { get; set; }

        [Required]
        public bool Completed { get; set; }
    }
}
