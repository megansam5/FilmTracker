using FilmTracker.Data;
using FilmTracker.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace FilmTracker.Services;

public interface IMovieService
{
    Task AddMovieToFavourites(User user, MovieApi movie);
    Task<List<Movie>> GetUserMovies(User user);
    Task RemoveMovieFromFavourites(User user, string imdbID);
}
public class MovieService : IMovieService
{
    private readonly DataContext _dataContext;

    public MovieService(DataContext dataContext)
    {
        this._dataContext = dataContext;
    }
    
    public async Task AddMovieToFavourites(User user, MovieApi movie)
    {
        user.Movies.Add(new Movie(){imdbID = movie.imdbID, Poster = movie.Poster, Title = movie.Title, User = user, UserId = user.UserId});
        await _dataContext.SaveChangesAsync();
    }

    public async Task RemoveMovieFromFavourites(User user, string imdbID)
    {
        user.Movies.Remove((await GetUserMovies(user)).Where(m => m.imdbID == imdbID).ToList()[0]);
        await _dataContext.SaveChangesAsync();
    }

    public async Task<List<Movie>> GetUserMovies(User user)
    {
        return await _dataContext.Movies.Where(m => m.UserId == user.UserId).ToListAsync();
    }
}