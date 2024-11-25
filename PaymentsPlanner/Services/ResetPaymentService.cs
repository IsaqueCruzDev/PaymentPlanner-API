
namespace PaymentsPlanner.Services
{
    public class ResetPaymentService : BackgroundService
    {
        private readonly PaymentService _paymentService;

        public ResetPaymentService(PaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (DateTime.Now.Day == 1)
                {
                    try
                    {
                        await _paymentService.ResetPayments();
                        Console.WriteLine("Pagamentos Resetados!");
                    } catch (Exception ex) 
                    {
                        throw;
                    }
                }

                await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
            }
        }
    }
}
