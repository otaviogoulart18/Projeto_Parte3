using System;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace SistemaCadastro

{
    internal class ConectaBanco
    {

        MySqlConnection conexao = new MySqlConnection("server=localhost;user=root;password=;database=banco_siscadastro");
        public string mensagem;
        // Método no Conecta Banco

        public bool insereBanda(Banda novaBanda)
        {
            try
            {
                conexao.Open();
                MySqlCommand cmd =
                    new MySqlCommand("sp_insereBanda", conexao);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("nome", novaBanda.Nome);
                cmd.Parameters.AddWithValue("integrantes", novaBanda.Integrantes);
                cmd.Parameters.AddWithValue("ranking", novaBanda.Ranking);
                cmd.Parameters.AddWithValue("genero", novaBanda.Genero);
                cmd.ExecuteNonQuery();//executar no banco
                return true;
            }
            catch (MySqlException erro)
            {
                mensagem = erro.Message;
                return false;
            }

        }// fim do insereBanda 
        public DataTable listaGeneros()
        {
            MySqlCommand cmd = MySqlCommand("sp_listaGeneros", conexao);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conexao.Open();
                MysqlDataAdapter da = new MysqlDataAdapter(cmd);
                DateTable tabela = new DateTable();
                da.Fill(tabela);
                return tabela;
            }
            catch (MySqlException e)
            {
                mensagem = "Errp:" + e.Message;
                return null;
            }
            conexao.Close();

        }
        public DataTable listaBandas()
        {
            MySqlCommand cmd = new MySqlCommand("sp_listaBandas", conexao);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conexao.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable tabela = new DataTable();
                da.Fill(tabela);
                return tabela;
            }// fim try
            catch (MySqlException e)
            {
                mensagem = "Erro:" + e.Message;
                return null;
            }
            finally
            {
                conexao.Close();
            }

        }// fim lista_bandas

        public bool deletaBanda(int idRemovebanda)
        {
            MySqlCommand cmd = new MySqlCommand("sp_removeBanda", conexao);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("idbanda", idRemovebanda);
            try
            {
                conexao.Open();
                cmd.ExecuteNonQuery(); // executa o comando
                return true;
            }
            catch (MySqlException e)
            {
                mensagem = "Erro:" + e.Message;
                return false;
            }
            finally
            {
                conexao.Close();
            }
        }// fim deletaBanda 

        public bool alteraBanda(Banda b, int idBanda)
        {
            MySqlCommand cmd = new MySqlCommand("update_banda", conexao)
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("id", idBanda);
            cmd.Parameters.AddWithValue("nome", b.Nome);
            cmd.Parameters.AddWithValue("integrantes", b.Integrantes);
            cmd.Parameters.AddWithValue("ranking", b.Ranking);
            cmd.Parameters.AddWithValue("genero", b.Genero);
            try
            {
                conexao.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (MySqlException e)
            {
                mensagem = "Erro:" + e.Message;
                return false;
            }
            finally
            { conexao.Close();
            }
        }
        public bool verifica(string user,string pass)
        {
            string senhaHash = Biblioteca.MakeHash(pass);
            MySqlCommand cmd = new MySqlCommand("consultaSenha", conexao);
            cmd .CommandType = CommandType.StoredProcedure;
            cmd .Parameters.AddWithValue("user", user); 
            cmd .Parameters.AddWithValue("password", senhaHash);
            try
            {
                conexao.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                    return true;
                else
                    return false;

            }catch(MySqlException ex)
            {

            }
        }
             public bool insereGenero(string nomeGenero)
        {
            try
            {
                conexao.Open();
                MySqlCommand cmd =
                    new MySqlCommand("sp_insereBanda", conexao);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("nomeGenero",.nomeGenero);
                cmd.ExecuteNonQuery();//executar no banco
                return true;
            }
            catch (MySqlException erro)
            {
                mensagem = erro.Message;
                return false;
            }

        }
        }


    }
}
