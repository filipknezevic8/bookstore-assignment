using AutoMapper;
using BookstoreApplication.DTOs;
using BookstoreApplication.Exceptions;
using BookstoreApplication.Models;

namespace BookstoreApplication.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BookService(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<List<BookDto>> GetAll()
        {
            var books = await _bookRepository.GetAll();
            var dtos = books.Select(_mapper.Map<BookDto>).ToList();
            return dtos;
        }

        public async Task<BookDetailsDto> GetById(int id)
        {
            var book = await _bookRepository.GetById(id);

            if (book == null)
            {
                throw new NotFoundException(id);
            }

            var dto = _mapper.Map<BookDetailsDto>(book);
            return dto;
        }

        public async Task<Book> Create(Book book)
        {
            if (book == null)
            {
                throw new BadRequestException("Book cannot be null.");
            }

            await _bookRepository.Add(book);
            return book;
        }

        public async Task<Book> Update(int id, Book book)
        {
            if (book == null)
            {
                throw new BadRequestException("Book cannot be null.");
            }

            if (id != book.Id)
            {
                throw new BadRequestException("Id mismatch between route and body.");
            }

            var existingBook = await _bookRepository.GetById(id);

            if (existingBook == null)
            {
                throw new NotFoundException(id);
            }

            await _bookRepository.Update(book);
            return book;
        }

        public async Task Delete(int id)
        {
            var book = await _bookRepository.GetById(id);

            if (book == null)
            {
                throw new NotFoundException(id);
            }

            await _bookRepository.Delete(id);
        }
    }
}
