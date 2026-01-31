namespace SeleniumTestFramework.Models
{
    public class AddUserModel
    {
        public string Title { get; }
        public string FirstName { get; }
        public string Surname { get; }
        public string Email { get; }
        public string Password { get; }
        public string Country { get; }
        public string City { get; }
        public bool IsAdmin { get; }

        public AddUserModel(string title, string firstName, string surname, string email, string password, string country, string city, bool isAdmin)
        {
            Title = title;
            FirstName = firstName;
            Surname = surname;
            Email = email;
            Password = password;
            Country = country;
            City = city;
            IsAdmin = isAdmin;
        }
    }
}
