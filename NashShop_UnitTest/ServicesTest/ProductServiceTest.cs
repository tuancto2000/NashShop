using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using NashShop_BackendApi.Data.EF;
using NashShop_BackendApi.Data.Entities;
using NashShop_BackendApi.Services;
using NashShop_ViewModel.ProductImages;
using NashShop_ViewModel.Products;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
namespace NashShop_UnitTest.ServicesTest
{
    public class ProductServiceTest
    {
        private readonly ProductService _service;

        public ProductServiceTest()
        {
            var options = new DbContextOptionsBuilder<NashShopDbContext>()
             .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
             .Options;
            using (var context = new NashShopDbContext(options))
            {
                context.Products.Add(new Product
                {
                    Id = 10,
                    Name = "Apple",
                    CategotyId = 1,
                    Price = 10,
                    Description = " Lorem ipsum dolor sit amet",

                });
                context.Users.Add(new User
                {
                    Id = new Guid("08336707-26CD-4E90-E38B-08D994A9A517"),
                    FirstName = "xuan",
                    LastName = "tuan",
                    UserName = "xuantuan",
                });
                context.Categories.Add(new Category
                {
                    Id = 1,
                    Name = "Fresh Fruit",
                    Description = "Lorem ipsum dolor sit amet",
                    ImagePath = "/user-content/cat-1.jpg"
                });
                context.ProductImages.AddRange(
                    new ProductImage
                    {
                        Id = 1,
                        ProductId = 10,
                        ImagePath = "/user-content/cat-1.jpg",
                        Caption = "Lorem ipsum dolor sit amet",
                        IsDefault = true,
                    },
                     new ProductImage
                     {
                         Id = 2,
                         ProductId = 10,
                         ImagePath = "/user-content/cat-1.jpg",
                         Caption = "Lorem ipsum dolor sit amet",
                         IsDefault = false,
                     });
                context.Ratings.Add(new Rating
                {
                    Id = 1,
                    UserId = new Guid("08336707-26CD-4E90-E38B-08D994A9A517"),
                    ProductId = 10,
                    Stars = 3,
                });

                context.SaveChanges();

            }
            var mockContext = new NashShopDbContext(options);
            var fileService = FileStorageService.IStorageService();
            _service = new ProductService(mockContext, fileService);
        }
        [Fact]
        public async Task GetProductWithIdEqual10_ReturnAProductVM()
        {
            //Arrange
            int productId = 10;
            // Act 
            var result = await _service.GetById(productId);
            // Assert
            Assert.IsType<ProductVM>(result);
            Assert.Equal(productId, result.Id);
            Assert.Equal(2, result.Images.Count);
        }
        [Fact]
        public async Task AddImageFullProperty_ReturnNewImageId_CheckByGetImageById()
        {
            //Arrange
            var image = MockImage();
            var productId = 10;
            var request = new ProductImageCreateRequest()
            {
                Caption = "Lorem ipsum dolor sit amet",
                Image = image
            };
            // Act 
            var newImageId = await _service.AddImage(productId, request);
            var newImage = await _service.GetImageById(newImageId);

            // Assert
            Assert.IsType<int>(newImageId);
            Assert.IsType<ProductImageVM>(newImage);
            Assert.Equal(newImageId, newImage.Id);
            Assert.Equal(productId, newImage.ProductId);
            Assert.Equal(newImageId, newImage.Id);
            Assert.Contains("user-content", newImage.ImagePath);
        }
        [Fact]
        public async Task AddImageWithoutImage_ReturnNewImageId_CheckByGetImageById()
        {
            //Arrange
            var productId = 10;
            var request = new ProductImageCreateRequest()
            {
                Caption = "Lorem ipsum dolor sit amet",
            };
            // Act 
            var newImageId = await _service.AddImage(productId, request);
            var newImage = await _service.GetImageById(newImageId);

            // Assert
            Assert.IsType<int>(newImageId);
            Assert.IsType<ProductImageVM>(newImage);
            Assert.Equal(newImageId, newImage.Id);
            Assert.Equal(productId, newImage.ProductId);
            Assert.Equal(newImageId, newImage.Id);
            Assert.Null(newImage.ImagePath);
        }
        [Fact]
        public async Task CreateProductThenCheckMainImageIsCreated()
        {
            //Arrange
            var image = MockImage();
            var request = new ProductCreateRequest()
            {
                Description = "Lorem ipsum dolor sit amet",
                Image = image,
                Price = 10,
                Name = "New Product",
                CategoryId = 1
            };
            // Act 
            var newProductId = await _service.Create(request);
            var newProduct = await _service.GetById(newProductId);

            // Assert
            Assert.IsType<int>(newProductId);
            Assert.IsType<ProductVM>(newProduct);
            Assert.Equal(newProductId, newProduct.Id);
            Assert.Equal(request.Price, newProduct.Price);
            Assert.Contains("user-content", newProduct.ImagePath);
            Assert.Single(newProduct.Images);
        }
        [Fact]
        public async Task CreateProductWithNotExistCategory_ReturnExeption()
        {
            //Arrange
            var image = MockImage();
            var request = new ProductCreateRequest()
            {
                Description = "Lorem ipsum dolor sit amet",
                Image = image,
                Price = 10,
                Name = "New Product",
                CategoryId = 10
            };
            // Act 
            Func<Task> act = () => _service.Create(request);
            // Assert
            var exception = await Assert.ThrowsAsync<Exception>(act);
            Assert.Contains("Cannot find a category with id", exception.Message);

        }
        [Fact]
        public async Task UpdateProductId10ThenCompareBetweenProductUpdatedAndRequest()
        {
            //Arrange
            var image = MockImage();
            var request = new ProductUpdateRequest()
            {
                Id = 10,
                Description = "Lorem ipsum dolor sit amet",
                Image = image,
                Price = 10,
                Name = "New Product",
                CategoryId = 1
            };
            // Act 
            var result = await _service.Update(request);
            var productAfterAddRating = await _service.GetById(request.Id);

            // Assert
            Assert.IsType<int>(result);
            Assert.IsType<ProductVM>(productAfterAddRating);
            Assert.Equal(productAfterAddRating.Id, request.Id);
            Assert.Equal(productAfterAddRating.Name, request.Name);
            Assert.Equal(productAfterAddRating.Price, request.Price);
            Assert.Equal(productAfterAddRating.Description, request.Description);
            Assert.Equal(productAfterAddRating.ProductCategory.Id, request.CategoryId);
            Assert.Contains("user-content", productAfterAddRating.ImagePath);
        }
        [Fact]
        public async Task AddRatingWithNotExist_ReturnExeption()
        {
            //Arrange
            var request = new ProductRatingRequest()
            {
                UserId = new Guid("08336707-26CD-4E90-E38B-08D994A9A517"),
                ProductId = 15,
                Stars = 4,
            };
            // Act 
            Func<Task> act = () => _service.AddRating(request);
            // Assert
            var exception = await Assert.ThrowsAsync<Exception>(act);
            Assert.Contains("Cannot find a product with id", exception.Message);
        }
        [Fact]
        public async Task AddRatingWithStarIsNotBetweenOneAndFive_ReturnFalse()
        {
            //Arrange
            var request = new ProductRatingRequest()
            {
                UserId = new Guid("08336707-26CD-4E90-E38B-08D994A9A517"),
                ProductId = 10,
                Stars = 8,
            };
            // Act 
            bool result = await _service.AddRating(request);
            // Assert
            Assert.False(result);
        }
        [Fact]
        public async Task AddRatingThenCompareRatingCountOfProductBetweenBeforeAndAfter()
        {
            //Arrange
            var request = new ProductRatingRequest()
            {
                UserId = new Guid("08336707-26CD-4E90-E38B-08D994A9A517"),
                ProductId = 10,
                Stars = 4,
            };
            // Act 
            var productBeforeAddRating = await _service.GetById(request.ProductId);
            await _service.AddRating(request);
            var productAfterAddRating = await _service.GetById(request.ProductId);
            // Assert
            Assert.Equal(productBeforeAddRating.RatingCount + 1, productAfterAddRating.RatingCount);
        }

        [Fact]
        public async Task DeleteProductSuccessThenCheckDeletedProductIsExist()
        {
            // Arrange
            int productId = 10;
            // Act
            var result = await _service.Delete(productId);
            Func<Task> act = () => _service.GetById(productId);
            // Assert
            var exception = await Assert.ThrowsAsync<Exception>(act);
            Assert.True(result > 0);
            Assert.Contains("Cannot find a product with id", exception.Message);

        }
        [Fact]
        public async Task DeleteProductSuccessThenCheckRelatedProductImages()
        {
            // Arrange
            int productId = 10;
            // Act
            var result = await _service.Delete(productId);
            var images = await _service.GetProductImages(productId);
            // Assert
            Assert.True(result > 0);
            Assert.IsType<List<ProductImageVM>>(images);
            Assert.Empty(images);
        }
        public IFormFile MockImage()
        {
            var file = new Mock<IFormFile>();
            var content = "source image path for unitTest";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            var fileName = "test.png";
            file.Setup(f => f.OpenReadStream()).Returns(ms);
            file.Setup(f => f.FileName).Returns(fileName).Verifiable();
            file.Setup(_ => _.ContentDisposition)
                .Returns($"form-data;name='file';filename ='{fileName}'");
            file.Setup(f => f.Length).Returns(ms.Length);
            return file.Object;
        }
    }
}
