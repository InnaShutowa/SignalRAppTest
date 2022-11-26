using SignalRApp.Entities;
using System;

namespace SignalRApp.Interfaces
{
    public interface IBaseRepository
    {
        void UpdateItem(BaseEntity item);
        Guid? AddItem(BaseEntity usr);
    }
}
