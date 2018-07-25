using Microsoft.AspNetCore.Http;
using movieCruiserByRohith.Data.Models;
using movieCruiserByRohith.Data.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace movieCruiserByRohith.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _repo;

        public MovieService(IMovieRepository repo) {
            _repo = repo;
        }

        public List<Movie> GetAllMovies() {
            return _repo.GetAllMovies();
        }

        public void AddMovie(Movie movie){
            _repo.AddMovie(movie);
        }

        public void DeleteMovie(int id){
            _repo.DeleteMovie(id);
        }

        public void EditMovie(Movie movie){
            _repo.EditMovie(movie);
        }

        public Movie GetMovie(int id){
            return _repo.GetMovie(id);
        }

        public bool MovieExists(int id){
            return _repo.MovieExists(id);
        }
    }
}
