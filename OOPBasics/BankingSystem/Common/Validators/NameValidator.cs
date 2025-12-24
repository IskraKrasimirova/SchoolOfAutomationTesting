namespace BankingSystem.Common.Validators
{
    public static class NameValidator
    {
        public static void Validate(string? name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name cannot be empty.");
            }

            name = name.Trim();

            if (name.Length < 2 || name.Length > 50)
            {
                throw new ArgumentException("Name must be between 2 and 50 characters.");
            }

            if (!name.All(ch => char.IsLetter(ch) || char.IsWhiteSpace(ch)))
            {
                throw new ArgumentException("Name can contain only letters and spaces.");
            }
        }
    }
}
