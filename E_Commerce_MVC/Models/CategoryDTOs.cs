namespace E_Commerce_MVC.Models {
	public class CategoryListDto {
		public int CategoryId { get; set; }
		public string? CategoryName { get; set; }
		public string? CategoryDescription { get; set; }
		public bool b_deleted { get; set; }
	}
}
