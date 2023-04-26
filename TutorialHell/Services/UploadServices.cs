using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace MyWebApi.Services
{
    public class UploadServices : IUploadServices
    {
        private readonly IConfiguration _configuration;
        public UploadServices(IConfiguration config)
        {
            _configuration = config;
        }
        public async Task<Dictionary<string, string>> UploadFileAsync(IFormFile file)
        {
            var account = new Account
            {
                Cloud = _configuration.GetSection("Cloudinary:CloudName").Value,
                ApiKey = _configuration.GetSection("Cloudinary:ApiKey").Value,
                ApiSecret = _configuration.GetSection("Cloudinary:ApiSecret").Value
               
            };

            var cloudinary = new Cloudinary(account);
          //  var cloudinary = new Cloudinary(new Account("dmogyjirt", "913785254395814", "xrMk1bjn86yMkITs3J8d_Tep-wo"));

            if (file.Length > 0 && file.Length <= (1024 * 1024 * 2))
            {
                if (file.ContentType.Equals("image/png") || file.ContentType.Equals("image/jpeg") || file.ContentType.Equals("image/jpg"))
                {
                    var uploadResult = new ImageUploadResult();
                    using(var stream = file.OpenReadStream())
                    {
                        var uploadParameteres = new ImageUploadParams
                        {
                            File = new FileDescription(file.FileName, stream),
                            Transformation = new Transformation().Width(300).Height(300).Crop("fill").Gravity("Face")
                        };
                        uploadResult = await cloudinary.UploadAsync(uploadParameteres);
                    }
                    var result = new Dictionary<string, string>();
                    result.Add("PublicId", uploadResult.PublicId);
                    result.Add("Url", uploadResult.Url.ToString());
                    return result;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }

        }
    }
}
