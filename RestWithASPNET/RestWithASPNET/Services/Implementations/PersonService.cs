using RestWithASPNET.Models;
using System;
using System.Collections.Generic;

namespace RestWithASPNET.Services.Implementations
{
    public class PersonService : IPersonService
    {
        public Person Create(Person person)
        {
            return person;
        }

        public void Delete(long id)
        {
        }

        public List<Person> FindAll()
        {
            List<Person> persons = new List<Person>();

            for (int i = 0; i < 8; i++)
            {
                Person person = MockPerson(i);

                persons.Add(person);
            }

            return persons;
        }

        private Person MockPerson(int i)
        {
            return new Person
            {
                Id = i,
                FirstName = "FName " + i.ToString(),
                LastName = "LName " + i.ToString(),
                Address = "Address " + i.ToString(),
                Gender = "Gender " + i.ToString()
            };
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
            return person;
        }
    }
}
