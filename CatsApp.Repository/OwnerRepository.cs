using System;
using System.Collections.Generic;
using CatsApp.Data;
using CatsApp.Model;

namespace CatsApp.Repository
{
    public class OwnerRepository : IOwnerRepository
    {
        private IDataContext _dataContext;

        public OwnerRepository(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IEnumerable<Owner> GetOwners()
        {
            return _dataContext.Get<Owner>();
        }
    }
}
