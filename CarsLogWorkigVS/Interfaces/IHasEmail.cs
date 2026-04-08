namespace CarsLogWorkigVS.Interfaces
{
    public interface IHasEmail
    {
        string Email { get; }
        void ChangeEmail(string newEmail);
    }
}
