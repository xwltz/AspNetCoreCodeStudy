using System.Collections.Generic;
using Models;

namespace Services
{
    public interface IPersonRepository
    {
        IEnumerable<Person> GetAll();
        Person Get(int id);
        Person Add(Person item);
        bool Update(Person item);
        bool Delete(int id);
    }
}