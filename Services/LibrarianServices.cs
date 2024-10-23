using AutoMapper;
using library_api.DTOs;
using library_api.Models;
using library_api.Repositories.Interfaces;
using library_api.Services.Interfaces;

namespace library_api.Services
{
	public class LibrarianServices : ILibrarianServices
	{
		private readonly IBookRepository _bookRepository;
		private readonly IMapper _mapper;

		public LibrarianServices(IBookRepository bookRepository, IMapper mapper)
		{
			_bookRepository = bookRepository;
			_mapper = mapper;
		}

		public async Task AddAsync(CreateBookDto createBookDto)
		{
			var book = _mapper.Map<Book>(createBookDto);
			await _bookRepository.AddAsync(book);
		}
	}
}
