using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext context;

        public GenericRepository(StoreContext _context)
        {
            context = _context;
        }


        public async Task<IEnumerable<T>> GetAllAsync()

            => await context.Set<T>().ToListAsync() ;
        

        public async Task<T> GetByIdAsync(int id)
             => await context.Set<T>().FindAsync(id);

    }
}
