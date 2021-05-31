using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace OptSklad
{
    public partial class Product
    {
        public Uri ImagePreview
        {
            get
            {
                var ImageName = Environment.CurrentDirectory + Image ?? "";
                return System.IO.File.Exists(ImageName) ? new Uri(ImageName) : new Uri("pack://application:,,,/Images/picture.png");
            }
        }
        public string Sklad
        {
            get
            {
                var res = "";
                //res = "Склад № ";
                foreach (var ps in ProductSklad)
                {
                    res = "Склад № " + ps.SkladID + ", " + ps.Sklad.Адрес + ", В количестве: " + ps.Count;
                }
                if(res=="")
                {
                    res = "Нет на складах";
                }
                return res;
            }
        }
    }
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public List<ProductType> ProductTypeList { get; set; }
        private IEnumerable<Product> _ProductList;

        public event PropertyChangedEventHandler PropertyChanged;
        private void Invalidate()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ProductList"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentPage"));
        }
        private int _CurrentPage = 1;

        public int CurrentPage
        {
            get
            {
                return _CurrentPage;
            }
            set
            {
                // тут проверка, чтобы номер не уходил в минус и в плюс
                if (value > 0)
                {
                    if ((_ProductList.Count() % 4)==0)
                    {
                        if (value <= _ProductList.Count() / 4)
                        {
                            _CurrentPage = value;
                            Invalidate();
                        }
                    } else
                    {
                        if (value <= (_ProductList.Count() / 4)+1)
                        {
                            _CurrentPage = value;
                            Invalidate();
                        }
                    }
                }
            }
        }

        public IEnumerable<Product> ProductList
        {
            get
            {
                var Result = _ProductList;

                if (SearchFilter != "")
                    Result = Result.Where(ai => (ai.Title.IndexOf(SearchFilter, StringComparison.OrdinalIgnoreCase) >= 0) || (ai.ArticleNumber.IndexOf(SearchFilter, StringComparison.OrdinalIgnoreCase)>=0));

                if (_ProductTypeFValue > 0)
                    Result = Result.Where(ai => ai.ProductType.ID == _ProductTypeFValue);

                switch (SortType)
                {
                    case 1:
                        Result = Result.OrderBy(p => p.Title);
                        break;
                    case 2:
                        Result = Result.OrderByDescending(p => p.Title);
                        break;
                    case 3:
                        Result = Result.OrderBy(p => p.Cost);
                        break;
                    case 4:
                        Result = Result.OrderByDescending(p => p.Cost);
                        break;
                }

                return Result.Skip((CurrentPage - 1) * 4).Take(4);
            }
            set
            {
                _ProductList = value;
            }
        }
        private int SortType = 0;
        public string[] SortList { get; set; } = {
            "Без сортировки",
            "название по убыванию",
            "название по возрастанию",
            "цена по убыванию",
            "цена по возрастанию" };
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            ProductList = Core.DB.Product.ToArray();
            ProductTypeList = Core.DB.ProductType.ToList();
            ProductTypeList.Insert(0, new ProductType { Title = "Все типы" });
        }
        private void PrevPage_Click(object sender, RoutedEventArgs e)
        {
            CurrentPage--;
        }

        private void NextPage_Click(object sender, RoutedEventArgs e)
        {
            CurrentPage++;
        }
        private void SortTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SortType = SortTypeComboBox.SelectedIndex;
            Invalidate();
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            var AddWindow = new ProductWindow(new Product(),0);
            if (AddWindow.ShowDialog() == true)
            {
                ProductList = Core.DB.Product.ToArray();
                Invalidate();
            }
        }

        private void ProductListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var EditWindow = new ProductWindow(ProductListView.SelectedItem as Product,1);
            if (EditWindow.ShowDialog() == true)
            {
                ProductList = Core.DB.Product.ToArray();
                Invalidate();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private int _ProductTypeFValue = 0;
        public int ProductTypeFValue
        {
            get
            {
                return _ProductTypeFValue;
            }
            set
            {
                _ProductTypeFValue = value;
                Invalidate();
            }
        }

        private void ProductTypeFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ProductTypeFValue = (ProductTypeFilter.SelectedItem as ProductType).ID;
        }
        private string _SearchFilter = "";
        public string SearchFilter
        {
            get
            {
                return _SearchFilter;
            }
            set
            {
                _SearchFilter = value;
                Invalidate();
            }
        }
        private void SearchFiltertextBox_KeyUp(object sender, KeyEventArgs e)
        {
            SearchFilter = SearchFiltertextBox.Text;
        }
    }
}
