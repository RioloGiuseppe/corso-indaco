using AutoMapper;
using sample.Data.Entities;
using sample.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sample.Data.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserInfo, UserModel>()
                .ForMember(model=> model.Name, 
                    opt=> opt.MapFrom(user => $"{user.FirstName} {user.LastName}"))
                .ReverseMap();
        }
    }
}
