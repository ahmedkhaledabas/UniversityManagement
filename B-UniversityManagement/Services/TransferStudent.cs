using B_UniversityManagement.DTOs;
using B_UniversityManagement.Models;

namespace B_UniversityManagement.Services
{
    public static class TransferStudent
    {
        public static StudentDTO TransferStudentToDTO(User student)
        {
            return new StudentDTO
            {
                CollegeId = student.CollegeId,
                UserName = student.UserName,
                Id = student.Id,
                FName = student.FName,
                LName = student.LName,
                Email = student.Email,
                PasswordHash = student.PasswordHash,
                Phone = student.Phone,
                BirthDate = student.BirthDate,
                DepartmentId = student.DepartmentId,
                Address = student.Address,
                Gender = student.Gender,
                Img = student.Img,
                levelYear = student.levelYear,
            };
        }

        public static User TransferDTOToStudent(StudentDTO studentDTO)
        {
            return new User
            {
                CollegeId = studentDTO.CollegeId,
                UserName = studentDTO.UserName,
                Id = studentDTO.Id,
                FName = studentDTO.FName,
                LName = studentDTO.LName,
                Email = studentDTO.Email,
                PasswordHash = studentDTO.PasswordHash,
                Phone = studentDTO.Phone,
                BirthDate = studentDTO.BirthDate,
                DepartmentId = studentDTO.DepartmentId,
                Address = studentDTO.Address,
                Gender = studentDTO.Gender,
                Img = studentDTO.Img,
                levelYear = studentDTO.levelYear,
            };
        }

        public static List<StudentDTO> ListOfStudentToDTOs (List<User> students)
        {
            List<StudentDTO> studentDTOs = new List<StudentDTO>();
            foreach(var  student in students)
            {
                var studentDTO = TransferStudentToDTO(student);
                studentDTOs.Add(studentDTO);
            }
            return studentDTOs;
        }

    }
}
