namespace ProductManagement.Carts
{
    public static class CartConsts
    {
        private const string DefaultSorting = "{0}UserId asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Cart." : string.Empty);
        }

    }
}