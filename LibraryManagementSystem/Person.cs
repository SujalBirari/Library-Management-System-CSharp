using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem
{
    internal abstract class Person
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime DateCreated { get; set; }

        protected Person(string id, string name, string email, string phone)
        {
            Id = id;
            Name = name;
            Email = email;
            Phone = phone;
            DateCreated = DateTime.Now;
        }

        public abstract void DisplayInfo();

        public virtual string GetFullDetails()
        {
            return $"ID: {Id}, Name: {Name}, Email: {Email}, Phone: {Phone}";
        }
    }
}
