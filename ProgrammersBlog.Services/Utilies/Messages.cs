using ProgrammersBlog.Entities.Dtos;

namespace ProgrammersBlog.Services.Utilies;

public static class Messages
{
    public static class Category
    {
        public static string NotFound(bool isPlural)
        {
            if (isPlural) 
                return "Hiçbir Kategori Bulunamadı!";
            return "Böyle Bir Kategori Bulunamadı!";
        }
        public static string Add(string categoryName)
        {
            return $"{categoryName} adlı kategori başarı ile eklenmiştir";
        }
        public static string Update(string categoryName)
        {
            return $"{categoryName} adlı kategori başarı ile güncellenmiştir";
        }
        public static string Delete(string categoryName)
        {
            return $"{categoryName} adlı kategori başarı ile silinmiştir";
        }
        public static string HardDelete(string categoryName)
        {
            return $"{categoryName} adlı kategori başarı ile veri tabanından silinmiştir";
        }
    }

    public static class Article
    {
        public static string NotFound(bool isPlural)
        {
            if (isPlural)
                return "Hiçbir Makale Bulunamadı!";
            return "Böyle Bir Makale Bulunamadı!";
        }
        public static string Add(string articleTitle)
        {
            return $"{articleTitle} başlıklı makale başarı ile eklenmiştir";
        }
        public static string Update(string articleTitle)
        {
            return $"{articleTitle} başlıklı makale başarı ile güncellenmiştir";
        }
        public static string Delete(string articleTitle)
        {
            return $"{articleTitle} başlıklı makale başarı ile silinmiştir";
        }
        public static string HardDelete(string articleTitle)
        {
            return $"{articleTitle} başlıklı makale başarı ile veri tabanından silinmiştir";
        }
    }
}