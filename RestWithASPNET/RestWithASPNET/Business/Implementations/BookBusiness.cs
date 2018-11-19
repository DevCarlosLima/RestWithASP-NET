using RestWithASPNET.Data.Converters;
using RestWithASPNET.Data.VO;
using RestWithASPNET.Models;
using RestWithASPNET.Repository.Generic;
using System.Collections.Generic;

namespace RestWithASPNET.Business.Implementations
{
    public class BookBusiness : IBookBusiness
    {
        private readonly IGenericRepository<Book> _repository;
        private readonly BookConverter _converter;

        public BookBusiness(IGenericRepository<Book> repository)
        {
            _repository = repository;
            _converter = new BookConverter();
        }

        public BookVO Create(BookVO bookVO)
        {
            var book = _repository.Create(_converter.Parse(bookVO));
            return _converter.Parse(book);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }

        public List<BookVO> FindAll()
        {
            return _converter.ParseList(_repository.FindAll());
        }

        public BookVO FindById(long id)
        {
            return _converter.Parse(_repository.FindById(id));
        }

        public BookVO Update(BookVO BookVO)
        {
            var book = _repository.Update(_converter.Parse(BookVO));
            return _converter.Parse(book);
        }
        
        public bool Exists(long id)
        {
            return _repository.Exists(id);
        }
    }
}
