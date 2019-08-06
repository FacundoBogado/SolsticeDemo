using System;
using System.ComponentModel.DataAnnotations;

namespace SolsticeDemo.Models
{
    public class Contact
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Company { get; set; }
        
        public string ProfilePhoto { get; set; }
        
        public string Email { get; set; }
        
        public DateTime BirthDate { get; set; }
        
        public int PhoneNumber { get; set; }
        
        public Address Address { get; set; }
    }
}