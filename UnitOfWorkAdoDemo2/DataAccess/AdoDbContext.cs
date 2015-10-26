using System;
using System.Collections.Generic;
using System.Data;
using UnitOfWorkAdoDemo2.DataAccess.DataSource;

namespace UnitOfWorkAdoDemo2.DataAccess
{
    public class AdoDbContext : IAdoUnitOfWork
    {
        private IDbTransaction _transaction;
        private readonly List<AdoNonQuerySource> _nonQueryActions = new List<AdoNonQuerySource>();

        public IDbConnection Connection { get; }

        public bool IsDisposed { get; private set; }

        public AdoDbContext(IDbConnection connection)
        {
            Connection = connection;
        }

        public IDbCommand CreateCommand()
        {
            var command = Connection.CreateCommand();
            command.Transaction = _transaction;
            return command;
        }

        public void Commit()
        {
            if(Connection.State != ConnectionState.Open)
                Connection.Open();

            _transaction = Connection.BeginTransaction();

            try
            {
                // run updates
                foreach (var command in _nonQueryActions)
                {
                    command.Execute();
                }

                _transaction.Commit();
                _nonQueryActions.Clear();
                _transaction = null;
            }
            catch(Exception)
            {
                _transaction?.Rollback();
                throw;
            }
        }

        public void AddNonQuery(AdoNonQuerySource source)
        {
            _nonQueryActions.Add(source);
        }

        public void Dispose()
        {
            if (IsDisposed)
                return;

            _transaction?.Rollback();

            if (Connection.State == ConnectionState.Open)
                Connection.Close();

            IsDisposed = true;
        }
    }
}