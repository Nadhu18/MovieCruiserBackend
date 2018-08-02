using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using movieCruiserByRohith.Data.Models;
using movieCruiserByRohith.Services;
using System;

namespace movieCruiserByRohith.Controllers
{
    [Produces("application/json")]
    [Route("api/Movie")]
    public class MovieController : Controller
    {
        private readonly IMovieService _service;

        public MovieController(IMovieService service)
        {
            _service = service;
        }

        // Method to get all the movies from the repository
        // GET: api/Movie
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var result = _service.GetAllMovies();
                return Ok(result);
            }
            catch (Exception)
            {
                return NotFound("There is an Error");
            }
        }

        //Method to get a particular movie from the repository
        // GET: api/Movie/123
        [HttpGet("{id}")]
        public IActionResult GetMovie([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var movie = _service.GetMovie(id);
            if (movie == null)
            {
                return NotFound();
            }
            return Ok(movie);
        }

        //Method to save a movie to the repository
        [HttpPost]
        public ActionResult PostMovie([FromBody] Movie movie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (_service.MovieExists(movie.Id))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    _service.AddMovie(movie);
                }
            }
            catch (Exception)
            {
                throw new Exception("Unkonown error occured");
            }

            return CreatedAtAction("GetMovie", new { id = movie.Id }, movie);
        }

        //Method to edit the already existing movie in the repository
        // PUT: api/Movie/5
        [HttpPut("{id}")]
        public IActionResult PutMovie([FromRoute] int id, [FromBody] Movie movie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != movie.Id)
            {
                return BadRequest();
            }

            try
            {
                _service.EditMovie(movie);
                return Ok(movie);
                
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_service.MovieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        //Method to delete a movie from repository
        // DELETE: api/Movie/5
        [HttpDelete("{id}")]
        public IActionResult DeleteMovie([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var movie = _service.GetMovie(id);

            if (movie == null)
            {
                return NotFound();
            }

            _service.DeleteMovie(id);
            return Ok(movie);
        }
        
    }
}
