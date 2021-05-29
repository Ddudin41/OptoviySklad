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
using System.Windows.Shapes;

namespace OptSklad
{
    /// <summary>
    /// Логика взаимодействия для ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        public Product CurrentProduct { get; set; }
        public IEnumerable<ProductType> ProdTypeList { get; set; }
        public IEnumerable<ProductSklad> SkladList { get; set; }
        public Sklad CurSklad;
        public ProductWindow(Product CurProduct,int type)
        {
            InitializeComponent();
            DataContext = this;
            CurrentProduct = CurProduct;
            ProdTypeList = Core.DB.ProductType.ToArray();
            SkladList = Core.DB.ProductSklad.ToArray();
            if (type == 0)
            {
                DeleteButton.Visibility = Visibility.Collapsed;
            } else
            {
                DeleteButton.Visibility = Visibility.Visible;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CurrentProduct.ProductType == null)
                    throw new Exception("Не выбран Тип продукта");
                if (CurrentProduct.Title == "")
                    throw new Exception("Не выбрано Наименование");
                if (CurrentProduct.ArticleNumber == null)
                    throw new Exception("Не указан артикул");
                if (CurrentProduct.Cost == 0)
                    throw new Exception("Не указан цена");
                if (CurrentProduct.ID == 0)
                    Core.DB.Product.Add(CurrentProduct);
                Core.DB.SaveChanges();
                DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentProduct.ProductSklad.Count > 0)
            {
                MessageBox.Show("Нельзя удалить товар, который лежит на складе");
                return;
            }
            try
            {
                Core.DB.Product.Remove(CurrentProduct);
                Core.DB.SaveChanges();
                DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении товара: {ex.Message}");
            }
        }
    }
}
