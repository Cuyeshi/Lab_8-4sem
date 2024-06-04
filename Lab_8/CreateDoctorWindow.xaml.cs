using LINQtoSQL_Library;
using System.Data.Linq;
using System.Windows;

namespace Lab_8
{
    public partial class CreateDoctorWindow : Window
    {
        private readonly DataContext _dataContext;

        public CreateDoctorWindow()
        {
            InitializeComponent();
            _dataContext = new DataContext(@"Data Source=DESKTOP-2GTDQ2V\SQLEXPRESS;Initial Catalog=OOPaP_67;Integrated Security=True");
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            var fullName = FullNameTextBox.Text;
            if (string.IsNullOrWhiteSpace(fullName))
            {
                MessageBox.Show("Please enter the doctor's full name.");
                return;
            }

            var newDoctor = new Doctors
            {
                FullName = fullName
            };
            _dataContext.GetTable<Doctors>().InsertOnSubmit(newDoctor);
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
