namespace CarsLogWorkigVS.Interfaces
{
    public interface IDriverLicenseNumberCreate
    {
        void ChangeLicenseNumber(string newLicenseNumber);
        void ChangeLicenseIssuedBy(string newIssuedBy);
    }
}
