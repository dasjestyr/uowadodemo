using System.Data;
using System.Diagnostics;

namespace UnitOfWorkAdoDemo2.DataAccess.DataSource
{
    public abstract class AdoNonQuerySource
    {
        private readonly IDbCommand _command;

        protected AdoNonQuerySource(IAdoUnitOfWork unitOfWork)
        {
            _command = unitOfWork.CreateCommand();
            _command.CommandType = CommandType.Text;
        }

        public IDataParameterCollection Execute()
        {
            _command.CommandText = GetCommand();

            if (_command.ExecuteNonQuery() == 0)
                Trace.WriteLine("No operation was performed!");
            
            return _command.Parameters;
        }

        protected abstract string GetCommand();
    }
}