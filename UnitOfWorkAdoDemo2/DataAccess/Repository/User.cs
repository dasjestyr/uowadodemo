namespace UnitOfWorkAdoDemo2.DataAccess.Repository
{
    public class User : IDbEntity
    {
        private int _id;
        private string _firstName;
        private string _lastName;

        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                IsDirty = true;
            }
        }

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                IsDirty = true;
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                IsDirty = true;
            }
        }

        public bool IsDirty { get; private set; }


    }

    public interface IDbEntity
    {
        bool IsDirty { get; }
    }
}