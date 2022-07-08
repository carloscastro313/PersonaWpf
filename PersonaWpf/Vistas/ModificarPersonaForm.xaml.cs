using System.Windows;

namespace PersonaWpf.Vistas
{
    /// <summary>
    /// Lógica de interacción para ModificarPersonaForm.xaml
    /// </summary>
    public partial class ModificarPersonaForm : Window
    {
        public Fetch<Persona> _fetch;
        public Persona _persona;
        public int _index;

        public ModificarPersonaForm(Persona persona, Fetch<Persona> fetch)
        {
            InitializeComponent();
            _persona = persona;
            frmPersona.DataContext = persona;
            _fetch = fetch;
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            if (!_persona.isValid())
            {
                throw new System.Exception("Todos los campos son obligatorios");
            }
            _fetch.UpdateById("Persona", _persona.Id, _persona);
            this.Close();
        }
    }
}
