using Microsoft.AspNetCore.Mvc;
using Moq;
using movieCruiserByRohith.Controllers;
using movieCruiserByRohith.Data.Models;
using movieCruiserByRohith.Services;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace test
{
    public class MovieControllerTest
    {
        //Test to get all the movies from the repository
        [Fact]
        public void GetMethodWithoutParameter_ShouldReturnListOfMovie()
        {
            //Arrange
            var mockService = new Mock<IMovieService>();
            mockService.Setup(service => service.GetAllMovies()).Returns(this.GetMovies());
            var controller = new MovieController(mockService.Object);

            //Act
            var result = controller.Get();

            //Assert
            var actionresult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Movie>>(actionresult.Value);

        }

        //Test to get a particular movie from the repository
        [Fact]
        public void GetMethodWithParameter_ShouldReturnAMovie()
        {
            var mockService = new Mock<IMovieService>();
            mockService.Setup(service => service.GetMovie(1)).Returns(this.GetMovies().Single(m => m.Id == 1));
            var controller = new MovieController(mockService.Object);

            var result = controller.GetMovie(1);

            var actionResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsAssignableFrom<Movie>(actionResult.Value);
            Assert.Equal("movie1", ((Movie)actionResult.Value).Name);
        }

        //Test to add a movie to repository
        [Fact]
        public void PostMovie_MovieIsAdded()
        {
            var mockService = new Mock<IMovieService>();
            var movie = new Movie { Id = 5, Name = "Movie Test", Comments = "comments1", PosterPath = "pp.jpg", ReleaseDate = "releaseDate1", VoteAverage = 5.00, VoteCount = 100 };
            List<Movie> addedMovies = new List<Movie>();

            mockService.Setup(service => service.AddMovie(movie)).Callback<Movie>((m) => addedMovies.Add(m));
            var controller = new MovieController(mockService.Object);

            var result = controller.PostMovie(movie);

            var actionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal("Movie Test", ((Movie)actionResult.Value).Name);
            Assert.True(1 == addedMovies.Count);
            Assert.NotNull(addedMovies.SingleOrDefault(m => m.Name == "Movie Test"));
        }

        //Test to edit a existing movie in repository
        [Fact]
        public void PutMovie_MovieIsEdited()
        {
            var mockService = new Mock<IMovieService>();
            var movie = new Movie { Id = 5, Name = "Movie Test", Comments = "comments1", PosterPath = "pp.jpg", ReleaseDate = "releaseDate1", VoteAverage = 5.00, VoteCount = 100 };
            var editedMovie = new Movie { Id = 5, Name = "Movie Test Edited", Comments = "comments1", PosterPath = "pp.jpg", ReleaseDate = "releaseDate1", VoteAverage = 5.00, VoteCount = 100 };

            mockService.Setup(service => service.EditMovie(editedMovie)).Callback<Movie>((m) => movie = m);

            var controller = new MovieController(mockService.Object);
            var result = controller.PutMovie(movie.Id, editedMovie);

            var actionResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Movie Test Edited", ((Movie)actionResult.Value).Name);
        }

        //Negative test case for put
        [Fact]
        public void PutMovie_EditedMovieDoesNotExist()
        {
            var movie = new Movie { Id = 111, Name = "movie", Comments = "comments1", PosterPath = "path1", ReleaseDate = "releaseDate1", VoteAverage = 5.00, VoteCount = 100 };
            var editedMovie = new Movie { Id = 11111, Name = "edited movie", Comments = "comments1", PosterPath = "path1", ReleaseDate = "releaseDate1", VoteAverage = 5.00, VoteCount = 100 };

            var mockService = new Mock<IMovieService>();
            mockService.Setup(service => service.MovieExists(editedMovie.Id)).Returns(false);

            var mockController = new MovieController(mockService.Object);
            var result = mockController.PutMovie(movie.Id, editedMovie);

            var actionResult = Assert.IsType<BadRequestResult>(result);
            Assert.Equal<int>(400, actionResult.StatusCode);
        }

        //Test to remove a record from the repository
        [Fact]
        public void DeleteMovie_MovieIsDeleted()
        {
            var mockService = new Mock<IMovieService>();
            var movie = new Movie { Id = 5, Name = "Movie Test", Comments = "comments1", PosterPath = "pp.jpg", ReleaseDate = "releaseDate1", VoteAverage = 5.00, VoteCount = 100 };
            List<Movie> addedMovies = new List<Movie> { movie };

            mockService.Setup(service => service.GetMovie(movie.Id)).Returns(movie);
            mockService.Setup(service => service.DeleteMovie(movie.Id)).Callback<int>((id) => addedMovies.Remove(addedMovies.Single(m => m.Id == id)));
            var controller = new MovieController(mockService.Object);

            var result = controller.DeleteMovie(movie.Id);

            var actionResult = Assert.IsType<OkObjectResult>(result);
            Assert.True(0 == addedMovies.Count);
        }

        //negative test case for delete
        [Fact]
        public void DeleteMovie_MovieDoesNotExists()
        {
            var movie = new Movie { Id = 1, Name = "movie1", Comments = "comments1", PosterPath = "path1", ReleaseDate = "releaseDate1", VoteAverage = 5.00, VoteCount = 100 };
            Movie deletedMovie = null;

            var mockService = new Mock<IMovieService>();
            mockService.Setup(service => service.GetMovie(movie.Id)).Returns(deletedMovie);

            var mockController = new MovieController(mockService.Object);
            var result = mockController.DeleteMovie(movie.Id);

            var actionResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal<int>(404, actionResult.StatusCode.Value);
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
