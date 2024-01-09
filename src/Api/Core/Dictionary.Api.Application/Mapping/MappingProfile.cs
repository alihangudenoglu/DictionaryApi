﻿using AutoMapper;
using Dictionary.Api.Domain.Models;
using Dictionary.Common.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary.Api.Application.Mapping;

public class MappingProfile:Profile
{
    public MappingProfile()
    {
        CreateMap<User,LoginUserViewModel>().ReverseMap();
    }
}
