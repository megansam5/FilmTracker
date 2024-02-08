namespace FilmTracker.Models;

public class Movie
{
    public string Title { get; set; }
    public string Poster { get; set; }
    public string imdbID { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    
}