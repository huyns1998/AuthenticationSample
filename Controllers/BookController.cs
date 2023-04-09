using AuthenticationSample.Models.Book;
using BookStore.Data2.Entities;
using BookStore.Data2.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AuthenticationSample.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;

        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Book> listBook = _bookRepository.GetAllBooks();
            List<BookViewModel> viewModels = new List<BookViewModel>();

            foreach (Book item in listBook)
            {
                BookViewModel viewModel = new BookViewModel();
                viewModel.Id = item.Id;
                viewModel.Price = item.Price;
                viewModel.Author = item.Author;
                viewModel.Publisher = item.Publisher;
                viewModel.BookName = item.BookName;

                viewModels.Add(viewModel);
            }

            return View(viewModels);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(BookCreateModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                Book bookCreate = new Book()
                {
                    Author = model.Author,
                    BookName = model.BookName,
                    Price = model.Price,
                    Publisher = model.Publisher,
                };

                _bookRepository.AddBook(bookCreate);
            }
            catch
            {
                ModelState.AddModelError("", "Create fail");
                return View(model);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            BookUpdateModel model = new BookUpdateModel() { Id = Id };
            Book book = _bookRepository.GetBookDetails(Id);
            model.BookName = book.BookName;
            model.Author = book.Author;
            model.Publisher = book.Publisher;
            model.Price = book.Price;
            return View(model);
        }
        [HttpPut]
        [HttpPost]
        public IActionResult Edit(BookUpdateModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                Book book = new Book()
                {
                    Id = model.Id,
                    BookName = model.BookName,
                    Price = model.Price,
                    Publisher = model.Publisher,
                    Author = model.Author
                };
                _bookRepository.Update(book);
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("", "Edit fail");
                return View(model);
            }
        }
        [HttpGet]
        public IActionResult Delete(int Id)
        {
            try
            {
                _bookRepository.Delete(Id);
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

    }
}
