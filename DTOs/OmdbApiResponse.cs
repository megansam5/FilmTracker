using FilmTracker.Models;

namespace FilmTracker.DTOs;

public class OmdbApiResponse
{
    public List<MovieApi> Search { get; set; }

    public OmdbApiResponse(List<MovieApi> search)
    {
        Search = search;
    }
}