using System.Collections.Generic;
using CatsApp.Model;

namespace CatsApp.Repository
{
    public interface IOwnerRepository
    {
        IEnumerable<Owner> GetOwners();
    }
}
