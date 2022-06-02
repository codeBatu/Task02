using Model;
using Repository.Models;
using Repository.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class BlogRepository : IBlogRepository
    {
        private readonly SmartPulse01DbContext? _smartPulse01DbContext;

        public BlogRepository(SmartPulse01DbContext smartPulse01DbContext)
        {
            _smartPulse01DbContext = smartPulse01DbContext;
        }

        public async Task<BlogServiceMessageModel> CreateBlog(PostTable blogtable)
        {
            BlogServiceMessageModel blogServiceMessageModel = new();

            blogServiceMessageModel.IsSucces = true;
            blogServiceMessageModel.Message = "Succes";
            var result = _smartPulse01DbContext.PostTables.SingleOrDefault(z => z.Title == blogtable.Title && z.Description == blogtable.Description);


            if (result is not null)
            {
                blogServiceMessageModel.IsSucces = false;
                blogServiceMessageModel.Message = "Kayıtlı";
                throw new Exception($"  Error Message : { blogServiceMessageModel.Message}");
            }
            _smartPulse01DbContext.Add(blogtable);

            return await saveData();
        }

        public IEnumerable<PostTable> GetLastFiveBlog()
        {


            return _smartPulse01DbContext.PostTables.OrderByDescending(x => x.Id).Take(5);
        }
        #region Blog Active / Passive
        public async Task InActive(int id)
        {
            var result = await validResultById(id);

            result.Status = false;
            result.LastUpdate = DateTime.Now;

            await saveData();
        }

        public async Task Active(int id)
        {
            var result = await validResultById(id);

            result.Status = true;
            result.LastUpdate = DateTime.Now;

            await saveData();
        }
        #endregion
        public IEnumerable<PostTable> GetAll()
        {
            var result = _smartPulse01DbContext.PostTables.OrderBy(x => x.CreateDateTime).Where(x => x.Status == true).ToList();
            if (result is null)
            {
                return new List<PostTable>();
            }
            return result;
        }
        public List<PostTable> GetAllUserBlog(int id)
        {
            var result = _smartPulse01DbContext.PostTables.OrderBy(x => x.CreateDateTime).Where(x => x.Status == true).Where(x => x.UserId == id).ToList();
            ;
            if (result is null)
            {
                return new List<PostTable>();
            }
            return result;
        }
        public async Task<BlogServiceMessageModel> Update(int id, PostTable entity)
        {

            var result = await validResultById(id);
            if (result.Title == entity.Title && result.Description == entity.Description)
            {
                result.Status = entity.Status;
                result.Image = entity.Image;
                result.LastUpdate = entity.LastUpdate;
                return await saveData();

            }

            result.Description = entity.Description;
            result.Image = entity.Image;
            result.LastUpdate = DateTime.Now.ToLocalTime();
            result.Title = entity.Title;
            result.Status = entity.Status;

            return await saveData();
        }

        public async Task<BlogServiceMessageModel> Delete(int id)
        {

            var result = await validResultById(id);

            _smartPulse01DbContext.Remove<PostTable>(result);

            return await saveData();
        }

        public async Task<PostTable> GetById(int id)
        {
            return await validResultById(id);
        }

        private async Task<BlogServiceMessageModel> saveData()
        {
            BlogServiceMessageModel blogServiceMessageModel = new();
            blogServiceMessageModel.IsSucces = true;

            blogServiceMessageModel.Message = "Succes";
            var saveDataResponseCode = await _smartPulse01DbContext.SaveChangesAsync();
            if (saveDataResponseCode != 1)
            {
                blogServiceMessageModel.IsSucces = false;
                blogServiceMessageModel.Message = "Not Save Value on Db";
                throw new Exception($"  Error Message : { blogServiceMessageModel.Message}");
            }

            return blogServiceMessageModel;
        }

        private async Task<PostTable> validResultById(int id)
        {
            BlogServiceMessageModel blogServiceMessageModel = new();
            var result = await _smartPulse01DbContext.PostTables.FindAsync(id);
            if (result is null)
            {
                blogServiceMessageModel.IsSucces = false;
                blogServiceMessageModel.Message = "Gçersi Id";
                throw new Exception($"  Error Message : { blogServiceMessageModel.Message}");
            }
            return result;
        }

    }
}