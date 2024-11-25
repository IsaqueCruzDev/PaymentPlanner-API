using System.ComponentModel.DataAnnotations;

namespace PaymentsPlanner.Models.DTOs
{
    public class PaymentDTO
    {
        [Required]
        public string Name { get; set; } = "";

        [Required]
        public string Description { get; set; } = "";

        [Required]
        public DateTime ExpirationDate { get; set; } = DateTime.Now;

        [Required]
        public double Value { get; set; }

        [Required]
        public Boolean IsActive { get; set; } = true;

        [Required]

        public int PaymentTypeId { get; set; }
    }
}
