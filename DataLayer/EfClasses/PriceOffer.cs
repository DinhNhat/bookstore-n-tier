namespace DataLayer.EfClasses
{
    public class PriceOffer //#A
    {
        public int PriceOfferId { get; set; }
        public decimal NewPrice { get; set; }
        public string PromotionalText { get; set; }

        //-----------------------------------------------
        //Relationships

        public int BookId { get; set; } //#b
    }

    /***************************************************
    #N The PriceOffer is designed to override the normal price. It is a One-to-ZeroOrOne relationhsip
    #O This foreign key links back to the book it should be applied to
     * *************************************************/
}