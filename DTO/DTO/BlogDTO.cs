using Model;
using Repository.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTO
{
    public class BlogDTO
    {
        private readonly IBlogRepository _blogRepository;

        public BlogDTO(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public async Task Active(int id)
        {
             await _blogRepository.Active(id);
        }

        public async Task<BlogServiceMessageModel> CreateBlog(PostTable blogtable)
        {
            return await _blogRepository.CreateBlog(blogtable);
        }

        public async Task<BlogServiceMessageModel> Delete(int id)
        {
            return await _blogRepository.Delete(id);
        }

        public IEnumerable<PostTable> GetAll()
        {
            return _blogRepository.GetAll();
        }

        public List<PostTable> GetAllUserBlog(int id)
        {
            return _blogRepository.GetAllUserBlog(id);
        }

        public async Task<PostTable> GetById(int id)
        {
            return await _blogRepository.GetById(id);
        }

        public IEnumerable<PostTable> GetLastFiveBlog()
        {
            return _blogRepository.GetLastFiveBlog();
        }

        public async Task InActive(int id)
        {
         await   _blogRepository.InActive(id);
        }

        public async Task<BlogServiceMessageModel> Update(int id, PostTable entity)
        {
            return await _blogRepository.Update(id, entity);
        }
    }
}
