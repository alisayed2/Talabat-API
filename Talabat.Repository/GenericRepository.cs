using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Core.Specifications;
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

        // Get All 
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Product))
                return (IEnumerable<T>) await context.Products
                                              .Include(P => P.ProductBrand)
                                              .Include(P => P.ProductType)
                                              .ToListAsync();
            else
                 return await context.Set<T>().ToListAsync();
        }

        // Get By Id 
        public async Task<T> GetByIdAsync(int id)
             => await context.Set<T>().FindAsync(id);


        // Get All With Specification
        public async Task<IEnumerable<T>> GetAllWithSpecAsync(ISpecification<T> spec)
            => await ApplySpecification(spec).ToListAsync();


        // Get By Id With Specification
        public async Task<T> GetByIdWithSpecAsync(ISpecification<T> spec)
            => await ApplySpecification(spec).FirstOrDefaultAsync();


        // Call Specification Evaluator
        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(context.Set<T>(), spec);
        }
    }
}
