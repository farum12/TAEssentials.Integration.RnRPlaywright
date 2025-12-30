using System.Runtime.Serialization;
using Bogus;
using TAEssentials.UI.DataClasses.RepresentativeData;
using TAEssentials.UI.Extensions;

namespace TAEssentials.UI.DataClasses
{
    public class DataGenerator
    {
        public List<User> FakeUsers(int count) => GenerateFakeUsers(count);
        public List<Book> FakeBooks(int count) => GenerateFakeBooks(count);
        public User FakeUser => GenerateFakeUsers(1).Single();
        public Book FakeBook => GenerateFakeBooks(1).Single();
        public BookReview FakeBookReview => GenerateFakeBookReviews(1).Single();

        private List<User> GenerateFakeUsers(int count)
        {
            return new Faker<User>()
                //.RuleFor(u => u.Username, f => $"{f.PickRandom<Something>().GetAttributeOfType<EnumMemberAttribute>().Value}")
                .RuleFor(u => u.Username, f => f.Internet.UserName())
                .RuleFor(u => u.Password, f => f.Internet.Password())
                .RuleFor(u => u.Email, f => f.Internet.Email())
                .RuleFor(u => u.FirstName, f => f.Name.FirstName())
                .RuleFor(u => u.LastName, f => f.Name.LastName())
                .RuleFor(u => u.PhoneNumber, f => f.Phone.PhoneNumber())
                .Generate(count);
        }

        private List<Book> GenerateFakeBooks(int count)
        {
            return new Faker<Book>()
                .RuleFor(b => b.Name, f => f.Lorem.Sentence(3, 5))
                .RuleFor(b => b.Author, f => f.Name.FullName())
                .RuleFor(b => b.Genre, f => f.Lorem.Word())
                .RuleFor(b => b.Isbn, f => f.Random.Replace("###-#-##-######-#"))
                .RuleFor(b => b.Price, f => double.Parse(f.Commerce.Price(5, 100, 2)))
                .RuleFor(b => b.Description, f => f.Lorem.Paragraphs(1, 3))
                .RuleFor(b => b.Type, f => $"{f.PickRandom<BookTypes>().GetAttributeOfType<EnumMemberAttribute>().Value}")
                .RuleFor(b => b.StockQuantity, f => f.Random.Int(0, 100))
                .RuleFor(b => b.LowStockThreshold, f => f.Random.Int(1, 10))
                .Generate(count);
        }

        private List<BookReview> GenerateFakeBookReviews(int count)
        {
            return new Faker<BookReview>()
                .RuleFor(br => br.ReviewText, f => f.Lorem.Paragraphs(1, 2))
                //.RuleFor(br => br.Author, f => f.Name.FullName())
                .RuleFor(br => br.Rating, f => f.Random.Int(1, 5))
                .Generate(count);
        }
    }
}