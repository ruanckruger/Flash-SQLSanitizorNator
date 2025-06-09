using Microsoft.AspNetCore.Mvc;
using SQLSanitizorNator.Data.Models;
using SQLSanitizorNator.Logic.CRUD;
using System.ComponentModel;

namespace SQLSanitizorNator.Logic.Services
{
    public interface INaughtyWordService<T> : IBaseCRUD<NaughtyWord, Guid> where T : class
    {
        Task<string> Sanitize(string toSanitize, int severity = 10, CancellationToken token = default);
        Task<List<NaughtyWord>> GetUnderSeverity(int severity = 10, CancellationToken token = default);
    }

    //public interface INaughtyWordServiceAPI<T> : INaughtyWordService where T : Component { }
    //public interface INaughtyWordServiceCRUD<T> : INaughtyWordService, IBaseCRUD<NaughtyWord,Guid> where T : ControllerBase { }


}
