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
                            Precio = Lector.GetDecimal(2);
                            Stock = Lector.GetInt32(3);
                            Categoria = Lector.GetString(4);
                            Descripcion = Lector.GetString(5);
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
                string EProducto = " DELETE FROM Productos " + "WHERE(" + CodigoProducto + "=[Codigo])";
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
                using (OleDbConnection conexionBD = new OleDbConnection(cadenaDeConexion))
                {
                    conexionBD.Open();

                    // Sentencia SQL para modificar el producto con parámetros
                    string MProducto = "UPDATE Productos SET " +
                                       "Nombre = @Nombre, " +
                                       "Precio = @Precio, " +
                                       "Stock = @Stock, " +
                                       "Categoria = @Categoria, " +
                                       "Descripcion = @Descripcion " +
                                       "WHERE Codigo = @Codigo";

                    using (OleDbCommand comandoBD = new OleDbCommand(MProducto, conexionBD))
                    {
                        // Asignar los valores a los parámetros
                        comandoBD.Parameters.AddWithValue("@Nombre", Nombre);
                        comandoBD.Parameters.AddWithValue("@Precio", Precio);
                        comandoBD.Parameters.AddWithValue("@Stock", Stock);
                        comandoBD.Parameters.AddWithValue("@Categoria", Categoria);
                        comandoBD.Parameters.AddWithValue("@Descripcion", Descripcion);
                        comandoBD.Parameters.AddWithValue("@Codigo", CodigoProducto);

                        // Ejecutar la actualización
                        comandoBD.ExecuteNonQuery();
                    }
                }
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
        public Dictionary<string, int> ObtenerNivelesDeStock() 
        {
            Dictionary<string, int> nivelesDeStock = new Dictionary<string, int>();
            try
            {
                using (OleDbConnection conexionBD = new OleDbConnection(cadenaDeConexion))
                {
                    conexionBD.Open();

                    // Consulta SQL para obtener el nombre y stock de cada producto
                    string query = "SELECT Nombre, Stock FROM Productos";

                    using (OleDbCommand comandoBD = new OleDbCommand(query, conexionBD))
                    {
                        OleDbDataReader lector = comandoBD.ExecuteReader();
                        while (lector.Read())
                        {
                            string nombreProducto = lector.GetString(0);
                            int stockProducto = lector.GetInt32(1);
                            nivelesDeStock[nombreProducto] = stockProducto;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                System.Windows.Forms.MessageBox.Show("Ocurrió un error al obtener los niveles de stock: " + ex.Message);
            }
            return nivelesDeStock;
        }
        public void guardarArchivo()
        {
            try
            {

                conexionBD.ConnectionString = cadenaDeConexion;
                conexionBD.Open();
                comandoBD.Connection = conexionBD;
                comandoBD.CommandType = CommandType.TableDirect;
                comandoBD.CommandText = Tabla;
                OleDbDataReader DR = comandoBD.ExecuteReader();
                using (StreamWriter sw = new StreamWriter("datos.csv", false))
                {
                    sw.WriteLine("Listado de productos\n"); //n es para el salto de linea
                    sw.WriteLine();
                    sw.WriteLine("Codigo;Nombre;Precio;Stock;Categoria;Descripcion");
                    if (DR.HasRows)
                    {
                        while (DR.Read())
                        {
                            sw.Write(DR.GetInt32(0));
                            sw.Write(";");
                            sw.Write(DR.GetString(1));
                            sw.Write(";");
                            sw.Write(DR.GetDecimal(2));
                            sw.Write(";");
                            sw.Write(DR.GetInt32(3));
                            sw.Write(";");
                            sw.Write(DR.GetString(4));
                            sw.Write(";");
                            sw.Write(DR.GetString(5));
                            sw.Write("\n");
                        }
                    }
                    conexionBD.Close();
                    sw.Close();
                    MessageBox.Show("Exportado con exito");
                }


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
    }
}
