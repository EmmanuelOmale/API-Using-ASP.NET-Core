namespace MyWebApi.Services
{
    public interface IUploadServices
    {
        public Task<Dictionary<string, string>> UploadFileAsync(IFormFile file);
    }
}
