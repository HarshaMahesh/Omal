using System;
using System.Threading.Tasks;

namespace Omal
{
    public interface IUtentiDataStore: IDataStore<Models.Utente>
    {
        Task<Models.Token> Login(string email, string password);
    }
}
