using ServiceLayer.DatabaseServices.Concrete;
using TestSupport.Helpers;
using Xunit.Extensions.AssertExtensions;

namespace DataLayer.Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        //SETUP
        const string searchFile = "Apress.books*.json";
        var testDataDir = TestData.GetTestDataDir();

        //ATTEMPT
        var books = BookJsonLoader.LoadBooks(testDataDir, searchFile);

        //VERIFY
        books.Count().ShouldEqual(53);
    }
    
    [Fact]
    public void TestBookLoadBuildReviewsOk()
    {
        //SETUP
        const string searchFile = "Apress.books*.json";
        var testDataDir = TestData.GetTestDataDir();

        //ATTEMPT
        var books = BookJsonLoader.LoadBooks(testDataDir, searchFile);

        //VERIFY
        var expectedAveVotes = new[] {5.0, 3.0, 4.0, 4.5};
        books.Select(x => x.Reviews.Average(y => y.NumStars)).ShouldEqual(expectedAveVotes);
    }
    
    [Fact]
    public void TestBookLoadTagsOk()
    {
        //SETUP
        const string searchFile = "JsonBooks01*.json";
        var testDataDir = TestData.GetTestDataDir();

        //ATTEMPT
        var books = BookJsonLoader.LoadBooks(testDataDir, searchFile).ToList();

        //VERIFY
        books[0].Tags.Select(x => x.TagId).ShouldEqual(new[] { "Java", "Web" });
        books[1].Tags.Select(x => x.TagId).ShouldEqual(new []{ "Web" });
        books[2].Tags.Select(x => x.TagId).ShouldEqual(new[] { "Android" });
        books[3].Tags.Select(x => x.TagId).ShouldEqual(new[] { "Microsoft .NET", "Web" });
        books.SelectMany(x => x.Tags).Distinct().Count().ShouldEqual(4);
    }
}