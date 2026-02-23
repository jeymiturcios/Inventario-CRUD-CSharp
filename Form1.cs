using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CRUD_Inventario
{
    public partial class Form1 : Form
    {
        List<Producto> inventario = new List<Producto>();

        public Form1()
        {
            InitializeComponent();
            dgvInventario.DataSource = inventario;

            
            dgvInventario.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvInventario.MultiSelect = false;
            dgvInventario.ReadOnly = true;
            dgvInventario.AllowUserToAddRows = false;

           
            dgvInventario.CellClick += dgvInventario_CellClick;
            btnAgregar.Click += btnAgregar_Click;
            btnEditar.Click += btnEditar_Click;
            btnEliminar.Click += btnEliminar_Click;
            btnLimpiar.Click += btnLimpiar_Click;
        }

        private void RefrescarGrid()
        {
            dgvInventario.DataSource = null;
            dgvInventario.DataSource = inventario;
        }

        private void LimpiarCampos()
        {
            txtID.Clear();
            txtNombre.Clear();
            txtCantidad.Clear();
            txtPrecio.Clear();
        }

        // ------------------- BOTONES -------------------

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            // Validar campos vacíos
            if (string.IsNullOrWhiteSpace(txtID.Text) ||
                string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtCantidad.Text) ||
                string.IsNullOrWhiteSpace(txtPrecio.Text))
            {
                MessageBox.Show("Complete todos los campos", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validar ID duplicado
            int id = int.Parse(txtID.Text);
            if (inventario.Any(p => p.ID == id))
            {
                MessageBox.Show("El ID ya existe", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Producto p = new Producto()
            {
                ID = id,
                Nombre = txtNombre.Text,
                Cantidad = int.Parse(txtCantidad.Text),
                Precio = decimal.Parse(txtPrecio.Text)
            };

            inventario.Add(p);
            RefrescarGrid();
            LimpiarCampos();
        

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvInventario.CurrentRow != null)
            {
                int index = dgvInventario.CurrentRow.Index;
                inventario[index].ID = int.Parse(txtID.Text);
                inventario[index].Nombre = txtNombre.Text;
                inventario[index].Cantidad = int.Parse(txtCantidad.Text);
                inventario[index].Precio = decimal.Parse(txtPrecio.Text);

                RefrescarGrid();
                LimpiarCampos();
            }
            else
            {
                MessageBox.Show("Seleccione un producto para editar");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvInventario.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un producto", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirmacion = MessageBox.Show(
                "¿Está seguro de eliminar este producto?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirmacion == DialogResult.Yes)
            {
                int index = dgvInventario.CurrentRow.Index;
                inventario.RemoveAt(index);
                RefrescarGrid();
                LimpiarCampos();
            }
        

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        // ------------------- SELECCIONAR FILA -------------------

        private void dgvInventario_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtID.Text = dgvInventario.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtNombre.Text = dgvInventario.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtCantidad.Text = dgvInventario.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtPrecio.Text = dgvInventario.Rows[e.RowIndex].Cells[3].Value.ToString();
            }
        }
    }
}
