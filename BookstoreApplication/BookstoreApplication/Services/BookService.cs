using AutoMapper;
using BookstoreApplication.DTOs;
using BookstoreApplication.Exceptions;
using BookstoreApplication.Models;
using BookstoreApplication.Utils;
using Microsoft.Extensions.Logging;

namespace BookstoreApplication.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<BookService> _logger;

        public BookService(IBookRepository bookRepository, IMapper mapper, ILogger<BookService> logger)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
            _logger = logger;
        }

        //public async Task<List<Book>> GetAll()
        //{
        //    _logger.LogInformation("Fetching all books.");

        //    var books = await _bookRepository.GetAll();

        //    _logger.LogInformation($"Fetched {books.Count} books.");

        //    return books;
        //}

        public async Task<List<BookDto>> GetAllDtos()
        {
            _logger.LogInformation("Fetching all books.");

            var books = await _bookRepository.GetAll();

            _logger.LogInformation($"Fetched {books.Count} books.");

            var dtos = books.Select(_mapper.Map<BookDto>).ToList();
            return dtos;
        }

        public async Task<BookDetailsDto> GetById(int id)
        {
            _logger.LogInformation($"Fetching book with id {id}.");

            var book = await _bookRepository.GetById(id);

            if (book == null)
            {
                _logger.LogError($"Book with id {id} not found.");
                throw new NotFoundException(id);
            }

            _logger.LogInformation($"Book with id {id} found.");

            var dto = _mapper.Map<BookDetailsDto>(book);
            return dto;
        }

        public async Task<Book> Create(Book book)
        {
            _logger.LogInformation("Creating new book.");

            if (book == null)
            {
                _logger.LogError("Book is null.");
                throw new BadRequestException("Book cannot be null.");
            }

            await _bookRepository.Add(book);

            _logger.LogInformation($"Book created with title: {book.Title}");

            return book;
        }

        public async Task<Book> Update(int id, Book book)
        {
            _logger.LogInformation($"Updating book with id {id}.");

            if (book == null)
            {
                _logger.LogError("Book is null.");
                throw new BadRequestException("Book cannot be null.");
            }

            if (id != book.Id)
            {
                _logger.LogError("Id mismatch.");
                throw new BadRequestException("Id mismatch between route and body.");
            }

            var existingBook = await _bookRepository.GetById(id);

            if (existingBook == null)
            {
                _logger.LogError($"Book with id {id} not found.");
                throw new NotFoundException(id);
            }

            await _bookRepository.Update(book);

            _logger.LogInformation($"Book with id {id} updated.");

            return book;
        }

        public async Task Delete(int id)
        {
            _logger.LogInformation($"Deleting book with id {id}.");

            var book = await _bookRepository.GetById(id);

            if (book == null)
            {
                _logger.LogError($"Book with id {id} not found.");
                throw new NotFoundException(id);
            }

            await _bookRepository.Delete(id);

            _logger.LogInformation($"Book with id {id} deleted.");
        }

        public async Task<IEnumerable<BookDto>> GetAllSorted(int sortType)
        {
            var books = await _bookRepository.GetAllSorted(sortType);
            var dtos = books.Select(_mapper.Map<BookDto>).ToList();
            return dtos;
        }

        public async Task<List<BookSortTypeOption>> GetSortTypes()
        {
            return await _bookRepository.GetSortTypes();
        }

        public async Task<IEnumerable<BookDto>> GetAllFilteredAndSorted(BookFilter filter, int sortType)
        {
            var books = await _bookRepository.GetAllFilteredAndSorted(filter, sortType);
            var dtos = books.Select(_mapper.Map<BookDto>).ToList();
            return dtos;
        }
    }
}
