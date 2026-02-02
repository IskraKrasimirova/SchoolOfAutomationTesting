namespace SeleniumTestFramework.Models.Factories
{
    public interface IUserFactory
    {
        UserModel CreateDefault();
        UserModel Create(string email, string password);
    }
}
