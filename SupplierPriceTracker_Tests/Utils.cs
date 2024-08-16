using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SupplierPriceTracker.Data;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplierPriceTracker_Tests
{
	public static class Utils
	{
		public static void InstantiateDB(out DbConnection? _connection, out ApplicationDbContext? _context)
		{
			_connection = new SqliteConnection("DataSource=:memory:");
			_connection.Open();
			var options = new
			DbContextOptionsBuilder<ApplicationDbContext>()
			.UseSqlite(_connection)
				.Options;
			_context = new ApplicationDbContext(options);
			_context.Database.EnsureCreatedAsync();
		}
	}
}
