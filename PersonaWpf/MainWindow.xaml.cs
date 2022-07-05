using PersonaWpf.Vistas;
using System.Collections.ObjectModel;
using System.Windows;

namespace PersonaWpf
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<Persona> Personas { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Personas = new ObservableCollection<Persona>();
            dgPersonas.ItemsSource = Personas;

        }

        private void btnCrear_Click(object sender, RoutedEventArgs e)
        {
            PersonaForm form = new PersonaForm(Personas);
            form.ShowDialog();
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {

            if (dgPersonas.SelectedIndex == -1)
            {
                MessageBox.Show("No hay seleccionado");
                return;
            }

            Persona persona = dgPersonas.SelectedItem as Persona;

            ModificarPersonaForm modificar = new ModificarPersonaForm(Personas, persona, dgPersonas.SelectedIndex);
            modificar.ShowDialog();
            dgPersonas.UnselectAll();
            dgPersonas.Items.Refresh();
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (dgPersonas.SelectedIndex == -1)
            {
                MessageBox.Show("No hay seleccionado");
                return;
            }

            MessageBoxResult result = MessageBox.Show($"¿Esta seguro?", "Eliminar persona", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.No) return;

            Persona persona = dgPersonas.SelectedItem as Persona;
            Personas.Remove(persona);
            dgPersonas.UnselectAll();
        }
    }
}
