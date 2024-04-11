namespace KingMeetup.Model.IRepository
{
    public interface IUserRepository
    {
        Task Update(User user, int userId);
        Task<User> GetUser(int userId);
    }
}
