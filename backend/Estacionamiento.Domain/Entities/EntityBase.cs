using System;

namespace Estacionamiento.Domain.Entities
{
    public class EntityBase<T>: DomainEntity, IEntityBase<T>
    {
        public virtual T Id { get; set; } = default!;
    }

    public class DomainEntity {
        public DateTime CreatedOn { get; init; }
        public DateTime UpdatedOn { get; set; }
    }
}
