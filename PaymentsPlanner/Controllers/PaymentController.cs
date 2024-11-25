using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaymentsPlanner.Models;
using PaymentsPlanner.Models.DTOs;
using PaymentsPlanner.Services;

namespace PaymentsPlanner.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : ControllerBase
    {

        private readonly PaymentService _paymentService;

        public PaymentController(PaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPayments()
        {
            try
            {
                var payments = await _paymentService.GetPayments();
                return Ok(payments);
            }
            catch (Exception ex) {
                throw;
            }
        }

        [HttpGet("changestate/{id}/{active}")]
        public async Task<IActionResult> ChangeStateActive(int id, bool active)
        {
            var payment = await _paymentService.ChangeStateActive(id, active);

            if (payment == null) {
                throw new Exception("Pagamento não encontrado.");
            }

            return Ok(payment);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePayment(PaymentDTO payment)
        {
            Payment newPayment = new Payment
            {
                Name = payment.Name,
                Description = payment.Description,
                IsActive = payment.IsActive,
                PaymentTypeId = payment.PaymentTypeId,
                Value = payment.Value,
                ExpirationDate = payment.ExpirationDate,
            };

            try
            {
                var createdPayment = await this._paymentService.CreatePayment(newPayment);
                return Ok(createdPayment);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePayment(int id, PaymentDTO payment)
        {
            Payment newPayment = new Payment
            {
                Name = payment.Name,
                Description = payment.Description,
                IsActive = payment.IsActive,
                PaymentTypeId = payment.PaymentTypeId,
                Value = payment.Value,
                ExpirationDate = payment.ExpirationDate,
            };

            try
            {
                var updatedPayment = await this._paymentService.UpdatePayment(id, newPayment);
                if (updatedPayment == null)
                {
                    return NotFound("Pagamento não encontrado");
                }
                return Ok(updatedPayment);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            try
            {
                var updatedPayment = await this._paymentService.DeletePayment(id);
                if (updatedPayment == null)
                {
                    return NotFound("Pagamento não encontrado");
                }
                return Ok(updatedPayment);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
