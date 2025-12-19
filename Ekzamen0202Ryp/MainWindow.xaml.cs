using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
namespace Ekzamen0202Ryp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double totalCost = 0;
        private int sverhNorm = 0;
        public MainWindow()
        {
            InitializeComponent();
        }
        public double CalculateTotalCost(int minut, int tarif)
        {
            double totalCost = 0;
            if (tarif == 0)
            {
                if (minut <= 200)
                {
                    totalCost = minut * 0.7;
                }
                else
                {
                    totalCost = (200 * 0.7) + ((minut - 200) * 1.6);
                }
            }else if (tarif == 1){
                if (minut <= 100)
                {
                    totalCost = minut * 0.3;
                } else
                {
                    totalCost = (100 * 0.3) + ((minut - 100) * 1.6);
                }
            }
            return totalCost;
        }
        public int CalculateSverhNorma(int minut, int tarif)
        {
            int svn = 0;
            if (tarif == 0 && minut > 200)
            {
                svn = minut - 200;
            }
            else if (tarif == 1 && minut > 100)
            {
                svn = minut - 100;              
            }
            return svn;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(MinutesTextBox.Text))
            {
                MessageBox.Show("Введите количество минут");
                return;
            }

            if (!int.TryParse(MinutesTextBox.Text, out int minut))
            {
                MessageBox.Show("Введите целое число или число меньше 10 символов");
                return;
            }

            if (minut < 0)
            {
                MessageBox.Show("Количество минут не может быть отрицательным");
                return;
            }
            int tarif = TariffComboBox.SelectedIndex;

            totalCost = CalculateTotalCost(minut, tarif);
            sverhNorm = CalculateSverhNorma(minut, tarif);

            TotalLabel.Content = $"{totalCost} ryb";
            SverhNormLabel.Content = $"{sverhNorm} minut";
        }

        private void GenerateReceiptButton_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(MinutesTextBox.Text, out int minutes))
            {
                MessageBox.Show("Сначала выполните расчет");
                return;
            }
            int tarif = TariffComboBox.SelectedIndex;
            string tarifname = tarif == 0 ? "tarif 1" : "tarif 2";
            int tariffLimit = tarif == 0 ? 200 : 100;
            int Minut = Math.Min(minutes, tariffLimit);
            string receipt = $"КВИТАНЦИЯ\n" +
               $"Дата: {DateTime.Now:dd.MM.yyyy HH:mm}\n" +
               $"Тариф: {tarifname}\n" +
               $"Всего минут: {minutes}\n" +
               $"Минут по тарифу: {Minut}\n" +
               $"Минут сверх нормы: {sverhNorm}\n" +
               $"Сумма к оплате: {totalCost:F2} руб\n";
               

            // Сохраняем файл
            string fileName = $"№чека_{DateTime.Now:ddMMyyyy_HHmm}.txt";
            File.WriteAllText(fileName, receipt);

            MessageBox.Show($"Квитанция сохранена в файл:\n{fileName}", "Квитанция создана");
        }
    }
}
