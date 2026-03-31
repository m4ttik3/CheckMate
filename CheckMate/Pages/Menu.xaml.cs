using CheckMate.Entities;
using CheckMate.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CheckMate.Pages
{
    /// <summary>
    /// Логика взаимодействия для Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        private static CheckMateEntities3 entities { get; } = new CheckMateEntities3();

        // Список корзины
        public static List<CartItems> Cart = new List<CartItems>();

        // Массив картинок для заказа
        private string[] imagePaths = { "/Tar-Tar.jpg", "/GrushaSnack.png", "/BaklajanSalat.png", "/KorkomusSalat.png", "/PinokaladaNapitok.png", "/KrovMaryNapitok.png" };

        public Menu()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var products = entities.menus.ToList();

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

            if (products[0].is_available == "В наличии") Card1is_available.Foreground = Brushes.Green;
            else Card1is_available.Foreground = Brushes.Red;

            if (products[1].is_available == "В наличии") Card2is_available.Foreground = Brushes.Green;
            else Card2is_available.Foreground = Brushes.Red;

            if (products[2].is_available == "В наличии") Card3is_available.Foreground = Brushes.Green;
            else Card3is_available.Foreground = Brushes.Red;

            if (products[3].is_available == "В наличии") Card4is_available.Foreground = Brushes.Green;
            else Card4is_available.Foreground = Brushes.Red;

            if (products[4].is_available == "В наличии") Card5is_available.Foreground = Brushes.Green;
            else Card5is_available.Foreground = Brushes.Red;

            if (products[5].is_available == "В наличии") Card6is_available.Foreground = Brushes.Green;
            else Card6is_available.Foreground = Brushes.Red;
        }

        private void Drink_Click(object sender, RoutedEventArgs e)
        {
            FilterCards("Drink");
        }
        private void Snacks_Click(object sender, RoutedEventArgs e)
        {
            FilterCards("Snacks");
        }
        private void Salat_Click(object sender, RoutedEventArgs e)
        {
            FilterCards("Salat");
        }

        private void FilterCards(string category)
        {
            foreach (Border card in ProductList.Children.OfType<Border>())
            {
                if (card.Tag.ToString() == category || category == "All")
                {
                    card.Visibility = Visibility.Visible;
                }
                else
                {
                    card.Visibility = Visibility.Collapsed;
                }

            }
        }

        private void AllTovar_Click(object sender, RoutedEventArgs e)
        {
            FilterCards("All");
        }

        private void Add_Click(object sender, RoutedEventArgs e)

        {
            Button clickedButton = (Button)sender;
            int index = Convert.ToInt32(clickedButton.Tag);

            var products = entities.menus.ToList();

            CartItems newItem = new CartItems
            {
                Name = products[index].name,
                Price = products[index].Price,
                ImagePath = imagePaths[index]
            };

            var existingItem = Cart.FirstOrDefault(c => c.Name == newItem.Name);
            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                Cart.Add(newItem);
            }

            MessageBox.Show($"{newItem.Name} добавлен в корзину!");
        }

        private void Order_Click(object sender, RoutedEventArgs e)
        {
            if (Cart.Count == 0)
            {
                MessageBox.Show("Корзина пуста! Добавьте товары.");
                return;
            }

            if (string.IsNullOrWhiteSpace(Stol.Text))
            {
                MessageBox.Show("Пожалуйста, укажите номер столика.");
                return;
            }

            // Создаем окно-обертку для Page
            Window hostWindow = new Window
            {
                Title = "Оформление заказа",
                Width = 800,
                Height = 600,
                Background = (System.Windows.Media.Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#121212"),
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };

            // Создаем страницу и передаем данные
            Order orderPage = new Order(Menu.Cart, Stol.Text);

            // Вставляем страницу в окно
            Frame frame = new Frame();
            frame.Navigate(orderPage);
            hostWindow.Content = frame;

            // Открываем заказ и закрываем меню
            hostWindow.Show();
            this.Close();
        }
    } 
} 