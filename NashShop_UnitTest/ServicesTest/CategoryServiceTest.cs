using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using NashShop_BackendApi.Data.EF;
using NashShop_BackendApi.Data.Entities;
using NashShop_BackendApi.Services;
using NashShop_ViewModel.Categories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NashShop_UnitTest.ServicesTest
{
    public class CategoryServiceTest
    {
        private readonly CategoryService _service;

        public CategoryServiceTest()
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
                context.Categories.Add(new Category
                {
                    Id = 1,
                    Name = "Fresh Fruit",
                    Description = "Lorem ipsum dolor sit amet",
                    ImagePath = "/user-content/cat-1.jpg"
                });
                
                context.SaveChanges();

            }
            var mockContext = new NashShopDbContext(options);
            var fileService = FileStorageService.IStorageService();
            _service = new CategoryService(mockContext, fileService);
        }
        [Fact]
        public async Task AddCategory_ReturnNewImageId_CheckByGetImageById()
        {
            //Arrange
            var image = MockImage();
            var request = new CategoryCreateRequest()
            {
                Description = "Lorem ipsum dolor sit amet",
                Image = image,
                Name = "New Category"
            };
            // Act 
            var categoryId = await _service.Create(request);
            var newCategory = await _service.GetById(categoryId);

            // Assert
            Assert.IsType<int>(categoryId);
            Assert.IsType<CategoryVM>(newCategory);
            Assert.Equal(request.Description, newCategory.Description);
            Assert.Equal(request.Name, newCategory.Name);
            Assert.Contains("user-content", newCategory.Image);
        }
        [Fact]
        public async Task UpdateCategoryWithFullProperty()
        {
            //Arrange
            var image = MockImage();
            var request = new CategoryUpdateRequest()
            {
                Id = 1,
                Description = "Lorem ipsum dolor sit amet",
                Image = image,
                Name = "Update Category"
            };
            // Act 
            var categoryId = await _service.Update(request);
            var categoryUpdated = await _service.GetById(request.Id);

            // Assert
            Assert.IsType<bool>(categoryId);
            Assert.Equal(request.Description, categoryUpdated.Description);
            Assert.Equal(request.Name, categoryUpdated.Name);
            Assert.Contains("user-content", categoryUpdated.Image);
        }
        [Fact]
        public async Task UpdateCategoryWithoutImage()
        {
            //Arrange
            var request = new CategoryUpdateRequest()
            {
                Id = 1,
                Description = "Lorem ipsum dolor sit amet",
                Name = "Update Category"
            };
            // Act 
            var categoryBeforeUpdated = await _service.GetById(request.Id);
            var categoryId = await _service.Update(request);
            var categoryAfterUpdated = await _service.GetById(request.Id);

            // Assert
            Assert.IsType<bool>(categoryId);
            Assert.Equal(request.Description, categoryAfterUpdated.Description);
            Assert.Equal(request.Name, categoryAfterUpdated.Name);
            Assert.Equal(categoryBeforeUpdated.Image, categoryAfterUpdated.Image);
        }
        [Fact]
        public async Task DeleteCategorySuccessThenCheckDeletedCategoryIsExist()
        {
            // Arrange
            int categoryId = 1;
            // Act
            var result = await _service.Delete(categoryId);
            Func<Task> act = () => _service.GetById(categoryId);
            // Assert
            var exception = await Assert.ThrowsAsync<Exception>(act);
            Assert.True(result);
            Assert.Contains("Cannot find a category with id", exception.Message);

        }
        [Fact]
        public async Task DeleteProductWithNotExistId()
        {
            // Arrange
            int categoryId = 10;
            // Act
            Func<Task> act = () => _service.Delete(categoryId);
            // Assert
            var exception = await Assert.ThrowsAsync<Exception>(act);
            Assert.Contains("Cannot find a category with id", exception.Message);
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
