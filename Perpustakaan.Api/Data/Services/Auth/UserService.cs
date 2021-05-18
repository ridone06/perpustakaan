namespace Perpustakaan.Api.Data.Services.Auth
{
    public class UserService : IUserService
    {
        public bool ValidateCredentials(string username, string password)
        {
            return username.Equals("admin") && password.Equals("P@ssw0rd");
        }
    }
}
