using System;
using System.Collections.Generic;

namespace DigitalBSignageApp
{
    public class Person
    {
        public string Name { get; set; }
        public string Age { get; set; }
        public string Job { get; set; }
        List<Person> _people { get; set; }

        public Person()
        {
            _people.Add(new Person()
            {
                Name = "Clovis",
                Age = "25",
                Job = "Designer"
            });

            _people.Add(new Person()
            {
                Name = "Scott",
                Age = "40",
                Job = "Developer"
            });

            _people.Add(new Person()
            {
                Name = "Wael",
                Age = "54",
                Job = "CEO"
            });
        }

        public List<Person> ReturnPeople(){
            return _people;
        }
    }
}
