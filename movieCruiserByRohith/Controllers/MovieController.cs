using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using movieCruiserByRohith.Data.Models;
using movieCruiserByRohith.Services;

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

        [HttpPost]
        public ActionResult PostMovie([FromBody] Movie movie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _service.AddMovie(movie);
            }
            catch (DbUpdateException)
            {
                if (_service.MovieExists(movie.Id))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("GetMovie", new { id = movie.Id }, movie);
        }
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
