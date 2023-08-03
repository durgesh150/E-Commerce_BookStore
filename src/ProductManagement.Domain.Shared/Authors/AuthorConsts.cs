namespace ProductManagement.Authors
{
    public static class AuthorConsts
    {
        private const string DefaultSorting = "{0}FirstName asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Author." : string.Empty);
        }

        public const int FirstNameMinLength = 2;
    }
}