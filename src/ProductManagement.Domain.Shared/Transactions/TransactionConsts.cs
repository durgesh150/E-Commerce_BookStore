namespace ProductManagement.Transactions
{
    public static class TransactionConsts
    {
        private const string DefaultSorting = "{0}UserId asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Transaction." : string.Empty);
        }

    }
}