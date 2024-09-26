using Dapper;
using System.Data.SqlClient;
using WebApiDapper.Models;

namespace WebApiDapper.Services.BookService
{
    public class BookService : IBookInterface {

        private readonly IConfiguration _configuration;
        private readonly string getConnection;
        
        public BookService(IConfiguration configuration) { 
            _configuration = configuration;
            getConnection = _configuration.GetConnectionString("DefaultConnection") ?? "";
        }


        public async Task<IEnumerable<Book>> GetAllBooks() {
            // vamos conseguir abrir a conexao e automaticamente vai fechar a conexao
            // getConnection tem a string de conexao
            using (var conn = new SqlConnection(getConnection)) {
                var query = "SELECT * FROM Books";
                return await conn.QueryAsync<Book>(query);
            }
        }

        public async Task<Book> GetBookById(int id) {
            using (var conn = new SqlConnection(getConnection)) {
                var query = "SELECT * FROM Books WHERE id = @Id";
                return await conn.QueryFirstOrDefaultAsync<Book>(query, new {Id = id});
            }
        }

        public async Task<IEnumerable<Book>> CreateBook(Book book) {
            using (var conn = new SqlConnection(getConnection)) {
                var query = "INSERT INTO Books (title, author) VALUES (@title, @author)";
                await conn.ExecuteAsync(query, book);

                return await conn.QueryAsync<Book>("SELECT * FROM Books");
            }
        }
        public async Task<IEnumerable<Book>> UpdateBook(Book book) {
            using (var conn = new SqlConnection(getConnection)) {
                var query = "UPDATE Books SET title = @title, author = @author WHERE id = @id";
                await conn.ExecuteAsync(query, book);

                return await conn.QueryAsync<Book>("SELECT * FROM Books");
            }
        }
        public async Task<IEnumerable<Book>> DeleteBook(int id) {
            using (var conn = new SqlConnection(getConnection)) {
                var query = "Delete FROM Books WHERE id = @id";
                await conn.ExecuteAsync(query, new {id = id});

                return await conn.QueryAsync<Book>("SELECT * FROM Books");
            }
        }

    }
}
