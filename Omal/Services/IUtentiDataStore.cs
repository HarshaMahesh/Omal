using System;
using System.Threading.Tasks;
using Omal.Models;

namespace Omal
{
    public interface IUtentiDataStore: IDataStore<Models.Utente>
    {
        Task<Models.Token> Login(string email, string password);
        Task<ResponseBase> RecoverPassword(string email);
        Task<ResponseBase> UpdateCurUtente(string userName, string emailBackOffice, string password, string passwordConfirm);
                   
    }
}
