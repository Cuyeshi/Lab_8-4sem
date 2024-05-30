
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;

namespace Lab_8
{
    public partial class MainWindow : Window
    {
        private DataSet dataSet = new DataSet();
        private SqlDataAdapter medicalRecordsAdapter;
        private SqlDataAdapter doctorsAdapter;
        private SqlDataAdapter patientsAdapter;

        public MainWindow()
        {
            InitializeComponent();
            InitializeDataSet();
            LoadData();
        }

        private void InitializeDataSet()
        {
            // Initialize adapters
            string connectionString = "Data Source=DESKTOP-2GTDQ2V\\SQLEXPRESS;Initial Catalog=OOPaP_67;Integrated Security=True";
            medicalRecordsAdapter = new SqlDataAdapter("SELECT * FROM MedicalRecords", connectionString);
            doctorsAdapter = new SqlDataAdapter("SELECT * FROM Doctors", connectionString);
            patientsAdapter = new SqlDataAdapter("SELECT * FROM Patients", connectionString);

            // Fill DataTables
            medicalRecordsAdapter.Fill(dataSet, "MedicalRecords");
            doctorsAdapter.Fill(dataSet, "Doctors");
            patientsAdapter.Fill(dataSet, "Patients");

            // Set up relationships
            dataSet.Relations.Add("DoctorMedicalRecords",
                dataSet.Tables["Doctors"].Columns["DoctorID"],
                dataSet.Tables["MedicalRecords"].Columns["DoctorID"]);

            dataSet.Relations.Add("PatientMedicalRecords",
                dataSet.Tables["Patients"].Columns["PatientID"],
                dataSet.Tables["MedicalRecords"].Columns["PatientID"]);
        }

        private void LoadData()
        {
            var query = from medicalRecord in dataSet.Tables["MedicalRecords"].AsEnumerable()
                        join doctor in dataSet.Tables["Doctors"].AsEnumerable()
                        on medicalRecord.Field<int>("DoctorID") equals doctor.Field<int>("DoctorID")
                        join patient in dataSet.Tables["Patients"].AsEnumerable()
                        on medicalRecord.Field<int>("PatientID") equals patient.Field<int>("PatientID")
                        select new
                        {
                            DoctorID = medicalRecord.Field<int>("DoctorID"),
                            PatientID = medicalRecord.Field<int>("PatientID"),
                            DoctorName = doctor.Field<string>("FullName"),
                            PatientName = patient.Field<string>("FullName"),
                            BirthYear = patient.Field<int>("BirthYear"),
                            Height = patient.Field<decimal>("Height"),
                            Weight = patient.Field<decimal>("Weight"),
                            BloodPressure = patient.Field<string>("BloodPressure"),
                            Diagnosis = medicalRecord.Field<string>("Diagnosis"),
                            ExaminationDate = medicalRecord.Field<DateTime>("ExaminationDate").ToString("yyyy-MM-dd")
                        };

            MedicalRecordsDataGrid.ItemsSource = query.ToList();
        }

        private void EditTable_Click(object sender, RoutedEventArgs e)
        {
            if (MedicalRecordsDataGrid.SelectedItem is null)
            {
                MessageBox.Show("Please select a record to edit.");
            }
            else
            {
                var selectedRecord = (dynamic)MedicalRecordsDataGrid.SelectedItem;
                var editWindow = new EditRecordsWindow(dataSet, selectedRecord);
                editWindow.ShowDialog();
                LoadData(); // Refresh the data grid after editing
            }
        }
    }
}
