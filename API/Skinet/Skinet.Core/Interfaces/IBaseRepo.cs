using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skinet.Core.Entities;
using Skinet.Core.Specifications;

namespace Skinet.Core.Interfaces
{
    public interface IBaseRepo<T> where T:BaseEntity
    {
        Task<T> GetEntityByIdAsync(int id);
        Task<IReadOnlyList<T>> GetEntitiesAsync();
        Task<T> GetEntityWithSpec(ISpecification<T> spec);
        Task<IReadOnlyList<T>> GetEntitiesAsync(ISpecification<T> spec);
    }
}
