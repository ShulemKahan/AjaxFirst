using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AjaxDemo.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }

    public class PeopleDb
    {
        private string _connectionString;

        public PeopleDb(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Person> GetAll()
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM People";
            conn.Open();
            var reader = cmd.ExecuteReader();
            List<Person> ppl = new();
            while (reader.Read())
            {
                ppl.Add(new Person
                {
                    Id = (int)reader["Id"],
                    FirstName = (string)reader["FirstName"],
                    LastName = (string)reader["LastName"],
                    Age = (int)reader["Age"],

                });
            }

            return ppl;
        }

        public void AddPerson(Person person)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO People (FirstName, LastName, Age) " +
                "VALUES(@f, @l, @a) SELECT SCOPE_IDENTITY()";
            cmd.Parameters.AddWithValue("@f", person.FirstName);
            cmd.Parameters.AddWithValue("@l", person.LastName);
            cmd.Parameters.AddWithValue("@a", person.Age);
            conn.Open();
            person.Id = (int)(decimal)cmd.ExecuteScalar();
        }

        public void Delete(int id)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM People WHERE Id = @id";
            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();
           cmd.ExecuteNonQuery();
        }

        public void Edit(Person person)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "UPDATE People SET FirstName = @first, LastName = @last, Age = @age WHERE Id = @id";
            cmd.Parameters.AddWithValue("@id", person.Id);
            cmd.Parameters.AddWithValue("@first", person.FirstName);
            cmd.Parameters.AddWithValue("@last", person.LastName);
            cmd.Parameters.AddWithValue("@age", person.Age);
            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }
}
