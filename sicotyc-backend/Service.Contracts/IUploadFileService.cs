namespace Service.Contracts
{
    public interface IUploadFileService
    {
        Task<bool> UpdateImageAsync(string type, Guid id, string rootPath, string fileName);
        Task<bool> DeleteImageAsync(string type, string rootPath, string fileName);
    }
}
