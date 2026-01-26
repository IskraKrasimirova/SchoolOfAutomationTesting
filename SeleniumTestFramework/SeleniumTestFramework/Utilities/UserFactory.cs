using Bogus;
using Bogus.Extensions;
using SeleniumTestFramework.Models;

namespace SeleniumTestFramework.Utilities
{
    public static class UserFactory
    {
        private static readonly Faker Faker = new();
        private static readonly string[] Titles = ["Mr.", "Mrs."];
        private static readonly List<string> ValidCities = ["Burgas", "Elin Pelin", "Kardjali", "Pleven", "Plovdiv", "Pravets", "Sofia", "Sopot", "Varna"];


        public static RegisterModel CreateValidUser()
        {
            return new RegisterModel
            (
                Faker.PickRandom(Titles),
                Faker.Name.FirstName().Replace("'", "").ClampLength(2, 15),
                Faker.Name.LastName().Replace("'", "").ClampLength(2, 15),
                Faker.Internet.Email(),
                Faker.Internet.Password(),
                "Bulgaria",
                Faker.PickRandom(ValidCities),
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
