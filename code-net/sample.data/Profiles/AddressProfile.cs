using AutoMapper;
using sample.Data.Entities;
using sample.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sample.Data.Profiles
{
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            CreateMap<Address, AddressModel>().ReverseMap();
        }
    }
}
