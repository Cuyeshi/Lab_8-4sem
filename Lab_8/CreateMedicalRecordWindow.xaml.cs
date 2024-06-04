using LINQtoSQL_Library;
using System;
using System.Data.Linq;
using System.Windows;

namespace Lab_8
{
    public partial class CreateMedicalRecordWindow : Window
    {
        private readonly DataContext _dataContext;

        public CreateMedicalRecordWindow()
        {
            InitializeComponent();
            _dataContext = new DataContext(@"Data Source=DESKTOP-2GTDQ2V\SQLEXPRESS;Initial Catalog=OOPaP_67;Integrated Security=True");
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(DoctorIDTextBox.Text, out int doctorId) ||
                !int.TryParse(PatientIDTextBox.Text, out int patientId) ||
                string.IsNullOrWhiteSpace(DiagnosisTextBox.Text) ||
                !DateTime.TryParse(ExaminationDateTextBox.Text, out DateTime examinationDate))
            {
                MessageBox.Show("Please enter valid values for Doctor ID, Patient ID, Diagnosis, and Examination Date.");
                return;
            }

            var newMedicalRecord = new MedicalRecords
            {
                DoctorID = doctorId,
                PatientID = patientId,
                Diagnosis = DiagnosisTextBox.Text,
                ExaminationDate = examinationDate
            };

            _dataContext.GetTable<MedicalRecords>().InsertOnSubmit(newMedicalRecord);
            _dataContext.SubmitChanges();
            this.DialogResult = true;
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
