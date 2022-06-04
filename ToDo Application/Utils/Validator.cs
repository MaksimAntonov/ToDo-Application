using System.Windows.Controls;

namespace ToDo_Application.Utils
{
    class Validator
    {
        public static bool TextField(TextBox textBox)
        {
            return textBox.Text.Length > 0;
        }

        public static bool DateTimeField(DatePicker dateTime)
        {
            return dateTime.SelectedDate != null && dateTime.SelectedDate.HasValue;
        }

        private Validator() { }
    }
}
