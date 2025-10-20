using WK_34.Data.Interfaces;

namespace WK_34.Data
{
    public class AuthorValidator : IAuthorValidator
    {
        public void ValidateId(int id)
        {
            if (id < 0)
            {
                throw new ArgumentException("Id cannot be less than 0.");
            }
        }
        public void ValidateName(string name)
        {
            name = name.Trim();

            if (string.IsNullOrWhiteSpace(name)){
                throw new ArgumentException("Name must contain at least 1 character.");
            }

            if (!name.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)))
                throw new ArgumentException("Name can only contain letters and spaces.");

            if (name.Length > 100)
            {
                throw new ArgumentException("Name could not be more than 100 characters.");
            }
        }

        public void ValidateDateOfBirth(DateTime dateOfBirth)
        {
            if (dateOfBirth > DateTime.Now)
            {
                throw new ArgumentException("Date of birth must be in the past.");
            }
        }

    }
}
