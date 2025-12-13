// Certifique-se de que o pacote NuGet "AWSSDK.S3" e "Microsoft.Extensions.Configuration" estejam instalados no projeto.
using Amazon.S3;
using Amazon.S3.Model;
using GestaoPedidos.Application.Interfaces.Storage;
using Microsoft.Extensions.Configuration;

namespace GestaoPedidos.Infrastructure.Storage
{
    public class S3ProductImageStorageService : IProductImageStorageService
    {
        private readonly IAmazonS3 _s3Client;
        private readonly string _bucketName;

        public S3ProductImageStorageService(
            IAmazonS3 s3Client,
            IConfiguration configuration)
        {
            _s3Client = s3Client;
            _bucketName = configuration["AWS:S3:ProductImagesBucket"]
                ?? throw new ArgumentNullException("Bucket S3 não configurado.");
        }

        public async Task<string> UploadAsync(
            Stream fileStream,
            string fileName,
            string contentType,
            CancellationToken cancellationToken)
        {
            var key = $"{Guid.NewGuid()}_{fileName}";

            var request = new PutObjectRequest
            {
                BucketName = _bucketName,
                Key = key,
                InputStream = fileStream,
                ContentType = contentType
            };

            await _s3Client.PutObjectAsync(request, cancellationToken);

            return key;
        }

        public Task<string> UploadAsync(object fotoStream, object fotoNome, object value, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}