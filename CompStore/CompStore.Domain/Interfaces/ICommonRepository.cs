using System.Collections.Generic;

namespace CompStore.Domain.Interfaces
{
    public interface ICommonRepository<T>
    {
        IEnumerable<T> AllItems { get; }
        void SaveChanges(T comp);
        T DeleteItem(int compId);
    }
}