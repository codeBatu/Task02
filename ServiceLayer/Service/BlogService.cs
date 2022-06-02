using DTO.DTO;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
   public class BlogService
    {
        private readonly BlogDTO _blogDTO;

        public BlogService(BlogDTO blogDTO)
        {
            _blogDTO = blogDTO;
        }
        public async Task Active(int id)
        {
            await _blogDTO.Active(id);
        }

        public async Task<BlogServiceMessageModel> CreateBlog(PostTable blogtable)
        {
            return await _blogDTO.CreateBlog(blogtable);
        }

        public async Task<BlogServiceMessageModel> Delete(int id)
        {
            return await _blogDTO.Delete(id);
        }

        public IEnumerable<PostTable> GetAll()
        {
            return _blogDTO.GetAll();
        }

        public List<PostTable> GetAllUserBlog(int id)
        {
            return _blogDTO.GetAllUserBlog(id);
        }

        public async Task<PostTable> GetById(int id)
        {
            return await _blogDTO.GetById(id);
        }

        public IEnumerable<PostTable> GetLastFiveBlog()
        {
            return _blogDTO.GetLastFiveBlog();
        }

        public async Task InActive(int id)
        {
            await _blogDTO.InActive(id);
        }

        public async Task<BlogServiceMessageModel> Update(int id, PostTable entity)
        {
            return await _blogDTO.Update(id, entity);
        }
    }
}
