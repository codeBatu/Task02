using Helper;
using Microsoft.AspNetCore.Mvc;
using Model;
using Repository;
using ServiceLayer;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartPulseWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class blogController : ControllerBase
    {
        private readonly BlogService _blogService;

        public blogController(BlogService blogService)
        {
            _blogService = blogService;
        }
     
        [HttpPut("activeBlog")]
        public IActionResult ActiveToBlog(int id)
        {
            var response = _blogService.Active(id);

            return Ok(response);
        }
        [HttpGet("getBlogById")]
        public IEnumerable<PostTable> GetAllByBlogs(int id)
        {
            return _blogService.GetAllUserBlog(id);
        }

        [HttpPost("inactiveBlog")]
        public IActionResult InActiveUser(int id)
        {
            var response = _blogService.InActive(id);

            return Ok(response);
        }
        [HttpGet("GetById")]
        public IActionResult GetById(int id)
        {
            var response = _blogService.GetById(id);

            return Ok(response);
        }

       
        [HttpGet("GetAllBlog")]
        public IEnumerable<PostTable> GetAll()
        {
            return _blogService.GetAll();
        }

        [HttpGet("GetLastFiveBlog")]
        public IEnumerable<PostTable> GetLastFiveBlog()
        {
            return _blogService.GetLastFiveBlog();
        }

   
        [HttpPost("CreateBlog")]
        public async Task<IActionResult> CreateBlogs([FromBody] PostTable blogtable)
        {
            return Ok(await _blogService.CreateBlog(blogtable));
        }

  
        [HttpPut("UpdateeBlog")]
        public async Task<IActionResult> UpdateBlogs(int id,[FromBody] PostTable blogtable)
        {
            return Ok(await _blogService.Update(id,(blogtable)));
        }

       
        [HttpDelete(("RemoveBlog"))]
        public async Task<IActionResult> RemoveBlogs(int id)
        {
            return Ok(await _blogService.Delete(id));
        }
   
    } 
}