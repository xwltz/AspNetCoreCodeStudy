using System.Collections.Generic;
using Models;

namespace Services
{
    public class PersonRepository : IPersonRepository
    {
        private readonly List<Person> _person = new List<Person>();

        public PersonRepository()
        {
            Add(new Person {Id = 1, Name = "joye.net1", Age = 18, Address = "中国上海"});
            Add(new Person {Id = 2, Name = "joye.net2", Age = 18, Address = "中国上海"});
            Add(new Person {Id = 3, Name = "joye.net3", Age = 18, Address = "中国上海"});
        }

        public IEnumerable<Person> GetAll()
        {
            return _person;
        }

        public Person Get(int id)
        {
            return _person.Find(p => p.Id == id);
        }

        public Person Add(Person item)
        {
            _person.Add(item);
            return item;
        }

        public bool Update(Person item)
        {
            var index = _person.FindIndex(p => p.Id == item.Id);
            if (index == -1) return false;
            _person.RemoveAt(index);
            _person.Add(item);
            return true;
        }

        public bool Delete(int id)
        {
            _person.RemoveAll(p => p.Id == id);
            return true;
        }
    }
}