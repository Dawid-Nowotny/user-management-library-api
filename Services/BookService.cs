using AutoMapper;
using library_api.DTOs;
using library_api.Repositories.Interfaces;
using library_api.Services.Interfaces;

namespace library_api.Services
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

		public async Task<IEnumerable<BookDto>> GetBookInfoAsync(string identifier)
		{
			var books = await _bookRepository.GetBookInfoAsync(identifier);

			if (books == null || !books.Any())
			{
				throw new KeyNotFoundException("Book not found with the provided identifier.");
			}

			return books.Select(book => _mapper.Map<BookDto>(book)).ToList();
		}
	}
}
