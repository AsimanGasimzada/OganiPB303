using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace Ogani.BLL.Services;

public class CloudinaryManager 
{
    private readonly IConfiguration _configuration;
    private readonly CloudinaryOptionsDto _optionsDto;
    private readonly Cloudinary _cloudinary = null!;

    public CloudinaryManager(IConfiguration configuration)
    {
        _configuration = configuration;
        _optionsDto = _configuration.GetSection("CloudinarySettings").Get<CloudinaryOptionsDto>() ?? new();

        var myAccount = new Account { ApiKey = _optionsDto.APIKey, ApiSecret = _optionsDto.APISecret, Cloud = _optionsDto.CloudName };

        _cloudinary = new Cloudinary(myAccount);
        _cloudinary.Api.Secure = true;
    }

    public async Task<string> FileCreateAsync(IFormFile file)
    {
        string fileName = string.Concat(Guid.NewGuid(), file.FileName.Substring(file.FileName.LastIndexOf('.')));

        var uploadResult = new ImageUploadResult();
        if (file.Length > 0)
        {
            using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(fileName, stream),
            
            };
            uploadResult = await _cloudinary.UploadAsync(uploadParams);
        }
        string url = uploadResult.SecureUrl.ToString();

        return url;
    }

    public async Task<bool> FileDeleteAsync(string filePath)
    {
        try
        {
            string publicIdWithExtension = filePath.Substring(filePath.LastIndexOf("connex.az"));
            string publicId = publicIdWithExtension.Substring(0, publicIdWithExtension.LastIndexOf('.'));

            var deleteParams = new DelResParams()
            {
                PublicIds = new List<string> { publicId },
                Type = "upload",
                ResourceType = ResourceType.Image
            };
            var result = await _cloudinary.DeleteResourcesAsync(deleteParams);

            return result.StatusCode == HttpStatusCode.OK;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }
    public class CloudinaryOptionsDto
    {
        public string APIKey { get; set; } = null!;
        public string APISecret { get; set; } = null!;
        public string CloudName { get; set; } = null!;
    }

}

