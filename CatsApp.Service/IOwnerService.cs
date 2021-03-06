﻿using System.Collections.Generic;
using CatsApp.Common;
using CatsApp.Dto;
using CatsApp.Model;

namespace CatsApp.Service
{
    public interface IOwnerService
    {
        IEnumerable<GenderDto> GetGendersForPetType(PetType petType);
    }
}
