namespace YouScan.InterviewTask
{
    public interface IPointOfSaleTerminal
    {
        /// <summary>
        /// Finialize order and return total price
        /// </summary>
        /// <returns></returns>   
        /// <exception cref="Exceptions.OrderNotCompletedException">
        /// Thrown when previous order has not completed</exception>
        void NewOrder();

        /// <summary>
        /// Finialize order and return total price
        /// </summary>
        /// <returns></returns>   
        /// <exception cref="Exceptions.OrderNotFoundException">
        /// Thrown when order has already completed</exception>
        double CompleteOrder();

        /// <summary>
        /// Allows to add product to the order
        /// </summary>
        /// <param name="productName">Product public code (e.g. barcode 'Code 39', 'Code 128')</param>
        /// <exception cref="Exceptions.OrderNotFoundException">
        /// Thrown when order has already completed</exception>
        /// <exception cref="Exceptions.ProductNotFoundException">
        /// Thrown when you pass unknown productName</exception>
        void Scan(string productName);

        /// <summary>
        /// Allows to set retail price for product
        /// </summary>
        /// <param name="productName">Product public code (e.g. barcode 'Code 39', 'Code 128')</param>
        /// <param name="priceForOne">Retail price</param>
        /// <exception cref="System.ArgumentException">
        /// Thrown when try to pass parameter 'priceForOne' less than or equal to zero</exception> 
        void SetProductRetailPrice(string productName, double priceForOne);

        /// <summary>
        /// Allows to set wholesale price for product
        /// </summary>
        /// <param name="productName">Product public code (e.g. barcode 'Code 39', 'Code 128')</param>
        /// <param name="quantity">Quantity at wholesale price</param>
        /// <param name="priceForQuantity">Wholesale price</param>
        /// <exception cref="Exceptions.ProductNotFoundException">
        /// Thrown when you try to set wholesale price before retail price.</exception>  
        /// <exception cref="System.ArgumentException">
        /// Thrown when try to pass parameter 'priceForQuantity' or 'quantity' less than or equal to zero</exception> 
        void SetProductWholesalePrice(string productName, int quantity, double priceForQuantity);
    }
}