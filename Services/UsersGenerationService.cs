using Bogus;
using FakeUserDataGeneration.Models;

namespace FakeUserDataGeneration.Services
{
    public class UsersGenerationService
    {
        private Faker<FakeUser> _faker = new();

        public List<FakeUser> GenerateTenUsers()
        {
            var users = new List<FakeUser>();
            foreach (var user in _faker.Generate(10))
                users.Add(user);
            return users;
        }

        public void UpdateFakerLocale(string locale, int seed)
        {
            Randomizer.Seed = new Random(seed);
            _faker = new Faker<FakeUser>(locale)
                .RuleFor(x => x.Id, Guid.NewGuid)
                .RuleFor(x => x.FullName, x => x.Person.FullName)
                .RuleFor(x => x.Address, x => {
                    string[] address = x.Address.FullAddress().Split(',');
                    address[^1] = x.Address.State();
                    return string.Join(",", address);
                    }
                )
                .RuleFor(x => x.Phone, x => x.Phone.PhoneNumberFormat());
        }
    }
}
