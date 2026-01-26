namespace SeleniumTestFramework.Models
{
    public class RegisterModel
    {
        public string Title { get; private set; }
        public string FirstName { get; private set; }
        public string Surname { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string Country { get; private set; }
        public string City { get; private set; }
        public bool AgreeToTerms { get; private set; }

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

        internal void Set(
            string? title = null, 
            string? firstName = null, 
            string? surname = null, 
            string? email = null, 
            string? password = null, 
            string? country = null, 
            string? city = null, 
            bool? agreeToTerms = null) 
        { 
            if (title != null)  Title = title; 
            if (firstName != null)  FirstName = firstName; 
            if (surname != null) Surname = surname; 
            if (email != null) Email = email; 
            if (password != null) Password = password;
            if (country != null) Country = country; 
            if (city != null) City = city; 
            if (agreeToTerms.HasValue) AgreeToTerms = agreeToTerms.Value;
        }
    }
}
