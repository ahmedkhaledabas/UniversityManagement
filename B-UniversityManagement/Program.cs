
using B_UniversityManagement.Data;
using B_UniversityManagement.IRepository;
using B_UniversityManagement.Repository;
using Microsoft.EntityFrameworkCore;

namespace B_UniversityManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  policy =>
                                  {
                                      policy.WithOrigins("http://localhost:4200")
                                      .AllowAnyHeader()
                                      .AllowAnyMethod();
                                  });
            });

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //ask service
            builder.Services.AddDbContext<UniversityDbContext>(
               option => option.UseSqlServer(builder.Configuration.GetConnectionString("name=ConnectionStrings:Default")));
            builder.Services.AddScoped<ICollegeRepo , CollegeRepo>();
            builder.Services.AddScoped<ICourseRepo, CourseRepo>();
            builder.Services.AddScoped<IDepartmentRepo , DepartmentRepo>();
            builder.Services.AddTransient<IFileService , FileService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
