using FilmTracker.Data;
using Microsoft.EntityFrameworkCore;

namespace FilmTracker.Services;

public interface IGroupService
{
    Task<int> GenerateNewCode();
    Task<bool> CheckGroupIdIsValid(int groupId);
}
public class GroupService : IGroupService
{
    private readonly DataContext _dataContext;

    public GroupService(DataContext dataContext)
    {
        this._dataContext = dataContext;
    }

    public async Task<int> GenerateNewCode()
    {
        var listOfGroupIds = await _dataContext.UserTable.Select(u => u.GroupId).Distinct().ToListAsync();
        Random rnd = new Random();
        int num;
        do
        {
            num = rnd.Next(1000, 9999);
        } while (listOfGroupIds.Contains(num));

        return num;
    }

    public async Task<bool> CheckGroupIdIsValid(int groupId)
    {
        var listOfGroupIds = await _dataContext.UserTable.Select(u => u.GroupId).Distinct().ToListAsync();
        return listOfGroupIds.Contains(groupId);
    }
}