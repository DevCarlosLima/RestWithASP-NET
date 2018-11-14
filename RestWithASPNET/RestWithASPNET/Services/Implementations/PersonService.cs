using RestWithASPNET.Models;
using RestWithASPNET.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestWithASPNET.Services.Implementations
{
    public class PersonService : IPersonService
    {
        private MySQLContext _context;

        public PersonService(MySQLContext context)
        {
            _context = context;
        }

        public Person Create(Person person)
        {
            try
            {
                _context.Persons.Add(person);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return person;
        }

        public void Delete(long id)
        {
            var result = _context.Persons.SingleOrDefault(p => p.Id.Equals(id));
            
            try
            {
                if (result != null)
                {
                    _context.Persons.Remove(result);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Person> FindAll()
        {
            return _context.Persons.ToList();
        }

        public Person FindById(long id)
        {
            return new Person
            {
                Id = id,
                FirstName = "Carlos",
                LastName = "Lima",
                Address = "Guarulhos - SP",
                Gender = "Male"
            };
        }

        public Person Update(Person person)
        {
            if (!Exist(person.Id)) return new Person();

            var result = _context.Persons.SingleOrDefault(p => p.Id.Equals(person.Id));

            try
            {
                _context.Entry(person).CurrentValues.SetValues(person);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }

            return person;
        }

        private bool Exist(long? id)
        {
            return _context.Persons.Any(p => p.Id.Equals(id));
        }
    }
}
