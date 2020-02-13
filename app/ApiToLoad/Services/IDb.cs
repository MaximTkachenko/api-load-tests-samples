using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiToLoad.Services
{
    public interface IDb
    {
        Task<IReadOnlyList<Num>> GetAll();
        Task<Num> Get(Guid id);
        Task Add(Num num);
        Task Update(Num num);
        Task Delete(Guid id);
    }
}
