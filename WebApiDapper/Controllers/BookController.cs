using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApiDapper.Models;
using WebApiDapper.Services.BookService;

namespace WebApiDapper.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase {

        private readonly IBookInterface _bookInterface;

        public BookController(IBookInterface bookInterface) { 
            _bookInterface = bookInterface;
        }

        [HttpGet("getAllBooks")]
        public async Task<ActionResult<IEnumerable<Book>>> GetAllBooks() {
            IEnumerable<Book> books = await _bookInterface.GetAllBooks();

            if (!books.Any()) {
                return NotFound("Nenhum livro localizado!");
            }

            return Ok(books);
        }

        [HttpGet("getBookById/{id}")]
        public async Task<ActionResult<Book>> GetBookById(int id) {
            Book book = await _bookInterface.GetBookById(id);

            if (book == null) {
                return NotFound("Nenhum livro localizado!");
            }

            return Ok(book);
        }

        [HttpPost("createBook")]
        public async Task<ActionResult<IEnumerable<Book>>> CreateBook(Book book) {
            IEnumerable<Book> books = await _bookInterface.CreateBook(book);

            if (!books.Any()) {
                return NotFound("Nenhum livro localizado!");
            }

            return Ok(book);
        }

        [HttpPut("updateBook")]
        public async Task<ActionResult<IEnumerable<Book>>> UpdateBook(Book book) {
            Book bookDb = await _bookInterface.GetBookById(book.Id);

            if (bookDb == null) {
                return NotFound("Livro não localizado!");
            }

            IEnumerable<Book> books = await _bookInterface.UpdateBook(book);

            if (!books.Any()) {
                return NotFound("Nenhum livro localizado!");
            }

            return Ok(books);
        }

        [HttpDelete("deleteBook/{id}")]
        public async Task<ActionResult<IEnumerable<Book>>> DeleteBook(int id) {
            Book bookDb = await _bookInterface.GetBookById(id);

            if (bookDb == null) {
                return NotFound("Livro não localizado!");
            }

            IEnumerable<Book> books = await _bookInterface.DeleteBook(id);

            return Ok(books);
        }

    }
}
