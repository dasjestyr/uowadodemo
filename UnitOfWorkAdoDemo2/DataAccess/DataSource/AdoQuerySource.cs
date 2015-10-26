using System;
using System.Collections.Generic;
using System.Data;

namespace UnitOfWorkAdoDemo2.DataAccess.DataSource
{
    public abstract class AdoQuerySource<TOutput> 
        where TOutput : class
    {
        private readonly IAdoUnitOfWork _unitOfWork;
        private readonly IDbCommand _command;
        
        protected AdoQuerySource(IAdoUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _command = _unitOfWork.CreateCommand();
            _command.CommandType = CommandType.Text;
        }

        public IEnumerable<TOutput> Execute()
        {
            if(_unitOfWork.Connection.State != ConnectionState.Open)
                _unitOfWork.Connection.Open();

            _command.CommandText = GetCommandText();
            
            using (var rdr = _command.ExecuteReader(CommandBehavior.CloseConnection))
            {
                var items = new List<TOutput>();
                while (rdr.Read())
                {
                    items.Add(GetFromReader(rdr));
                }
                
                return items;
            }
        }

        protected abstract TOutput GetFromReader(IDataRecord rdr);

        protected abstract string GetCommandText();
    }
}