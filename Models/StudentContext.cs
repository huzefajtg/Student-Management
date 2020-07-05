using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace StudentProject.Models
{
    public partial class StudentContext : DbContext
    {
        public StudentContext()
        {
        }

        public StudentContext(DbContextOptions<StudentContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Courses> Courses { get; set; }
        public virtual DbSet<CourseStudent> CourseStudent { get; set; }
        public virtual DbSet<CourseSubject> CourseSubject { get; set; }
        public virtual DbSet<LoginInfo> LoginInfo { get; set; }
        public virtual DbSet<NotiFicationTeachers> NotiFicationTeachers { get; set; }
        public virtual DbSet<Students> Students { get; set; }
        public virtual DbSet<TeacherNotification> TeacherNotification { get; set; }
        public virtual DbSet<Teachers> Teachers { get; set; }
        public virtual DbSet<TeacherStudent> TeacherStudent { get; set; }
        public virtual DbSet<Tracker> Tracker { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-4MTGO38;Database=StudentProject;integrated security=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Courses>(entity =>
            {
                entity.Property(e => e.CourseId).ValueGeneratedNever();

                entity.Property(e => e.CourseName).IsUnicode(false);

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.SubjectId)
                    .HasConstraintName("FK__Courses__Subject__1273C1CD");
            });

            modelBuilder.Entity<CourseStudent>(entity =>
            {
                entity.HasOne(d => d.Course)
                    .WithMany(p => p.CourseStudent)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK__CourseStu__Cours__24927208");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.CourseStudent)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK__CourseStu__Stude__239E4DCF");
            });

            modelBuilder.Entity<CourseSubject>(entity =>
            {
                entity.Property(e => e.SubjectId).ValueGeneratedNever();

                entity.Property(e => e.SubjectName).IsUnicode(false);
            });

            modelBuilder.Entity<LoginInfo>(entity =>
            {
                entity.HasIndex(e => e.UserName)
                    .HasName("UQ__LoginInf__C9F2845623FF09F3")
                    .IsUnique();

                entity.Property(e => e.UserName).IsUnicode(false);

                entity.Property(e => e.UserPassword).IsUnicode(false);

                entity.Property(e => e.UserType).IsUnicode(false);
            });

            modelBuilder.Entity<NotiFicationTeachers>(entity =>
            {
                entity.Property(e => e.MessageDate).IsUnicode(false);

                entity.Property(e => e.NotificationMessage).IsUnicode(false);

                entity.HasOne(d => d.NotificationForNavigation)
                    .WithMany(p => p.NotiFicationTeachers)
                    .HasForeignKey(d => d.NotificationFor)
                    .HasConstraintName("FK__NotiFicat__Notif__48CFD27E");
            });

            modelBuilder.Entity<Students>(entity =>
            {
                entity.Property(e => e.StudentId).ValueGeneratedNever();

                entity.Property(e => e.ContactAddress).IsUnicode(false);

                entity.Property(e => e.ContactNumber).IsUnicode(false);

                entity.Property(e => e.Dob).IsUnicode(false);

                entity.Property(e => e.EmailId).IsUnicode(false);

                entity.Property(e => e.FirstName).IsUnicode(false);

                entity.Property(e => e.Gender).IsUnicode(false);

                entity.Property(e => e.LastName).IsUnicode(false);

                entity.Property(e => e.SecondName).IsUnicode(false);
            });

            modelBuilder.Entity<TeacherNotification>(entity =>
            {
                entity.Property(e => e.NotiDate).IsUnicode(false);

                entity.Property(e => e.NotiMessage).IsUnicode(false);

                entity.Property(e => e.OtherType).IsUnicode(false);

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.TeacherNotification)
                    .HasForeignKey(d => d.TeacherId)
                    .HasConstraintName("FK__TeacherNo__Teach__5CD6CB2B");
            });

            modelBuilder.Entity<Teachers>(entity =>
            {
                entity.Property(e => e.TeacherId).ValueGeneratedNever();

                entity.Property(e => e.ContactAddress).IsUnicode(false);

                entity.Property(e => e.ContactNumber).IsUnicode(false);

                entity.Property(e => e.Dob).IsUnicode(false);

                entity.Property(e => e.EmailId).IsUnicode(false);

                entity.Property(e => e.FirstName).IsUnicode(false);

                entity.Property(e => e.Gender).IsUnicode(false);

                entity.Property(e => e.LastName).IsUnicode(false);

                entity.Property(e => e.SecondName).IsUnicode(false);

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Teachers)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK__Teachers__Course__1920BF5C");
            });

            modelBuilder.Entity<TeacherStudent>(entity =>
            {
                entity.HasOne(d => d.Student)
                    .WithMany(p => p.TeacherStudent)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK__TeacherSt__Stude__20C1E124");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.TeacherStudent)
                    .HasForeignKey(d => d.TeacherId)
                    .HasConstraintName("FK__TeacherSt__Teach__21B6055D");
            });

            modelBuilder.Entity<Tracker>(entity =>
            {
                entity.Property(e => e.ByType).IsUnicode(false);

                entity.Property(e => e.DatePerformed).IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.OnType).IsUnicode(false);

                entity.Property(e => e.Operation).IsUnicode(false);
            });
        }
    }
}
