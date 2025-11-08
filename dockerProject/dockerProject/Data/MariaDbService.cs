using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using dockerProject.Models;

namespace dockerProject.Data
{
    public class MariaDbService
    {
        private readonly string _connectionString;

        public MariaDbService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        }

        // TESTE de ligação
        public async Task<string> TestConnectionAsync()
        {
            try
            {
                await using var conn = new MySqlConnection(_connectionString);
                await conn.OpenAsync();

                await using var cmd = new MySqlCommand("SELECT 1;", conn);
                await cmd.ExecuteScalarAsync();

                return "Ligação à MariaDB OK";
            }
            catch (Exception ex)
            {
                return $"Erro na ligação à MariaDB: {ex.Message}";
            }
        }

        // LISTAR TODOS OS STUDENTS
        public async Task<List<Student>> GetStudentsAsync()
        {
            var students = new List<Student>();

            await using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            const string sql = @"
                SELECT Id, Name, Email, Nif, DateOfBirth
                FROM students
                ORDER BY Id DESC;";

            await using var cmd = new MySqlCommand(sql, conn);
            await using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var s = new Student
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Email = reader.GetString(2),
                    Nif = reader.GetInt32(3),
                    DateOfBirth = DateOnly.FromDateTime(reader.GetDateTime(4))
                };

                students.Add(s);
            }

            return students;
        }

        // OBTÉM UM STUDENT POR ID
        public async Task<Student?> GetStudentByIdAsync(int id)
        {
            await using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            const string sql = @"
                SELECT Id, Name, Email, Nif, DateOfBirth
                FROM students
                WHERE Id = @Id;";

            await using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Id", id);

            await using var reader = await cmd.ExecuteReaderAsync();

            if (!await reader.ReadAsync())
            {
                return null;
            }

            return new Student
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Email = reader.GetString(2),
                Nif = reader.GetInt32(3),
                DateOfBirth = DateOnly.FromDateTime(reader.GetDateTime(4))
            };
        }

        // ADICIONAR STUDENT
        public async Task AddStudentAsync(Student student)
        {
            await using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            const string sql = @"
                INSERT INTO students (Name, Email, Nif, DateOfBirth)
                VALUES (@Name, @Email, @Nif, @DateOfBirth);";

            await using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Name", student.Name);
            cmd.Parameters.AddWithValue("@Email", student.Email);
            cmd.Parameters.AddWithValue("@Nif", student.Nif);
            cmd.Parameters.AddWithValue("@DateOfBirth", student.DateOfBirth.ToDateTime(TimeOnly.MinValue));

            await cmd.ExecuteNonQueryAsync();
        }

        // ATUALIZAR STUDENT
        public async Task UpdateStudentAsync(Student student)
        {
            await using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            const string sql = @"
                UPDATE students
                SET Name = @Name,
                    Email = @Email,
                    Nif = @Nif,
                    DateOfBirth = @DateOfBirth
                WHERE Id = @Id;";

            await using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Id", student.Id);
            cmd.Parameters.AddWithValue("@Name", student.Name);
            cmd.Parameters.AddWithValue("@Email", student.Email);
            cmd.Parameters.AddWithValue("@Nif", student.Nif);
            cmd.Parameters.AddWithValue("@DateOfBirth", student.DateOfBirth.ToDateTime(TimeOnly.MinValue));

            await cmd.ExecuteNonQueryAsync();
        }

        // APAGAR STUDENT
        public async Task DeleteStudentAsync(int id)
        {
            await using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            const string sql = "DELETE FROM students WHERE Id = @Id;";

            await using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Id", id);

            await cmd.ExecuteNonQueryAsync();
        }

        public async Task EnsureSeedDataAsync()
        {
            await using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            // 1) Garante que a tabela students existe
            const string createTableSql = @"
                CREATE TABLE IF NOT EXISTS students (
                    Id INT AUTO_INCREMENT PRIMARY KEY,
                    Name VARCHAR(100) NOT NULL,
                    Email VARCHAR(100) NOT NULL,
                    Nif INT NOT NULL,
                    DateOfBirth DATE NOT NULL
                );";

            await using (var createCmd = new MySqlCommand(createTableSql, conn))
            {
                await createCmd.ExecuteNonQueryAsync();
            }

            // 2) Verifica se já existem dados
            const string countSql = "SELECT COUNT(*) FROM students;";
            await using (var countCmd = new MySqlCommand(countSql, conn))
            {
                var countObj = await countCmd.ExecuteScalarAsync();
                int count = Convert.ToInt32(countObj);

                if (count > 0)
                {
                    // Já há dados entao não faz seed outra vez
                    return;
                }
            }

            // 3) Se chegou aqui, a tabela está vazia → insere dados de exemplo
            const string insertSql = @"
                INSERT INTO students (Name, Email, Nif, DateOfBirth)
                VALUES (@Name, @Email, @Nif, @DateOfBirth);";

            // Alice
            await using (var cmd = new MySqlCommand(insertSql, conn))
            {
                cmd.Parameters.AddWithValue("@Name", "Alice");
                cmd.Parameters.AddWithValue("@Email", "alice@example.com");
                cmd.Parameters.AddWithValue("@Nif", 123456789);
                cmd.Parameters.AddWithValue("@DateOfBirth", new DateTime(2000, 1, 1));
                await cmd.ExecuteNonQueryAsync();
            }

            // Bob
            await using (var cmd = new MySqlCommand(insertSql, conn))
            {
                cmd.Parameters.AddWithValue("@Name", "Bob");
                cmd.Parameters.AddWithValue("@Email", "bob@example.com");
                cmd.Parameters.AddWithValue("@Nif", 987654321);
                cmd.Parameters.AddWithValue("@DateOfBirth", new DateTime(1998, 5, 15));
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }
    
    
}
