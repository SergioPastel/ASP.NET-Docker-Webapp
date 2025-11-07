using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace src.Pages;

public class PessoasModel : PageModel
{
    private readonly IConfiguration _configuration;

    public string Status { get; set; } = "";
    public List<Pessoa> Pessoas { get; set; } = new List<Pessoa>();

    public PessoasModel(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void OnGet()
    {
        try
        {
            var connStr = _configuration.GetConnectionString("DefaultConnection");

            using var conn = new MySqlConnection(connStr);
            conn.Open();

            using var cmd = new MySqlCommand("SELECT Id, Nome, Email FROM pessoas ORDER BY Id", conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Pessoas.Add(new Pessoa
                {
                    Id = reader.GetInt32("Id"),
                    Nome = reader.GetString("Nome"),
                    Email = reader.GetString("Email")
                });
            }

            Status = "Ligação com sucesso";
        }
        catch (Exception ex)
        {
            Status = "Erro: " + ex.Message;
        }
    }
}

public class Pessoa
{
    public int Id { get; set; }
    public string Nome { get; set; } = "";
    public string Email { get; set; } = "";
}
