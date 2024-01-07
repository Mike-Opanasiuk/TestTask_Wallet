using Firebase.Storage;

namespace Wallet.Shared.Services;

public interface IImageService
{
    Task<string> LoadImageAsync(string folder, string url);
}

public class ImageService : IImageService
{
    public async Task<string> LoadImageAsync(string folder, string url)
    {
        var httpClient = new HttpClient();

        byte[] imageBytes = await httpClient.GetByteArrayAsync(url);

        using MemoryStream stream = new MemoryStream(imageBytes);

        return await new FirebaseStorage(AppConstant.FireBase.Url)
         .Child(folder + Guid.NewGuid() + Path.GetExtension(url))
         .PutAsync(stream);
    }
}