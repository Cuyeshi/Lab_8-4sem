
using System.Data.Linq;

namespace LINQtoSQL_Library
{
    public partial class MedicalDataContext : DataContext
    {
        public Table<Doctors> Doctors;
        public Table<Patients> Patients;
        public Table<MedicalRecords> MedicalRecords;

        public MedicalDataContext(string connection) : base(connection) { }
    }

}
