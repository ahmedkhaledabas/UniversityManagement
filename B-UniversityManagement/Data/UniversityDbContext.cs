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
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Fee> Fees { get; set; }
        public DbSet<Library> Libraries { get; set; }
        public DbSet<Professor> Professors {  get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentCourse> StudentCourses {  get; set; }
        public DbSet<CollegeProfessor> CollegeProfessors { get; set; }
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
            modelBuilder.Entity<College>().HasMany(c => c.Professors).WithMany(p => p.Colleges).UsingEntity<CollegeProfessor>();
            modelBuilder.Entity<Student>().HasMany(s=>s.Books).WithMany(b=>b.Students).UsingEntity<StudentBook>();
            modelBuilder.Entity<Student>().HasMany(s => s.Courses).WithMany(c => c.Students).UsingEntity<StudentCourse>();

        }
    }
}
