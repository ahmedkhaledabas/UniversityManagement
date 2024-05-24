using B_UniversityManagement.DTOs;
using B_UniversityManagement.Models;

namespace B_UniversityManagement.Services
{
    public static class TransferEmployee
    {
        public static EmployeeDTO EmpToDTO(User employee)
        {
            return new EmployeeDTO
            {
                Id = employee.Id,
                FName = employee.FName,
                LName = employee.LName,
                UserName = employee.UserName,
                Email = employee.Email,
                Address = employee.Address,
                Password = employee.PasswordHash,
                Phone = employee.Phone,
                BirthDate = employee.BirthDate,
                CollegeId = employee.CollegeId,
                DepaertmentId = employee.DepartmentId,
                EmpSalary = employee.EmpSalary,
                Gender = employee.Gender,
                Img = employee.Img
            };
        }

        public static User DTOToEmployee(EmployeeDTO employeeDTO)
        {
            return new User
            {
                Id = employeeDTO.Id,
                FName = employeeDTO.FName,
                LName = employeeDTO.LName,
                UserName = employeeDTO.UserName,
                Email = employeeDTO.Email,
                Address = employeeDTO.Address,
                PasswordHash = employeeDTO.Password,
                Phone = employeeDTO.Phone,
                BirthDate = employeeDTO.BirthDate,
                CollegeId = employeeDTO.CollegeId,
                DepartmentId = employeeDTO.DepaertmentId,
                EmpSalary = employeeDTO.EmpSalary,
                Gender = employeeDTO.Gender,
                Img = employeeDTO.Img
            };
        }

        public static List<EmployeeDTO> ListEmpsToDTOs (List<User> employees)
        {
            var DTOs = new List<EmployeeDTO>();
            foreach (var emp in employees)
            {
                var DTO = EmpToDTO(emp);
                DTOs.Add(DTO);
            }
            return DTOs;
        }

    }
}
