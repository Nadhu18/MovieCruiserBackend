using Microsoft.EntityFrameworkCore;
using movieCruiserByRohith.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace movieCruiserByRohith.Data.Persistence
{
    public class MovieRepository : IMovieRepository
    {
        private readonly IMoviesDbContext _context;
        
        public MovieRepository(IMoviesDbContext context) {
            _context = context;
        }

        public List<Movie> GetAllMovies() {
            return _context.Movies.ToList();
        }

        public void AddMovie(Movie movie) {
            _context.Movies.Add(movie);
            _context.SaveChanges();
        }

        public void DeleteMovie(int id) {
            var movie = GetMovie(id);
            _context.Movies.Remove(movie);
            _context.SaveChanges();
        }

        public void EditMovie(Movie movie) {
            _context.Entry(movie).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public Movie GetMovie(int id) {
            return _context.Movies.SingleOrDefault(m => m.Id == id);
        }

        public bool MovieExists(int id) {
            return _context.Movies.Any(e => e.Id == id);
        }
    }
}
