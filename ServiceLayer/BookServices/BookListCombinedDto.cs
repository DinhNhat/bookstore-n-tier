using System.Collections.Generic;
using ServiceLayer.Logger;

namespace ServiceLayer.BookServices
{
    public class BookListCombinedDto
    {
        public BookListCombinedDto(SortFilterPageOptions sortFilterPageData, 
            IEnumerable<BookListDto> booksList)
        {
            SortFilterPageData = sortFilterPageData;
            BooksList = booksList;
        }

        public SortFilterPageOptions SortFilterPageData { get; private set; }

        public IEnumerable<BookListDto> BooksList { get; private set; }
    }
}