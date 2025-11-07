using System;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace src.Pages;  // <- mantém igual ao Index.cshtml.cs

public class DbTestModel : PageModel
{
    private readonly IConfiguration _configuration;

    public string ServerTime { get; set; } = "";
    public string Status { get; set; } = "";

    public DbTestModel(IConfiguration configuration)
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

            using var cmd = new MySqlCommand("SELECT NOW()", conn);
            var result = cmd.ExecuteScalar();
            ServerTime = result?.ToString() ?? "(sem resultado)";
            Status = "Ligação com sucesso";
        }
        catch (Exception ex)
        {
            Status = "Erro: " + ex.Message;
        }
    }
}
