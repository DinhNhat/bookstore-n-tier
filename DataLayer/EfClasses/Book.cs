namespace DataLayer.EfClasses;

public class Book
{
    public int BookId { get; set; } //#B
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime PublishedOn { get; set; }
    public string Publisher { get; set; }
    public decimal Price { get; set; }
    public string ImageUrl { get; set; }

    public bool SoftDeleted { get; set; }

    //-----------------------------------------------
    //relationships

    public PriceOffer Promotion { get; set; } //#C
    public ICollection<Review> Reviews { get; set; } //#D
    public ICollection<Tag> Tags { get; set; } //#E
    public ICollection<BookAuthor> AuthorsLink { get; set; } //#F
}