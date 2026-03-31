using CheckMate.Models;
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

namespace CheckMate.Pages
{
    /// <summary>
    /// Логика взаимодействия для Order.xaml
    /// </summary>
    public partial class Order : Page
    {
        private List<CartItems> _cart;
        private string _tableNumber;
        public Order(List<CartItems> cart, string tableNumber)
        {
            InitializeComponent();
            _cart = cart;
            _tableNumber = tableNumber;
            TableNumberText.Text = tableNumber;

            LoadOrderItems();
            CalculateTotal();
        }
        private void LoadOrderItems()
        {
            OrderItemsPanel.Children.Clear();

            foreach (var item in _cart)
            {
                // Создаем карточку товара программно
                Border cardBorder = new Border
                {
                    CornerRadius = new CornerRadius(15),
                    Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF87898A")),
                    Margin = new Thickness(0, 0, 0, 15),
                    Padding = new Thickness(10)
                };

                Grid grid = new Grid();
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(100) }); // Фото
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }); // Название/Цена
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto }); // Кнопки +/- и цена за все

                // Фото
                Image img = new Image { Source = new System.Windows.Media.Imaging.BitmapImage(new System.Uri(item.ImagePath, System.UriKind.Relative)), Width = 100, Height = 100 };
                Grid.SetColumn(img, 0);
                grid.Children.Add(img);

                // Название и цена за 1 шт
                StackPanel infoStack = new StackPanel { Margin = new Thickness(15, 0, 0, 0) };
                TextBlock nameText = new TextBlock { Text = item.Name, Foreground = Brushes.White, FontSize = 20, FontFamily = new FontFamily("Segoe UI") };
                TextBlock priceText = new TextBlock { Text = $"{item.Price} ₽ / шт", Foreground = Brushes.LightGray, FontSize = 14, FontFamily = new FontFamily("Segoe UI") };
                infoStack.Children.Add(nameText);
                infoStack.Children.Add(priceText);
                Grid.SetColumn(infoStack, 1);
                grid.Children.Add(infoStack);

                // Кнопки количества
                StackPanel quantityStack = new StackPanel { Orientation = Orientation.Horizontal, VerticalAlignment = VerticalAlignment.Center };

                Button minusBtn = new Button { Content = "-", Width = 30, Height = 30, FontSize = 16, FontWeight = FontWeights.Bold, Background = Brushes.Transparent, Foreground = Brushes.White, BorderThickness = new Thickness(0) };
                minusBtn.Click += (s, e) => ChangeQuantity(item, -1);

                TextBlock qtyText = new TextBlock { Text = item.Quantity.ToString(), Foreground = Brushes.White, FontSize = 20, VerticalAlignment = VerticalAlignment.Center, Width = 40, TextAlignment = TextAlignment.Center };

                Button plusBtn = new Button { Content = "+", Width = 30, Height = 30, FontSize = 16, FontWeight = FontWeights.Bold, Background = Brushes.Transparent, Foreground = Brushes.White, BorderThickness = new Thickness(0) };
                plusBtn.Click += (s, e) => ChangeQuantity(item, 1);

                TextBlock totalPriceItem = new TextBlock { Text = $"{item.Price * item.Quantity} ₽", Foreground = Brushes.White, FontSize = 18, FontWeight = FontWeights.Bold, VerticalAlignment = VerticalAlignment.Center, Margin = new Thickness(15, 0, 0, 0) };

                quantityStack.Children.Add(minusBtn);
                quantityStack.Children.Add(qtyText);
                quantityStack.Children.Add(plusBtn);
                quantityStack.Children.Add(totalPriceItem);

                Grid.SetColumn(quantityStack, 2);
                grid.Children.Add(quantityStack);

                cardBorder.Child = grid;
                OrderItemsPanel.Children.Add(cardBorder);
            }
        }
        private void ChangeQuantity(CartItems item, int change)
        {
            item.Quantity += change;

            if (item.Quantity <= 0)
            {
                _cart.Remove(item); // Удаляем, если стало 0
            }

            // Перерисовываем список и пересчитываем сумму
            LoadOrderItems();
            CalculateTotal();
        }

        private void CalculateTotal()
        {
            decimal total = _cart.Sum(item => item.Price * item.Quantity);
            TotalPriceText.Text = $"{total} ₽";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // Тут в будущем будет код для сохранения заказа в БД
            MessageBox.Show($"Заказ для столика №{_tableNumber} успешно оформлен на сумму {TotalPriceText.Text}!");

            // Возвращаемся в меню (открываем заново, так как мы его закрыли)
            Menu newMenu = new Menu();
            newMenu.Show();
        }
    }
}
