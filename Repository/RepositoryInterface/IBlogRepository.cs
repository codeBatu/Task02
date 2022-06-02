using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.RepositoryInterface
{
    public interface IBlogRepository : ICrudRepository<PostTable, int, BlogServiceMessageModel>
    {
        public Task<BlogServiceMessageModel> CreateBlog(PostTable blogtable);

        public IEnumerable<PostTable> GetLastFiveBlog();

        List<PostTable> GetAllUserBlog(int id);
        Task InActive(int id);

        Task Active(int id);
    }
}