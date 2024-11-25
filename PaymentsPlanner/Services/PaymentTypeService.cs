using Microsoft.EntityFrameworkCore;
using PaymentsPlanner.Models;

namespace PaymentsPlanner.Services
{
    public class PaymentTypeService
    {
        private readonly AppDbContext _appDbContext;

        public PaymentTypeService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<PaymentType>> GetPaymentTypes()
        {
            try
            {
                var paymentTypes = await _appDbContext.PaymentType.ToListAsync();

                if (paymentTypes.Count == 0)
                {
                    throw new Exception("Não há nenhum PaymentType!");
                }

                return paymentTypes;
            } catch (Exception ex)
            {
                throw;
            }
        }
    }
}
