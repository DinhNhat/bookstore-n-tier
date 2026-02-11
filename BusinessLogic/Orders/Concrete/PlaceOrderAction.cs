using BizDbAccess;
using BusinessLogic.GenericInterfaces;
using DataLayer.EfClasses;

namespace BusinessLogic.Orders.Concrete;

public class PlaceOrderAction : BizActionErrors,  //#A
IBizAction<PlaceOrderInDto, Order> //#B
{
    private readonly IPlaceOrderDbAccess _dbAccess;

    public PlaceOrderAction(IPlaceOrderDbAccess dbAccess) => _dbAccess = dbAccess; //#C
        

    /// <summary>
    /// This validates the input and if OK creates 
    /// an order and calls the _dbAccess to add to orders
    /// </summary>
    /// <param name="dto"></param>
    /// <returns>returns an Order. Will be null if there are errors</returns>
    public Order Action(PlaceOrderInDto dto) //#D 
    {
        if (!dto.AcceptTAndCs)                    
        {                                         
            AddError("You must accept the T&Cs to place an order.");   //#E
            return null;
        }                                         
        if (!dto.LineItems.Any())                 //#E
        {                                         
            AddError("No items in your basket."); //#E
            return null;  
        }                                        

        var booksDict = _dbAccess.FindBooksByIdsWithPriceOffers(dto.LineItems.Select(x => x.BookId));//#F
        var order = new Order   //#G
        {
            CustomerId = dto.UserId,
            LineItems = FormLineItemsWithErrorChecking(dto.LineItems, booksDict)
        };

        if (!HasErrors)
            _dbAccess.Add(order); //#H

        return HasErrors ? null : order; //#I

    }

        private List<LineItem> FormLineItemsWithErrorChecking(
            IEnumerable<OrderLineItem> lineItems, 
            IDictionary<int,Book> booksDict)  //#J
        {
            var result = new List<LineItem>();
            var i = 1;
            
            foreach (var lineItem in lineItems)  //#K
            {
                if (!booksDict.ContainsKey(lineItem.BookId)) //#L
                    throw new InvalidOperationException($"An order failed because book, id = {lineItem.BookId} was missing."); //#L

                var book = booksDict[lineItem.BookId];
                var bookPrice = book.Promotion?.NewPrice ?? book.Price; //#M
                if (bookPrice <= 0)                         //#N
                    AddError($"Sorry, the book '{book.Title}' is not for sale.");    //#N
                else
                {
                    //Valid, so add to the order
                    result.Add(new LineItem 
                    {                               
                        BookPrice = bookPrice,      
                        ChosenBook = book,          
                        LineNum = (byte)(i++),      
                        NumBooks = lineItem.NumBooks
                    }); //#O
                }
            }
            return result; //#P
        }
}

/**************************************************************
    #A The BizActionErrors class provides error handling for the business logic
    #B The IBizAction interface makes the business logic conform to a standard interface
    #C The PlaceOrderAction uses PlaceOrderDbAccess class to handle database accesses
    #D This is the method that is called by the BizRunner to execute this business logic
    #E This is some basic validation
    #F The PlaceOrderDbAccess class finds all the bought books, with  optional PriceOffers
    #G This creates the Order, using FormLineItemsWithErrorChecking to create the LineItems
    #H I only add the order to the database if there are no errors 
    #I If there are errors I return null, otherwise I return the order
    #J This private method handles the creation of each LineItem for each book ordered
    #K This goes through each book type that the person has ordered
    #L I treat a book being missing as a system error, and throw an exception
    #M I calculate the price at the time of the order
    #N More validation where I check the book can be sold
    #O All is OK, so now I can create the LineItem entity class with the details
    #P I return all the LineItems for this order
     * ***********************************************************/