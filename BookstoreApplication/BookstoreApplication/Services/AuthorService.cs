using AutoMapper;
using BookstoreApplication.DTOs;
using BookstoreApplication.Exceptions;
using BookstoreApplication.Models;
using BookstoreApplication.Utils;

namespace BookstoreApplication.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;
        private const int PageSize = 4;

        public AuthorService(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        public async Task<List<Author>> GetAll()
        {
            return await _authorRepository.GetAll();
        }

        public async Task<Author> GetById(int id)
        {
            var author = await _authorRepository.GetById(id);

            if (author == null)
            {
                throw new NotFoundException(id);
            }

            return author;
        }

        public async Task<Author> Create(Author author)
        {
            if (author == null)
            {
                throw new BadRequestException("Author cannot be null.");
            }

            await _authorRepository.Add(author);
            return author;
        }

        public async Task<Author> Update(int id, Author author)
        {
            if (author == null)
            {
                throw new BadRequestException("Author cannot be null.");
            }

            if (id != author.Id)
            {
                throw new BadRequestException("Id mismatch between route and body.");
            }

            var existingAuthor = await _authorRepository.GetById(id);

            if (existingAuthor == null)
            {
                throw new NotFoundException(id);
            }

            await _authorRepository.Update(author);
            return author;
        }

        public async Task Delete(int id)
        {
            var author = await _authorRepository.GetById(id);

            if (author == null)
            {
                throw new NotFoundException(id);
            }

            await _authorRepository.Delete(id);
        }

        public async Task<PaginatedList<AuthorDTO>> GetAllPaged(int page)
        {
            var authors = await _authorRepository.GetAllPaged(page);
            var dtos = authors.Items.Select(_mapper.Map<AuthorDTO>).ToList();

            return new PaginatedList<AuthorDTO>(dtos, authors.Count, authors.PageIndex, PageSize);
        }
    }
}
