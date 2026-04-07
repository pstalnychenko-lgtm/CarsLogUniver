namespace CarsLogWorkigVS.Interfaces
{
    public interface IHasLogin
    {
        string Login { get; }
        void ChangeLogin(string newLogin);
    }
}