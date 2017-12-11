using System.Collections.Generic;
using CatsApp.Common;

namespace CatsApp.Model
{
    public class Owner
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public Gender Gender { get; set; }

        public IEnumerable<Pet> Pets;

    }
}
