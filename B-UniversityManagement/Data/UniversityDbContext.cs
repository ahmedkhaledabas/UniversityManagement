using B_UniversityManagement.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace B_UniversityManagement.Data
{
    public class UniversityDbContext : IdentityDbContext<User>
    {
        public DbSet<College> Colleges { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Question> Questions {  get; set; }
        public DbSet<Department> Departments { get; set; }
        //public DbSet<Employee> Employees { get; set; }
        public DbSet<Fee> Fees { get; set; }
        public DbSet<Library> Libraries { get; set; }
        //public DbSet<Professor> Professors {  get; set; }
        //public DbSet<Student> Students { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<StudentCourse> StudentCourses {  get; set; }
        public DbSet<StudentBook> StudentBooks {  get; set; }

        public UniversityDbContext(DbContextOptions<UniversityDbContext> options) : base() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build().GetConnectionString("Default");
            optionsBuilder.UseSqlServer(builder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Department>().HasMany(c => c.Courses).WithOne(c => c.Department).HasForeignKey(d => d.DepartmentId).IsRequired();
            modelBuilder.Entity<College>().HasMany(c => c.Departments).WithOne(d => d.College).HasForeignKey(d => d.CollegeId).IsRequired();
            modelBuilder.Entity<College>().HasMany(c => c.Users).WithOne(u => u.College).HasForeignKey(u => u.CollegeId).IsRequired();
            //modelBuilder.Entity<User>().HasMany(s=>s.Books).WithMany(b=>b.Students).UsingEntity<StudentBook>();
            //modelBuilder.Entity<User>().HasMany(student => student.Courses).WithMany(c => c.Users).UsingEntity<StudentCourse>();
            modelBuilder.Entity<StudentCourse>().HasKey(c => new {c.CourseId , c.StudentId});
            modelBuilder.Entity<StudentCourse>().HasOne(u => u.User).WithMany(u => u.StudentCourses).HasForeignKey(u => u.StudentId);
            modelBuilder.Entity<StudentCourse>().HasOne(u => u.Course).WithMany(u => u.StudentCourses).HasForeignKey(u => u.CourseId);
            modelBuilder.Entity<StudentBook>().HasKey(s => new { s.BookId, s.StudentId });
            modelBuilder.Entity<StudentBook>().HasOne(s => s.User).WithMany(b => b.StudentBooks).HasForeignKey(s => s.StudentId);
            modelBuilder.Entity<StudentBook>().HasOne(b => b.Book).WithMany(s => s.StudentBooks).HasForeignKey(b => b.BookId);

            modelBuilder.Entity<User>().HasMany(student=>student.Fees).WithOne(f=>f.Student).HasForeignKey(f=>f.StudentId).IsRequired();
            modelBuilder.Entity<Department>().HasMany(d => d.Students).WithOne(student => student.Department).HasForeignKey(student => student.DepartmentId).IsRequired();

        }
    }
}
