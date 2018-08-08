using System;

namespace UnityAuthorizatioin.Query.Interface
{
    public interface IQuery<T> where T:class
    {
        T Get(string key);
    }
}
