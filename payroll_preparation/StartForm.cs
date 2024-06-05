using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace payroll_preparation
{
    public partial class StartForm : Form
    {
        public StartForm()
        {
            InitializeComponent();
        }

        private void CalculationButton_Click(object sender, EventArgs e)
        {
            try
            {
                int oplata = Convert.ToInt32(OplataTextBox.Text);
                if (oplata > 0)
                {
                    Calculation calculation = new Calculation(oplata);
                    calculation.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Введите только положительное число", "Ошибка:");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Введите только положительное число", "Ошибка:");
            }
        }
    }
}
