using System.Collections.Generic;
using System.Linq;
using RepositoryDependency.Models;
using System.Data.Entity;


namespace RepositoryDependency.Repository
{
    //The EmployeeInfo Repository Class. This is used to 
    //Isolate the EntityFramework based Data Access Layer from
    //the MVC Controller class
    public class EmployeeInfoRepository : IRepository<EmployeeInfo, int>
    {
        public EmployeeInfoRepository(EmployeeDbContext db)
        {
            this.context = db;
        }

      
        public EmployeeDbContext context { get; set; }

        public IEnumerable<EmployeeInfo> Get()
        {
            return context.EmployeeInfoes.ToList();
        }

        public EmployeeInfo Get(int id)
        {
            return context.EmployeeInfoes.Find(id);
        }

        public void Update(EmployeeInfo entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Add(EmployeeInfo entity)
        {
            context.EmployeeInfoes.Add(entity);
            context.SaveChanges();
        }

        public void Remove(EmployeeInfo entity)
        {
            var obj = context.EmployeeInfoes.Find(entity.EmpNo);
            context.EmployeeInfoes.Remove(obj);
            context.SaveChanges();
        }
    }
}