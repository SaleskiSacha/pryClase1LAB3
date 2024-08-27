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
    public partial class frmEliminar : Form
    {
        public frmEliminar()
        {
            InitializeComponent();
        }
        clsProductos objProductos = new clsProductos();
        private clsProductos objBaseDatos;
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Int32 Codigo = Convert.ToInt32(txtCodigo.Text);
            string Nombre = (txtNombre.Text);
            Decimal Precio = Convert.ToDecimal(txtPrecio.Text);
            Int32 Stock = Convert.ToInt32(txtStock.Text);
            string Categoria = (txtCategoria.Text);
            string Descripcion = (txtDescripcion.Text);

            clsProductos EProdcuto = new clsProductos();
            EProdcuto.Codigo = Codigo;
            EProdcuto.Nombre = Nombre;
            EProdcuto.Precio = Precio;
            EProdcuto.Stock = Stock;
            EProdcuto.Categoria = Categoria;
            EProdcuto.Descripcion = Descripcion;
            EProdcuto.ModificarProducto(Codigo);

            txtNombre.Text = "";
            txtCodigo.Text = "";
            txtPrecio.Text = "";
            txtStock.Text = "";
            txtCategoria.Text = "";
            txtDescripcion.Text = "";
            MessageBox.Show("Producto modificado con éxito");
            LimpiarComandos();


        }
        private void LimpiarComandos() 
        {
            txtNombre.Text = "";
            txtCodigo.Text = "";
            txtPrecio.Text = "";
            txtStock.Text = "";
            txtCategoria.Text = "";
            txtDescripcion.Text = "";
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            LimpiarComandos();
            Int32 Codigo = Convert.ToInt32(txtBuscar.Text);
            clsProductos EProdcuto = new clsProductos();
            EProdcuto.EliminarProducto(Codigo);
            txtNombre.Text = "";
            txtCodigo.Text = "";
            txtPrecio.Text = "";
            txtStock.Text = "";
            txtCategoria.Text = "";
            txtDescripcion.Text = "";
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            btnEliminar.Enabled = false;
            btnModificar.Enabled = false;
            btnGuardar.Enabled = true;

            
            txtNombre.ReadOnly = false;
            txtPrecio.ReadOnly = false;
            txtCodigo.ReadOnly = false;
            txtStock.ReadOnly = false;
            txtCategoria.ReadOnly = false;
            txtDescripcion.ReadOnly = false;

        }
        private void Habilitar() 
        {
            if (txtBuscar.Text == "")
            {
                btnModificar.Enabled = false;
                btnEliminar.Enabled = false;
            }
            else
            {
                btnModificar.Enabled = true;
                btnEliminar.Enabled = true;
            }
        }

        private void frmEliminar_Load(object sender, EventArgs e)
        {
            clsProductos Productos = new clsProductos();
            Limpiar();
        }
        private void Limpiar() 
        {
            
            txtNombre.Text = "";
            txtNombre.ReadOnly = true;
            txtPrecio.ReadOnly = true;
            txtCodigo.ReadOnly = true;
            txtStock.ReadOnly = true;
            txtCategoria.ReadOnly = true;
            txtDescripcion.ReadOnly = true;
            btnEliminar.Enabled = false;
            btnModificar.Enabled = false;
            btnGuardar.Enabled = false;

        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Productos newobj = new Productos();
            this.Hide();
            newobj.ShowDialog();
            
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            Int32 codi = Convert.ToInt32(txtBuscar.Text);
            clsProductos Productos = new clsProductos();
            Productos.Buscar(codi);
            if (Productos.Codigo != codi)
            {
                MessageBox.Show("El Producto no se encuentra registrado");
                txtBuscar.Text = "";
            }
            else
            {
                txtCodigo.Text = Convert.ToString(Productos.Codigo);
                txtNombre.Text = Productos.Nombre;
                txtPrecio.Text = Convert.ToString(Productos.Precio);
                txtStock.Text = Convert.ToString(Productos.Stock);
                txtCategoria.Text = Productos.Categoria;
                txtDescripcion.Text = Productos.Descripcion;
            }
        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            Habilitar();
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            Habilitar();
        }

        private void txtPrecio_TextChanged(object sender, EventArgs e)
        {
            Habilitar();
        }

        private void txtStock_TextChanged(object sender, EventArgs e)
        {
            Habilitar();
        }

        private void txtCategoria_TextChanged(object sender, EventArgs e)
        {
            Habilitar();
        }

        private void txtDescripcion_TextChanged(object sender, EventArgs e)
        {
            Habilitar();
        }
    }
}
