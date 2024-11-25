using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PaymentsPlanner.Models.DTOs
{
    public class GetFullPaymentDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = "";

        public string Description { get; set; } = "";

        public DateTime ExpirationDate { get; set; } = DateTime.Now;

        public double Value { get; set; }

        public Boolean IsActive { get; set; } = true;

        public int PaymentTypeId { get; set; }

        public string PaymentTypeName { get; set; }

    }
}
