using System;

namespace Estacionamiento.Domain.Entities
{
    public interface IEntityBase<T>
    {
        T Id { get; set; }
    }
}
