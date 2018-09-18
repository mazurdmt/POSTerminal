using System;

namespace YouScan.InterviewTask.DomainLayer.Service
{
    public interface IOrderingService
    {
        /// <summary>
        /// Allows to create new order.
        /// </summary>
        /// <returns>OrderId</returns>
        Guid CreateNewOrder();

        /// <summary>
        /// Allows to add position to the order
        /// </summary>
        /// <param name="productName">Product public code (e.g. barcode 'Code 39', 'Code 128')</param>
        /// <param name="orderId">OrderId</param>
        /// <exception cref="Exceptions.OrderNotFoundException">
        /// Thrown when you pass unknown orderId</exception>
        /// <exception cref="Exceptions.ProductNotFoundException">
        /// Thrown when you pass unknown productName</exception>
        void AddPosition(string productName, Guid orderId);

        /// <summary>
        /// Allows to set retail price for product
        /// </summary>
        /// <param name="productName">Product public code (e.g. barcode 'Code 39', 'Code 128')</param>
        /// <param name="price">Retail price</param>
        /// <exception cref="ArgumentException">
        /// Thrown when try to pass parameter 'price' less than or equal to zero</exception> 
        void SetProductRetailPrice(string productName, double price);

        /// <summary>
        /// Allows to set wholesale price for product
        /// </summary>
        /// <param name="productName">Product public code (e.g. barcode 'Code 39', 'Code 128')</param>
        /// <param name="quantity">Quantity at wholesale price</param>
        /// <param name="priceForQuantity">Wholesale price</param>
        /// <exception cref="Exceptions.ProductNotFoundException">
        /// Thrown when you try to set wholesale price before retail price.</exception>  
        /// <exception cref="ArgumentException">
        /// Thrown when try to pass parameter 'priceForQuantity' or 'quantity' less than or equal to zero</exception> 
        void SetProductWholesalePrice(string productName, int quantity, double priceForQuantity);

        /// <summary>
        /// Allows to calculate total price
        /// </summary>
        /// <param name="orderId">OrderId</param>
        /// <returns>Total price</returns>        
        /// <exception cref="Exceptions.OrderNotFoundException">
        /// Thrown when you pass unknown orderId</exception>
        double CalculateTotal(Guid orderId);
    }
}