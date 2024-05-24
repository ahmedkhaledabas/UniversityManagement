using B_UniversityManagement.DTOs;
using B_UniversityManagement.Models;

namespace B_UniversityManagement.Services
{
    public static class TransferCourse
    {
        public static Course DTOToCourse(CourseDTO courseDTO)
        {
            return new Course
            {
                Id = courseDTO.Id,
                Name = courseDTO.Name,
                Description = courseDTO.Description,
                DepartmentId = courseDTO.DepartmentId,
                Img = courseDTO.Img,
                LevelYear = courseDTO.LevelYear,
                ProfessorId = courseDTO.ProfessorId
            };
        }

        public static CourseDTO CourseToDTO(Course course)
        {
            return new CourseDTO
            {
                Id = course.Id,
                Name = course.Name,
                Description = course.Description,
                DepartmentId = course.DepartmentId,
                Img = course.Img,
                LevelYear = course.LevelYear,
                ProfessorId = course.ProfessorId
            };
        }

        public static List<CourseDTO> ListCourseToDTOs (List<Course> courses)
        {
            var DTOs = new List<CourseDTO>();
            foreach (var course in courses)
            {
                var DTO = CourseToDTO(course);
                DTOs.Add(DTO);
            }
            return DTOs;
        }
    }
}
