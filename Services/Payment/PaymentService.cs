using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core;

namespace Services.Payment
{
    public class PaymentService : IPaymentService
    {
        public string Name => "PaymentService";

        public Task StartAsync()
        {
            Console.WriteLine($"{Name} started.");
            return Task.CompletedTask;
        }

        public Task StopAsync()
        {
            Console.WriteLine($"{Name} stopped.");
            return Task.CompletedTask;
        }

        public Task<IEnumerable<Payment>> ListPaymentsAsync()
        {
            Console.WriteLine($"{Name}: Listing payments.");
            return Task.FromResult<IEnumerable<Payment>>(new List<Payment>());
        }

        public Task<Payment?> GetPaymentByIdAsync(string id)
        {
            Console.WriteLine($"{Name}: Getting payment by id: {id}");
            return Task.FromResult<Payment?>(null);
        }
    }
} 