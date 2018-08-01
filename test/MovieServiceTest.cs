using movieCruiserByRohith.Data.Models;
using movieCruiserByRohith.Data.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using movieCruiserByRohith.Services;
using System.Linq;

namespace test
{
    public class MovieServiceTest
    {
        [Fact]
        public void GetAllMovies_ShouldReturnListOfMovie()
        {
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

        [Fact]
        public void GetMovie_ReturnAMovie()
        {
            var movieRepo = new Mock<IMovieRepository>();
            movieRepo.Setup(repo => repo.GetMovie(1)).Returns(this.GetMovies().Single(m => m.Id == 1));

            var service = GetMovieService(movieRepo.Object);
            var actual = service.GetMovie(1);

            Assert.IsAssignableFrom<Movie>(actual);
            Assert.Equal("movie1", actual.Name);
        }

        [Fact]
        public void AddMovie_MovieIsAdded()
        {
            var movieRepo = new Mock<IMovieRepository>();
            List<Movie> addedMovies = new List<Movie>();
            var movie = new Movie { Id = 5, Name = "Movie Test", Comments = "comments1", PosterPath = "pp.jpg", ReleaseDate = "releaseDate1", VoteAverage = 5.00, VoteCount = 100 };

            movieRepo.Setup(repo => repo.AddMovie(movie)).Callback<Movie>((m) => addedMovies.Add(m));
            var service = GetMovieService(movieRepo.Object);
            service.AddMovie(movie);
            Assert.True(1 == addedMovies.Count);

            Assert.NotNull(addedMovies.SingleOrDefault(m => m.Name == "Movie Test"));
        }

        [Fact]
        public void EditMovie_MovieIsEdited()
        {
            var movieRepo = new Mock<IMovieRepository>();

            var movie = new Movie { Id = 5, Name = "Movie Test", Comments = "comments1", PosterPath = "pp.jpg", ReleaseDate = "releaseDate1", VoteAverage = 5.00, VoteCount = 100 };
            var editedMovie = new Movie { Id = 5, Name = "Movie Test Edited", Comments = "comments1", PosterPath = "pp.jpg", ReleaseDate = "releaseDate1", VoteAverage = 5.00, VoteCount = 100 }; ;

            movieRepo.Setup(repo => repo.EditMovie(editedMovie)).Callback<Movie>((m) => movie = m);
            var service = GetMovieService(movieRepo.Object);
            service.EditMovie(editedMovie);

            Assert.Equal("Movie Test Edited", movie.Name);
        }

        [Fact]
        public void DeleteMovie_MovieIsDeleted()
        {
            var movieRepo = new Mock<IMovieRepository>();
            var movie = new Movie { Id = 5, Name = "Movie Test", Comments = "comments1", PosterPath = "pp.jpg", ReleaseDate = "releaseDate1", VoteAverage = 5.00, VoteCount = 100 };
            List<Movie> addedMovies = new List<Movie> { movie };

            movieRepo.Setup(repo => repo.DeleteMovie(movie.Id)).Callback<int>((id) => addedMovies.Remove(addedMovies.Single(m => m.Id == id)));
            var service = GetMovieService(movieRepo.Object);
            service.DeleteMovie(movie.Id);

            Assert.True(0 == addedMovies.Count);
        }

        private IMovieService GetMovieService(IMovieRepository movieRepository)
        {
            var service = new MovieService(movieRepository);
            return service;
        }

        private List<Movie> GetMovies()
        {
            var movie = new List<Movie>();

            movie.Add(new Movie { Id = 1, Name = "movie1", Comments = "comments1", PosterPath = "path1", ReleaseDate = "releaseDate1", VoteAverage = 5.00, VoteCount = 100 });
            movie.Add(new Movie { Id = 2, Name = "movie2", Comments = "comments2", PosterPath = "path2", ReleaseDate = "releaseDate2", VoteAverage = 6.00, VoteCount = 200 });
            movie.Add(new Movie { Id = 3, Name = "movie3", Comments = "comments3", PosterPath = "path3", ReleaseDate = "releaseDate3", VoteAverage = 7.00, VoteCount = 300 });

            return movie;
        }
    }
}
