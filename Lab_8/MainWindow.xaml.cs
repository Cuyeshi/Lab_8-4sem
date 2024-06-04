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
                Window createWindow = null;
                switch (selectedItem.Content.ToString())
                {
                    case "Doctors":
                        createWindow = new CreateDoctorWindow();
                        break;
                    case "Patients":
                        createWindow = new CreatePatientWindow();
                        break;
                    case "MedicalRecords":
                        createWindow = new CreateMedicalRecordWindow();
                        break;
                }
                createWindow.ShowDialog();
                TableComboBox_SelectionChanged(null, null);
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, выберите запись для обновления.");
                return;
            }

            try
            {
                if (TableComboBox.SelectedItem is ComboBoxItem selectedItem && selectedItem.Content.ToString() == "MedicalRecords")
                {
                    var selectedRecord = DataGrid.SelectedItem as MedicalRecords;

                    if (selectedRecord == null)
                    {
                        MessageBox.Show("Выбранная запись равна null.");
                        return;
                    }

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
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, выберите запись для удаления.");
                return;
            }

            try
            {
                if (TableComboBox.SelectedItem is ComboBoxItem selectedItem)
                {
                    switch (selectedItem.Content.ToString())
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
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
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
            if (TableComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                if (selectedItem.Content.ToString() == "MedicalRecords")
                {
                    if (e.PropertyName == "RecordID" || e.PropertyName == "Doctors" || e.PropertyName == "Patients")
                    {
                        e.Cancel = true;
                    }
                }
                else if (selectedItem.Content.ToString() == "Doctors" || selectedItem.Content.ToString() == "Patients")
                {
                    if (e.PropertyName == "MedicalRecords")
                    {
                        e.Cancel = true;
                    }
                }
            }
        }





        private void FilterByBirthYear_Click(object sender, RoutedEventArgs e)
        {
            var filterWindow = new FilterByBirthYearWindow();
            if (filterWindow.ShowDialog() == true)
            {
                if (filterWindow.BirthYear.HasValue)
                {
                    FilterPatientsByBirthYear(filterWindow.BirthYear.Value);
                }
            }
        }

        private void GroupByDiagnosis_Click(object sender, RoutedEventArgs e)
        {
            GroupMedicalRecordsByDiagnosis();
        }

        private void CountPatientsPerDoctor_Click(object sender, RoutedEventArgs e)
        {
            CountPatientsPerDoctor();
        }

        private void FilterPatientsByBirthYear(int year)
        {
            var filteredPatients = from patient in _dataContext.GetTable<Patients>()
                                   where patient.BirthYear > year
                                   select patient;

            DataGrid.ItemsSource = filteredPatients.ToList();
        }

        private void GroupMedicalRecordsByDiagnosis()
        {
            var groupedRecords = from record in _dataContext.GetTable<MedicalRecords>()
                                 group record by record.Diagnosis into diagnosisGroup
                                 select new
                                 {
                                     Diagnosis = diagnosisGroup.Key,
                                     Count = diagnosisGroup.Count()
                                 };

            DataGrid.ItemsSource = groupedRecords.ToList();
        }

        private void CountPatientsPerDoctor()
        {
            var patientCountPerDoctor = from record in _dataContext.GetTable<MedicalRecords>()
                                        group record by record.DoctorID into doctorGroup
                                        select new
                                        {
                                            DoctorID = doctorGroup.Key,
                                            PatientCount = doctorGroup.Select(r => r.PatientID).Distinct().Count()
                                        };

            var result = from doctor in _dataContext.GetTable<Doctors>()
                         join groupData in patientCountPerDoctor on doctor.DoctorID equals groupData.DoctorID
                         select new
                         {
                             DoctorName = doctor.FullName,
                             groupData.PatientCount
                         };

            DataGrid.ItemsSource = result.ToList();
        }


    }
}
