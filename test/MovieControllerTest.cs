using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using movieCruiserByRohith.Services;
using movieCruiserByRohith.Data.Models;
using Moq;
using Microsoft.AspNetCore.Mvc;
using movieCruiserByRohith.Controllers;

namespace test
{
    public class MovieControllerTest
    {
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
