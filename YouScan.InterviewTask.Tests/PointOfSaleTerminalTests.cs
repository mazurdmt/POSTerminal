using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using YouScan.InterviewTask.DomainLayer.Repository;
using YouScan.InterviewTask.DomainLayer.Service;

namespace YouScan.InterviewTask.Tests
{
    [TestClass]
    public class PointOfSaleTerminalTests
    {
        [ExpectedException(typeof(System.ArgumentException))]
        [TestMethod]
        public void Pass_Incorrect_Product_Price_Thrown_Argument_Exception()
        {
            using (var scope = AutofacInjections.GetContainer().BeginLifetimeScope())
            {
                // Arrange - create a terminal
                var terminal = scope.Resolve<IPointOfSaleTerminal>();

                // Act 
                terminal.SetProductRetailPrice("Z", -0.5);
            }
        }

        [ExpectedException(typeof(System.ArgumentException))]
        [TestMethod]
        public void Pass_Incorrect_Product_Quantity_Thrown_Argument_Exception()
        {
            using (var scope = AutofacInjections.GetContainer().BeginLifetimeScope())
            {
                // Arrange - create a terminal
                var terminal = scope.Resolve<IPointOfSaleTerminal>();

                // Act 
                terminal.SetProductRetailPrice("Z", 1);
                terminal.SetProductWholesalePrice("Z", 0, 1);
            }
        }

        [ExpectedException(typeof(Exceptions.ProductNotFoundException))]
        [TestMethod]
        public void Set_Wholesale_Price_Before_Retail_Thrown_Exception_Product_Not_Found()
        {
            using (var scope = AutofacInjections.GetContainer().BeginLifetimeScope())
            {
                // Arrange - create a terminal
                var terminal = scope.Resolve<IPointOfSaleTerminal>();

                // Act 
                terminal.SetProductWholesalePrice("Z", 1, 1);
                terminal.SetProductRetailPrice("Z", 1);
            }
        }

        [ExpectedException(typeof(Exceptions.ProductNotFoundException))]
        [TestMethod]
        public void Scan_Unknown_Product_Thrown_Exception_Product_Not_Found()
        {
            using (var scope = AutofacInjections.GetContainer().BeginLifetimeScope())
            {
                // Arrange - create a terminal
                var terminal = scope.Resolve<IPointOfSaleTerminal>();

                // Arrange - set pricing
                SetPricing(terminal);

                // Act 
                terminal.Scan("Z");
            }
        }

        [ExpectedException(typeof(Exceptions.OrderNotFoundException))]
        [TestMethod]
        public void Scan_After_Order_Complete_Thrown_Exception_Order_Not_Found()
        {
            using (var scope = AutofacInjections.GetContainer().BeginLifetimeScope())
            {
                // Arrange - create a terminal
                var terminal = scope.Resolve<IPointOfSaleTerminal>();

                // Arrange - set pricing
                SetPricing(terminal);                
                terminal.Scan("A");
                // Arrange - complete order
                terminal.CompleteOrder();

                // Act 
                terminal.Scan("A");
            }
        }

        [ExpectedException(typeof(Exceptions.OrderNotFoundException))]
        [TestMethod]
        public void Complete_Already_Completed_Order_Thrown_Exception_Order_Not_Found()
        {
            using (var scope = AutofacInjections.GetContainer().BeginLifetimeScope())
            {
                // Arrange - create a terminal
                var terminal = scope.Resolve<IPointOfSaleTerminal>();

                // Arrange - set pricing
                SetPricing(terminal);
                terminal.Scan("A");
                // Arrange - complete order
                terminal.CompleteOrder();

                // Act 
                terminal.CompleteOrder();
            }
        }

        [ExpectedException(typeof(Exceptions.OrderNotCompletedException))]
        [TestMethod]
        public void New_Order_When_Prev_Not_Completed_Thrown_Exception_Order_Not_Completed()
        {
            using (var scope = AutofacInjections.GetContainer().BeginLifetimeScope())
            {
                // Arrange - create a terminal
                var terminal = scope.Resolve<IPointOfSaleTerminal>();

                // Arrange - set pricing
                SetPricing(terminal);
                terminal.Scan("A");                

                // Act 
                terminal.NewOrder();
            }
        }

        [TestMethod]
        public void Can_Create_New_Order()
        {
            using (var scope = AutofacInjections.GetContainer().BeginLifetimeScope())
            {
                // Arrange - create a terminal
                var terminal = scope.Resolve<IPointOfSaleTerminal>();

                // Arrange - set pricing
                SetPricing(terminal);

                // Act 
                terminal.Scan("A");
                terminal.Scan("A");
                terminal.Scan("A");

                var total_first_order = terminal.CompleteOrder();

                // Act 
                terminal.NewOrder();

                terminal.Scan("D");
                terminal.Scan("D");
                terminal.Scan("D");

                var total_second_order = terminal.CompleteOrder();

                // Assert
                Assert.AreEqual(total_first_order, 3);
                
                Assert.AreEqual(total_second_order, 2.25);
            }
        }

        [TestMethod]
        public void Can_Calculate_Total_Price_First()
        {
            using (var scope = AutofacInjections.GetContainer().BeginLifetimeScope())
            {
                // Arrange - create a terminal
                var terminal = scope.Resolve<IPointOfSaleTerminal>();

                // Arrange - set pricing
                SetPricing(terminal);

                // Arrange - scan products 
                terminal.Scan("A");
                terminal.Scan("B");
                terminal.Scan("C");
                terminal.Scan("D");
                terminal.Scan("A");
                terminal.Scan("B");
                terminal.Scan("A");

                // Act 
                var total = terminal.CompleteOrder();
                // Assert
                Assert.AreEqual(total, 13.25);
            }
        }



        [TestMethod]
        public void Can_Calculate_Total_Price_Second()
        {
            using (var scope = AutofacInjections.GetContainer().BeginLifetimeScope())
            {
                // Arrange - create a terminal
                var terminal = scope.Resolve<IPointOfSaleTerminal>();

                // Arrange - set pricing
                SetPricing(terminal);

                // Arrange - scan products 
                terminal.Scan("C");
                terminal.Scan("C");
                terminal.Scan("C");
                terminal.Scan("C");
                terminal.Scan("C");
                terminal.Scan("C");
                terminal.Scan("C");

                // Act 
                var total = terminal.CompleteOrder();
                // Assert
                Assert.AreEqual(total, 6);
            }
        }

        [TestMethod]
        public void Can_Calculate_Total_Price_Third()
        {
            using (var scope = AutofacInjections.GetContainer().BeginLifetimeScope())
            {
                // Arrange - create a terminal
                var terminal = scope.Resolve<IPointOfSaleTerminal>();

                // Arrange - set pricing
                SetPricing(terminal);

                // Arrange - scan products 
                terminal.Scan("A");
                terminal.Scan("B");
                terminal.Scan("C");
                terminal.Scan("D");

                // Act 
                var total = terminal.CompleteOrder();
                // Assert
                Assert.AreEqual(total, 7.25);
            }
        }

        private void SetPricing(IPointOfSaleTerminal posTerminal)
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
