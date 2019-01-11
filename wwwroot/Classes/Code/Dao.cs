using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes.Code
{
    public class Dao
    {
        private string szConnection { get { return "Server=127.0.0.1;Database=boa_agenda;Uid=root;Pwd=root;"; } }
       private readonly MySqlParameterCollection parametros = new MySqlCommand().Parameters;
        public void AddParameter(string name, object value) { parametros.AddWithValue(name, value); }
        public DataTable ExecuteReader(string comand, CommandType type)
        {
            MySqlConnection connection = new MySqlConnection(szConnection);
            MySqlCommand com = new MySqlCommand();
            DataTable table = new DataTable();
            foreach (MySqlParameter item in parametros)
            {
                com.Parameters.AddWithValue(item.ParameterName, item.Value);
            }
            com.CommandType = type;
            com.CommandText = comand;
            com.Connection = connection;
            try
            {
                connection.Open();
                com.ExecuteNonQuery();
                MySqlDataAdapter ad = new MySqlDataAdapter(com);
                ad.Fill(table);
                return table;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                connection.Close();
                parametros.Clear();
            }
        }
        public object ExecuteCommand(string comand, CommandType type)
        {
            MySqlConnection connection = new MySqlConnection(szConnection);
            MySqlCommand com = new MySqlCommand();
            foreach (MySqlParameter item in parametros)
            {
                com.Parameters.AddWithValue(item.ParameterName, item.Value);
            }
            com.CommandType = type;
            com.CommandText = comand;
            com.Connection = connection;
            try
            {
                connection.Open();
                return com.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
                //return null;
            }
            finally
            {
                connection.Close();
                parametros.Clear();
            }
        }
    }
}
