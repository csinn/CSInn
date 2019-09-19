using System;
using System.Collections.Generic;
using System.Text;
using CSInn.Infrastructure.Repository.Tests.Data;
using Xunit;

namespace CSInn.Infrastructure.Repository.Tests
{
    public class LessonRepositoryTests: IClassFixture<LessonRepositoryFixture>
    {
        private readonly object _repo;

        public LessonRepositoryTests(LessonRepositoryFixture fixture)
        {
            _repo = fixture.Repo;
        }

        [Fact]
        public void Get_By_Id_Ok()
        {

        }
    }
}
