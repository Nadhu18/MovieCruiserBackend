using movieCruiserByRohith.Data.Models;
using movieCruiserByRohith.Data.Persistence;
using System.Collections.Generic;

namespace movieCruiserByRohith.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _repo;

        public MovieService(IMovieRepository repo) {
            _repo = repo;
        }

        //Returns all the movies from the repository
        public List<Movie> GetAllMovies() {
            return _repo.GetAllMovies();
        }

        //Adds a movie to the repository
        public void AddMovie(Movie movie){
            _repo.AddMovie(movie);
        }

        //Deletes the movie from repository
        public void DeleteMovie(int id){
            _repo.DeleteMovie(id);
        }

        //Edits the existing movie
        public void EditMovie(Movie movie){
            _repo.EditMovie(movie);
        }

        //Returns a particular movie
        public Movie GetMovie(int id){
            return _repo.GetMovie(id);
        }

        //Returns true if movie exists, else false
        public bool MovieExists(int id){
            return _repo.MovieExists(id);
        }
    }
}
