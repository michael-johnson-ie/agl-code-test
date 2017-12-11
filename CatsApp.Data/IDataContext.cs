using System.Collections.Generic;

namespace CatsApp.Data
{
    public interface IDataContext
    {
        IEnumerable<T> Get<T>(); 
    }
}
