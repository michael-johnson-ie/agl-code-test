using System;
using System.Collections.Generic;
using System.Linq;
using CatsApp.Model;

namespace CatsApp.Repository
{
    public interface IOwnerRepository
    {
        IEnumerable<Owner> GetOwners();
    }
}
