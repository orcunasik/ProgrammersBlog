using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProgrammersBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Data.Concrete.EntityFramework.Mappings
{
    public class ArticleMap : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd();//eklendikçe birbir id alanın artmasını sağlıyor
            builder.Property(a => a.Title).HasMaxLength(100).IsRequired();
            builder.Property(a => a.Content).IsRequired().HasColumnType("NVARCHAR(MAX)");
            builder.Property(a => a.Date).IsRequired();
            builder.Property(a => a.SeoAuthor).IsRequired().HasMaxLength(50);
            builder.Property(a => a.SeoDescription).IsRequired().HasMaxLength(150);
            builder.Property(a => a.SeoTags).IsRequired().HasMaxLength(70);
            builder.Property(a => a.ViewsCount).IsRequired();
            builder.Property(a => a.CommentCount).IsRequired();
            builder.Property(a => a.Thumbnail).IsRequired().HasMaxLength(250);
            builder.Property(a => a.CreatedByName).IsRequired().HasMaxLength(50);
            builder.Property(a => a.ModifiedByName).IsRequired().HasMaxLength(50);
            builder.Property(a => a.CreatedDate).IsRequired();
            builder.Property(a => a.ModifiedDate).IsRequired();
            builder.Property(a => a.IsActive).IsRequired();
            builder.Property(a => a.IsDeleted).IsRequired();
            builder.Property(a => a.Note).HasMaxLength(500);

            builder.HasOne<Category>(a => a.Category).WithMany(c => c.Articles).HasForeignKey(a => a.CategoryId);
            builder.HasOne<User>(a => a.User).WithMany(u => u.Articles).HasForeignKey(a => a.UserId);
            builder.ToTable("Articles");
            //builder.HasData(
            //    new Article
            //    {
            //        Id = 1,
            //        CategoryId = 1,
            //        UserId = 1,
            //        Title = "C# 10.0 ve .Net 6 ile Gelen Yenilikler",
            //        Content = "Lorem Ipsum, dizgi ve baskı endüstrisinde kullanılan mıgır metinlerdir. Lorem Ipsum, adı bilinmeyen bir matbaacının bir hurufat numune kitabı oluşturmak üzere bir yazı galerisini alarak karıştırdığı 1500'lerden beri endüstri standardı sahte metinler olarak kullanılmıştır. Beşyüz yıl boyunca varlığını sürdürmekle kalmamış, aynı zamanda pek değişmeden elektronik dizgiye de sıçramıştır. 1960'larda Lorem Ipsum pasajları da içeren Letraset yapraklarının yayınlanması ile ve yakın zamanda Aldus PageMaker gibi Lorem Ipsum sürümleri içeren masaüstü yayıncılık yazılımları ile popüler olmuştur.",
            //        Date = DateTime.Now,
            //        Thumbnail = "Default.jpg",
            //        SeoDescription = "C# 10.0 ve .Net 6 ile Gelen Yenilikler",
            //        SeoTags = "C#, .Net6, .NetCore",
            //        SeoAuthor = "Orçun Aşık",
            //        ViewsCount = 100,
            //        CommentCount = 1,
            //        IsActive = true,
            //        IsDeleted = false,
            //        CreatedByName = "InitialCreate",
            //        CreatedDate = DateTime.Now,
            //        ModifiedByName = "InitialCreate",
            //        ModifiedDate = DateTime.Now,
            //        Note = "C# 10.0 ve .Net 6 ile Gelen Yenilikler"
            //    },
            //    new Article
            //    {
            //        Id = 2,
            //        CategoryId = 2,
            //        UserId = 1,
            //        Title = "Angular 15 ile Gelen Yenilikler",
            //        Content = "Lorem Ipsum, dizgi ve baskı endüstrisinde kullanılan mıgır metinlerdir. Lorem Ipsum, adı bilinmeyen bir matbaacının bir hurufat numune kitabı oluşturmak üzere bir yazı galerisini alarak karıştırdığı 1500'lerden beri endüstri standardı sahte metinler olarak kullanılmıştır. Beşyüz yıl boyunca varlığını sürdürmekle kalmamış, aynı zamanda pek değişmeden elektronik dizgiye de sıçramıştır. 1960'larda Lorem Ipsum pasajları da içeren Letraset yapraklarının yayınlanması ile ve yakın zamanda Aldus PageMaker gibi Lorem Ipsum sürümleri içeren masaüstü yayıncılık yazılımları ile popüler olmuştur.",
            //        Date = DateTime.Now,
            //        Thumbnail = "Default.jpg",
            //        SeoDescription = "Angular 15 ile Gelen Yenilikler",
            //        SeoTags = "Angular, AngularJS",
            //        SeoAuthor = "Orçun Aşık",
            //        ViewsCount = 150,
            //        CommentCount = 1,
            //        IsActive = true,
            //        IsDeleted = false,
            //        CreatedByName = "InitialCreate",
            //        CreatedDate = DateTime.Now,
            //        ModifiedByName = "InitialCreate",
            //        ModifiedDate = DateTime.Now,
            //        Note = "Angular 15 ile Gelen Yenilikler"
            //    },
            //    new Article
            //    {
            //        Id = 3,
            //        CategoryId = 3,
            //        UserId = 1,
            //        Title = "Javascript Best Practies",
            //        Content = "Lorem Ipsum, dizgi ve baskı endüstrisinde kullanılan mıgır metinlerdir. Lorem Ipsum, adı bilinmeyen bir matbaacının bir hurufat numune kitabı oluşturmak üzere bir yazı galerisini alarak karıştırdığı 1500'lerden beri endüstri standardı sahte metinler olarak kullanılmıştır. Beşyüz yıl boyunca varlığını sürdürmekle kalmamış, aynı zamanda pek değişmeden elektronik dizgiye de sıçramıştır. 1960'larda Lorem Ipsum pasajları da içeren Letraset yapraklarının yayınlanması ile ve yakın zamanda Aldus PageMaker gibi Lorem Ipsum sürümleri içeren masaüstü yayıncılık yazılımları ile popüler olmuştur.",
            //        Date = DateTime.Now,
            //        Thumbnail = "Default.jpg",
            //        SeoDescription = "Javascript Best Practies",
            //        SeoTags = "Javascript, Best Practies",
            //        SeoAuthor = "Orçun Aşık",
            //        ViewsCount = 250,
            //        CommentCount = 1,
            //        IsActive = true,
            //        IsDeleted = false,
            //        CreatedByName = "InitialCreate",
            //        CreatedDate = DateTime.Now,
            //        ModifiedByName = "InitialCreate",
            //        ModifiedDate = DateTime.Now,
            //        Note = "Javascript Best Practies"
            //    }
            //    );
        }
    }
}
