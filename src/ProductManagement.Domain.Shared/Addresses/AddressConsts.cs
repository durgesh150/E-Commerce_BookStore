namespace ProductManagement.Addresses
{
    public static class AddressConsts
    {
        private const string DefaultSorting = "{0}CIty asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Address." : string.Empty);
        }

    }
}