using AutoMapper;
using DataAccess.Repositories;
using Dto.Request;
using Dto.Response;
using Entities;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BookService : GenericServiceAsync<BookService, DtoBookResponse, DtoBookRequest>, IBookService
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
