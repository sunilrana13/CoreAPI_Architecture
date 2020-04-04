using AutoMapper;
using Sample.DataContract;
using Sample.Dtos;
using Sample.RepositoryContract;
using Sample.ServiceContract;
using System;
using System.Collections.Generic;

namespace Sample.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public EmployeeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public List<EmployeeDTO> GetEmployee()
        {
            return _mapper.Map<List<EmployeeDTO>>(_unitOfWork.Employees.GetEmployee());
            
        }
    }
}
