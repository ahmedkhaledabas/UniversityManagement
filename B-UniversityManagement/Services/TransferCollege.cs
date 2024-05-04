using B_UniversityManagement.DTOs;
using B_UniversityManagement.Models;

namespace B_UniversityManagement.Services
{
    public static class TransferCollege
    {

        public static CollegeDTO TransferCollegeToDto(College college)
        {
            return new CollegeDTO(){
                Name = college.Name,
                Description = college.Description,
                Img = college.Img
            };
        }

        public static List<CollegeDTO> TransferListToDto(List<College> colleges)
        {
            List<CollegeDTO> collegeDTOs = new List<CollegeDTO>();
            foreach (var college in colleges)
            {
                var dto = TransferCollegeToDto(college);
                collegeDTOs.Add(dto);
            }
            return collegeDTOs;
        }


        public static College TransferDtoToCollege(CollegeDTO collegeDto)
        {
            return new College()
            {
                Name = collegeDto.Name,
                Description = collegeDto.Description,
                Img = collegeDto.Img
                
            };
        }
    }
}
