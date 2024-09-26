using WebApiDapper.Models;

namespace WebApiDapper.Services.BookService {
    public interface IBookInterface {
        Task<IEnumerable<Book>> GetAllBooks();
        Task<Book> GetBookById(int id);
        Task<IEnumerable<Book>> CreateBook(Book book);
        Task<IEnumerable<Book>> UpdateBook(Book book);
        Task<IEnumerable<Book>> DeleteBook(int id);
    }
}
