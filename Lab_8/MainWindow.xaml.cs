using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Data.Linq;
using LINQtoSQL_Library;

namespace Lab_8
{
    public partial class MainWindow : Window
    {
        private DataContext _dataContext;

        public MainWindow()
        {
            InitializeComponent();
            LoadTableComboBox();
        }

        private void LoadTableComboBox()
        {
            TableComboBox.Items.Add("Doctors");
            TableComboBox.Items.Add("Patients");
            TableComboBox.Items.Add("MedicalRecords");
        }

        private void TableComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            _dataContext = new DataContext("Data Source=DESKTOP-2GTDQ2V\\SQLEXPRESS;Initial " +
                                                    "Catalog=OOPaP_67;Integrated Security=True");

            switch (TableComboBox.SelectedItem.ToString())
            {
                case "Doctors":
                    RecordsDataGrid.ItemsSource = _dataContext.GetTable<Doctors>().ToList();
                    break;
                case "Patients":
                    RecordsDataGrid.ItemsSource = _dataContext.GetTable<Patients>().ToList();
                    break;
                case "MedicalRecords":
                    RecordsDataGrid.ItemsSource = _dataContext.GetTable<MedicalRecords>().ToList();
                    break;
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TableComboBox.SelectedItem == null)
                {
                    MessageBox.Show("Please select a table.");
                    return;
                }

                switch (TableComboBox.SelectedItem.ToString())
                {
                    case "Doctors":
                        var newDoctor = new Doctors
                        {
                            FullName = FullNameTextBox.Text
                        };

                        if (string.IsNullOrEmpty(newDoctor.FullName))
                        {
                            MessageBox.Show("Full Name is required for Doctors.");
                            return;
                        }

                        _dataContext.GetTable<Doctors>().InsertOnSubmit(newDoctor);
                        break;

                    case "Patients":
                        if (string.IsNullOrEmpty(FullNameTextBox.Text) ||
                            string.IsNullOrEmpty(BirthYearTextBox.Text) ||
                            string.IsNullOrEmpty(HeightTextBox.Text) ||
                            string.IsNullOrEmpty(WeightTextBox.Text) ||
                            string.IsNullOrEmpty(BloodPressureTextBox.Text))
                        {
                            MessageBox.Show("All fields are required for Patients.");
                            return;
                        }

                        var newPatient = new Patients
                        {
                            FullName = FullNameTextBox.Text,
                            BirthYear = int.Parse(BirthYearTextBox.Text),
                            Height = int.Parse(HeightTextBox.Text),
                            Weight = int.Parse(WeightTextBox.Text),
                            BloodPressure = BloodPressureTextBox.Text
                        };

                        _dataContext.GetTable<Patients>().InsertOnSubmit(newPatient);
                        break;

                    case "MedicalRecords":
                        if (string.IsNullOrEmpty(FullNameTextBox.Text) ||
                            string.IsNullOrEmpty(BirthYearTextBox.Text) ||
                            string.IsNullOrEmpty(HeightTextBox.Text) ||
                            string.IsNullOrEmpty(WeightTextBox.Text) ||
                            string.IsNullOrEmpty(BloodPressureTextBox.Text))
                        {
                            MessageBox.Show("All fields are required for Medical Records.");
                            return;
                        }

                        var newRecord = new MedicalRecords
                        {
                            DoctorID = int.Parse(BirthYearTextBox.Text), // Example value, replace with actual input
                            PatientID = int.Parse(HeightTextBox.Text), // Example value, replace with actual input
                            Diagnosis = WeightTextBox.Text, // Example value, replace with actual input
                            ExaminationDate = DateTime.Now
                        };

                        _dataContext.GetTable<MedicalRecords>().InsertOnSubmit(newRecord);
                        break;
                }

                _dataContext.SubmitChanges();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }



        private void Update_Click(object sender, RoutedEventArgs e)
        {
            _dataContext.SubmitChanges();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = RecordsDataGrid.SelectedItem;

            if (selectedItem != null)
            {
                switch (TableComboBox.SelectedItem.ToString())
                {
                    case "Doctors":
                        _dataContext.GetTable<Doctors>().DeleteOnSubmit((Doctors)selectedItem);
                        break;
                    case "Patients":
                        _dataContext.GetTable<Patients>().DeleteOnSubmit((Patients)selectedItem);
                        break;
                    case "MedicalRecords":
                        _dataContext.GetTable<MedicalRecords>().DeleteOnSubmit((MedicalRecords)selectedItem);
                        break;
                }
                _dataContext.SubmitChanges();
                LoadData();
            }
        }
    }
}
