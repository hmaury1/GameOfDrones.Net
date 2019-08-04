﻿using System.Collections.Generic;
using System.Linq;
using GameOfDronesDataAccessLayer.DataAccess.Entities;

namespace GameOfDronesDataAccessLayer.DataAccess.Interfaces
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        IQueryable<Employee> GetPagedEmployeeByName(string name, int pageSize = 10, int pageNo = 1);
        IQueryable<Employee> GetPagedEmployeeById(int id, int pageSize = 15, int pageNo = 1);
        IQueryable<Employee> EmployeePagedData(int pageSize = 15, int pageNo = 1);
    }
}
