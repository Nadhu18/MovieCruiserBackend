using movieCruiserByRohith.Data.Models;
using movieCruiserByRohith.Data.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace test
{
    public class MovieRepositoryTest : IClassFixture<DatabaseFixture>
    {
        private readonly IMovieRepository _repo;
        DatabaseFixture _fixture;

        public MovieRepositoryTest(DatabaseFixture fixture)
        {
            _fixture = fixture;
            _repo = new MovieRepository(_fixture.dbcontext);
        }

        [Fact]
        public void GetAllMovies_ShouldReturnListOfMovie()
        {
            //Act
            var actual = _repo.GetAllMovies();

            //Assert
            Assert.IsAssignableFrom<List<Movie>>(actual);
            Assert.Equal(3, actual.Count);
        }
    }
}
