using FilmTracker.Data;
using FilmTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmTracker.Services;

public interface IUserService
{
    Task<int> AddUser(string name, int groupId);
    Task<List<User>> GetAllUsersInGroup(int groupId);
    Task<List<User>> GetAllUsers();
}
public class UserService : IUserService
{
    private readonly DataContext _dataContext;

    public UserService(DataContext dataContext)
    {
        this._dataContext = dataContext;
    }

    public async Task<int> AddUser(string name, int groupId)
    {
        _dataContext.UserTable.Add(new User(name, groupId) { Movies = new List<Movie>() });
        await _dataContext.SaveChangesAsync();
        var userAdded = _dataContext.UserTable.First(u => u.Name == name && u.GroupId == groupId);
        return userAdded.UserId;
    }

    public async Task<List<User>> GetAllUsersInGroup(int groupId)
    {
        return await _dataContext.UserTable.Where(u => u.GroupId == groupId).ToListAsync();
    }
    
    public async Task<List<User>> GetAllUsers()
    {
        return await _dataContext.UserTable.ToListAsync();
    }
}