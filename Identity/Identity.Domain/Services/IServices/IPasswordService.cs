namespace Module.Identity.Domain.Services.IServices
{
    public interface IPasswordService
    {
        string Hash(string password);

        bool Verify(string password, string hash);
    }
}
