using System;
using System.Collections.Generic;

namespace ApiToLoad.Services
{
    public interface IDb
    {
        IReadOnlyList<Num> GetAll();
        Num Get(Guid id);
        void Add(Num num);
        void Update(Num num);
        void Delete(Guid id);
    }
}
