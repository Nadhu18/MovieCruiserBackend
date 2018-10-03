using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using movieCruiserByRohith.Data.Models;
using movieCruiserByRohith.Data.Persistence;
using System.Collections.Generic;

namespace movieCruiserByRohith.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _repo;
        private StringValues UserId = new StringValues();

        public MovieService(IMovieRepository repo)
        {
            _repo = repo;
        }

        public MovieService(IMovieRepository repo, IHttpContextAccessor contextAccessor) {
            _repo = repo;
            var context = contextAccessor;
            context.HttpContext.Request.Headers.TryGetValue("userId", out UserId);
        }

        //Returns all the movies from the repository
        public List<Movie> GetAllMovies() {
            var movies = _repo.GetAllMovies().FindAll(x => x.UserId==UserId);
            return movies;
        }

        //Adds a movie to the repository
        public void AddMovie(Movie movie){
            movie.UserId = UserId;
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
            return _repo.MovieExists(id, UserId);
        }
    }
}
