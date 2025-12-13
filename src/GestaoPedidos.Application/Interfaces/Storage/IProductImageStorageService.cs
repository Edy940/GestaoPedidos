using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace GestaoPedidos.Application.Interfaces.Storage
{
    public interface IProductImageStorageService
    {
        Task<string> UploadAsync(
            Stream fileStream,
            string fileName,
            string contentType,
            CancellationToken cancellationToken);
        Task<string> UploadAsync(object fotoStream, object fotoNome, object value, CancellationToken cancellationToken);
    }
}
