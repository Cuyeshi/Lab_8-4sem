using System.Windows;

namespace Lab_8
{
    public partial class FilterByBirthYearWindow : Window
    {
        public int? BirthYear { get; private set; }

        public FilterByBirthYearWindow()
        {
            InitializeComponent();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(YearTextBox.Text, out int year))
            {
                BirthYear = year;
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите действительный год.");
            }
        }
    }
}
