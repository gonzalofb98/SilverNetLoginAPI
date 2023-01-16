using Dto.Request;
using Dto.Response;
using Entities;

namespace Services.Interfaces
{
    public interface IBookService :IGenericServiceAsync<Book, DtoBookResponse, DtoBookRequest>
    {
    }
}
