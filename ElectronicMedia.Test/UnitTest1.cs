using ElectronicMedia.Core.Repository.DataContext;
using ElectronicMedia.Core.Repository.Models;
using ElectronicMedia.Core.Services.Interfaces;
using ElectronicMedia.Core.Services.Service;

namespace ElectronicMedia.Test
{
    public class Tests
    {
        private ElectronicMediaDbContext _context;
        //private IPostDetailService _postDetailService;
        //private IPostService _postService;
        private IPostCategoryService _cateService;
        [SetUp]
        public void Setup()
        {
            _context = new ElectronicMediaDbContext();
            //_postDetailService = new PostDetailService(_context);
            //_postService = new PostService(_context, _postDetailService);
            _cateService = new PostCategoryService(_context);
        }

        [Test]
        public void Test1()
        {
            PostCategoryModel model = new PostCategoryModel()
            {
                Name = "Cai nay sub",
                //ParentId = Guid.Parse("94614383-CE26-47E4-10B8-08DB5A9FBC58"),
                Description = "Day la category sub tesst"
            };
            //Assert.AreEqual(true, _postService.CreatePostCategory(model).Result);
        }
        //[Test]
        //public void Test2()
        //{
        //    //Assert.AreEqual()
        //}
    }
}