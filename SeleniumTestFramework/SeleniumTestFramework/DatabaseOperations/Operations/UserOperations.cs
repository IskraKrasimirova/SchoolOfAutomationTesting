using SeleniumTestFramework.DatabaseOperations.Entities;
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

            AddParameter(command, "@Email", email);

            command.ExecuteNonQuery();
        }

        public int InsertUser(UserEntity user)
        {
            using var command = this._connection.CreateCommand();
            command.CommandText = UserQueries.InsertUser;

            AddParameter(command, "@FirstName", user.FirstName);
            AddParameter(command, "@Surname", user.Surname);
            AddParameter(command, "@Title", user.Title);
            AddParameter(command, "@Country", user.Country);
            AddParameter(command, "@City", user.City);
            AddParameter(command, "@Email", user.Email);
            AddParameter(command, "@Password", user.Password);
            AddParameter(command,"@IsAdmin", user.IsAdmin);

            var result = command.ExecuteScalar();

            return Convert.ToInt32(result);
        }

        public void Dispose()
        {
            this._connection.Close();
            this._connection.Dispose();

            GC.SuppressFinalize(this);
        }

        private void AddParameter(IDbCommand command, string parameterName, object value)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = parameterName;
            parameter.Value = value;
            command.Parameters.Add(parameter);
        }
    }
}
