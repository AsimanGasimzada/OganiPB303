using AutoMapper;
using Microsoft.EntityFrameworkCore.Query;
using Ogani.BLL.Constants;
using Ogani.BLL.Exceptions;
using Ogani.BLL.Extentions;
using Ogani.BLL.Services.Contracts;
using Ogani.BLL.ViewModels.CategoryViewModels;
using Ogani.DAL.DataContext.Entities;
using Ogani.DAL.Repositories.Contracts;
using System.Linq.Expressions;

namespace Ogani.BLL.Services
{
    public class CategoryManager : CrudManager<Category, CategoryViewModel, CategoryCreateViewModel, CategoryUpdateViewModel>, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly CloudinaryManager _cloudinaryManager;
        public CategoryManager(IRepository<Category> repository, IMapper mapper, ICategoryRepository categoryRepository, CloudinaryManager cloudinaryManager) : base(repository, mapper)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
            _cloudinaryManager = cloudinaryManager;
        }


        public override async Task<CategoryViewModel> CreateAsync(CategoryCreateViewModel createViewModel)
        {
            if (!createViewModel.ImageFile.IsImage())
            {
                throw new Exception("Not an Image");
            }
            if (!createViewModel.ImageFile.AllowedSize(2))
            {
                throw new Exception("Invalid image size");
            }

            //var imageName = await createViewModel.ImageFile.GenerateFile(FilePathConstants.CategoryImagePath);

            var imageName = await _cloudinaryManager.FileCreateAsync(createViewModel.ImageFile);
            createViewModel.ImageUrl = imageName;

            return await base.CreateAsync(createViewModel);
        }


        public override async Task<CategoryViewModel> DeleteAsync(int id)
        {
            var category = await _categoryRepository.GetAsync(id);

            if (category is null)
                throw new NotFoundException("This category is not found");

            category.IsDeleted = true;

            await _categoryRepository.UpdateAsync(category);

            var vm = _mapper.Map<CategoryViewModel>(category);

            return vm;
        }

        public async Task<CategoryUpdateViewModel> GetUpdatedCategoryAsync(int id)
        {
            var category = await _categoryRepository.GetAsync(id);

            if (category is null)
                throw new NotFoundException($"{id}-this category is not found");

            CategoryUpdateViewModel vm = new() { Name=""} ;
            vm = _mapper.Map(category,vm);

            return vm;
        }

        public override async Task<CategoryViewModel> UpdateAsync(CategoryUpdateViewModel updateViewModel)
        {
            if (!updateViewModel.ImageFile?.IsImage() ?? false)
            {
                throw new InvalidInputException();
            }
            if (!updateViewModel.ImageFile?.AllowedSize(2) ?? false)
            {
                throw new Exception("Invalid image size");
            }


            var existCategory = await _categoryRepository.GetAsync(x=>x.Id==updateViewModel.Id, IsTracking: false);

            if (existCategory is null)
                throw new InvalidInputException();

            if (updateViewModel.ImageFile != null)
            {
                var imageName = await _cloudinaryManager.FileCreateAsync(updateViewModel.ImageFile);
                await _cloudinaryManager.FileDeleteAsync(existCategory.ImageUrl);
                updateViewModel.ImageUrl = imageName;
            }


            return await base.UpdateAsync(updateViewModel);
        }
    }
}
