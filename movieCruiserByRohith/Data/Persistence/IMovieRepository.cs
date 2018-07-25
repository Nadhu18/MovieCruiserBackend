﻿using movieCruiserByRohith.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace movieCruiserByRohith.Data.Persistence
{
    public interface IMovieRepository
    {
        List<Movie> GetAllMovies();
        
        Movie GetMovie(int id);

        bool MovieExists(int id);

        void AddMovie(Movie movie);

        void EditMovie(Movie movie);

        void DeleteMovie(int id);
    }
}
