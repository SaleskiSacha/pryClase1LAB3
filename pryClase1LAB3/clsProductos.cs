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
        

        OleDbConnection conexionBD;
        OleDbCommand comandoBD;
        OleDbDataReader lectorBD;
        OleDbDataAdapter adaptadorBD;
        DataSet objDS;

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

        public void AgregarClientes() 
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
            catch (Exception ex) 
            {
                MessageBox.Show("No se pudo registrar cliente" );
            }
           
        }
        public void TraerDatos()
        {
            
        }
    }
}
