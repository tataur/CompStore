namespace CompStore.Web.Infrastructure
{
    public interface IAuthenticate
    {
        bool Authenticate(string login, string password);
    }
}