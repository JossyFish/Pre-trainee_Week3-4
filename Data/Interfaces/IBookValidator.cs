namespace WK_34.Data.Interfaces
{
    public interface IBookValidator
    {
        void ValidateId(int id);
        void ValidateTitle(string title);
        void ValidatePublishedYear(DateTime publishedYear);
        void ValidateInterval(DateTime startDate, DateTime endDate);
    }
}