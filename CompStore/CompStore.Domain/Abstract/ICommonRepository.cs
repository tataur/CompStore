using System.Collections.Generic;
using CompStore.Domain.Entities;

namespace CompStore.Domain.Abstract
{
    public interface ICommonRepository<T>
    {
        IEnumerable<T> Items { get; }
        void SaveChanges(T comp);
        T Delete(int compId);
    }
}
