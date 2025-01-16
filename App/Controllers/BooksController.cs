using App.Services;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    public class BooksController : Controller
    {
        private readonly BookService _bookService;

        public BooksController(BookService bookService)
        {
            _bookService = bookService;
        }

        // Action para listar os livros
        public async Task<IActionResult> Index()
        {
            var books = await _bookService.GetAllBooksAsync();
            return View(books);
        }
    }
}
