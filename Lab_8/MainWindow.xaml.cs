using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using LINQtoData_Library;

namespace Lab_8
{
    public partial class MainWindow : Window
    {
        private readonly MedicalDataSet _dataSet = new MedicalDataSet();
        private readonly string _connectionString = "Data Source=DESKTOP-2GTDQ2V\\SQLEXPRESS;Initial Catalog=OOPaP_67;Integrated Security=True";

        public MainWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlDataAdapter doctorAdapter = new SqlDataAdapter("SELECT * FROM Doctors", connection);
                doctorAdapter.Fill(_dataSet.DoctorsTable);

                SqlDataAdapter patientAdapter = new SqlDataAdapter("SELECT * FROM Patients", connection);
                patientAdapter.Fill(_dataSet.PatientsTable);

                SqlDataAdapter medicalRecordAdapter = new SqlDataAdapter("SELECT * FROM MedicalRecords", connection);
                medicalRecordAdapter.Fill(_dataSet.MedicalRecordsTable);
            }

            var query = from record in _dataSet.MedicalRecordsTable.AsEnumerable()
                        join doctor in _dataSet.DoctorsTable.AsEnumerable() on record.Field<int>("DoctorID") equals doctor.Field<int>("DoctorID")
                        join patient in _dataSet.PatientsTable.AsEnumerable() on record.Field<int>("PatientID") equals patient.Field<int>("PatientID")
                        select new
                        {
                            DoctorName = doctor.Field<string>("FullName"),
                            PatientName = patient.Field<string>("FullName"),
                            BirthYear = patient.Field<int>("BirthYear"),
                            Height = patient.Field<int>("Height"),
                            Weight = patient.Field<int>("Weight"),
                            BloodPressure = patient.Field<string>("BloodPressure"),
                            Diagnosis = record.Field<string>("Diagnosis"),
                            ExaminationDate = record.Field<DateTime>("ExaminationDate")
                        };

            MedicalRecordsDataGrid.ItemsSource = query.ToList();
        }

        private void EditRecords_Click(object sender, RoutedEventArgs e)
        {
            EditRecordsWindow editWindow = new EditRecordsWindow(_dataSet, this);
            editWindow.ShowDialog();
        }

        public void CreateRecord(MedicalRecord record, Patient patient)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        SqlCommand insertPatientCommand = new SqlCommand(
                            "INSERT INTO Patients (FullName, BirthYear, Height, Weight, BloodPressure) VALUES (@FullName, @BirthYear, @Height, @Weight, @BloodPressure); SELECT CAST(scope_identity() AS int)",
                            connection, transaction);
                        insertPatientCommand.Parameters.AddWithValue("@FullName", patient.FullName);
                        insertPatientCommand.Parameters.AddWithValue("@BirthYear", patient.BirthYear);
                        insertPatientCommand.Parameters.AddWithValue("@Height", patient.Height);
                        insertPatientCommand.Parameters.AddWithValue("@Weight", patient.Weight);
                        insertPatientCommand.Parameters.AddWithValue("@BloodPressure", patient.BloodPressure);

                        patient.PatientID = (int)insertPatientCommand.ExecuteScalar();

                        SqlCommand insertMedicalRecordCommand = new SqlCommand(
                            "INSERT INTO MedicalRecords (DoctorID, PatientID, Diagnosis, ExaminationDate) VALUES (@DoctorID, @PatientID, @Diagnosis, @ExaminationDate)",
                            connection, transaction);
                        insertMedicalRecordCommand.Parameters.AddWithValue("@DoctorID", record.DoctorID);
                        insertMedicalRecordCommand.Parameters.AddWithValue("@PatientID", patient.PatientID);
                        insertMedicalRecordCommand.Parameters.AddWithValue("@Diagnosis", record.Diagnosis);
                        insertMedicalRecordCommand.Parameters.AddWithValue("@ExaminationDate", record.ExaminationDate);

                        insertMedicalRecordCommand.ExecuteNonQuery();

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show($"Error: {ex.Message}");
                    }
                }
            }
            LoadData();
        }

        public void UpdateRecord(MedicalRecord record, Patient patient)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        SqlCommand updatePatientCommand = new SqlCommand(
                            "UPDATE Patients SET FullName = @FullName, BirthYear = @BirthYear, Height = @Height, Weight = @Weight, BloodPressure = @BloodPressure WHERE PatientID = @PatientID",
                            connection, transaction);
                        updatePatientCommand.Parameters.AddWithValue("@FullName", patient.FullName);
                        updatePatientCommand.Parameters.AddWithValue("@BirthYear", patient.BirthYear);
                        updatePatientCommand.Parameters.AddWithValue("@Height", patient.Height);
                        updatePatientCommand.Parameters.AddWithValue("@Weight", patient.Weight);
                        updatePatientCommand.Parameters.AddWithValue("@BloodPressure", patient.BloodPressure);
                        updatePatientCommand.Parameters.AddWithValue("@PatientID", patient.PatientID);

                        updatePatientCommand.ExecuteNonQuery();

                        SqlCommand updateMedicalRecordCommand = new SqlCommand(
                            "UPDATE MedicalRecords SET DoctorID = @DoctorID, Diagnosis = @Diagnosis, ExaminationDate = @ExaminationDate WHERE RecordID = @RecordID",
                            connection, transaction);
                        updateMedicalRecordCommand.Parameters.AddWithValue("@DoctorID", record.DoctorID);
                        updateMedicalRecordCommand.Parameters.AddWithValue("@Diagnosis", record.Diagnosis);
                        updateMedicalRecordCommand.Parameters.AddWithValue("@ExaminationDate", record.ExaminationDate);
                        updateMedicalRecordCommand.Parameters.AddWithValue("@RecordID", record.RecordID);

                        updateMedicalRecordCommand.ExecuteNonQuery();

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show($"Error: {ex.Message}");
                    }
                }
            }
            LoadData();
        }

        public void DeleteRecord(MedicalRecord record)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        SqlCommand deleteMedicalRecordCommand = new SqlCommand(
                            "DELETE FROM MedicalRecords WHERE RecordID = @RecordID",
                            connection, transaction);
                        deleteMedicalRecordCommand.Parameters.AddWithValue("@RecordID", record.RecordID);

                        deleteMedicalRecordCommand.ExecuteNonQuery();

                        SqlCommand deletePatientCommand = new SqlCommand(
                            "DELETE FROM Patients WHERE PatientID = @PatientID",
                            connection, transaction);
                        deletePatientCommand.Parameters.AddWithValue("@PatientID", record.PatientID);

                        deletePatientCommand.ExecuteNonQuery();

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show($"Error: {ex.Message}");
                    }
                }
            }
            LoadData();
        }

    }
}
