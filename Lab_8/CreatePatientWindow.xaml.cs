using LINQtoSQL_Library;
using System.Data.Linq;
using System.Windows;

namespace Lab_8
{
    public partial class CreatePatientWindow : Window
    {
        private readonly DataContext _dataContext;

        public CreatePatientWindow()
        {
            InitializeComponent();
            _dataContext = new DataContext(@"Data Source=DESKTOP-2GTDQ2V\SQLEXPRESS;Initial Catalog=OOPaP_67;Integrated Security=True");
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            var fullName = FullNameTextBox.Text;
            if (string.IsNullOrWhiteSpace(fullName))
            {
                MessageBox.Show("Please enter the patient's full name.");
                return;
            }

            if (!int.TryParse(BirthYearTextBox.Text, out int birthYear) ||
                !int.TryParse(HeightTextBox.Text, out int height) ||
                !int.TryParse(WeightTextBox.Text, out int weight))
            {
                MessageBox.Show("Please enter valid numerical values for birth year, height, and weight.");
                return;
            }

            var newPatient = new Patients
            {
                FullName = fullName,
                BirthYear = birthYear,
                Height = height,
                Weight = weight,
                BloodPressure = BloodPressureTextBox.Text
            };

            _dataContext.GetTable<Patients>().InsertOnSubmit(newPatient);
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
