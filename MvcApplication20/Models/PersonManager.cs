using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Microsoft.SqlServer.Server;

namespace MvcApplication20.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }

    public class PersonManager
    {
        private string _connectionString;

        public PersonManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Person> SearchPeople(string name)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM People";
                if (!String.IsNullOrEmpty(name))
                {
                    command.CommandText += " WHERE FirstName LIKE @name";
                    command.Parameters.AddWithValue("@name", "%" + name + "%");
                }
                connection.Open();
                List<Person> people = new List<Person>();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Person p = new Person();
                    p.Id = (int)reader["Id"];
                    p.FirstName = (string)reader["FirstName"];
                    p.LastName = (string)reader["LastName"];
                    p.Age = (int)reader["Age"];
                    people.Add(p);
                }

                return people;
            }
        }

        public void Add(Person person)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "INSERT INTO People (FirstName, LastName, Age) " +
                                      "Values (@firstName, @lastName, @age)";
                command.Parameters.AddWithValue("@firstName", person.FirstName);
                command.Parameters.AddWithValue("@lastName", person.LastName);
                command.Parameters.AddWithValue("@age", person.Age);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int personId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "DELETE FROM People WHERE Id = @personId";
                command.Parameters.AddWithValue("@personId", personId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}