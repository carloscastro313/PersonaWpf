using System.Configuration;
using System.Data.SqlClient;
using System.Windows;

namespace PersonaWpf
{
    /// <summary>
    /// Lógica de interacción para App.xaml
    /// </summary>
    public partial class App : Application
    {
        MainWindow mainWindow;
        SqlConnection connection;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            string stringConnection = ConfigurationManager.ConnectionStrings["PersonaWpf.Properties.Settings.TestingConnectionString"].ConnectionString;

            try
            {
                connection = new SqlConnection(stringConnection);

                mainWindow = new MainWindow(connection);

                mainWindow.Show();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Error : {ex.Message}");
                App.Current.Shutdown();
            }
        }

        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {

            MessageBox.Show($"{e.Exception.Message} , {e.Exception.StackTrace}");

            e.Handled = true;
        }
    }
}
