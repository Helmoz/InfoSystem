using System.Collections.Generic;
using System.Linq;
using InfoSystem.Core.Entities.Basic;
using InfoSystem.Infrastructure.DataBase.Context;
using InfoSystem.Infrastructure.DataBase.ReposInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Npgsql;

namespace InfoSystem.Infrastructure.DataBase.Repos
{
	public class TypeRepository : ITypeRepository
	{
		public TypeRepository(InfoSystemDbContext context)
		{
			_context = context;
		}

		public EntityType Add(string newTypeName, string requiredProperty)
		{
			newTypeName = newTypeName.ToLower();
			if (_context.Types.Any(t => t.Name == newTypeName))
				throw new NpgsqlException("EntityType already exists");

			var entityEntry = AddNewType(newTypeName, requiredProperty);
			AddAttributesTable(newTypeName);

			return entityEntry.Entity;
		}

		private void ExecuteSqlAndSave(string sqlQuery)
		{
			_context.Database.ExecuteSqlCommand(new RawSqlString(sqlQuery));
			_context.SaveChanges();
		}

		public IEnumerable<EntityType> Get() => _context.Types;

		public EntityType GetById(int id) => _context.Types.Find(id);

		private readonly InfoSystemDbContext _context;

		private void AddAttributesTable(string newTypeName)
		{
			var sqlQuery = SqlOptions.GenerateCreateAttributesTableScript(newTypeName);
			ExecuteSqlAndSave(sqlQuery);
		}

		private EntityEntry<EntityType> AddNewType(string newTypeName, string requiredProperty)
		{
			var entityEntry = _context.Types.Add(new EntityType(newTypeName, requiredProperty));
			var sqlQuery = SqlOptions.GenerateCreateTableScript(newTypeName);
			ExecuteSqlAndSave(sqlQuery);
			return entityEntry;
		}
	}
}