using movieCruiserByRohith.Data.Models;
using movieCruiserByRohith.Data.Persistence;
using System.Collections.Generic;
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
            Assert.True(actual.Count>3);
        }

        [Fact]
        public void GetMovie_ShouldReturnAMovie()
        {
            var actual = _repo.GetMovie(1);

            Assert.IsAssignableFrom<Movie>(actual);
            Assert.Equal("movie1", actual.Name);
        }

        [Fact]
        public void AddMovie_MovieIsAdded()
        {
            var movie = new Movie { Id = 5, Name = "Movie Test", Comments = "comments1", PosterPath = "pp.jpg", ReleaseDate = "releaseDate1", VoteAverage = 5.00, VoteCount = 100 };

            _repo.AddMovie(movie);
            var savedMovie = _repo.GetMovie(5);

            Assert.Equal("Movie Test", savedMovie.Name);
        }

        [Fact]
        public void EditMovie_MovieIsEdited()
        {
            var movie = new Movie { Id = 6, Name = "Movie Test", Comments = "comments1", PosterPath = "pp.jpg", ReleaseDate = "releaseDate1", VoteAverage = 5.00, VoteCount = 100 };

            _repo.AddMovie(movie);
            movie.Name = "Movie Test Edited";

            _repo.EditMovie(movie);
            var savedMovie = _repo.GetMovie(movie.Id);

            Assert.Equal("Movie Test Edited", savedMovie.Name);
        }

        [Fact]
        public void DeleteMovie_MovieIsDeleted()
        {
            _repo.DeleteMovie(1);

            Assert.Null(_repo.GetMovie(1));
        }
    }
}
