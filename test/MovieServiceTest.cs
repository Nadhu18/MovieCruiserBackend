using movieCruiserByRohith.Data.Models;
using movieCruiserByRohith.Data.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using movieCruiserByRohith.Services;

namespace test
{
    public class MovieServiceTest
    {
        [Fact]
        public void GetAllMovies_ShouldReturnListOfMovie() {
            //Arrange
            var mockRepo = new Mock<IMovieRepository>();
            mockRepo.Setup(repo => repo.GetAllMovies()).Returns(this.GetMovies());
            var service = new MovieService(mockRepo.Object);

            //Act
            var actual = service.GetAllMovies();

            //Assert
            Assert.IsAssignableFrom<List<Movie>>(actual);
            Assert.NotNull(actual);
            Assert.Equal(3, actual.Count);
        }

        private List<Movie> GetMovies() {
            var movie = new List<Movie>();

            movie.Add(new Movie { Id = 1, Name = "movie1", Comments = "comments1", PosterPath = "path1", ReleaseDate = "releaseDate1", VoteAverage = 5.00, VoteCount = 100 });
            movie.Add(new Movie { Id = 2, Name = "movie2", Comments = "comments2", PosterPath = "path2", ReleaseDate = "releaseDate2", VoteAverage = 6.00, VoteCount = 200 });
            movie.Add(new Movie { Id = 3, Name = "movie3", Comments = "comments3", PosterPath = "path3", ReleaseDate = "releaseDate3", VoteAverage = 7.00, VoteCount = 300 });

            return movie;
        }
    }
}
