using Autofac;
using YouScan.InterviewTask;
using YouScan.InterviewTask.DomainLayer.Repository;
using YouScan.InterviewTask.DomainLayer.Service;

namespace LibraryUsage
{
    public class AutofacInjections
    {
        public static IContainer GetContainer()
        {
            var builder = new ContainerBuilder();

            builder
                .RegisterType<ProductRepository>()
                .As<IProductRepository>();

            builder
               .RegisterType<OrderRepository>()
               .As<IOrderRepository>();

            builder
               .RegisterType<OrderingService>()
               .As<IOrderingService>();

            builder
               .RegisterType<PointOfSaleTerminal>()
               .As<IPointOfSaleTerminal>();

            return builder.Build();
        }
    }
}
