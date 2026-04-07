namespace CarsLogWorkig.Models
{
    public interface IHasPasswordHash
    {
        string PasswordHash { get; }
        void ChangePassword(System.Guid requestingUserId, string newPassword);
    }
}