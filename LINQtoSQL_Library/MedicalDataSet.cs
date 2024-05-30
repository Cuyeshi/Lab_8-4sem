using System;
using System.Data;

namespace LINQtoData_Library
{
    public class MedicalDataSet : DataSet
    {
        public DataTable DoctorsTable { get; private set; }
        public DataTable PatientsTable { get; private set; }
        public DataTable MedicalRecordsTable { get; private set; }

        public MedicalDataSet()
        {
            DoctorsTable = new DataTable("Doctors");
            DoctorsTable.Columns.Add("DoctorID", typeof(int));
            DoctorsTable.Columns.Add("FullName", typeof(string));
            DoctorsTable.PrimaryKey = new DataColumn[] { DoctorsTable.Columns["DoctorID"] };

            PatientsTable = new DataTable("Patients");
            PatientsTable.Columns.Add("PatientID", typeof(int));
            PatientsTable.Columns.Add("FullName", typeof(string));
            PatientsTable.Columns.Add("BirthYear", typeof(int));
            PatientsTable.Columns.Add("Height", typeof(int));
            PatientsTable.Columns.Add("Weight", typeof(int));
            PatientsTable.Columns.Add("BloodPressure", typeof(string));
            PatientsTable.PrimaryKey = new DataColumn[] { PatientsTable.Columns["PatientID"] };

            MedicalRecordsTable = new DataTable("MedicalRecords");
            MedicalRecordsTable.Columns.Add("RecordID", typeof(int));
            MedicalRecordsTable.Columns.Add("DoctorID", typeof(int));
            MedicalRecordsTable.Columns.Add("PatientID", typeof(int));
            MedicalRecordsTable.Columns.Add("Diagnosis", typeof(string));
            MedicalRecordsTable.Columns.Add("ExaminationDate", typeof(DateTime));
            MedicalRecordsTable.PrimaryKey = new DataColumn[] { MedicalRecordsTable.Columns["RecordID"] };

            Tables.Add(DoctorsTable);
            Tables.Add(PatientsTable);
            Tables.Add(MedicalRecordsTable);

            DataRelation relationDoctor = new DataRelation("Doctor_MedicalRecord", DoctorsTable.Columns["DoctorID"], MedicalRecordsTable.Columns["DoctorID"]);
            DataRelation relationPatient = new DataRelation("Patient_MedicalRecord", PatientsTable.Columns["PatientID"], MedicalRecordsTable.Columns["PatientID"]);

            Relations.Add(relationDoctor);
            Relations.Add(relationPatient);
        }
    }
}
