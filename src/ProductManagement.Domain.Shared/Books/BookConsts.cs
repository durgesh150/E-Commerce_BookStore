namespace ProductManagement.Books
{
    public static class BookConsts
    {
        private const string DefaultSorting = "{0}Title asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Book." : string.Empty);
        }

        public const int TitleMinLength = 3;
    }
}