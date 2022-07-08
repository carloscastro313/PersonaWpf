using PersonaWpf.Vistas;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;

namespace PersonaWpf
{
    public partial class MainWindow : Window
    {
        public List<Persona> Personas { get; set; }
        private Fetch<Persona> _fetch;
        public MainWindow(SqlConnection connection)
        {
            InitializeComponent();
            _fetch = new Fetch<Persona>(connection);

            dgPersonas.SelectedValuePath = "Id";
            ActualizarLista();
        }

        private void btnCrear_Click(object sender, RoutedEventArgs e)
        {
            PersonaForm form = new PersonaForm(_fetch);
            form.ShowDialog();

            ActualizarLista();
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {

            if (dgPersonas.SelectedIndex == -1)
            {
                MessageBox.Show("No hay seleccionado");
                return;
            }

            Persona persona = dgPersonas.SelectedItem as Persona;

            ModificarPersonaForm modificar = new ModificarPersonaForm(persona, _fetch);
            modificar.ShowDialog();

            dgPersonas.UnselectAll();
            ActualizarLista();
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

            if (_fetch.DeleteById("Persona", persona.Id)) throw new System.Exception("No se pudo eliminar :(");

            dgPersonas.UnselectAll();
            ActualizarLista();
        }

        private void ActualizarLista()
        {
            Personas = _fetch.SelectFrom("*", "persona");

            dgPersonas.ItemsSource = Personas;
        }
    }
}
