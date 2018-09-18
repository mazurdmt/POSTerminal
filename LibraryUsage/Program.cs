using Autofac;
using System;
using YouScan.InterviewTask;

namespace LibraryUsage
{
    class Program
    {
        static void Main(string[] args)
        {
            var firstOrder = new string[] { "A", "B", "C", "D" };
            var secondOrder = new string[] { "C", "C", "C", "C", "C", "C", "C" };

            using (var scope = AutofacInjections.GetContainer().BeginLifetimeScope())
            {
                var posTerminal = scope.Resolve<IPointOfSaleTerminal>();
                SetPricing(posTerminal);

                for (int i = 0; i < firstOrder.Length; i++)
                {
                    posTerminal.Scan(firstOrder[i]);
                    Console.Write(firstOrder[i]);
                }
                
                var firstTotal = posTerminal.CompleteOrder();
                Console.WriteLine();
                Console.WriteLine($"First order total price: {firstTotal}");

                posTerminal.NewOrder();
                for (int i = 0; i < secondOrder.Length; i++)
                {
                    posTerminal.Scan(secondOrder[i]);
                    Console.Write(secondOrder[i]);
                }

                var secondTotal = posTerminal.CompleteOrder();
                Console.WriteLine();
                Console.WriteLine($"First order total price: {secondTotal}");
            }

            Console.ReadKey();
        }

        private static void SetPricing(IPointOfSaleTerminal posTerminal)
        {
            posTerminal.SetProductRetailPrice("A", 1.25);
            posTerminal.SetProductWholesalePrice("A", 3, 3);
            posTerminal.SetProductRetailPrice("B", 4.25);
            posTerminal.SetProductRetailPrice("C", 1);
            posTerminal.SetProductWholesalePrice("C", 6, 5);
            posTerminal.SetProductRetailPrice("D", 0.75);
        }
    }
}
