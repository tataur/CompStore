using System.Collections.Generic;
using CompStore.Domain.Entities;

namespace CompStore.Domain.Abstract
{
    public interface ICommonRepository<T>
    {
        IEnumerable<T> AllItems { get; }
        void SaveChanges(T comp);
        T DeleteItem(int compId);
    }
}
