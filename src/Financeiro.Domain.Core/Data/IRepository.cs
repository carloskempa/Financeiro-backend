using Financeiro.Domain.Core.DomainObjects;
using System;

namespace Financeiro.Domain.Core.Data
{
    public interface IRepository<T> : IDisposable where T : Entity
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
