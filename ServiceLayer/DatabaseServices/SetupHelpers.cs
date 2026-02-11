using DataLayer.EfCode;
using ServiceLayer.DatabaseServices.Concrete;

namespace ServiceLayer.DatabaseServices;

public static class SetupHelpers
{
    private const string SeedDataSearchName = "Apress books*.json";
    public const string SeedFileSubDirectory = "seedData";

    public static async Task<int> SeedDatabaseIfNoBooksAsync(this EfCoreContext context, string dataDirectory)
    {
        var numBooks = context.Books.Count();
        if (numBooks == 0)
        {
            //the database is empty so we fill it from a json file
            string seedFilePath = Path.Combine(dataDirectory, SeedFileSubDirectory);
            var books = BookJsonLoader
                .LoadBooks(seedFilePath, SeedDataSearchName)
                .ToList();
            // Console.WriteLine($"Attempting to load JSON from: {Path.GetFullPath(seedFilePath)}");
            context.Books.AddRange(books);
            await context.SaveChangesAsync();

            //We add this separately so that it has the highest Id. That will make it appear at the top of the default list
            context.Books.Add(SpecialBook.CreateSpecialBook());
            await context.SaveChangesAsync();
            numBooks = books.Count + 1;
        }

        return numBooks;
    }
}