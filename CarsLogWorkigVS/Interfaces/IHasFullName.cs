namespace CarsLogWorkigVS.Interfaces
{
    public interface IHasFullName
    {
        string FirstName { get; }
        string LastName { get; }
        string FullName { get; }
        void ChangeFirstName(string newFirstName);
        void ChangeLastName(string newLastName);
    }
}
