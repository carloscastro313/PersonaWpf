using System.Collections.ObjectModel;
using System.Windows;

namespace PersonaWpf.Vistas
{
    /// <summary>
    /// Lógica de interacción para ModificarPersonaForm.xaml
    /// </summary>
    public partial class ModificarPersonaForm : Window
    {
        private ObservableCollection<Persona> _personas;
        public Persona _persona;
        public int _index;

        public ModificarPersonaForm(ObservableCollection<Persona> personas, Persona persona, int index)
        {
            InitializeComponent();
            _persona = persona;

            _index = index;
            frmPersona.DataContext = persona;
            _personas = personas;
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            if (!_persona.isValid())
            {
                throw new System.Exception("Todos los campos son obligatorios");
            }
            _personas[_index] = _persona;

            this.Close();
        }
    }
}
