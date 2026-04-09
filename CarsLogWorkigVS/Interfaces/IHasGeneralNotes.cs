namespace CarsLogWorkigVS.Interfaces
{
    public interface IHasGeneralNotes
    {
        string GeneralNotes { get; set; }
        void ChangeGeneralNotes(string newNotes);
    }
}
