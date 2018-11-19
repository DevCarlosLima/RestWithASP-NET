using RestWithASPNET.Data.Converters;
using RestWithASPNET.Data.VO;
using RestWithASPNET.Models;
using RestWithASPNET.Repository.Generic;
using System.Collections.Generic;

namespace RestWithASPNET.Business.Implementations
{
    public class PersonBusiness : IPersonBusiness
    {
        private IGenericRepository<Person> _repository;
        private readonly PersonConverter _converter;

        public PersonBusiness(IGenericRepository<Person> repository)
        {
            _repository = repository;
            _converter = new PersonConverter();
        }

        public PersonVO Create(PersonVO PersonVO)
        {
            var person = _repository.Create(_converter.Parse(PersonVO));

            return _converter.Parse(person);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }

        public List<PersonVO> FindAll()
        {
            return _converter.ParseList(_repository.FindAll());
        }

        public PersonVO FindById(long id)
        {
            return _converter.Parse(_repository.FindById(id));
        }

        public PersonVO Update(PersonVO PersonVO)
        {
            var person = _repository.Update(_converter.Parse(PersonVO));

            return _converter.Parse(person);
        }
    }
}
