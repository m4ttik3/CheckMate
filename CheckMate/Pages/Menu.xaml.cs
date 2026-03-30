using CheckMate.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.SqlTypes;
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
using System.Windows.Shapes;


namespace CheckMate.Pages
{
    /// <summary>
    
    /// Логика взаимодействия для Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        private static CheckMateEntities2 entities { get; } = new CheckMateEntities2();
        public Menu()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            var products = entities.menus.ToList();
            int count = products.Count;

            Card1Title.Text = products[0].name;
            Card1is_available.Text = products[0].is_available.ToString();
            Card1Price.Text = products[0].Price.ToString();

            Card2Title.Text = products[1].name;
            Card2is_available.Text = products[1].is_available.ToString();
            Card2Price.Text = products[1].Price.ToString();

            Card3Title.Text = products[2].name;
            Card3is_available.Text = products[2].is_available.ToString();
            Card3Price.Text = products[2].Price.ToString();

            Card4Title.Text = products[3].name;
            Card4is_available.Text = products[3].is_available.ToString();
            Card4Price.Text = products[3].Price.ToString();

            Card5Title.Text = products[4].name;
            Card5is_available.Text = products[4].is_available.ToString();
            Card5Price.Text = products[4].Price.ToString();

            Card6Title.Text = products[5].name;
            Card6is_available.Text = products[5].is_available.ToString();
            Card6Price.Text = products[5].Price.ToString();

            if (products[0].is_available == "В наличии")
            {
                Card1is_available.Foreground = Brushes.Green;
            }
            else
            {
                Card1is_available.Foreground = Brushes.Red;
            }

            if (products[1].is_available == "В наличии")
            {
                Card2is_available.Foreground = Brushes.Green;
            }
            else
            {
                Card2is_available.Foreground = Brushes.Red;
            }

            if (products[2].is_available == "В наличии")
            {
                Card3is_available.Foreground = Brushes.Green;
            }
            else
            {
                Card4is_available.Foreground = Brushes.Red;
            }

            if (products[3].is_available == "В наличии")
            {
                Card4is_available.Foreground = Brushes.Green;
            }
            else
            {
                Card4is_available.Foreground = Brushes.Red;
            }

            if (products[4].is_available == "В наличии")
            {
                Card5is_available.Foreground = Brushes.Green;
            }
            else
            {
                Card5is_available.Foreground = Brushes.Red;
            }

            if (products[5].is_available == "В наличии")
            {
                Card6is_available.Foreground = Brushes.Green; 
            }
            else
            {
                Card6is_available.Foreground = Brushes.Red;
            }
         

        }

        private void Drink_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Snacks_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Salat_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Order_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
