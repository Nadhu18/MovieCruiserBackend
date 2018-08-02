using Microsoft.EntityFrameworkCore;
using movieCruiserByRohith.Data.Models;
using movieCruiserByRohith.Data.Persistence;
using System;
using System.Collections.Generic;

namespace test
{
    public class DatabaseFixture : IDisposable
    {
        private IEnumerable<Movie> Movies { get; set; }
        public IMoviesDbContext dbcontext;

        public DatabaseFixture()
        {
            var options = new DbContextOptionsBuilder<MoviesDbContext>()
                .UseInMemoryDatabase(databaseName: "MovieDB")
                .Options;
            dbcontext = new MoviesDbContext(options);

            dbcontext.Movies.Add(new Movie { Id = 1, Name = "movie1", Comments = "comments1", PosterPath = "path1", ReleaseDate = "releaseDate1", VoteAverage = 5.00, VoteCount = 100 });
            dbcontext.Movies.Add(new Movie { Id = 2, Name = "movie2", Comments = "comments2", PosterPath = "path2", ReleaseDate = "releaseDate2", VoteAverage = 6.00, VoteCount = 200 });
            dbcontext.Movies.Add(new Movie { Id = 3, Name = "movie3", Comments = "comments3", PosterPath = "path3", ReleaseDate = "releaseDate3", VoteAverage = 7.00, VoteCount = 300 });

            dbcontext.SaveChanges();
        }

        public void Dispose()
        {
            Movies = null;
            dbcontext = null;
        }
    }
}
