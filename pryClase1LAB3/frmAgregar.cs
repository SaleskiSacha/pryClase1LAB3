using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.OleDb;

namespace pryClase1LAB3
{
    public partial class frmAgregar : Form
    {
        public frmAgregar()
        {
            InitializeComponent();
        }
        private clsProductos objBaseDatos;
        private void btnVolver_Click(object sender, EventArgs e)
        {
           Productos newobj = new Productos();
            this.Hide();
            newobj.ShowDialog();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            
            clsProductos clsProductos = new clsProductos();
            Int32 codi = Convert.ToInt32(txtCodigo.Text);
            clsProductos.Buscar(codi);

            if (clsProductos.Codigo != codi)
            {
                clsProductos.Codigo = Convert.ToInt32(txtCodigo.Text);
                clsProductos.Nombre = (txtNombre.Text);
                clsProductos.Precio = Convert.ToDecimal(txtPrecio.Text);
                clsProductos.Stock = Convert.ToInt32(txtStock.Text);
                clsProductos.Categoria = (txtCategoria.Text);
                clsProductos.Descripcion = (txtDescripcion.Text);
                clsProductos.AgregarProducto();
                MessageBox.Show("Producto agregado con éxito");
                txtCodigo.Text = "";
                txtNombre.Text = "";
                txtPrecio.Text = "";
                txtStock.Text = "";
                txtCategoria.Text = "";
                txtDescripcion.Text = "";
            }
            else
            {
                MessageBox.Show("Producto YA REGISTRADO");
                txtCodigo.Text = "";
                txtNombre.Text = "";
                txtPrecio.Text = "";
                txtStock.Text = "";
                txtCategoria.Text = "";
                txtDescripcion.Text = "";
            }
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            objBaseDatos.ListarProductos(dgv1);
        }

        private void frmAgregar_Load(object sender, EventArgs e)
        {
            objBaseDatos = new clsProductos();
            objBaseDatos.ConectarBD();
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            objBaseDatos = new clsProductos();
            objBaseDatos.guardarArchivo();
            dgv1.Rows.Clear();
            MessageBox.Show("Archivo exportado con Exito");
        }
    }

}
