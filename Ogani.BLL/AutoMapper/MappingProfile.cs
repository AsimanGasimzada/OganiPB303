using AutoMapper;
using Ogani.BLL.ViewModels.AppUserViewModels;
using Ogani.BLL.ViewModels.CategoryViewModels;
using Ogani.BLL.ViewModels.ProductImageViewModels;
using Ogani.BLL.ViewModels.ProductViewModels;
using Ogani.DAL.DataContext.Entities;

namespace Ogani.BLL.AutoMapper;

public class MappingProfile:Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductViewModel>().ReverseMap();
        CreateMap<Product, ProductCreateViewModel>().ReverseMap();
        CreateMap<Product, ProductUpdateViewModel>().ReverseMap();

        CreateMap<ProductCategory, CategoryViewModel>()
            .ForMember(dest=>dest.Id, opt=>opt.MapFrom(src=>src.CategoryId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Category == null? "" :src.Category.Name))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Category == null? "" :src.Category.ImageUrl))
            .ReverseMap();

        CreateMap<Category, CategoryViewModel>().ReverseMap();
        CreateMap<Category, CategoryCreateViewModel>().ReverseMap();
        CreateMap<Category, CategoryUpdateViewModel>().ReverseMap();

        CreateMap<AppUser,RegisterViewModel>().ReverseMap();
        CreateMap<AppUser,LoginViewModel>().ReverseMap();

        //CreateMap<ProductCategory, ProductCategoryViewModel>().ReverseMap();
        //CreateMap<ProductCategory, ProductCategoryCreateViewModel>().ReverseMap();
        //CreateMap<ProductCategory, CategoryUpdateViewModel>().ReverseMap();

        CreateMap<ProductImage, ProductImageViewModel>().ReverseMap();
        CreateMap<ProductImage, ProductImageCreateViewModel>().ReverseMap();
        CreateMap<ProductImage, ProductImageUpdateViewModel>().ReverseMap();

    }
}
