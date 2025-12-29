using Bogus;

namespace TAEssentials.UI.DataClasses
{
    public class DataGenerator
    {
        public List<User> FakeUsers(int count) => GenerateFakeUsers(count);
        public User FakeUser => GenerateFakeUsers(1).Single();


        private List<User> GenerateFakeUsers(int count)
        {
            return new Faker<User>()
                .RuleFor(u => u.Username, f => f.Internet.UserName())
                .RuleFor(u => u.Password, f => f.Internet.Password())
                .RuleFor(u => u.Email, f => f.Internet.Email())
                .RuleFor(u => u.FirstName, f => f.Name.FirstName())
                .RuleFor(u => u.LastName, f => f.Name.LastName())
                .RuleFor(u => u.PhoneNumber, f => f.Phone.PhoneNumber())
                .Generate(count);
        }
    }
}