using System.Collections.Generic;
using System.Data;

namespace UnitOfWorkAdoDemo2.DataAccess.DataSource
{
    public abstract class AdoQuerySource<TInput, TOutput>
    {
        protected readonly IAdoUnitOfWork UnitOfWWork;
        protected readonly IDbCommand Command;

        protected string CommandText
        {
            get { return Command.CommandText; }
            set { Command.CommandText = value; }
        }

        protected AdoQuerySource(IAdoUnitOfWork unitOfWork)
        {
            UnitOfWWork = unitOfWork;
            Command = UnitOfWWork.CreateCommand();
            Command.CommandType = CommandType.Text;
        }

        public IEnumerable<TOutput> Execute(TInput input)
        {

            using (var rdr = Command.ExecuteReader())
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
    }
}