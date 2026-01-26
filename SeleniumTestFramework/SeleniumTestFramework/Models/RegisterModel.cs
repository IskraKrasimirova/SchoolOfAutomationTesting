namespace SeleniumTestFramework.Models
{
    public class RegisterModel
    {
        public string Title { get; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; }
        public string Country { get; }
        public string City { get; set; }
        public bool AgreeToTerms { get; }

        public RegisterModel(string title, string firstName, string surname, string email, string password, string country, string city, bool agreeToTerms)
        {
            Title = title;
            FirstName = firstName;
            Surname = surname;
            Email = email;
            Password = password;
            Country = country;
            City = city;
            AgreeToTerms = agreeToTerms;
        }
    }
}
