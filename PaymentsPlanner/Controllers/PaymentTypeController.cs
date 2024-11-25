using Microsoft.AspNetCore.Mvc;
using PaymentsPlanner.Services;

namespace PaymentsPlanner.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class PaymentTypeController : ControllerBase
    {
        private readonly PaymentTypeService _paymentTypeService;

        public PaymentTypeController(PaymentTypeService paymentTypeService)
        {
            _paymentTypeService = paymentTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPaymentTypes()
        {
            try
            {
                var paymentTypes = await _paymentTypeService.GetPaymentTypes();
                return Ok(paymentTypes);
            } catch (Exception ex)
            {
                throw;
            }
        }
    }
}
