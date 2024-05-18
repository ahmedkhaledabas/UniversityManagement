using B_UniversityManagement.DTOs;
using B_UniversityManagement.Models;

namespace B_UniversityManagement.Services
{
    public static class TransferDepartment
    {
            public static DepartmentDTO TransferDepartmentToDto(Department department)
            {
                return new DepartmentDTO()
                {
                    Id = department.Id,
                    Name = department.Name,
                    Description = department.Description,
                    CollegeId = department.CollegeId
                };
            }

            public static List<DepartmentDTO> TransferListToDto(List<Department> departments)
            {
                List<DepartmentDTO> departmentDTOs = new List<DepartmentDTO>();
                foreach (var department in departments)
                {
                    var dto = TransferDepartmentToDto(department);
                    departmentDTOs.Add(dto);
                }
                return departmentDTOs;
            }
            public static Department TransferDtoToDepartment(DepartmentDTO departmentDto)
            {
                return new Department()
                {
                    Id = departmentDto.Id,
                    Name = departmentDto.Name,
                    Description = departmentDto.Description,
                    CollegeId = departmentDto.CollegeId
                };
            }
        }
}
