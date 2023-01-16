using Entities;

namespace DataAccess.Repositories
{
    public class BookRepository : GenericRepositoryAsync<Book>, IBookRepository
    {
        private readonly Context _context;

        public BookRepository(Context context):base(context)
        {
            _context = context;
        }

    }
}
