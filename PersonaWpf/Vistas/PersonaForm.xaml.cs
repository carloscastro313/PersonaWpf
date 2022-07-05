using System.Collections.ObjectModel;
using System.Windows;

namespace PersonaWpf.Vistas
{
    /// <summary>
    /// Lógica de interacción para PersonaForm.xaml
    /// </summary>
    public partial class PersonaForm : Window
    {
        private ObservableCollection<Persona> _personas;
        public Persona persona;

        public PersonaForm(ObservableCollection<Persona> personas)
        {
            InitializeComponent();
            persona = new Persona();
            frmPersona.DataContext = persona;
            _personas = personas;
        }

        private void btnCrear_Click(object sender, RoutedEventArgs e)
        {
            if (!persona.isValid())
            {
                throw new System.Exception("Todos los campos son obligatorios");
            }
            _personas.Add(persona);

            this.Close();
        }
    }
}
