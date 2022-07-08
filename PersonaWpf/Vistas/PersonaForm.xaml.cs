using System.Windows;

namespace PersonaWpf.Vistas
{
    /// <summary>
    /// Lógica de interacción para PersonaForm.xaml
    /// </summary>
    public partial class PersonaForm : Window
    {
        public Persona persona;
        public Fetch<Persona> _fetch;
        public PersonaForm(Fetch<Persona> fetch)
        {
            InitializeComponent();
            persona = new Persona();
            _fetch = fetch;
            frmPersona.DataContext = persona;
        }

        private void btnCrear_Click(object sender, RoutedEventArgs e)
        {
            if (!persona.isValid())
            {
                throw new System.Exception("Todos los campos son obligatorios");
            }

            _fetch.InsertInto("Persona", persona);

            this.Close();
        }
    }
}
