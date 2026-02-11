using System.Collections.Generic;
namespace DataLayer.EfClasses
{
    public class Author //#E
    {
        public int AuthorId { get; set; }
        public string Name { get; set; }

        //------------------------------
        //Relationships

        public ICollection<BookAuthor> BooksLink { get; set; } //#F
    }
    
    /*********************************************************
    #E The Author class just contains the name of the author
    #F This points to, via the linking table, all the books the Author has participated in
    * *****************************************************/
}