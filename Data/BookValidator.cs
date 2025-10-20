using WK_34.Data.Interfaces;

namespace WK_34.Data
{
    public class BookValidator : IBookValidator
    {
        public void ValidateId(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Id cannot be less than 0.");
            }
        }

        public void ValidateTitle(string title)
        {

            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Title must contain at least 1 character.");
            }

            if (title.Length > 200)
            {
                throw new ArgumentException("Title could not be more than 100 characters.");
            }
        }

        public void ValidatePublishedYear(DateTime publishedYear)
        {
            if (publishedYear > DateTime.Now)
            {
                throw new ArgumentException("Published year must be in the past..");
            }
        }

        public void ValidateInterval(DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
                throw new ArgumentException("Start date cannot be greater than end date.");
        }

    }
}
