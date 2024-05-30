using System;
using System.Data;
using System.Linq;
using System.Windows;

namespace Lab_8
{
    public partial class EditRecordsWindow : Window
    {
        private DataSet dataSet;
        private dynamic selectedRecord;

        public EditRecordsWindow(DataSet ds, dynamic record = null)
        {
            InitializeComponent();
            dataSet = ds;
            selectedRecord = record;
            LoadComboBoxes();
            if (selectedRecord != null)
            {
                PopulateFields();
            }
        }

        private void LoadComboBoxes()
        {
            var doctors = dataSet.Tables["Doctors"].AsEnumerable()
                .Select(row => new
                {
                    DoctorID = row.Field<int>("DoctorID"),
                    FullName = row.Field<string>("FullName")
                }).ToList();

            DoctorsComboBox.ItemsSource = doctors;
            DoctorsComboBox.DisplayMemberPath = "FullName";
            DoctorsComboBox.SelectedValuePath = "DoctorID";

            var diagnoses = dataSet.Tables["MedicalRecords"].AsEnumerable()
                .Select(row => row.Field<string>("Diagnosis"))
                .Distinct().ToList();

            DiagnosisComboBox.ItemsSource = diagnoses;
        }

        private void PopulateFields()
        {
            DoctorsComboBox.SelectedValue = selectedRecord.DoctorID;
            PatientNameTextBox.Text = selectedRecord.PatientName;
            BirthYearTextBox.Text = selectedRecord.BirthYear.ToString();
            HeightTextBox.Text = selectedRecord.Height.ToString();
            WeightTextBox.Text = selectedRecord.Weight.ToString();
            BloodPressureTextBox.Text = selectedRecord.BloodPressure;
            DiagnosisComboBox.SelectedItem = selectedRecord.Diagnosis;
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            var doctor = DoctorsComboBox.SelectedItem;
            var diagnosis = DiagnosisComboBox.SelectedItem as string;

            if (doctor == null || string.IsNullOrEmpty(diagnosis) ||
                string.IsNullOrEmpty(PatientNameTextBox.Text) ||
                string.IsNullOrEmpty(BirthYearTextBox.Text) ||
                string.IsNullOrEmpty(HeightTextBox.Text) ||
                string.IsNullOrEmpty(WeightTextBox.Text) ||
                string.IsNullOrEmpty(BloodPressureTextBox.Text))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            DataRow newPatientRow = dataSet.Tables["Patients"].NewRow();
            newPatientRow["FullName"] = PatientNameTextBox.Text;
            newPatientRow["BirthYear"] = int.Parse(BirthYearTextBox.Text);
            newPatientRow["Height"] = decimal.Parse(HeightTextBox.Text);
            newPatientRow["Weight"] = decimal.Parse(WeightTextBox.Text);
            newPatientRow["BloodPressure"] = BloodPressureTextBox.Text;
            dataSet.Tables["Patients"].Rows.Add(newPatientRow);

            DataRow newMedicalRecordRow = dataSet.Tables["MedicalRecords"].NewRow();
            newMedicalRecordRow["DoctorID"] = ((dynamic)doctor).DoctorID;
            newMedicalRecordRow["PatientID"] = newPatientRow["PatientID"];
            newMedicalRecordRow["Diagnosis"] = diagnosis;
            newMedicalRecordRow["ExaminationDate"] = DateTime.Now;
            dataSet.Tables["MedicalRecords"].Rows.Add(newMedicalRecordRow);
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (selectedRecord == null)
            {
                MessageBox.Show("No record selected for update.");
                return;
            }

            var doctor = DoctorsComboBox.SelectedItem;
            var diagnosis = DiagnosisComboBox.SelectedItem as string;

            if (doctor == null || string.IsNullOrEmpty(diagnosis) ||
                string.IsNullOrEmpty(PatientNameTextBox.Text) ||
                string.IsNullOrEmpty(BirthYearTextBox.Text) ||
                string.IsNullOrEmpty(HeightTextBox.Text) ||
                string.IsNullOrEmpty(WeightTextBox.Text) ||
                string.IsNullOrEmpty(BloodPressureTextBox.Text))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            DataRow patientRow = dataSet.Tables["Patients"].Rows
                .Cast<DataRow>()
                .First(row => row.Field<int>("PatientID") == selectedRecord.PatientID);
            patientRow["FullName"] = PatientNameTextBox.Text;
            patientRow["BirthYear"] = int.Parse(BirthYearTextBox.Text);
            patientRow["Height"] = decimal.Parse(HeightTextBox.Text);
            patientRow["Weight"] = decimal.Parse(WeightTextBox.Text);
            patientRow["BloodPressure"] = BloodPressureTextBox.Text;

            DataRow medicalRecordRow = dataSet.Tables["MedicalRecords"].Rows
                .Cast<DataRow>()
                .First(row => row.Field<int>("DoctorID") == selectedRecord.DoctorID &&
                              row.Field<int>("PatientID") == selectedRecord.PatientID);
            medicalRecordRow["DoctorID"] = ((dynamic)doctor).DoctorID;
            medicalRecordRow["Diagnosis"] = diagnosis;
            medicalRecordRow["ExaminationDate"] = DateTime.Now;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (selectedRecord == null)
            {
                MessageBox.Show("No record selected for deletion.");
                return;
            }

            DataRow patientRow = dataSet.Tables["Patients"].Rows
                .Cast<DataRow>()
                .First(row => row.Field<int>("PatientID") == selectedRecord.PatientID);
            DataRow medicalRecordRow = dataSet.Tables["MedicalRecords"].Rows
                .Cast<DataRow>()
                .First(row => row.Field<int>("DoctorID") == selectedRecord.DoctorID &&
                              row.Field<int>("PatientID") == selectedRecord.PatientID);

            patientRow.Delete();
            medicalRecordRow.Delete();
        }
    }
}
