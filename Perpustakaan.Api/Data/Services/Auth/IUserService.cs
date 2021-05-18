namespace Perpustakaan.Api.Data.Services.Auth
{
    public interface IUserService
    {
        bool ValidateCredentials(string username, string password);
    }
}
