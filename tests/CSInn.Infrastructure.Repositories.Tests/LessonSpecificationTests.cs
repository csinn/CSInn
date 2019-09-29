using System.Threading.Tasks;
using CSInn.Domain.Repositories.Extensions;
using CSInn.Domain.Repositories.Specifications.Lesson;
using CSInn.Domain.Repositories.UnitOfWork;
using CSInn.Infrastructure.Repositories.Tests.Input;
using Xunit;

namespace CSInn.Infrastructure.Repositories.Tests
{
    public class LessonSpecificationTests : IClassFixture<UoWFixture>
    {
        private readonly ICSInnUnitOfWork _uow;

        public LessonSpecificationTests(UoWFixture fixture)
        {
            _uow = fixture.UoW;
        }

        [Fact]
        public void Composite_Specification_Ok()
        {
            var filter = new TitleLike("Lesson 2")
                .Or(new TitleLike("Lesson 1")).Not();

            var result = _uow.Lessons.Get(filter);

            Assert.Single(result);
        }

        [Fact]
        public async Task Composite_Specification_Async_Ok()
        {
            var filter = new TitleLike("Lesson 2")
                .Or(new TitleLike("Lesson 1")).Not();

            var result = await _uow.Lessons.GetAsync(filter);

            Assert.Single(result);
        }
    }
}
