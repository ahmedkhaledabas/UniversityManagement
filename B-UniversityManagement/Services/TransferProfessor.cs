using B_UniversityManagement.DTOs;
using B_UniversityManagement.Models;

namespace B_UniversityManagement.Services
{
    public static class TransferProfessor
    {
        public static ProfessorDTO ProfessorToDTO(User professor)
        {
            return new ProfessorDTO
            {
                UserName = professor.UserName,
                DepartmentId = professor.DepartmentId,
                CollegeId = professor.CollegeId,
                Id = professor.Id,
                FName = professor.FName,
                LName = professor.LName,
                Email = professor.Email,
                Password = professor.PasswordHash,
                Phone = professor.Phone,
                Address = professor.Address,
                Gender = professor.Gender,
                Specialist = professor.Specialist,
                BirthDate = professor.BirthDate,
                Img = professor.Img,
                Rank = professor.Rank,
            };
        }

        public static User DTOToProfessor(ProfessorDTO professorDTO)
        {
            return new User
            {
                UserName = professorDTO.UserName,
                DepartmentId = professorDTO.DepartmentId,
                CollegeId = professorDTO.CollegeId,
                Id = professorDTO.Id,
                FName = professorDTO.FName,
                LName = professorDTO.LName,
                Email = professorDTO.Email,
                PasswordHash = professorDTO.Password,
                Phone = professorDTO.Phone,
                Address = professorDTO.Address,
                Gender = professorDTO.Gender,
                Specialist = professorDTO.Specialist,
                BirthDate = professorDTO.BirthDate,
                Img = professorDTO.Img,
                Rank = professorDTO.Rank,
            };
        }

        public static List<ProfessorDTO> ListProfessorToDTOs (List<User> professors)
        {
            var DTOs = new List<ProfessorDTO>();
            foreach (var professor in professors)
            {
                var DTO = ProfessorToDTO(professor);
                DTOs.Add(DTO);
            }
            return DTOs;
        }

    }
}
