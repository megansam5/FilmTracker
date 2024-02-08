namespace FilmTracker.Models;

public class User
{
    public int UserId { get; set; }
    public string Name { get; set; }
    public int GroupId { get; set; }
    public ICollection<Movie> Movies { get; set; } = null!;

    public User(string name, int groupId)
    {
        Name = name;
        GroupId = groupId;
    }
}