using MauiSqlite.Mobile.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiSqlite.Mobile.Repositories
{
    public interface IRepository
    {
        Task<HttpResponseWrapper<T>> GetPersoN<T>(string urlBase, string url, LoginDTO modeld);
        Task<HttpResponseWrapper<T>> Get<T>(string urlBase, string url);
    }
}
