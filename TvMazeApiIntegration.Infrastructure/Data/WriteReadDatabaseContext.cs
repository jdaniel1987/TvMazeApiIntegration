using Microsoft.EntityFrameworkCore;

namespace TvMazeApiIntegration.Infrastructure.Data;

public class WriteReadDatabaseContext(DbContextOptions<WriteReadDatabaseContext> options) : BaseDatabaseContext(options)
{

}
