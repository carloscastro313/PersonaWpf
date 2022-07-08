using System.Data;

namespace PersonaWpf
{
    public class Persona
    {
        public int Id { get; set; }
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

        public static implicit operator Persona(DataRow row)
        {
            if (row["nombre"] == null || row["apellido"] == null || row["dni"] == null || row["legajo"] == null) throw new System.Exception("DataRow no compatible");

            Persona persona = new Persona()
            {
                Nombre = (string)row["nombre"],
                Apellido = (string)row["apellido"],
                Dni = (string)row["dni"],
                Legajo = (string)row["legajo"],
            };

            return persona;
        }
    }
}
