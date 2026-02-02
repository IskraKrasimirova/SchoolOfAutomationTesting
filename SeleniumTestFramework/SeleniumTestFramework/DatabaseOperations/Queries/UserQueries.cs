namespace SeleniumTestFramework.DatabaseOperations.Queries
{
    public static class UserQueries
    {
        public const string DeleteUserByEmail = @"
            DELETE FROM users 
            WHERE Email = @Email; 
        ";

        public const string InsertUser = @" 
            INSERT INTO users (first_name, sir_name, title, country, city, email, password) 
            VALUES (@FirstName, @Surname, @Title, @Country, @City, @Email, @Password); 
            SELECT LAST_INSERT_ID(); 
        ";

        public static string GetUserByEmail(string email)
        {
            return $@"
                SELECT 1 FROM users
                WHERE email = '{email}';
            ";
        }
    }
}
