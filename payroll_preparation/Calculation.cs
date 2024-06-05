using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace payroll_preparation
{
    public partial class Calculation : Form
    {
        int oplata;
        public Calculation(int oplata)
        {
            InitializeComponent();
            this.oplata = oplata;
            bonusTextBox.Text = "10000";
        }

        private void Calculation_Click(object sender, EventArgs e)
        {
            try
            {
                int number_of_days_worked = Convert.ToInt32(textBox1.Text);
                int number_of_hours_worked = Convert.ToInt32(textBox2.Text);
                int hourly_bonus_amount = Convert.ToInt32(textBox3.Text);

                if (number_of_days_worked >= 1 && number_of_hours_worked >= 1 && hourly_bonus_amount >= 0) {
                    if (number_of_days_worked < 8) {
                        double weekly_salary = (oplata + hourly_bonus_amount) * number_of_hours_worked * number_of_days_worked;
                        double two_weeks_salary = (oplata + hourly_bonus_amount) * number_of_hours_worked * (number_of_days_worked * 2);
                        double mounthly_salary = (oplata + hourly_bonus_amount) * number_of_hours_worked * number_of_days_worked * 4;

                        textBox6.Text = weekly_salary.ToString();
                        textBox5.Text = two_weeks_salary.ToString();
                        textBox4.Text = mounthly_salary.ToString();
                    }
                    else
                    {
                        MessageBox.Show("Число рабочих дней (в неделю) должно быть в диапозоне от 1 до 7", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    MessageBox.Show("Все числа должны быть целыми и положительными", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch
            {
                MessageBox.Show("Все поля для расчётов должны быть заполнениы целыми числами", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void Generation_File_Click(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;


            string fileStr = "Текущая дата: " + dateTime.ToString() + "\n" + "Заработная плата за неделю: " + textBox6.Text.ToString() + "\n" + "Заработная плата за 2 неделе: " + textBox5.Text.ToString() + "\n" + "Заработная плата за месяц: " + textBox4.Text.ToString();


            if (textBox6.Text == ""|| textBox5.Text == "" || textBox4.Text == "") {
                MessageBox.Show("Файл не может быть создан без произведения расчётов", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                saveFileDialog1.FileName = @"calculation.txt";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var writer = new StreamWriter(
                        saveFileDialog1.FileName, false, Encoding.GetEncoding(1251));

                        writer.Write(fileStr);
                        writer.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message,
                            "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }

            }
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            StartForm startForm = new StartForm();
            startForm.Show();
            this.Close();
        }

        private void tax_calculationButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox4.Text == "")
                {
                    MessageBox.Show("Для расчёта налога необходимо произвести расчёт заработной платы", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    if (procentTextBox.Text == "")
                    {
                        MessageBox.Show("Введите процентную ставку", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        int procent = Convert.ToInt32(procentTextBox.Text);

                        if (procent < 0 || procent > 100)
                        {
                            MessageBox.Show("Процентная ставка не может быть меньше 0 или больше 100", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else
                        {
                            int bonus = Convert.ToInt32(bonusTextBox.Text);

                            int one_percent = Convert.ToInt32(textBox4.Text) / 100;
                            int tax = one_percent * procent;

                            int one_bonus_percent = tax / 100;
                            int bonus_percent = one_bonus_percent * 10;

                            if (bonus_percent > bonus)
                            {
                                MessageBox.Show("У вас не достаточно бонусо они не будут использованы в расчёте", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                int sumOplata = tax;
                                amount_to_be_paid.Text = Convert.ToString(sumOplata);
                            }
                            else
                            {
                                int currentBonus = bonus - bonus_percent;
                                bonusTextBox.Text = Convert.ToString(currentBonus);

                                int sumOplata = tax - bonus_percent;
                                amount_to_be_paid.Text = Convert.ToString(sumOplata);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Все поля для расчётов должны быть заполнениы целыми числами", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void bonus_calculationButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (number_of_orders_per_week.Text == "" || number_of_orders_per_month.Text == "")
                {
                    MessageBox.Show("Заполнети все поля количества заказов", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    int number_of_orders_per_weekCount = Convert.ToInt32(number_of_orders_per_week.Text);
                    int number_of_orders_per_monthCount = Convert.ToInt32(number_of_orders_per_month.Text);

                    int weekBonus = 0;
                    int monthBonus = 0;

                    if (number_of_orders_per_weekCount > 200)
                    {
                        weekBonus = (number_of_orders_per_weekCount - 200) * 3;
                    }

                    if (number_of_orders_per_monthCount > 100)
                    {
                        monthBonus = 1000;
                    }

                    int yourBonus = weekBonus + monthBonus;
                    your_bonus.Text = Convert.ToString(yourBonus);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Все поля для расчётов должны быть заполнениы целыми числами", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void Back_button2_Click(object sender, EventArgs e)
        {
            StartForm startForm = new StartForm();
            startForm.Show();
            this.Close();
        }

        private void Back_button3_Click(object sender, EventArgs e)
        {
            StartForm startForm = new StartForm();
            startForm.Show();
            this.Close();
        }
    }
}
