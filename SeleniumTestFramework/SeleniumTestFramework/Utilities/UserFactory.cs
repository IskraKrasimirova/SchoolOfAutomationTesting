using Bogus;
using Bogus.Extensions;
using SeleniumTestFramework.Models;

namespace SeleniumTestFramework.Utilities
{
    public static class UserFactory
    {
        private static readonly Faker Faker = new();
        private static readonly string[] Titles = ["Mr.", "Mrs."];
        private static readonly Dictionary<string, List<string>> CountryCities = new()
        {
            ["Bulgaria"] = ["Sofia", "Varna", "Plovdiv", "Burgas"],
            ["Germany"] = ["Berlin", "Hamburg", "Munich", "Frankfurt"],
            ["USA"] = ["New York", "Chicago", "Los Angeles", "Houston"],
            ["UK"] = ["London", "Manchester", "Liverpool", "Birmingham"]
        };


        public static RegisterModel CreateValidUser()
        {
            var country = Faker.PickRandom(CountryCities.Keys.ToList());
            var city = Faker.PickRandom(CountryCities[country]);

            return new RegisterModel
            (
                Faker.PickRandom(Titles),
                Faker.Name.FirstName().ClampLength(2, 15),
                Faker.Name.LastName().ClampLength(2, 15),
                Faker.Internet.Email(),
                Faker.Internet.Password(),
                country,
                city,
                true
            );
        }

        public static RegisterModel CreateUserWith(Action<RegisterModel> overrides)
        {
            var user = CreateValidUser();

            overrides(user);
            return user;
        }
    }
}
