using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace The_fifth_group_FinalProject.Models
{
    public partial class TheFifthGroupOfTopicsContext : DbContext
    {
        public TheFifthGroupOfTopicsContext()
        {
        }

        public TheFifthGroupOfTopicsContext(DbContextOptions<TheFifthGroupOfTopicsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AutoReply> AutoReplies { get; set; } = null!;
        public virtual DbSet<AutoReplyKeyWord> AutoReplyKeyWords { get; set; } = null!;
        public virtual DbSet<Brand> Brands { get; set; } = null!;
        public virtual DbSet<CartItem> CartItems { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<ChatContent> ChatContents { get; set; } = null!;
        public virtual DbSet<ChatRoom> ChatRooms { get; set; } = null!;
        public virtual DbSet<ChatRoomAutoReply> ChatRoomAutoReplies { get; set; } = null!;
        public virtual DbSet<Color> Colors { get; set; } = null!;
        public virtual DbSet<CommonAnswer> CommonAnswers { get; set; } = null!;
        public virtual DbSet<CommonQuestion> CommonQuestions { get; set; } = null!;
        public virtual DbSet<Contest> Contests { get; set; } = null!;
        public virtual DbSet<ContestCategory> ContestCategories { get; set; } = null!;
        public virtual DbSet<Coupon> Coupons { get; set; } = null!;
        public virtual DbSet<CustomerFeedback> CustomerFeedbacks { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<ForumSection> ForumSections { get; set; } = null!;
        public virtual DbSet<ForumSectionBranch> ForumSectionBranches { get; set; } = null!;
        public virtual DbSet<ForumSectionBranch1Topic> ForumSectionBranch1Topics { get; set; } = null!;
        public virtual DbSet<ForumSectionBranch1TopicsThread> ForumSectionBranch1TopicsThreads { get; set; } = null!;
        public virtual DbSet<Information> Information { get; set; } = null!;
        public virtual DbSet<Member> Members { get; set; } = null!;
        public virtual DbSet<MembersCoupon> MembersCoupons { get; set; } = null!;
        public virtual DbSet<OnSale> OnSales { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductSize> ProductSizes { get; set; } = null!;
        public virtual DbSet<ProductsImage> ProductsImages { get; set; } = null!;
        public virtual DbSet<QuestionType> QuestionTypes { get; set; } = null!;
        public virtual DbSet<QuickReply> QuickReplies { get; set; } = null!;
        public virtual DbSet<QuickReplyKeyWord> QuickReplyKeyWords { get; set; } = null!;
        public virtual DbSet<Registration> Registrations { get; set; } = null!;
        public virtual DbSet<RegistrationInformation> RegistrationInformations { get; set; } = null!;
        public virtual DbSet<Stock> Stocks { get; set; } = null!;
        public virtual DbSet<Supplier> Suppliers { get; set; } = null!;

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=TheFifthGroupOfTopics;Integrated Security=true");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AutoReply>(entity =>
            {
                entity.Property(e => e.AutoReplyContent).HasMaxLength(1000);
            });

            modelBuilder.Entity<AutoReplyKeyWord>(entity =>
            {
                entity.Property(e => e.AutoReplyId).HasColumnName("AutoReplyID");

                entity.Property(e => e.KeyWord).HasMaxLength(200);

                entity.HasOne(d => d.AutoReply)
                    .WithMany(p => p.AutoReplyKeyWords)
                    .HasForeignKey(d => d.AutoReplyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AutoReplyKeyWords_AutoReplies");
            });

            modelBuilder.Entity<Brand>(entity =>
            {
                entity.Property(e => e.Brand1)
                    .HasMaxLength(50)
                    .HasColumnName("Brand");

                entity.Property(e => e.BrandImageUrl).HasMaxLength(50);

                entity.Property(e => e.BrandIntroduce).HasMaxLength(350);

                entity.Property(e => e.OnSaleId).HasColumnName("OnSale_Id");

                entity.HasOne(d => d.OnSale)
                    .WithMany(p => p.Brands)
                    .HasForeignKey(d => d.OnSaleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Brands_OnSale");
            });

            modelBuilder.Entity<CartItem>(entity =>
            {
                entity.Property(e => e.MemberId).HasColumnName("Member_Id");

                entity.Property(e => e.ProductId).HasColumnName("Product_Id");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.CartItems)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CartItems_Members");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.CartItems)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CartItems_Products");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Category1)
                    .HasMaxLength(50)
                    .HasColumnName("Category");
            });

            modelBuilder.Entity<ChatContent>(entity =>
            {
                entity.Property(e => e.ChatContent1)
                    .HasMaxLength(200)
                    .HasColumnName("ChatContent");

                entity.Property(e => e.SentTime).HasColumnType("datetime");

                entity.HasOne(d => d.ChatRoom)
                    .WithMany(p => p.ChatContents)
                    .HasForeignKey(d => d.ChatRoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ChatContents_ChatRooms");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.ChatContents)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ChatContents_Employees");
            });

            modelBuilder.Entity<ChatRoom>(entity =>
            {
                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.StartTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<ChatRoomAutoReply>(entity =>
            {
                entity.Property(e => e.SentTime).HasColumnType("datetime");

                entity.HasOne(d => d.AutoReply)
                    .WithMany(p => p.ChatRoomAutoReplies)
                    .HasForeignKey(d => d.AutoReplyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ChatRoomAutoReplies_AutoReplies");

                entity.HasOne(d => d.ChatRoom)
                    .WithMany(p => p.ChatRoomAutoReplies)
                    .HasForeignKey(d => d.ChatRoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ChatRoomAutoReplies_ChatRooms");
            });

            modelBuilder.Entity<Color>(entity =>
            {
                entity.Property(e => e.Color1)
                    .HasMaxLength(50)
                    .HasColumnName("Color");
            });

            modelBuilder.Entity<CommonAnswer>(entity =>
            {
                entity.Property(e => e.Answer).HasMaxLength(1000);

                entity.HasOne(d => d.CommonQuestion)
                    .WithMany(p => p.CommonAnswers)
                    .HasForeignKey(d => d.CommonQuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CommonAnswer_CommonQuestion");
            });

            modelBuilder.Entity<CommonQuestion>(entity =>
            {
                entity.Property(e => e.Question).HasMaxLength(1000);

                entity.HasOne(d => d.QuestionType)
                    .WithMany(p => p.CommonQuestions)
                    .HasForeignKey(d => d.QuestionTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CommonQuestions_QuestionTypes");
            });

            modelBuilder.Entity<Contest>(entity =>
            {
                entity.Property(e => e.Area).HasMaxLength(50);

                entity.Property(e => e.ContestDate).HasColumnType("datetime");

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.Location).HasMaxLength(50);

                entity.Property(e => e.MapUrl).HasColumnName("MapURL");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.RegistrationDeadline).HasColumnType("datetime");

                entity.Property(e => e.RegistrationUrl)
                    .HasMaxLength(50)
                    .HasColumnName("RegistrationURL");

                entity.Property(e => e.SupplierId).HasColumnName("SupplierID");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.Contests)
                    .HasForeignKey(d => d.SupplierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Contests_Suppliers");
            });

            modelBuilder.Entity<ContestCategory>(entity =>
            {
                entity.ToTable("Contest_Category");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.ContestId).HasColumnName("ContestID");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.ContestCategories)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Contest_Category_Category");

                entity.HasOne(d => d.Contest)
                    .WithMany(p => p.ContestCategories)
                    .HasForeignKey(d => d.ContestId)
                    .HasConstraintName("FK_Contest_Category_Contest");
            });

            modelBuilder.Entity<Coupon>(entity =>
            {
                entity.Property(e => e.CouponContent).HasMaxLength(500);

                entity.Property(e => e.CouponImage).HasMaxLength(50);

                entity.Property(e => e.CouponName).HasMaxLength(50);

                entity.Property(e => e.CouponNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EndAt).HasColumnType("datetime");

                entity.Property(e => e.IsCombine).HasDefaultValueSql("((0))");

                entity.Property(e => e.SoftDelete).HasDefaultValueSql("((0))");

                entity.Property(e => e.StartAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<CustomerFeedback>(entity =>
            {
                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.CustomerName).HasMaxLength(100);

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.FeedbackContent).HasMaxLength(1000);

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.QuestionType)
                    .WithMany(p => p.CustomerFeedbacks)
                    .HasForeignKey(d => d.QuestionTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerFeedbacks_QuestionTypes");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.Account).HasMaxLength(200);

                entity.Property(e => e.Address).HasMaxLength(60);

                entity.Property(e => e.Email).HasMaxLength(200);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(200);

                entity.Property(e => e.Permission).HasDefaultValueSql("((5))");

                entity.Property(e => e.Title).HasMaxLength(30);
            });

            modelBuilder.Entity<ForumSection>(entity =>
            {
                entity.ToTable("ForumSection");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.SectionName)
                    .HasMaxLength(50)
                    .HasColumnName("sectionName");
            });

            modelBuilder.Entity<ForumSectionBranch>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AdministratorId).HasColumnName("administratorId");

                entity.Property(e => e.BranchName)
                    .HasMaxLength(50)
                    .HasColumnName("branchName");

                entity.Property(e => e.SectionAdministrator)
                    .HasMaxLength(50)
                    .HasColumnName("sectionAdministrator");

                entity.Property(e => e.SectionNameId).HasColumnName("sectionNameId");

                entity.HasOne(d => d.Administrator)
                    .WithMany(p => p.ForumSectionBranches)
                    .HasForeignKey(d => d.AdministratorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ForumSectionBranches_Members");

                entity.HasOne(d => d.SectionName)
                    .WithMany(p => p.ForumSectionBranches)
                    .HasForeignKey(d => d.SectionNameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ForumSectionBranches_ForumSection");
            });

            modelBuilder.Entity<ForumSectionBranch1Topic>(entity =>
            {
                entity.ToTable("Forum_SectionBranch1Topics");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Topic).HasMaxLength(100);

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.ForumSectionBranch1Topics)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Forum_SectionBranch1Topics_ForumSectionBranches");
            });

            modelBuilder.Entity<ForumSectionBranch1TopicsThread>(entity =>
            {
                entity.ToTable("Forum_SectionBranch1TopicsThread");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ReplyContent)
                    .HasMaxLength(500)
                    .HasColumnName("replyContent");

                entity.Property(e => e.ReplyMemberId).HasColumnName("replyMemberId");

                entity.Property(e => e.ReplyNumber).HasColumnName("replyNumber");

                entity.Property(e => e.ReplyState).HasColumnName("replyState");

                entity.Property(e => e.ReplyTime)
                    .HasColumnType("datetime")
                    .HasColumnName("replyTime");

                entity.Property(e => e.TopicId).HasColumnName("topicId");

                entity.Property(e => e.TopicState).HasColumnName("topicState");

                entity.HasOne(d => d.ReplyMember)
                    .WithMany(p => p.ForumSectionBranch1TopicsThreads)
                    .HasForeignKey(d => d.ReplyMemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Forum_SectionBranch1TopicsThread_Members");

                entity.HasOne(d => d.Topic)
                    .WithMany(p => p.ForumSectionBranch1TopicsThreads)
                    .HasForeignKey(d => d.TopicId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Forum_SectionBranch1TopicsThread_Forum_SectionBranch1Topics");
            });

            modelBuilder.Entity<Information>(entity =>
            {
                entity.Property(e => e.Address).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.Property(e => e.MemberId).HasColumnName("Member_Id");

                entity.Property(e => e.Account)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Mail)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<MembersCoupon>(entity =>
            {
                entity.HasKey(e => new { e.MemberId, e.CouponId });

                entity.ToTable("Members_Coupons");

                entity.HasOne(d => d.Coupon)
                    .WithMany(p => p.MembersCoupons)
                    .HasForeignKey(d => d.CouponId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Members_Coupons_Coupons");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.MembersCoupons)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Members_Coupons_Members");
            });

            modelBuilder.Entity<OnSale>(entity =>
            {
                entity.ToTable("OnSale");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.OrderAddress).HasMaxLength(200);

                entity.Property(e => e.OrderContent).HasMaxLength(500);

                entity.Property(e => e.OrderNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ShippingMethod).HasMaxLength(50);

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_Members");

                entity.HasOne(d => d.UseCouponNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UseCoupon)
                    .HasConstraintName("FK_Orders_Coupons");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.BrandId).HasColumnName("Brand_Id");

                entity.Property(e => e.ColorId).HasColumnName("Color_Id");

                entity.Property(e => e.ImageUrl).HasMaxLength(300);

                entity.Property(e => e.ProductIntroduce).HasMaxLength(500);

                entity.Property(e => e.ProductName).HasMaxLength(50);

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.BrandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Products_Brands");

                entity.HasOne(d => d.Color)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ColorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Products_Colors");
            });

            modelBuilder.Entity<ProductSize>(entity =>
            {
                entity.ToTable("ProductSize");

                entity.Property(e => e.Size).HasMaxLength(50);
            });

            modelBuilder.Entity<ProductsImage>(entity =>
            {
                entity.Property(e => e.ImageUrl).HasMaxLength(300);
            });

            modelBuilder.Entity<QuestionType>(entity =>
            {
                entity.Property(e => e.QuestionType1)
                    .HasMaxLength(256)
                    .HasColumnName("QuestionType");
            });

            modelBuilder.Entity<QuickReply>(entity =>
            {
                entity.Property(e => e.QuickReplyContent).HasMaxLength(1000);
            });

            modelBuilder.Entity<QuickReplyKeyWord>(entity =>
            {
                entity.Property(e => e.KeyWord).HasMaxLength(200);

                entity.Property(e => e.QuickReplyId).HasColumnName("QuickReplyID");

                entity.HasOne(d => d.QuickReply)
                    .WithMany(p => p.QuickReplyKeyWords)
                    .HasForeignKey(d => d.QuickReplyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QuickReplyKeyWords_QuickReplies");
            });

            modelBuilder.Entity<Registration>(entity =>
            {
                entity.ToTable("Registration");

                entity.Property(e => e.ContestCategoryId).HasColumnName("Contest_CategoryID");

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.HasOne(d => d.ContestCategory)
                    .WithMany(p => p.Registrations)
                    .HasForeignKey(d => d.ContestCategoryId)
                    .HasConstraintName("FK_RegistrationForm_Contest_Category");
            });

            modelBuilder.Entity<RegistrationInformation>(entity =>
            {
                entity.ToTable("Registration_Information");

                entity.Property(e => e.InformationId).HasColumnName("InformationID");

                entity.Property(e => e.RegistrationId).HasColumnName("RegistrationID");

                entity.HasOne(d => d.Information)
                    .WithMany(p => p.RegistrationInformations)
                    .HasForeignKey(d => d.InformationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Registration_Information_Information");

                entity.HasOne(d => d.Registration)
                    .WithMany(p => p.RegistrationInformations)
                    .HasForeignKey(d => d.RegistrationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Registration_Information_Registration");
            });

            modelBuilder.Entity<Stock>(entity =>
            {
                entity.Property(e => e.ProductId).HasColumnName("Product_Id");

                entity.Property(e => e.ProductSizeId).HasColumnName("ProductSize_Id");

                entity.Property(e => e.Stock1).HasColumnName("Stock");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Stocks)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Stocks_Products");

                entity.HasOne(d => d.ProductSize)
                    .WithMany(p => p.Stocks)
                    .HasForeignKey(d => d.ProductSizeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Stocks_ProductSize");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.Property(e => e.SupplierAdd).HasMaxLength(100);

                entity.Property(e => e.SupplierName).HasMaxLength(50);

                entity.Property(e => e.SupplierTel)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
