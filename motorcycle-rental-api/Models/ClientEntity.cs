using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace motorcycle_rental_api.Models
{
    [Table("mr_client")]
    public class ClientEntity
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(11, ErrorMessage = "O número do CPF deve conter 11 dígitos.")]
        public string CPF { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Street { get; set; } = string.Empty;

        [Required]
        public int HouseNumber { get; set; }

        [StringLength(200)]
        public string Address2 { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string District { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string City { get; set; } = string.Empty;

        [Required]
        [StringLength(2, ErrorMessage = "Sigla UF deve conter 2 caracteres.")]
        public string State { get; set; } = string.Empty;

        [Required]
        [StringLength(8, ErrorMessage = "O CEP deve conter 8 dígitos.")]
        public string CEP { get; set; } = string.Empty;

        [Required]
        [StringLength(11, ErrorMessage = "O número de telefone deve conter até 11 dígitos.")]
        public string Fone { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Email { get; set; } = string.Empty;

        public ICollection<RentalEntity> Rentals { get; set; } = new List<RentalEntity>();
    }
}
