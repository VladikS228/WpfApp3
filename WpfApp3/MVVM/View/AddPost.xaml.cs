using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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

namespace WpfApp3.MVVM.View
{
    /// <summary>
    /// Логика взаимодействия для AddPost.xaml
    /// </summary>
    public partial class AddPost : Window
    {
        public AddPost()
        {
            InitializeComponent();
            LoadGrid();
        }

        //Сюда вставляем скопированную строку подключения
        SqlConnection con = new SqlConnection(@"Data Source=OPARISH;Initial Catalog=News;Integrated Security=True;Integrated Security=True;Encrypt=False;");

        //создаем метод для очистки textbox - ов
        public void clearData()
        {
            name_txt.Clear();
            age_txt.Clear();
            gender_txt.Clear();
            group_txt.Clear();
            id_txt.Clear();
        }
        //метод для открытия таблицы на DataGrid - е
        public void LoadGrid()
        {
            SqlCommand cmd = new SqlCommand("select * from UsersForm", con);
            DataTable dataTable = new DataTable();
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            dataTable.Load(reader);
            con.Close(); //обязательно нужно закрыть после оканчания действии
            datagrid.ItemsSource = dataTable.DefaultView;
        }
        //кнопка Clear 
        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            clearData();

        }
        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }

        public bool isValid() //метод для проверки на пустоту текстбоксов
        {
            if (name_txt.Text == string.Empty)
            {
                MessageBox.Show("Name is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (age_txt.Text == string.Empty)
            {
                MessageBox.Show("Age is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (gender_txt.Text == string.Empty)
            {
                MessageBox.Show("Gender is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (group_txt.Text == string.Empty)
            {
                MessageBox.Show("Group is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (id_txt.Text == string.Empty)
            {
                MessageBox.Show("ID is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
        //кнопка - добавить данные в таблицу
        private void InsertBtn_Click(object sender, RoutedEventArgs e)
        {
            //исключение - чтобы программа не перестала работать даже если появится ошибка/исключение
            try
            {
                if (isValid())
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO UsersForm VALUES (@ID,@Name,@Age, @Gender, @Email)", con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@ID", id_txt.Text);
                    cmd.Parameters.AddWithValue("@Name", name_txt.Text);
                    cmd.Parameters.AddWithValue("@Age", age_txt.Text);
                    cmd.Parameters.AddWithValue("@Gender", gender_txt.Text);
                    cmd.Parameters.AddWithValue("@Email", group_txt.Text);

                    con.Open();
                    cmd.ExecuteNonQuery(); //выполнение команд
                    con.Close();
                    LoadGrid();
                    MessageBox.Show("Successfully!", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);
                    clearData();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //удаление данных по ID
        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Delete from UsersForm where ID = " + id_txt.Text + "", con);
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record has been deleted", "Delete", MessageBoxButton.OK, MessageBoxImage.Information);
                con.Close();
                clearData();
                LoadGrid();
                con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Not Deleted" + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
        //кнопка для обновления данных
        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("update UsersForm set [Name] = '" + name_txt.Text + "',[Age] = '" + age_txt.Text + "',[Gender] = '" + gender_txt.Text + "',[Post] = '" + group_txt.Text + "' WHERE [ID] = '" + id_txt.Text + "'", con);
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record has been updated succesfully", "Updated", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
                clearData();
                LoadGrid();
            }
        }
    }
}
