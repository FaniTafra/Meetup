namespace KingMeetup.Model.IRepository
{
    public interface IAuthRepository
    {
        Task<User?> FindByEmailAndPassword(string email, string password, CancellationToken cancellationToken);
        Task Add(User user, CancellationToken cancellationToken);
        Task<User?> FindByEmail(string email, CancellationToken cancellationToken);
    }
}
