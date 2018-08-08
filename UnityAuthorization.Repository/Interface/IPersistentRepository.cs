using System;
using System.Collections.Generic;
using System.Text;

namespace UnityAuthorization.Repository.Interface
{
    public interface IPersistentRepository<T>:IRepository<T> where T:class
    {
    }
}
