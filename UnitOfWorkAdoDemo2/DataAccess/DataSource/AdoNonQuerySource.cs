using System.Data;
using System.Diagnostics;

namespace UnitOfWorkAdoDemo2.DataAccess.DataSource
{
    public abstract class AdoNonQuerySource<TInput>
    {
        protected readonly IAdoUnitOfWork UnitOfWork;
        private readonly IDbCommand _command;

        protected string CommandText
        {
            get { return _command.CommandText; }
            set { _command.CommandText = value; }
        }

        protected AdoNonQuerySource(IAdoUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            _command = UnitOfWork.CreateCommand();
            _command.CommandType = CommandType.Text;
        }

        public IDataParameterCollection Execute(TInput input)
        {
            if (_command.ExecuteNonQuery() == 0)
                Trace.WriteLine("No operation was performed!");

            return _command.Parameters;
        }
    }
}