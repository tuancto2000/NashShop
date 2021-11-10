using NashShop_BackendApi.Services;
using Moq;
using NashShop_BackendApi.Interfaces;

namespace NashShop_UnitTest.ServicesTest
{
    public static class FileStorageService
    {
        public static IFileStorageService IStorageService()
        {
            var fileStorageService = Mock.Of<IFileStorageService>();

            return fileStorageService;
        }
    }
}
