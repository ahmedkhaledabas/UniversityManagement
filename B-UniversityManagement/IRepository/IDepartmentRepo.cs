﻿using B_UniversityManagement.Models;

namespace B_UniversityManagement.IRepository
{
    public interface IDepartmentRepo
    {
        void Create(Department department);
        void Update(Department department);
        void Delete(string id);
        List<Department> GetAll();
        Department GetById(string id);
    }
}
