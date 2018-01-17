using System;
namespace Omal
{
    public interface IUtentiDataStore: IDataStore<Models.Utente>
    {
        Models.Utente Login(string email, string password);
    }
}
