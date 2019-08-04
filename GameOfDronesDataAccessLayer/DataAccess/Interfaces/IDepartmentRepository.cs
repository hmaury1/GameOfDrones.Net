using System.Collections.Generic;
using System.Linq;
using GameOfDronesDataAccessLayer.DataAccess.Entities;

namespace GameOfDronesDataAccessLayer.DataAccess.Interfaces
{
    public interface IDepartmentRepository : IRepository<Department>
    {
        IQueryable<Department> GetPagedDepartmentByName(string name, int pageSize = 10, int pageNo = 1);
        IQueryable<Department> GetPagedDepartmentById(int id, int pageSize = 15, int pageNo = 1);
        IQueryable<Department> DepartmentPagedData(int pageSize = 15, int pageNo = 1);
    }
}
