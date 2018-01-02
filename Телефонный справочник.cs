using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Телефонный_справочник
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBox_homeNumber.Text = "-";
            textBox_workNumber.Text = "-";
        }

        private void clearFields()
        {
            textBox_surname.Clear();
            textBox_homeNumber.Text = "-";
            textBox_workNumber.Text = "-";
            textBox_surname.Focus();
        }

        string path = AppDomain.CurrentDomain.BaseDirectory + @"\Tel.txt";

        List<string> list = new List<string>();
        int line = -1;
        int lineForDel = 0;

        private void button_clear_Click(object sender, EventArgs e)
        {
            clearFields();
        }

        private void button_AddNew_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox_surname.Text == "" || textBox_homeNumber.Text == "" && textBox_workNumber.Text == "")
                {
                    MessageBox.Show("Необходимо ввести фамилию и телефоны");
                }
                else
                {
                    string text = File.ReadAllText(path);
                    using (StreamReader reader = new StreamReader(path))
                    {
                        if (text.Contains(textBox_surname.Text.ToUpper()))
                        {
                            MessageBox.Show("Такой контакт уже существует");
                            reader.Close();
                        }
                        else
                        {
                            reader.Close();
                            StreamWriter writer = new StreamWriter(path, true);
                            {
                                Abonent user = new Abonent(textBox_surname.Text, textBox_homeNumber.Text, textBox_workNumber.Text);
                                writer.WriteLine(user.surName);
                                writer.WriteLine(user.homeNumber);
                                writer.WriteLine(user.workNumber);
                                writer.WriteLine("-");
                                writer.Close();
                                MessageBox.Show("Контакт сохранён");
                                textBox_surname.Clear();
                                textBox_homeNumber.Clear();
                                textBox_workNumber.Clear();
                                textBox_surname.Focus();
                            }
                        }

                    }
                }
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("База номеров не найдена");
            }
            catch (FileLoadException)
            {
                MessageBox.Show("Ошибка загрузки файла");
            }
            catch (DirectoryNotFoundException)
            {
                MessageBox.Show("Ошибка адреса");
            }
            catch (IOException)
            {
                MessageBox.Show("Обнаружена ошибка при чтении файла");
            }
        }

        private void button_show_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox_surname.Text == "")
                {
                    MessageBox.Show("Необходимо ввести фамилию");
                }
                else
                {
                    using (StreamReader reader = new StreamReader(path))
                    {
                        string input = null;
                        while ((input = reader.ReadLine()) != null)
                        {
                            list.Add(input);
                        }
                        reader.Close();
                    }
                    for (int i = 0; i < list.Count(); i++)
                    {
                        if (textBox_surname.Text.ToUpper() == list[i])
                            line = i;
                    }
                    if (line != -1)
                    {
                        //textBox_surname.Text = list[line];
                        textBox_homeNumber.Text = list[line + 1];
                        textBox_workNumber.Text = list[line + 2];
                        lineForDel = line;
                        line = -1;
                    }
                    else
                    {
                        MessageBox.Show("Контакт не найден");
                    }

                }
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("База номеров не найдена");
            }
            catch (FileLoadException)
            {
                MessageBox.Show("Ошибка загрузки файла");
            }
            catch (DirectoryNotFoundException)
            {
                MessageBox.Show("Ошибка адреса");
            }
            catch (IOException)
            {
                MessageBox.Show("Обнаружена ошибка при чтении файла");
            }
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox_surname.Text == "" || textBox_homeNumber.Text == "" && textBox_workNumber.Text == "")
                {
                    MessageBox.Show("Необходимо ввести фамилию и телефоны");
                }
                else
                {
                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    DialogResult dialogResult = MessageBox.Show("Вы действительно хотите удалить данный контакт?", "Удаление контакта", buttons);
                    if (dialogResult == DialogResult.Yes)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            list.RemoveAt(lineForDel);
                        }

                        File.WriteAllText(path, string.Empty);
                        StreamWriter writer = new StreamWriter(path, false);

                        foreach (string x in list)
                        {
                            writer.WriteLine(x);
                        }

                        writer.Close();
                        clearFields();
                        line = -1;

                        MessageBox.Show("Контакт " + textBox_surname.Text + " удален");
                    }
                    else
                    {
                        MessageBox.Show("Операция удаления отменена", "Удаление контакта");
                    }
                }
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("База номеров не найдена");
            }
            catch (FileLoadException)
            {
                MessageBox.Show("Ошибка загрузки файла");
            }
            catch (DirectoryNotFoundException)
            {
                MessageBox.Show("Ошибка адреса");
            }
            catch (IOException)
            {
                MessageBox.Show("Обнаружена ошибка при чтении файла");
            }

        }
        public class Abonent
        {
            public string surName;
            public string homeNumber;
            public string workNumber;

            public Abonent(string surName, string homeNumber, string workNumber)
            {
                this.surName = surName.ToUpper();
                this.homeNumber = homeNumber;
                this.workNumber = workNumber;
            }
        }
    }
}
