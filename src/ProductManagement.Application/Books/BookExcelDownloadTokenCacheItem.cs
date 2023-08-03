using System;

namespace ProductManagement.Books;

[Serializable]
public class BookExcelDownloadTokenCacheItem
{
    public string Token { get; set; }
}