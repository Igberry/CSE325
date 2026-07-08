using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApp.Data;
using MovieApp.Models;

namespace MovieApp.Controllers;

public class MoviesController : Controller
{
    private readonly MovieDbContext _context;

    public MoviesController(MovieDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(string? searchString, int? year)
    {
        var movies = from m in _context.Movies
                     select m;

        if (!string.IsNullOrEmpty(searchString))
        {
            movies = movies.Where(m => m.Title.Contains(searchString));
        }

        if (year.HasValue)
        {
            movies = movies.Where(m => m.ReleaseYear >= year.Value);
            ViewData["YearFilter"] = year.Value;
        }

        ViewData["CurrentFilter"] = searchString;
        return View(await movies.ToListAsync());
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
            return NotFound();

        var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id.Value);
        if (movie == null)
            return NotFound();

        return View(movie);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Title,Genre,ReleaseYear,Rating,Director")] Movie movie)
    {
        if (ModelState.IsValid)
        {
            _context.Add(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(movie);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
            return NotFound();

        var movie = await _context.Movies.FindAsync(id.Value);
        if (movie == null)
            return NotFound();

        return View(movie);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Genre,ReleaseYear,Rating,Director")] Movie movie)
    {
        if (id != movie.Id)
            return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(movie);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(movie.Id))
                    return NotFound();
                throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(movie);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
            return NotFound();

        var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id.Value);
        if (movie == null)
            return NotFound();

        return View(movie);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var movie = await _context.Movies.FindAsync(id);
        if (movie == null)
            return NotFound();

        _context.Movies.Remove(movie);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool MovieExists(int id)
    {
        return _context.Movies.Any(e => e.Id == id);
    }
}
