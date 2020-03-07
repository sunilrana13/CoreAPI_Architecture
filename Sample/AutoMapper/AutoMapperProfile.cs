using AutoMapper;
using Sample.DataContract;
using Sample.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.Web.API.AutoMapper
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Employee, EmployeeDTO>();
        }
       
    }
}
