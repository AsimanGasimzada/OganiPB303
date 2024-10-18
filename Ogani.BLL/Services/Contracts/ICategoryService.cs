using Ogani.BLL.ViewModels.CategoryViewModels;
using Ogani.DAL.DataContext.Entities;

namespace Ogani.BLL.Services.Contracts;

public interface ICategoryService : ICrudService<Category,CategoryViewModel,CategoryCreateViewModel,CategoryUpdateViewModel>
{
    Task<CategoryUpdateViewModel> GetUpdatedCategoryAsync(int id);
}
