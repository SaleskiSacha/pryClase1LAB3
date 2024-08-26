using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data;

namespace pryClase1LAB3
{
    internal class clsProductos
    {


        OleDbConnection conexionBD = new OleDbConnection();
        OleDbCommand comandoBD = new OleDbCommand();
        OleDbDataReader lectorBD;
        OleDbDataAdapter adaptadorBD = new OleDbDataAdapter();
        //DataSet objDS;

        string cadenaDeConexion = @"Provider = Microsoft.ACE.OLEDB.12.0;" + " Data Source = ..\\..\\Resources\\Inventario.accdb";

        public string EstadoDeConexion = "";
        private string Tabla = "Productos";
        Int32 cod;
        string nom;
        Decimal pre;
        Int32 sto;
        string cate;
        string des;
        public Int32 Codigo 
        {
            get { return cod; }
            set { cod = value;  }
        }
        public Int32 Stock
        {
            get { return sto; }
            set { sto = value; }
        }
        public string Nombre
        {
            get { return nom; }
            set { nom = value; }
        }
        public string Categoria
        {
            get { return cate; }
            set { cate = value; }
        }
        public Decimal Precio
        {
            get { return pre; }
            set { pre = value; }
        }
        public string Descripcion
        {
            get { return des; }
            set { des = value; }
        }
        public void ConectarBD()
        {
            try
            {
                conexionBD = new OleDbConnection();
                conexionBD.ConnectionString = cadenaDeConexion;
                conexionBD.Open();
                EstadoDeConexion = "Conectado";
            }
            catch (Exception ex)
            {
                EstadoDeConexion = "Error" + ex.Message;
            }

        }

        public void AgregarProducto() 
        {
            
            try
            {
                conexionBD.ConnectionString = cadenaDeConexion;
                conexionBD.Open();
                comandoBD.Connection = conexionBD;
                comandoBD.CommandType = CommandType.TableDirect;
                comandoBD.CommandText = Tabla;

                adaptadorBD = new OleDbDataAdapter(comandoBD);
                DataSet DS = new DataSet();
                //LLENA EL DATA SET CON LOS DATOS DE LA TABLA
                adaptadorBD.Fill(DS, Tabla);
                //RECIBE LOS DATOS
                DataTable tabla = DS.Tables[Tabla];
                DataRow Fila = tabla.NewRow();

                Fila["Codigo"] = Codigo;
                Fila["Nombre"] = Nombre;
                Fila["Precio"] = Precio;
                Fila["Stock"] = Stock;
                Fila["Categoria"] = Categoria;
                Fila["Descripcion"] = Descripcion;

                tabla.Rows.Add(Fila);

                OleDbCommandBuilder HacerCompatiblesLosCambios = new OleDbCommandBuilder(adaptadorBD);
                adaptadorBD.Update(DS, Tabla);
                conexionBD.Close();
            }
            catch (Exception ) 
            {
                MessageBox.Show("No se pudo registrar cliente", "ERROR ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }
        public void Buscar(Int32 i) 
        {
            OleDbConnection conexionBD = new OleDbConnection();
            OleDbCommand comandoBD = new OleDbCommand();

            try
            {
                conexionBD.ConnectionString = cadenaDeConexion;
                conexionBD.Open();
                comandoBD.Connection = conexionBD;
                comandoBD.CommandType = CommandType.TableDirect;
                comandoBD.CommandText = Tabla;

                OleDbDataReader Lector = comandoBD.ExecuteReader();
                if (Lector.HasRows)
                {
                    while (Lector.Read())
                    {
                        if (Lector.GetInt32(0) == i)
                        {
                            Codigo = Lector.GetInt32(0);
                            Nombre = Lector.GetString(1);
                            Precio = Lector.GetDecimal(3);
                            Stock = Lector.GetInt32(4);
                            Categoria = Lector.GetString(5);
                            Descripcion = Lector.GetString(6);
                        }
                    }
                }

                conexionBD.Close();
            }
            catch (Exception MensajeAviso)
            {
                MessageBox.Show(MensajeAviso.Message);
            }
        }
        public void EliminarProducto(Int32 CodigoProducto)
        {
            try
            {
                string EProducto = "DELETE FROM Productos" + "WHERE(" + CodigoProducto + "=[Codigo])";
                conexionBD.ConnectionString = cadenaDeConexion;
                conexionBD.Open();
                comandoBD.Connection = conexionBD;
                comandoBD.CommandType = CommandType.Text;
                comandoBD.CommandText = EProducto;
                comandoBD.ExecuteNonQuery();
                conexionBD.Close();
                MessageBox.Show("Producto Eliminado con éxito");
            }
            catch (Exception Mensaje)
            {
                MessageBox.Show("El cliente no se pudo eliminar " + Mensaje.Message);
                //throw;
            }
        }
        public void ModificarProducto( Int32 CodigoProducto) 
        {
            try
            {
                String Mproducto = "UPDATE PRODUCTOS SET CODIGO= " + Codigo + ", Nombre=' " + Nombre + ", Precio= "
                    + Precio + ", Stock=" + Stock + ", Categoria='" + Categoria + ", Descripcion='" + Descripcion + "WHERE [Codigo] = " + CodigoProducto + "";
                conexionBD.ConnectionString = cadenaDeConexion;
                conexionBD.Open();
                comandoBD.Connection = conexionBD;
                comandoBD.CommandType = CommandType.Text;
                comandoBD.CommandText = Mproducto;
                comandoBD.ExecuteNonQuery();
                conexionBD.Close();
                MessageBox.Show("Producto Modificado con éxito");
            }
            catch (Exception)
            {
                MessageBox.Show("el Producto no se pudo Modificar ");
                //throw;
            }
        }
        public void ListarProductos(DataGridView grilla) 
        {
            //try
            //{
                comandoBD = new OleDbCommand();
                //conexionBD.ConnectionString = cadenaDeConexion;
                //conexionBD.Open();
                comandoBD.Connection = conexionBD;
                comandoBD.CommandType = CommandType.TableDirect;
                comandoBD.CommandText = Tabla;
                //OleDbDataReader DR = comandoBD.ExecuteReader();
                grilla.Rows.Clear();
                lectorBD = comandoBD.ExecuteReader();
                clsProductos clsProductos = new clsProductos();
                if (lectorBD.HasRows)
                {
                    while (lectorBD.Read())
                    {
                    //grilla.Rows.Add(lectorBD.GetInt32(0), lectorBD.GetString(1), lectorBD.GetDecimal(2), lectorBD.GetInt32(3), lectorBD.GetString(4), lectorBD.GetString(5));
                        grilla.Rows.Add(lectorBD[0], lectorBD[1], lectorBD[2], lectorBD[3], lectorBD[4], lectorBD[5]);

                    }
                }
                //conexionBD.Close();
            //}
            //catch (Exception Mensaje)
            //{
                //MessageBox.Show(Mensaje.Message);
                //throw;
            //}
        }
    }
}
