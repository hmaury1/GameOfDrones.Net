﻿using System;
using System.Collections.Generic;
using System.Linq;
using GameOfDronesDataAccessLayer.DataAccess.Entities;
using GameOfDronesDataAccessLayer.DataAccess.Interfaces;

namespace GameOfDronesDataAccessLayer.DataAccess.Repositories
{
    public class DepartmentRepository : BaseRepository<Department, AppContext>, IDepartmentRepository
    {
        private readonly AppContext _context;

        public DepartmentRepository(AppContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<Department> GetPagedDepartmentByName(string name, int pageSize = 15, int pageNo = 1)
        {
            var query = DepartmentPagedData(pageSize, pageNo).Where(x => x.DepartmentName.ToUpper() == name.ToUpper());
            return query;

        }

        public IQueryable<Department> GetPagedDepartmentById(int id, int pageSize = 15, int pageNo = 1)
        {
            var query = DepartmentPagedData(pageSize, pageNo).Where(x => x.DepartmentId == id);
            return query;

        }

        public IQueryable<Department> DepartmentPagedData(int pageSize = 15, int pageNo = 1)
        {
            var query = _context.Departments.OrderBy(x => x.DepartmentId).Skip((pageNo - 1)*pageSize).Take(pageSize);
            return query;

        }
    }
}
