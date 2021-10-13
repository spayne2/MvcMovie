using Microsoft.AspNetCore.Mvc;
using MvcMovie.Data;
using MvcMovie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MvcMovie.Controllers
{
    [Route("/api/movies")]
    [ApiController]
    public class MoviesApi : Controller
    {
        private readonly MvcMovieContext _context;

        public MoviesApi(MvcMovieContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            Console.Out.WriteLine("Get all Movies");
            return await _context.Movie.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovies(int id)
        {
            var movie = await _context.Movie
               .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return movie;
        }

        [HttpPost]
        public async Task<ActionResult<Movie>> PostTodoItem(Movie movie)
        {
            _context.Movie.Add(movie);
            await _context.SaveChangesAsync();

            return movie;
        }
    }
}
