using E_Commerce_MVC.Models.EntitiesViewModel;

namespace E_Commerce_MVC.Models
{
    public class HomeViewModel
    {
        public List<ProductViewModel>? Products { get; set; }
        public List<Category>? Categories { get; set; }
        public List<BrandViewModel>? Brands { get; set; }
        public List<SubCategory>? SubCategories { get; set; }
        public List<StoreViewModel>? Stores { get; set; }
    }
}