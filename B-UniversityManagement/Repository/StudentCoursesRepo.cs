﻿using B_UniversityManagement.Data;
using B_UniversityManagement.IRepository;
using B_UniversityManagement.Models;

namespace B_UniversityManagement.Repository
{
    public class StudentCoursesRepo : IStudentCourses
    {
        private readonly UniversityDbContext context;

        public StudentCoursesRepo(UniversityDbContext context)
        {
            this.context = context;
        }

        public void Create(StudentCourse studentCourse)
        {
            context.StudentCourses.Add(studentCourse);
            context.SaveChanges();
        }

        public List<StudentCourse> GetAll() => context.StudentCourses.ToList();
    }
}