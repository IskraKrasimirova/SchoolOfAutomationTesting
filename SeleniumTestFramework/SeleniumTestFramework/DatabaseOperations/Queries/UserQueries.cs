namespace SeleniumTestFramework.DatabaseOperations.Queries
{
    public static class UserQueries
    {
        public const string DeleteUserByEmail = @"
            DELETE FROM users 
            WHERE Email = @Email; 
        ";
    }
}
