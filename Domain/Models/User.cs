using System;
using System.Collections.Generic;

namespace YourProject.Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty; // ← вот это нужно
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public bool IsAdmin { get; set; } = false;
        public User() { }

        public User(int id, string email, string password, string firstName, string lastName, string phone)
        {
            Id = id;
            Email = email;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
        }
    }



    public class Address
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Country { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        public bool IsDefault { get; set; }

        public Address(int id, int userId, string country, string city, string street, string zipCode, bool isDefault)
        {
            Id = id;
            UserId = userId;
            Country = country;
            City = city;
            Street = street;
            ZipCode = zipCode;
            IsDefault = isDefault;
        }
    }

    public class UserWithAddress
    {
        public User User { get; set; }
        public List<Address> Addresses { get; set; } = new List<Address>();

        public UserWithAddress(User user, List<Address> addresses)
        {
            User = user;
            Addresses = addresses ?? new List<Address>();
        }
    }
}
