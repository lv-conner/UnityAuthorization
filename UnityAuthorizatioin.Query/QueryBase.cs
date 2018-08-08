using System;
using System.Collections.Generic;
using System.Text;
using UnityAuthorizatioin.Query.Interface;

namespace UnityAuthorizatioin.Query
{
    public abstract class QueryBase<T> : IQuery<T> where T:class
    {
        public T Get(string key)
        {
            throw new NotImplementedException();
        }
    }
}
