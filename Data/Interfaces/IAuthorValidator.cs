namespace WK_34.Data.Interfaces
{
    public interface IAuthorValidator
    {
        void ValidateId(int id);
        void ValidateName(string name);
        void ValidateDateOfBirth(DateTime dateOfBirth);
    }
}