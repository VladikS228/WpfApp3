using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
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
using MaterialDesignThemes.Wpf;
using WpfApp3.ApplicationData;
using System.Data;


namespace WpfApp3
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(@"Data Source=OPARISH;Initial Catalog=News;Integrated Security=True;Integrated Security=True;Encrypt=False;");

        public bool IsDarkTheme { get; set; }
        private readonly PaletteHelper paletteHelper = new PaletteHelper(); 
        
        public bool isValid() //метод для проверки на пустоту текстбоксов
        {
            if (txtUsername.Text == string.Empty)
            {
                MessageBox.Show("Name is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtPassword.Password == string.Empty)
            {
                MessageBox.Show("Password is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

       

        private void toggleTheme(object sender, RoutedEventArgs e)
        {
            ITheme theme = paletteHelper.GetTheme();

            if (IsDarkTheme = theme.GetBaseTheme() == BaseTheme.Dark)
            {
                IsDarkTheme = false;
                theme.SetBaseTheme(Theme.Light);
            }
            else
            {
                IsDarkTheme = true;
                theme.SetBaseTheme(Theme.Dark);
            }
            paletteHelper.SetTheme(theme);
        }

        private void exitApp(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();

        }

        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            SignUp signUp = new SignUp();
            signUp.Show();
            this.Close();
        }

        public static DataTable Select(string selectSQL) // функция для подключение к БД и обработка запросов
        {
            DataTable dataTable = new DataTable("UsersQ"); // создаем таблицу в приложении
            SqlConnection SqlConnection = new SqlConnection("Data Source=OPARISH;Initial Catalog=News;Integrated Security=True;Integrated Security=True;Encrypt=False;");
            SqlConnection.Open(); // открываем подключение
            SqlCommand sqlCommand = SqlConnection.CreateCommand(); // создать команду
            sqlCommand.CommandText = selectSQL; // присваеваем команде текст
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand); // создает адаптер
            sqlDataAdapter.Fill(dataTable); // возвращаем таблицу с результатом
            return dataTable;
        }

        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            if (txtUsername.Text.Length > 3 || txtPassword.Password.Length > 3) // проверяем введён ли логин и пароль     
            {
                DataTable cmd = Login.Select("SELECT * FROM [dbo].[user] WHERE [username] = '" + txtUsername.Text + "' AND [password1] = '" + txtPassword.Password + "'");
                if (cmd.Rows.Count > 0) // если такая запись существует       
                {
                    // говорим, что авторизовался  // ищем в базе данных пользователя с такими данным
                    MainWindow mw = new MainWindow();
                    mw.Show();
                    this.Close();
                }
                else
                {
                    txtUsername.ToolTip = "This field is entered incorrectly"; // hover hint
                    txtUsername.Foreground = Brushes.Red; // change styles
                    txtUsername.CaretBrush = Brushes.Red;

                    txtPassword.ToolTip = "This field is entered incorrectly"; // hover hint
                    txtPassword.Foreground = Brushes.Red; // change styles
                    txtPassword.CaretBrush = Brushes.Red;
                }
            }

        }

    }
}


