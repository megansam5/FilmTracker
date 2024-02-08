using FilmTracker.Models;

namespace FilmTracker.Services;

public interface IComparisonService
{
    Task<List<Movie>> CompareUserMovies(List<User> selectedUsers);
}

public class ComparisonService : IComparisonService
{
    private readonly IMovieService movieService;

    public ComparisonService(IMovieService movieService)
    {
        this.movieService = movieService;
    }
    
    public async Task<List<Movie>> CompareUserMovies(List<User> selectedUsers)
    {
        var list1 = await movieService.GetUserMovies(selectedUsers[0]);
        foreach (var t in selectedUsers)
        {
            var nextMovies = await movieService.GetUserMovies(t);
            var sharedMoviesIds = list1.Select(m => m.imdbID).Intersect(nextMovies.Select(m => m.imdbID).ToList())
                .ToList();
            var sharedMovies = list1.Where(m => sharedMoviesIds.Contains(m.imdbID)).ToList();
            list1 = sharedMovies;
        }

        return list1;
    }
}