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
        /// <summary>
        /// string de connexão
        /// </summary>
        private string szConnection { get { return "Server=127.0.0.1;Database=boa_agenda;Uid=root;Pwd=root;"; } }
        /// <summary>
        /// Coleção de parametros
        /// </summary>
        private readonly MySqlParameterCollection parametros = new MySqlCommand().Parameters;

        /// <summary>
        /// Metodo que vai adicionar um paramentro a coleção;
        /// </summary>
        /// <param name="name">nome do parametro</param>
        /// <param name="value">Valor do parametro</param>
        public void AddParameter(string name, object value)
        {
            parametros.AddWithValue(name, value);
        }

        /// <summary>
        /// Metodo que vai fazer uma leitura no banco e vai retornar um tada table de acordo com o comando passado
        /// </summary>
        /// <param name="comand">Comando sql que vai ser executado</param>
        /// <param name="type"> tipo do comando, se vai ser Text ou uma procedure</param>
        /// <returns></returns>
        public DataTable ExecuteReader(string comand, CommandType type)
        {
            MySqlConnection connection = new MySqlConnection(szConnection); // Cria a instancia da connexão 
            MySqlCommand cmd = new MySqlCommand(); // cria a instancia do MySqlCommand
            DataTable table = new DataTable(); // DataTable que vai ser retornado no final do metodo

            foreach (MySqlParameter item in parametros) // Adiciona a coleção de parametros a o comando instanciado la em cima no cmd
            {
                cmd.Parameters.AddWithValue(item.ParameterName, item.Value);
            }

            cmd.CommandType = type; // Adiciona o tipo de comando passado de acordo com o parametro passado
            cmd.CommandText = comand; // comando a ser executado
            cmd.Connection = connection; // adiciona a refencia da connexão instanciada la em cima no connection
            try
            {
                connection.Open(); // abre a conexão
                cmd.ExecuteNonQuery(); // executa o comando
                MySqlDataAdapter ad = new MySqlDataAdapter(cmd); // cria a instancia do MySqlDataAdapter e passa cmd no contrutor, convertendo os dados em um objeto que o c# possa entender
                ad.Fill(table); //  joga todos os dados no DataTable criado la em cima
                connection.Close(); // fecha a connexão
                parametros.Clear(); // zera os parametro la em cima 
                return table;  // retorna o DataTable
            }
            catch (Exception ex)
            {

                return null; // caso de algum erro vai retorna um valor nulo 
            }

        }

        /// <summary>
        /// Executa qualque comando sql menos select e retorna um objeto generico
        /// </summary>
        /// <param name="comand">Comando sql que vai ser executado</param>
        /// <param name="type"> tipo do comando, se vai ser Text ou uma procedure</param>
        /// <returns></returns>
        public object ExecuteCommand(string comand, CommandType type)
        {
            MySqlConnection connection = new MySqlConnection(szConnection); // Cria a instancia da connexão 
            MySqlCommand cmd = new MySqlCommand(); // cria a instancia do MySqlCommand          

            foreach (MySqlParameter item in parametros) // Adiciona a coleção de parametros a o comando instanciado la em cima no cmd
            {
                cmd.Parameters.AddWithValue(item.ParameterName, item.Value);
            }

            cmd.CommandType = type; // Adiciona o tipo de comando passado de acordo com o parametro passado
            cmd.CommandText = comand; // comando a ser executado
            cmd.Connection = connection; // adiciona a refencia da connexão instanciada la em cima no connection

            try
            {
                connection.Open(); // abre a connexão 
                object ob = cmd.ExecuteScalar(); //retorna um comand scalar que vai 
                connection.Close();
                parametros.Clear();
                return ob; // retorna um valor qualquer, ou de acordo com a procedure procedure ou comando passado
            }
            catch (Exception ex)
            {
                throw ex; // estoura uma excessao
                //return null;
            }
           
        }
    }
}
