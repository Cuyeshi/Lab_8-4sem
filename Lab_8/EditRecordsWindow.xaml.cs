using System;
using System.Data;
using System.Linq;
using System.Windows;
using LINQtoData_Library;

namespace Lab_8
{
    public partial class EditRecordsWindow : Window
    {
        private readonly MedicalDataSet _dataSet;
        private readonly MainWindow _mainWindow;

        public EditRecordsWindow(MedicalDataSet dataSet, MainWindow mainWindow)
        {
            InitializeComponent();
            _dataSet = dataSet;
            _mainWindow = mainWindow;
            LoadComboBoxes();
        }

        private void LoadComboBoxes()
        {
            var doctors = from doctor in _dataSet.DoctorsTable.AsEnumerable()
                          select new
                          {
                              DoctorID = doctor.Field<int>("DoctorID"),
                              FullName = doctor.Field<string>("FullName")
                          };

            var diagnoses = (from record in _dataSet.MedicalRecordsTable.AsEnumerable()
                             select record.Field<string>("Diagnosis")).Distinct();

            DoctorsComboBox.ItemsSource = doctors.ToList();
            DoctorsComboBox.DisplayMemberPath = "FullName";
            DoctorsComboBox.SelectedValuePath = "DoctorID";

            DiagnosisComboBox.ItemsSource = diagnoses.ToList();
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

            Patient patient = new Patient
            {
                FullName = PatientNameTextBox.Text,
                BirthYear = int.Parse(BirthYearTextBox.Text),
                Height = int.Parse(HeightTextBox.Text),
                Weight = int.Parse(WeightTextBox.Text),
                BloodPressure = BloodPressureTextBox.Text
            };

            MedicalRecord record = new MedicalRecord
            {
                DoctorID = (int)doctor.GetType().GetProperty("DoctorID").GetValue(doctor),
                Diagnosis = diagnosis,
                ExaminationDate = DateTime.Now
            };

            _mainWindow.CreateRecord(record, patient);
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            var selectedRecord = _mainWindow.MedicalRecordsDataGrid.SelectedItem as dynamic;
            if (selectedRecord == null)
            {
                MessageBox.Show("Please select a record to update.");
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

            Patient patient = new Patient
            {
                PatientID = selectedRecord.PatientID,
                FullName = PatientNameTextBox.Text,
                BirthYear = int.Parse(BirthYearTextBox.Text),
                Height = int.Parse(HeightTextBox.Text),
                Weight = int.Parse(WeightTextBox.Text),
                BloodPressure = BloodPressureTextBox.Text
            };

            MedicalRecord record = new MedicalRecord
            {
                RecordID = selectedRecord.RecordID,
                DoctorID = (int)doctor.GetType().GetProperty("DoctorID").GetValue(doctor),
                Diagnosis = diagnosis,
                ExaminationDate = DateTime.Now
            };

            _mainWindow.UpdateRecord(record, patient);
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var selectedRecord = _mainWindow.MedicalRecordsDataGrid.SelectedItem as dynamic;
            if (selectedRecord == null)
            {
                MessageBox.Show("Please select a record to delete.");
                return;
            }

            MedicalRecord record = new MedicalRecord
            {
                RecordID = selectedRecord.RecordID,
                PatientID = selectedRecord.PatientID
            };

            _mainWindow.DeleteRecord(record);
        }
    }
}
