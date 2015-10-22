using System;
using System.Data;

namespace UnitOfWorkAdoDemo2.DataAccess
{
    public class AdoUnitOfWork : IAdoUnitOfWork
    {
        private IDbTransaction _transaction;
        private IDbConnection _connection;
        private readonly bool _ownsConnection;

        public bool IsDisposed { get; private set; }

        public AdoUnitOfWork(IDbConnection connection, bool ownsConnection = true)
        {
            _connection = connection;
            _ownsConnection = ownsConnection;

            if (connection.State != ConnectionState.Open)
                connection.Open();

            _transaction = _connection.BeginTransaction();
        }

        public IDbCommand CreateCommand()
        {
            var command = _connection.CreateCommand();
            command.Transaction = _transaction;
            return command;
        }

        public void Complete()
        {
            if (_transaction == null)
                throw new InvalidOperationException("There was no transaction set. Was it already committed?");

            _transaction.Commit();
            _transaction = null;
        }

        public void Dispose()
        {
            if (_transaction != null)
            {
                _transaction.Rollback();
                _transaction = null;
            }

            if (_connection != null && _ownsConnection)
            {
                _connection.Close();
                _connection = null;
            }

            IsDisposed = true;
        }
    }
}