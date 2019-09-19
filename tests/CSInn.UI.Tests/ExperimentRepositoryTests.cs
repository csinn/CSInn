using System.Linq;
using CSInn.Experimental.EF.Repositories;
using CSInn.UI.Tests.Data;
using FluentAssertions;
using Xunit;

namespace CSInn.UI.Tests
{
    public class ExperimentRepositoryTests: IClassFixture<ExperimentRepositoryFixture>
    {
        private readonly ExperimantalRepository _repo;

        public ExperimentRepositoryTests(ExperimentRepositoryFixture fixture)
        {
            _repo = fixture.Repo;
        }

        [Fact]
        public void Get_By_Id_Ok()
        {
            var experiment = _repo.Get("Hey");
            experiment.Count().Should().Be(1);
        }
    }
}
