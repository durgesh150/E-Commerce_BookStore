namespace ProductManagement.TransactionDetails
{
    public static class TransactionDetailConsts
    {
        private const string DefaultSorting = "{0}Quantity asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "TransactionDetail." : string.Empty);
        }

    }
}