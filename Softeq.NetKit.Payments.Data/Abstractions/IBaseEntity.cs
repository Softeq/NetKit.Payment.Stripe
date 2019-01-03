// Developed by Softeq Development Corporation
// http://www.softeq.com

namespace Softeq.NetKit.Payments.Data.Abstractions
{
    public interface IBaseEntity<T> : IEntity
    {
        T Id { get; set; }
    }
}