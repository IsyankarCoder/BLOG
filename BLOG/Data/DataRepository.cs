using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLOG.Data
{
    public class DataRepository<T>
        : IRepository<T> where T : class
    {
        private readonly BlogPostsContext _blogPostsContext;

        public DataRepository(BlogPostsContext blogPostsContext)
        {
            _blogPostsContext = blogPostsContext;
        }

        public void Add(T entity)
        {
            _blogPostsContext.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _blogPostsContext.Set<T>().Remove(entity);
        }

        public async Task<T> SaveAsync(T entity)
        {
            await _blogPostsContext.SaveChangesAsync();
            return entity;
        }

        public void Update(T entity)
        {
            _blogPostsContext.Set<T>().Update(entity);
        }
    }
}
