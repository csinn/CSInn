using CSInn.Domain.Repositories.Extensions;
using CSInn.Domain.Repositories.Repositories;
using CSInn.Domain.Repositories.Specifications.Lesson;
using CSInn.Infrastructure.Repositories.Tests.Input;
using Xunit;

namespace CSInn.Infrastructure.Repositories.Tests
{
    public class LessonSpecificationTests: IClassFixture<LessonsRepositoryFixture>
    {
        private readonly ILessonsRepository _repository;

        public LessonSpecificationTests(LessonsRepositoryFixture fixture)
        {
            _repository = fixture.Repo;
        }

        [Fact]
        public void Composite_Specification_Ok()
        {
            var filter = new TitleLike("Lesson 2");

            var result = _repository.Get(filter);
            Assert.Single(result);
        }
    }
}
