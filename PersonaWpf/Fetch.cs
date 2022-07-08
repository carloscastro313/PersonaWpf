using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace PersonaWpf
{
    public class Fetch<T> where T : class, new()
    {
        private SqlConnection connection;

        public Fetch(SqlConnection connection)
        {
            this.connection = connection;
        }

        public List<T> SelectFrom(string columns, string table)
        {
            List<T> list = new List<T>();

            string statement = $"SELECT {columns} FROM {table}";
            SqlDataAdapter adapter = new SqlDataAdapter(statement, connection);

            using (adapter)
            {
                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {

                    list.Add(RowToObject(row));
                }
            }

            return list;
        }

        public bool InsertInto(string table, T obj)
        {
            Dictionary<string, string> values = GetPropsAsDictionary(obj);

            string queryValues = GetQueryValues(values.Values.ToList());
            string queryColumns = GetQueryColumns(values.Keys.ToList());


            string statement = $"INSERT INTO {table} {queryColumns} VALUES {queryValues}";

            return ExecuteCommand(statement);
        }

        public bool DeleteById(string table, int id)
        {
            string statement = $"DELETE FROM {table} WHERE id = {id}";

            return ExecuteCommand(statement);
        }

        public bool UpdateById(string table, int id, T obj)
        {
            Dictionary<string, string> values = GetPropsAsDictionary(obj);

            string queryUpdate = GetUpdateQuery(values);

            string statement = $"UPDATE {table} SET {queryUpdate} WHERE id = {id}";

            return ExecuteCommand(statement);
        }

        private T RowToObject(DataRow values)
        {
            T obj = new T();

            PropertyInfo[] properties = obj.GetType().GetProperties();

            foreach (PropertyInfo prop in properties)
            {
                string propName = prop.Name.ToLower();
                MethodInfo method = typeof(Fetch<T>).GetMethod("SetValue", BindingFlags.Instance | BindingFlags.NonPublic);
                if (values[propName] == null && values[propName].GetType() == prop.PropertyType) continue;

                System.Type[] type = { values[propName].GetType() };

                method.MakeGenericMethod(type).Invoke(this, new object[] { prop, obj, values[propName] });
            }

            return obj;
        }

        private void SetValue<k>(PropertyInfo prop, object obj, object value)
        {
            prop.SetValue(obj, (k)value, null);
        }

        private Dictionary<string, string> GetPropsAsDictionary(T obj)
        {
            Dictionary<string, string> values = new Dictionary<string, string>();

            PropertyInfo[] properties = obj.GetType().GetProperties();

            foreach (PropertyInfo prop in properties)
            {
                if (prop.Name.ToLower() == "id") continue;

                string name = prop.Name;

                values.Add(prop.Name, prop.GetValue(obj).ToString());
            }

            return values;
        }

        private List<string> GetPropsAsList(object obj)
        {
            List<string> values = new List<string>();

            PropertyInfo[] properties = typeof(T).GetType().GetProperties();

            foreach (PropertyInfo prop in properties)
            {
                if (prop.Name.ToLower() == "id") continue;

                values.Add(prop.GetValue(obj).ToString());
            }

            return values;
        }

        private static string GetQueryValues(List<string> values)
        {
            string queryValues = "( ";

            for (int i = 0; i < values.Count; i++)
            {
                if (int.TryParse(values[i], out _))
                {
                    queryValues += values[i];
                }
                else
                {
                    queryValues += $"'{values[i]}'";
                }

                if (i != values.Count - 1)
                {
                    queryValues += ", ";
                }
            }

            queryValues += " )";

            return queryValues;
        }

        private static string GetQueryColumns(List<string> values)
        {
            string queryColumns = "( ";

            for (int i = 0; i < values.Count; i++)
            {
                queryColumns += values[i];

                if (i != values.Count - 1)
                {
                    queryColumns += ", ";
                }
            }

            queryColumns += " )";

            return queryColumns;
        }

        private static string GetUpdateQuery(Dictionary<string, string> values)
        {
            string query = "";
            int index = 0;

            foreach (var item in values)
            {
                if (item.Key == "id") continue;

                query += $"{item.Key} = ";

                if (int.TryParse(item.Value, out _))
                {
                    query += item.Value;
                }
                else
                {
                    query += $"'{item.Value}'";
                }
                index++;

                if (index < values.Count) query += ", ";
            }

            return query;
        }

        private bool ExecuteCommand(string statement)
        {
            int rowAffected = -1;

            SqlCommand command = new SqlCommand(statement, connection);

            connection.Open();

            rowAffected = command.ExecuteNonQuery();

            connection.Close();

            return rowAffected > 0;
        }
    }
}
