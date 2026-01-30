using SeleniumTestFramework.DatabaseOperations.Queries;
using System.Data;

namespace SeleniumTestFramework.DatabaseOperations.Operations
{
    public class UserOperations:IDisposable
    {
        private readonly IDbConnection _connection;

        public UserOperations(IDbConnection connection)
        {
            this._connection = connection;
            this._connection.Open();
        }

        public void DeleteUserWithEmail(string email)
        {
            using var command = this._connection.CreateCommand();
            command.CommandText = UserQueries.DeleteUserByEmail;

            var emailParameter = command.CreateParameter();
            emailParameter.ParameterName = "@Email";
            emailParameter.Value = email;
            command.Parameters.Add(emailParameter);

            command.ExecuteNonQuery();
        }
        public void Dispose()
        {
            this._connection.Close();
            this._connection.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}
