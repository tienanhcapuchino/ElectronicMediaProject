using ElectronicMedia.Core.Repository.DataContext;
using ElectronicMedia.Core.Services.Interfaces;
using ElectronicMedia.Core.Services.Service;

namespace ElectronicMedia.Test
{
    public class Tests
    {
        private readonly ElectronicMediaDbContext _context = new ElectronicMediaDbContext();
        private readonly IPostDetailService _postDetailService;
        private readonly IPostService _postService;
        [SetUp]
        public void Setup()
        {

            //_postService = new PostService(_context, _postDetailService);
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}