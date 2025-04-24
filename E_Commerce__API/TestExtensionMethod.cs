using DAL.Entities;

namespace E_Commerce__API
{
    public static class TestExtensionMethod
    {
        public static string Greet_Function(this string name, string message)
        {
            return $"Hello {name},{message}";
        }

        public static string Greet_Function2(this Categories categories)
        {
            return $"Hello {categories.CategoryName},{categories.CategoryDescription}";
        }
    }
}