﻿namespace E_Commerce_MVC.Models.EntitiesViewModel
{
	public class Category
	{
		public int? CategoryId { get; set; }
		public string? CategoryName { get; set; }
		public string? CategoryDescription { get; set; }
		public bool b_deleted { get; set; }
	}
}