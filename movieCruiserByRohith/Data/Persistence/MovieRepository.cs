using Microsoft.EntityFrameworkCore;
using movieCruiserByRohith.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace movieCruiserByRohith.Data.Persistence
{
    public class MovieRepository : IMovieRepository
    {
        private readonly IMoviesDbContext _context;
        
        public MovieRepository(IMoviesDbContext context) {
            _context = context;
        }

        //Returns all the movies from the DB
        public List<Movie> GetAllMovies() {
            return _context.Movies.ToList();
        }

        //Adds a movie to the DB
        public void AddMovie(Movie movie) {
            _context.Movies.Add(movie);
            _context.SaveChanges();
        }

        //Deletes a particular movie from the DB
        public void DeleteMovie(int id) {
            var movie = GetMovie(id);
            _context.Movies.Remove(movie);
            _context.SaveChanges();
        }

        //Edits a particular movie that exists in the DB
        public void EditMovie(Movie movie) {
            _context.Entry(movie).State = EntityState.Modified;
            _context.SaveChanges();
        }

        //Returns a particular movie from DB
        public Movie GetMovie(int id) {
            return _context.Movies.SingleOrDefault(m => m.Id == id);
        }

        //Returns true if movie exists in DB, else false
        public bool MovieExists(int id, string userId) {
            return _context.Movies.Any(e => e.Id == id && e.UserId==userId);
        }
    }
}
