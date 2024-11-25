using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentsPlanner.Models
{
    public class Payment
    {
        // Nome do fornecedor
        // Descrição 
        // Data de vencimento (LEMBRETE 5 DIAS ANTES DA DATA DE VENCIMENTO)
        // Valor - Pode ser alterado
        // Tipo (Rateio, PR, Transferencia bancaria, NF, BOLETO)
        // Ativo, Inativo

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = "";

        [Required]
        public string Description { get; set; } = "";

        [Required]
        public DateTime ExpirationDate { get; set; } =  DateTime.Now;

        [Required]
        public double Value { get; set; }

        [Required]
        public Boolean IsActive { get; set; } = true;

        [Required]
        public int PaymentTypeId { get; set; }

        public PaymentType PaymentType { get; set; }

    }
}
