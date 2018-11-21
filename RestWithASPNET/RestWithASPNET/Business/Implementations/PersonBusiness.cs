using RestWithASPNET.Data.Converters;
using RestWithASPNET.Data.VO;
using RestWithASPNET.Models;
using RestWithASPNET.Repository.Generic;
using System;
using System.Collections.Generic;
using Tapioca.HATEOAS.Utils;

namespace RestWithASPNET.Business.Implementations
{
    public class PersonBusiness : IPersonBusiness
    {
        private IPersonRepository _repository;
        private readonly PersonConverter _converter;

        public PersonBusiness(IPersonRepository repository)
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

        public List<PersonVO> FindByName(string firstName, string lastName)
        {
            return _converter.ParseList(_repository.FindByName(firstName, lastName));
        }

        public PagedSearchDTO<PersonVO> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page)
        {
            page = page > 0 ? page - 1 : 0;
            string query = "SELECT * FROM PERSONS P ";
            if (!string.IsNullOrEmpty(name)) query += $"WHERE 1 = 1 AND P.FIRSTNAME LIKE '%{name}%' ";
            query += $"ORDER BY P.FIRSTNAME {sortDirection} LIMIT {pageSize} OFFSET {page * pageSize}";

            string countQuery = "SELECT * FROM PERSONS P ";
            if (!string.IsNullOrEmpty(name)) countQuery += $"WHERE 1 = 1 AND P.FIRSTNAME LIKE '%{name}%' ";

            var persons = _converter.ParseList(_repository.FindWithPagedSearch(query));
            var totalResults = _repository.GetCount(countQuery);

            return new PagedSearchDTO<PersonVO>
            {
                CurrentPage = page + 1,
                List = persons,
                PageSize = pageSize,
                SortDirections = sortDirection,
                TotalResults = totalResults
            };
        }
    }
}
