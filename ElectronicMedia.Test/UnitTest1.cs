using ElectronicMedia.Core.Repository.DataContext;
using ElectronicMedia.Core.Repository.Models;
using ElectronicMedia.Core.Services.Interfaces;
using ElectronicMedia.Core.Services.Service;

namespace ElectronicMedia.Test
{
    public class Tests
    {
        private ElectronicMediaDbContext _context;
        private IPostDetailService _postDetailService;
        private IPostService _postService;
        [SetUp]
        public void Setup()
        {
            _context = new ElectronicMediaDbContext();
            _postDetailService = new PostDetailService(_context);
            _postService = new PostService(_context, _postDetailService);
        }

        [Test]
        public void Test1()
        {
            PostCategoryModel model = new PostCategoryModel()
            {
                Name = "Test cate",
                ParentId = Guid.Parse("F7BEFD8A-A66F-4ACA-8021-F23B5E244945"),
                Description = "Day la category test"
            };
            Assert.AreEqual(true, _postService.CreatePostCategory(model).Result);
        }
        [Test]
        public void Test2()
        {
            //Assert.AreEqual()
        }
    }
}