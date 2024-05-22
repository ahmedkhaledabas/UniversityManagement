using B_UniversityManagement.Models;

namespace B_UniversityManagement.IRepository
{
    public interface IBookRepo
    {
        void Create(Book book);
        void Update(Book book);
        void Delete(Book book);
        List<Book> GetAll();
        Book GetById(string id);
    }
}
