﻿using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Data.Linq;
using LINQtoSQL_Library;

namespace Lab_8
{
    public partial class MainWindow : Window
    {
        private readonly DataContext _dataContext;
        private int _tempDoctorId;
        private int _tempPatientId;

        public MainWindow()
        {
            InitializeComponent();
            _dataContext = new DataContext(@"Data Source=DESKTOP-2GTDQ2V\SQLEXPRESS;Initial Catalog=OOPaP_67;Integrated Security=True");
        }

        private void TableComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TableComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                switch (selectedItem.Content.ToString())
                {
                    case "Doctors":
                        DataGrid.ItemsSource = _dataContext.GetTable<Doctors>().ToList();
                        break;
                    case "Patients":
                        DataGrid.ItemsSource = _dataContext.GetTable<Patients>().ToList();
                        break;
                    case "MedicalRecords":
                        DataGrid.ItemsSource = _dataContext.GetTable<MedicalRecords>().ToList();
                        break;
                }
            }
        }

        private void LoadData_Click(object sender, RoutedEventArgs e)
        {
            TableComboBox_SelectionChanged(null, null);
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            if (TableComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                switch (selectedItem.Content.ToString())
                {
                    case "Doctors":
                        var newDoctor = new Doctors
                        {
                            FullName = GetTextBoxValue("DoctorNameTextBox")
                        };
                        if (string.IsNullOrWhiteSpace(newDoctor.FullName))
                        {
                            MessageBox.Show("Please enter the doctor's full name.");
                            return;
                        }
                        _dataContext.GetTable<Doctors>().InsertOnSubmit(newDoctor);
                        break;

                    case "Patients":
                        var newPatient = new Patients
                        {
                            FullName = GetTextBoxValue("PatientNameTextBox"),
                            BirthYear = GetIntValue("BirthYearTextBox"),
                            Height = GetIntValue("HeightTextBox"),
                            Weight = GetIntValue("WeightTextBox"),
                            BloodPressure = GetTextBoxValue("BloodPressureTextBox")
                        };
                        if (string.IsNullOrWhiteSpace(newPatient.FullName))
                        {
                            MessageBox.Show("Please enter the patient's full name.");
                            return;
                        }
                        _dataContext.GetTable<Patients>().InsertOnSubmit(newPatient);
                        break;

                    case "MedicalRecords":
                        var doctorId = GetIntValue("DoctorIDTextBox");
                        var patientId = GetIntValue("PatientIDTextBox");
                        var diagnosis = GetTextBoxValue("DiagnosisTextBox");

                        if (doctorId == 0 || patientId == 0 || string.IsNullOrWhiteSpace(diagnosis))
                        {
                            MessageBox.Show("Please enter all medical record details.");
                            return;
                        }

                        var newMedicalRecord = new MedicalRecords
                        {
                            DoctorID = doctorId,
                            PatientID = patientId,
                            Diagnosis = diagnosis,
                            ExaminationDate = DateTime.Now
                        };
                        _dataContext.GetTable<MedicalRecords>().InsertOnSubmit(newMedicalRecord);
                        break;
                }
                _dataContext.SubmitChanges();
                TableComboBox_SelectionChanged(null, null);
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedItem == null)
            {
                MessageBox.Show("Please select a record to update.");
                return;
            }

            try
            {
                if (TableComboBox.SelectedItem is ComboBoxItem selectedItem && selectedItem.Content.ToString() == "MedicalRecords")
                {
                    var selectedRecord = DataGrid.SelectedItem as MedicalRecords;

                    // Save the new values to temporary variables
                    int newDoctorId = _tempDoctorId != 0 ? _tempDoctorId : selectedRecord.DoctorID;
                    int newPatientId = _tempPatientId != 0 ? _tempPatientId : selectedRecord.PatientID;

                    // Delete the existing record
                    _dataContext.GetTable<MedicalRecords>().DeleteOnSubmit(selectedRecord);
                    _dataContext.SubmitChanges();

                    // Create a new record with the updated values
                    var updatedRecord = new MedicalRecords
                    {
                        DoctorID = newDoctorId,
                        PatientID = newPatientId,
                        Diagnosis = selectedRecord.Diagnosis,
                        ExaminationDate = selectedRecord.ExaminationDate
                    };

                    _dataContext.GetTable<MedicalRecords>().InsertOnSubmit(updatedRecord);
                    _dataContext.SubmitChanges();

                    // Reset temporary variables
                    _tempDoctorId = 0;
                    _tempPatientId = 0;
                }
                else
                {
                    _dataContext.SubmitChanges();
                }
                TableComboBox_SelectionChanged(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedItem == null)
            {
                MessageBox.Show("Please select a record to delete.");
                return;
            }

            try
            {
                switch (TableComboBox.SelectedItem.ToString())
                {
                    case "Doctors":
                        _dataContext.GetTable<Doctors>().DeleteOnSubmit(DataGrid.SelectedItem as Doctors);
                        break;
                    case "Patients":
                        _dataContext.GetTable<Patients>().DeleteOnSubmit(DataGrid.SelectedItem as Patients);
                        break;

                    case "MedicalRecords":
                        _dataContext.GetTable<MedicalRecords>().DeleteOnSubmit(DataGrid.SelectedItem as MedicalRecords);
                        break;
                }
                _dataContext.SubmitChanges();
                TableComboBox_SelectionChanged(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private string GetTextBoxValue(string name)
        {
            var textBox = this.FindName(name) as TextBox;
            return textBox?.Text;
        }

        private int GetIntValue(string name)
        {
            int.TryParse(GetTextBoxValue(name), out int value);
            return value;
        }

        private void DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            var column = e.Column as DataGridBoundColumn;
            if (column != null && e.Row.Item is MedicalRecords record)
            {
                var bindingPath = (column.Binding as System.Windows.Data.Binding).Path.Path;
                if (bindingPath == "DoctorID")
                {
                    _tempDoctorId = Convert.ToInt32((e.EditingElement as TextBox).Text);
                }
                else if (bindingPath == "PatientID")
                {
                    _tempPatientId = Convert.ToInt32((e.EditingElement as TextBox).Text);
                }
            }
        }

        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (TableComboBox.SelectedItem is ComboBoxItem selectedItem && selectedItem.Content.ToString() == "MedicalRecords")
            {
                if (e.PropertyName == "RecordID" || e.PropertyName == "Doctors" || e.PropertyName == "Patients")
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
