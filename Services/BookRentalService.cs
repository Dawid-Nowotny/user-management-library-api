using AutoMapper;
using library_api.DTOs;
using library_api.Models;
using library_api.Repositories;
using library_api.Repositories.Interfaces;
using library_api.Services.Interfaces;

namespace library_api.Services
{
	public class BookRentalService : IBookRentalService
	{
		private readonly IUserRepository _userRepository;
		private readonly IBookRepository _bookRepository;
		private readonly IBookRentalRepository _rentalRepository;
		private readonly IMapper _mapper;

		public BookRentalService(IUserRepository userRepository, IBookRepository bookRepository, IBookRentalRepository rentalRepository, IMapper mapper)
		{
			_userRepository = userRepository;
			_bookRepository = bookRepository;
			_rentalRepository = rentalRepository;
			_mapper = mapper;
		}

		public async Task<IEnumerable<BookRentalDto>> GetAllRentalsAsync()
		{
			var rentals = await _rentalRepository.GetAllRentalsAsync();
			return _mapper.Map<IEnumerable<BookRentalDto>>(rentals);
		}

		public async Task<IEnumerable<BookRentalDto>> GetRentalsByUserAsync(string identifier, bool? isReturned = null)
		{
			var user = await _userRepository.GetUserInfoAsync(identifier);
			if (user == null)
			{
				throw new KeyNotFoundException("User not found with the provided identifier.");
			}

			var rentals = await _rentalRepository.GetRentalsByUserAsync(user.Id, isReturned);
			return _mapper.Map<IEnumerable<BookRentalDto>>(rentals);
		}

		public async Task<IEnumerable<BookRentalDto>> GetUserRentalsAsync(string username, bool? isReturned = null)
		{
			var user = await _userRepository.GetByUsernameAsync(username);
			if (user == null)
			{
				throw new KeyNotFoundException("User not found.");
			}

			var rentals = await _rentalRepository.GetRentalsByUserAsync(user.Id, isReturned);
			return _mapper.Map<IEnumerable<BookRentalDto>>(rentals);
		}

		public async Task RentBookAsync(RentBookDto rentBookDto, string username)
		{
			var user = await _userRepository.GetByUsernameAsync(username);
			if (user == null)
			{
				throw new KeyNotFoundException("User not found.");
			}

			var book = await _bookRepository.GetBookByIsbnAsync(rentBookDto.ISBN);
			if (book == null)
			{
				throw new KeyNotFoundException("Book not found.");
			}

			if (await _rentalRepository.BookAlreadyRentedByUserAsync(user.Id, book.Id))
			{
				throw new InvalidOperationException("This book is already rented by the user.");
			}

			if (book.CopiesAvailable <= 0)
			{
				throw new InvalidOperationException("No copies available for rent.");
			}

			book.CopiesAvailable--;

			var rental = _mapper.Map<BookRental>(rentBookDto);
			rental.UserId = user.Id;
			rental.BookId = book.Id;

			await _rentalRepository.AddAsync(rental);
			await _bookRepository.UpdateAsync(book);
		}

		public async Task ExtendRentalByIsbnAsync(string username, string isbn)
		{
			var user = await _userRepository.GetByUsernameAsync(username);
			if (user == null)
			{
				throw new KeyNotFoundException("User not found.");
			}

			var rental = await _rentalRepository.GetActiveRentalByUserAndIsbnAsync(user.Id, isbn);
			if (rental == null)
			{
				throw new KeyNotFoundException("No active rental found for this book.");
			}

			if (rental.IsReturned)
			{
				throw new InvalidOperationException("Cannot extend a returned rental.");
			}

			if (rental.IsExtended)
			{
				throw new InvalidOperationException("This rental has already been extended.");
			}

			rental.ReturnDate = rental.ReturnDate.AddDays(7);
			rental.IsExtended = true;

			await _rentalRepository.UpdateAsync(rental);
		}
	}
}
