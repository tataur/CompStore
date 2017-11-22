using System.Collections.Generic;
using CompStore.Domain.Entities;

namespace CompStore.Domain.Abstract
{
    public interface ICompRepository
    {
        IEnumerable<Comp> Computers { get; }
        void SaveChanges(Comp comp);
        Comp DeleteComp(int compId);
    }
}
