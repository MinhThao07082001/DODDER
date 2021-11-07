using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace Dodder.Models
{
    public partial class PRN211Context : DbContext
    {
        public PRN211Context()
        {
        }

        public PRN211Context(DbContextOptions<PRN211Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<AuthenticationCode> AuthenticationCodes { get; set; }
        public virtual DbSet<Bill> Bills { get; set; }
        public virtual DbSet<Conversation> Conversations { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<GenderInterested> GenderInteresteds { get; set; }
        public virtual DbSet<Hobby> Hobbies { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<Relation> Relations { get; set; }
        public virtual DbSet<RelationInterested> RelationInteresteds { get; set; }
        public virtual DbSet<UpgradeService> UpgradeServices { get; set; }
        public virtual DbSet<UserAccount> UserAccounts { get; set; }
        public virtual DbSet<UserBlock> UserBlocks { get; set; }
        public virtual DbSet<UserDislike> UserDislikes { get; set; }
        public virtual DbSet<UserHobby> UserHobbies { get; set; }
        public virtual DbSet<UserLike> UserLikes { get; set; }
        public virtual DbSet<UserPhoto> UserPhotos { get; set; }
        public virtual DbSet<UserReport> UserReports { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot config = builder.Build();
            optionsBuilder.UseSqlServer(config.GetConnectionString("Dodder"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("admin");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .HasColumnName("password");
            });

            modelBuilder.Entity<AuthenticationCode>(entity =>
            {
                entity.ToTable("authentication_code");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Code)
                    .HasMaxLength(10)
                    .HasColumnName("code");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("createTime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("email");
            });

            modelBuilder.Entity<Bill>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("bill");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("createTime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.ServiceMonth).HasColumnName("service_month");

                entity.Property(e => e.UpgradeServiceId).HasColumnName("upgrade_service_id");

                entity.Property(e => e.UserAccountId).HasColumnName("user_account_id");

                entity.HasOne(d => d.UpgradeService)
                    .WithMany()
                    .HasForeignKey(d => d.UpgradeServiceId)
                    .HasConstraintName("FK__bill__upgrade_se__5535A963");

                entity.HasOne(d => d.UserAccount)
                    .WithMany()
                    .HasForeignKey(d => d.UserAccountId)
                    .HasConstraintName("FK__bill__user_accou__5629CD9C");
            });

            modelBuilder.Entity<Conversation>(entity =>
            {
                entity.ToTable("conversation");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserAccountId2).HasColumnName("user_account_id_2");

                entity.Property(e => e.UserAccountIdCreator).HasColumnName("user_account_id_creator");

                entity.HasOne(d => d.UserAccountId2Navigation)
                    .WithMany(p => p.ConversationUserAccountId2Navigations)
                    .HasForeignKey(d => d.UserAccountId2)
                    .HasConstraintName("FK__conversat__user___3F466844");

                entity.HasOne(d => d.UserAccountIdCreatorNavigation)
                    .WithMany(p => p.ConversationUserAccountIdCreatorNavigations)
                    .HasForeignKey(d => d.UserAccountIdCreator)
                    .HasConstraintName("FK__conversat__user___3E52440B");
            });

            modelBuilder.Entity<Gender>(entity =>
            {
                entity.ToTable("gender");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<GenderInterested>(entity =>
            {
                entity.ToTable("gender_interested");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.GenderId).HasColumnName("gender_id");

                entity.Property(e => e.UserAccountId).HasColumnName("user_account_id");

                entity.HasOne(d => d.Gender)
                    .WithMany(p => p.GenderInteresteds)
                    .HasForeignKey(d => d.GenderId)
                    .HasConstraintName("FK__gender_in__gende__22AA2996");

                entity.HasOne(d => d.UserAccount)
                    .WithMany(p => p.GenderInteresteds)
                    .HasForeignKey(d => d.UserAccountId)
                    .HasConstraintName("FK__gender_in__user___21B6055D");
            });

            modelBuilder.Entity<Hobby>(entity =>
            {
                entity.ToTable("hobby");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("message");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Content)
                    .HasMaxLength(1000)
                    .HasColumnName("content");

                entity.Property(e => e.ConversationId).HasColumnName("conversation_id");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("createTime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.UserAccountIdReceiver).HasColumnName("user_account_id_receiver");

                entity.Property(e => e.UserAccountIdSender).HasColumnName("user_account_id_sender");

                entity.HasOne(d => d.Conversation)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.ConversationId)
                    .HasConstraintName("FK__message__convers__44FF419A");

                entity.HasOne(d => d.UserAccountIdReceiverNavigation)
                    .WithMany(p => p.MessageUserAccountIdReceiverNavigations)
                    .HasForeignKey(d => d.UserAccountIdReceiver)
                    .HasConstraintName("FK__message__user_ac__440B1D61");

                entity.HasOne(d => d.UserAccountIdSenderNavigation)
                    .WithMany(p => p.MessageUserAccountIdSenderNavigations)
                    .HasForeignKey(d => d.UserAccountIdSender)
                    .HasConstraintName("FK__message__user_ac__4316F928");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.ToTable("notification");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Content)
                    .HasMaxLength(1000)
                    .HasColumnName("content");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("createTime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.NotiType).HasColumnName("notiType");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.UserAccountId).HasColumnName("user_account_id");

                entity.HasOne(d => d.UserAccount)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.UserAccountId)
                    .HasConstraintName("FK__notificat__user___4AB81AF0");
            });

            modelBuilder.Entity<Relation>(entity =>
            {
                entity.ToTable("relation");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<RelationInterested>(entity =>
            {
                entity.ToTable("relation_interested");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.RelationId).HasColumnName("relation_id");

                entity.Property(e => e.UserAccountId).HasColumnName("user_account_id");

                entity.HasOne(d => d.Relation)
                    .WithMany(p => p.RelationInteresteds)
                    .HasForeignKey(d => d.RelationId)
                    .HasConstraintName("FK__relation___relat__276EDEB3");

                entity.HasOne(d => d.UserAccount)
                    .WithMany(p => p.RelationInteresteds)
                    .HasForeignKey(d => d.UserAccountId)
                    .HasConstraintName("FK__relation___user___267ABA7A");
            });

            modelBuilder.Entity<UpgradeService>(entity =>
            {
                entity.ToTable("upgrade_service");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Details)
                    .HasMaxLength(500)
                    .HasColumnName("details");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Price).HasColumnName("price");
            });

            modelBuilder.Entity<UserAccount>(entity =>
            {
                entity.ToTable("user_account");

                entity.HasIndex(e => e.Email, "UQ__user_acc__AB6E6164EFE0429A")
                    .IsUnique();

                entity.HasIndex(e => e.Phone, "UQ__user_acc__B43B145F33172C9F")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasMaxLength(200)
                    .HasColumnName("address");

                entity.Property(e => e.AgeFrom)
                    .HasColumnName("ageFrom")
                    .HasDefaultValueSql("((18))");

                entity.Property(e => e.AgeTo)
                    .HasColumnName("ageTo")
                    .HasDefaultValueSql("((30))");

                entity.Property(e => e.Authentication)
                    .HasColumnName("authentication")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Dob)
                    .HasColumnType("date")
                    .HasColumnName("dob");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("email");

                entity.Property(e => e.GenderId).HasColumnName("gender_id");

                entity.Property(e => e.Introduce)
                    .HasMaxLength(500)
                    .HasColumnName("introduce");

                entity.Property(e => e.Latitude).HasColumnName("latitude");

                entity.Property(e => e.Longitude).HasColumnName("longitude");

                entity.Property(e => e.MoneyLeft)
                    .HasColumnName("moneyLeft")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Nickname)
                    .HasMaxLength(500)
                    .HasColumnName("nickname");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("password");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(12)
                    .HasColumnName("phone");

                entity.Property(e => e.Status)
                    .HasMaxLength(1000)
                    .HasColumnName("status");

                entity.HasOne(d => d.Gender)
                    .WithMany(p => p.UserAccounts)
                    .HasForeignKey(d => d.GenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__user_acco__gende__164452B1");
            });

            modelBuilder.Entity<UserBlock>(entity =>
            {
                entity.ToTable("user_block");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserAccountId).HasColumnName("user_account_id");

                entity.Property(e => e.UserAccountIdBlock).HasColumnName("user_account_id_block");

                entity.HasOne(d => d.UserAccount)
                    .WithMany(p => p.UserBlockUserAccounts)
                    .HasForeignKey(d => d.UserAccountId)
                    .HasConstraintName("FK__user_bloc__user___300424B4");

                entity.HasOne(d => d.UserAccountIdBlockNavigation)
                    .WithMany(p => p.UserBlockUserAccountIdBlockNavigations)
                    .HasForeignKey(d => d.UserAccountIdBlock)
                    .HasConstraintName("FK__user_bloc__user___30F848ED");
            });

            modelBuilder.Entity<UserDislike>(entity =>
            {
                entity.ToTable("user_dislike");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserAccountId).HasColumnName("user_account_id");

                entity.Property(e => e.UserAccountIdDislike).HasColumnName("user_account_id_dislike");

                entity.HasOne(d => d.UserAccount)
                    .WithMany(p => p.UserDislikeUserAccounts)
                    .HasForeignKey(d => d.UserAccountId)
                    .HasConstraintName("FK__user_disl__user___34C8D9D1");

                entity.HasOne(d => d.UserAccountIdDislikeNavigation)
                    .WithMany(p => p.UserDislikeUserAccountIdDislikeNavigations)
                    .HasForeignKey(d => d.UserAccountIdDislike)
                    .HasConstraintName("FK__user_disl__user___35BCFE0A");
            });

            modelBuilder.Entity<UserHobby>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("user_hobby");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("createTime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.HobbyId).HasColumnName("hobby_id");

                entity.Property(e => e.UserAccountId).HasColumnName("user_account_id");

                entity.HasOne(d => d.Hobby)
                    .WithMany()
                    .HasForeignKey(d => d.HobbyId)
                    .HasConstraintName("FK__user_hobb__hobby__4F7CD00D");

                entity.HasOne(d => d.UserAccount)
                    .WithMany()
                    .HasForeignKey(d => d.UserAccountId)
                    .HasConstraintName("FK__user_hobb__user___5070F446");
            });

            modelBuilder.Entity<UserLike>(entity =>
            {
                entity.ToTable("user_like");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserAccountId).HasColumnName("user_account_id");

                entity.Property(e => e.UserAccountIdLike).HasColumnName("user_account_id_like");

                entity.HasOne(d => d.UserAccount)
                    .WithMany(p => p.UserLikeUserAccounts)
                    .HasForeignKey(d => d.UserAccountId)
                    .HasConstraintName("FK__user_like__user___2B3F6F97");

                entity.HasOne(d => d.UserAccountIdLikeNavigation)
                    .WithMany(p => p.UserLikeUserAccountIdLikeNavigations)
                    .HasForeignKey(d => d.UserAccountIdLike)
                    .HasConstraintName("FK__user_like__user___2C3393D0");
            });

            modelBuilder.Entity<UserPhoto>(entity =>
            {
                entity.ToTable("user_photo");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Detail)
                    .HasMaxLength(1000)
                    .HasColumnName("detail");

                entity.Property(e => e.Link)
                    .HasMaxLength(1000)
                    .HasColumnName("link");

                entity.Property(e => e.PhotoType).HasColumnName("photo_type");

                entity.Property(e => e.UserAccountId).HasColumnName("user_account_id");

                entity.HasOne(d => d.UserAccount)
                    .WithMany(p => p.UserPhotos)
                    .HasForeignKey(d => d.UserAccountId)
                    .HasConstraintName("FK__user_phot__user___1DE57479");
            });

            modelBuilder.Entity<UserReport>(entity =>
            {
                entity.ToTable("user_report");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Details)
                    .HasMaxLength(500)
                    .HasColumnName("details");

                entity.Property(e => e.UserAccountId).HasColumnName("user_account_id");

                entity.Property(e => e.UserAccountIdReport).HasColumnName("user_account_id_report");

                entity.HasOne(d => d.UserAccount)
                    .WithMany(p => p.UserReportUserAccounts)
                    .HasForeignKey(d => d.UserAccountId)
                    .HasConstraintName("FK__user_repo__user___398D8EEE");

                entity.HasOne(d => d.UserAccountIdReportNavigation)
                    .WithMany(p => p.UserReportUserAccountIdReportNavigations)
                    .HasForeignKey(d => d.UserAccountIdReport)
                    .HasConstraintName("FK__user_repo__user___3A81B327");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
