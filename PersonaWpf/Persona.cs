namespace PersonaWpf
{
    public class Persona
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Dni { get; set; }
        public string Legajo { get; set; }


        public bool isValid()
        {
            int aux = -1;
            if (string.IsNullOrEmpty(Nombre) || string.IsNullOrEmpty(Apellido) || string.IsNullOrEmpty(Dni) || string.IsNullOrEmpty(Legajo))
            {
                return false;
            }

            if (!int.TryParse(Dni, out aux) || !int.TryParse(Legajo, out aux))
            {
                return false;
            }

            return true;
        }
    }
}
