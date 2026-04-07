namespace CarsLogWorkig.Models
{
    public interface IHasFullName
    {
        string FirstName { get; }
        string LastName { get; }
        void ChangeFirstName(string newFirstName);
        void ChangePatronymic(string newPatronymic);

        string FullName { get; }


    }
}