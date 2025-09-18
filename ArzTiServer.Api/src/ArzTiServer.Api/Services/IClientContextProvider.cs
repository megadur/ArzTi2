namespace ArzTiServer.Api.Services
{
    public interface IClientContextProvider
    {
        string GetConnectionString(string clientId);
    }
}