# Login System

## User Secrets Format

This project uses [User Secrets](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-10.0&tabs=windows) to securely store sensitive data such as login credentials locally.
If you clone this repository, you’ll need to configure your own secrets before running the login system.

### Example secrets.json content:
```json
{
	"Username": "your-username",
	"Password": "your-password"
}
```