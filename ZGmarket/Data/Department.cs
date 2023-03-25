using NuGet.Protocol;

namespace ZGmarket.Data
{
    public static class Department
    {
        public static string unknown = "Прочее";
        public static string green = "Фрукты-овощи";
        public static string bread = "Хлебная продукция";
        public static string meatFish = "Мясо\\Рыба";
        public static string confectionery = "Кондитерские изделия";
        public static string grocery = "Бакалея";
        public static string milky = "Молочная продукция";
        public static string juiceWater = "Соки\\Вода";
        public static string alcohol = "Алкоголь";
        public static string animals = "Товары для животных";

        public static readonly List<string> DepartList = new List<string>()
        {
            unknown,
            green,
            bread,
            meatFish,
            confectionery,
            grocery,
            milky,
            juiceWater,
            alcohol,
            animals
        };
    }
}
