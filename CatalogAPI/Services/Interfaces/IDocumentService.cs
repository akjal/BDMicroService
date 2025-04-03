namespace CatalogAPI.Services.Interfaces;

    public interface IDocumentService

    {
        Task UploadSingleDocumentAsync(Guid applicationId, IFormFile file, string documentType);

        //Task<byte[]> DownloadDocumentAsync(string fileName);
    }

