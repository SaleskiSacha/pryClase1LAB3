using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;


namespace pryClase1LAB3
{
    public partial class Productos : Form
    {
        public Productos()
        {
            InitializeComponent();
        }
        clsProductos objBaseDatos;
        private void Productos_Load(object sender, EventArgs e)
        {
            objBaseDatos = new clsProductos();
            objBaseDatos.ConectarBD();

            lblStatus.Text = objBaseDatos.EstadoDeConexion;

            lblStatus.BackColor = Color.Green;
            lblStatus.ForeColor = Color.White;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmAgregar newobj = new frmAgregar();
            newobj.ShowDialog();
            this.Hide();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            frmEliminar newobj = new frmEliminar();
            newobj.ShowDialog();
            this.Hide();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {

        }

        private void controlStockYGraficosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ControlStock newobj = new ControlStock();
            newobj.ShowDialog();
            this.Hide();
        }
    }
}
