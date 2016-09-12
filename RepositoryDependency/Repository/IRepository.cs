using System;
using System.Collections.Generic;


namespace RepositoryDependency.Repository
{
        //The Generic Interface Repository for Performing Read/Add/Delete operations
        public interface IRepository<TEnt, in TPk> where TEnt : class
        {
            IEnumerable<TEnt> Get();
            TEnt Get(TPk id);
            void Add(TEnt entity);
            void Update(TEnt entity);
            void Remove(TEnt entity);
        }
    
}
