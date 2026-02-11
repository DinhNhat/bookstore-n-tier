using System.Collections.Immutable;

namespace BusinessLogic.Orders;

public class PlaceOrderInDto
{
    public PlaceOrderInDto(bool acceptTAndCs, 
        Guid userId, 
        IImmutableList<OrderLineItem> lineItems)
    {
        AcceptTAndCs = acceptTAndCs;
        UserId = userId;
        LineItems = lineItems;
    }

    public bool AcceptTAndCs { get; private set; }

    public Guid UserId { get; private set; }

    public IImmutableList<OrderLineItem> LineItems { get; private set; }
}