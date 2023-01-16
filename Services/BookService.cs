using AutoMapper;
using DataAccess.Repositories;
using Dto.Request;
using Dto.Response;
using Entities;
using Services.Interfaces;

namespace Services
{
    public class BookService : GenericServiceAsync<Book, DtoBookResponse, DtoBookRequest>, IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BookService(IBookRepository bookRepository, IMapper mapper) : 
            base(bookRepository,mapper)
        {
           _bookRepository= bookRepository;
            _mapper= mapper;
        }
    }
}
