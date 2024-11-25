using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using PaymentsPlanner.Models;
using PaymentsPlanner.Models.DTOs;

namespace PaymentsPlanner.Services
{
    public class PaymentService
    {
        private readonly AppDbContext _appDbContext;

        public PaymentService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<Payment>> ResetPayments()
        {
            List<Payment> payments = await this._appDbContext.Payment.Where(p => p.IsActive == false).ToListAsync();

            foreach (var payment in payments) 
            {
                    payment.IsActive = true;

                    _appDbContext.Update(payment);
            }

            await _appDbContext.SaveChangesAsync();

            return payments;
        }

        public async Task<List<GetFullPaymentDTO>> GetPayments()
        {
            try
            {
                List<PaymentType> paymentType = await this._appDbContext.PaymentType.ToListAsync();
                List<Payment> payments = await this._appDbContext.Payment.ToListAsync();

                if (payments.Count == 0)
                {
                    throw new Exception("Nenhum pagamento foi encontrado.");
                }

                    var fullyPayments = payments.Select(payment => new GetFullPaymentDTO
                    {
                        Id = payment.Id,
                        Name = payment.Name,
                        Description = payment.Description,
                        Value = payment.Value,
                        ExpirationDate = payment.ExpirationDate,
                        IsActive = payment.IsActive,
                        PaymentTypeId = payment.PaymentTypeId,
                        PaymentTypeName = paymentType.FirstOrDefault(pt => payment.PaymentTypeId == pt.Id)?.Type
                    }).ToList();

                return fullyPayments;
            }
            catch (Exception ex) {
                throw;
            }
        }

        public async Task<Payment> ChangeStateActive(int id, bool active)
        {
            var payment = _appDbContext.Payment.FirstOrDefault(p => p.Id == id);

            payment.IsActive = active;

            _appDbContext.Update(payment);

            await _appDbContext.SaveChangesAsync();

            return payment;
        }

        public async Task<Payment> CreatePayment(Payment payment)
        {
            try
            {
                var createdPayment = await this._appDbContext.AddAsync(payment);
                _appDbContext.SaveChanges();
                return createdPayment.Entity;
            }
            catch (Exception ex) {
                throw;
            }  
        }

        public async Task<Payment> UpdatePayment(int id, Payment payment)
        {
            var foundPayment = await this._appDbContext.Payment.FindAsync(id);

            if (foundPayment == null)
            {
                return null;
            }

            foundPayment.Name = payment.Name;
            foundPayment.Description = payment.Description;
            foundPayment.IsActive = true;
            foundPayment.Value = payment.Value;
            foundPayment.ExpirationDate = payment.ExpirationDate;
            foundPayment.PaymentType = payment.PaymentType;

            _appDbContext.Update(foundPayment);

            try
            {
                await this._appDbContext.SaveChangesAsync();
                return payment;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Payment> DeletePayment(int id)
        {
            var payment = this._appDbContext.Payment.Find(id);
            if (payment == null)
            {
                return null;
            }
            try
            {
                this._appDbContext.Payment.Remove(payment);
                await this._appDbContext.SaveChangesAsync();
                return payment;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
