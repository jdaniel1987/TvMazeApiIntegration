using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

namespace TvMazeApiIntegration.Infrastructure.Data.DatabaseSeed;

public static class DatabaseCreation
{
    public static void CreateDatabase(IConfiguration configuration)
    {
        var databaseName = "TvMazeApiIntegration";
        var connectionString = configuration.GetConnectionString("DbCreation");
        var scriptsDir = "../TvMazeApiIntegration.Infrastructure/Data/DatabaseSeed/Scripts";

        var serverConnection = new ServerConnection(new SqlConnection(connectionString));
        var server = new Server(serverConnection);

        if (!server.Databases.Contains(databaseName))
        {
            var database = new Database(server, databaseName);
            database.Create();

            connectionString = configuration.GetConnectionString("DbTablesCreation");
            foreach (var scriptFile in Directory.GetFiles(scriptsDir, "*.sql", SearchOption.AllDirectories))
            {
                var script = File.ReadAllText(scriptFile);
                using var connection = new SqlConnection(connectionString);
                connection.Open();
                var command = new SqlCommand(script, connection);
                command.ExecuteNonQuery();
            }
        }
    }
}
