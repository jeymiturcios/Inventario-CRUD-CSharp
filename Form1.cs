using System;
using System.Windows.Forms;
using CRUD_Inventario.Models;
using CRUD_Inventario.Services;
using CRUD_Inventario.Validators;

namespace CRUD_Inventario
{
    public partial class Form1 : Form
    {
        private readonly InventarioService _inventarioService;
        private Producto? _productoSeleccionado;

        public Form1()
        {
            InitializeComponent();
            
            // Inicializar servicio
            _inventarioService = new InventarioService();
            
            // Configurar DataGridView
            ConfigurarDataGridView();
            
            // Enlazar datos
            dgvInventario.DataSource = _inventarioService.ObtenerInventario();
            
            // Suscribir eventos
            SuscribirEventos();
        }

        private void ConfigurarDataGridView()
        {
            dgvInventario.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvInventario.MultiSelect = false;
            dgvInventario.ReadOnly = true;
            dgvInventario.AllowUserToAddRows = false;
            dgvInventario.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void SuscribirEventos()
        {
            dgvInventario.CellClick += DgvInventario_CellClick;
            dgvInventario.SelectionChanged += DgvInventario_SelectionChanged;
            btnAgregar.Click += BtnAgregar_Click;
            btnEditar.Click += BtnEditar_Click;
            btnEliminar.Click += BtnEliminar_Click;
            btnLimpiar.Click += BtnLimpiar_Click;
            
            // Limpiar selección al hacer clic fuera del grid
            this.Click += (s, e) => LimpiarSeleccion();
        }

        private void LimpiarCampos()
        {
            txtID.Clear();
            txtNombre.Clear();
            txtCantidad.Clear();
            txtPrecio.Clear();
            _productoSeleccionado = null;
        }

        private void LimpiarSeleccion()
        {
            if (dgvInventario.Focused) return;
            dgvInventario.ClearSelection();
            LimpiarCampos();
        }

        // ------------------- EVENTOS DEL DATAGRIDVIEW -------------------

        private void DgvInventario_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                CargarProductoEnFormulario(e.RowIndex);
            }
        }

        private void DgvInventario_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvInventario.SelectedRows.Count > 0)
            {
                CargarProductoEnFormulario(dgvInventario.SelectedRows[0].Index);
            }
        }

        private void CargarProductoEnFormulario(int indiceFila)
        {
            try
            {
                var producto = dgvInventario.Rows[indiceFila].DataBoundItem as Producto;
                if (producto != null)
                {
                    _productoSeleccionado = producto;
                    txtID.Text = producto.ID.ToString();
                    txtNombre.Text = producto.Nombre;
                    txtCantidad.Text = producto.Cantidad.ToString();
                    txtPrecio.Text = producto.Precio.ToString("F2");
                }
            }
            catch (Exception ex)
            {
                MostrarError($"Error al cargar producto: {ex.Message}");
            }
        }

        // ------------------- EVENTOS DE BOTONES -------------------

        private void BtnAgregar_Click(object? sender, EventArgs e)
        {
            try
            {
                // Validar campos del formulario
                var (esValido, mensajeError, producto) = ValidadorProducto.ValidarCamposFormulario(
                    txtID.Text,
                    txtNombre.Text,
                    txtCantidad.Text,
                    txtPrecio.Text
                );

                if (!esValido || producto == null)
                {
                    MostrarAdvertencia(mensajeError);
                    return;
                }

                // Intentar agregar producto
                var (exito, mensaje) = _inventarioService.AgregarProducto(producto);

                if (exito)
                {
                    LimpiarCampos();
                    MostrarExito("Producto agregado correctamente.");
                }
                else
                {
                    MostrarError(mensaje);
                }
            }
            catch (Exception ex)
            {
                MostrarError($"Error inesperado al agregar producto: {ex.Message}");
            }
        }

        private void BtnEditar_Click(object? sender, EventArgs e)
        {
            try
            {
                if (_productoSeleccionado == null)
                {
                    MostrarAdvertencia("Por favor, seleccione un producto de la tabla para editar.");
                    return;
                }

                // Validar campos del formulario
                var (esValido, mensajeError, productoActualizado) = ValidadorProducto.ValidarCamposFormulario(
                    txtID.Text,
                    txtNombre.Text,
                    txtCantidad.Text,
                    txtPrecio.Text
                );

                if (!esValido || productoActualizado == null)
                {
                    MostrarAdvertencia(mensajeError);
                    return;
                }

                // Intentar actualizar producto
                var (exito, mensaje) = _inventarioService.ActualizarProducto(
                    _productoSeleccionado,
                    productoActualizado
                );

                if (exito)
                {
                    LimpiarCampos();
                    MostrarExito("Producto actualizado correctamente.");
                }
                else
                {
                    MostrarError(mensaje);
                }
            }
            catch (Exception ex)
            {
                MostrarError($"Error inesperado al editar producto: {ex.Message}");
            }
        }

        private void BtnEliminar_Click(object? sender, EventArgs e)
        {
            try
            {
                if (_productoSeleccionado == null)
                {
                    MostrarAdvertencia("Por favor, seleccione un producto de la tabla para eliminar.");
                    return;
                }

                // Confirmar eliminación
                DialogResult confirmacion = MessageBox.Show(
                    $"¿Está seguro de eliminar el producto '{_productoSeleccionado.Nombre}' (ID: {_productoSeleccionado.ID})?",
                    "Confirmar Eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2
                );

                if (confirmacion == DialogResult.Yes)
                {
                    bool eliminado = _inventarioService.EliminarProducto(_productoSeleccionado);

                    if (eliminado)
                    {
                        LimpiarCampos();
                        MostrarExito("Producto eliminado correctamente.");
                    }
                    else
                    {
                        MostrarError("No se pudo eliminar el producto.");
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarError($"Error inesperado al eliminar producto: {ex.Message}");
            }
        }

        private void BtnLimpiar_Click(object? sender, EventArgs e)
        {
            LimpiarCampos();
            dgvInventario.ClearSelection();
        }

        // ------------------- MÉTODOS AUXILIARES -------------------

        private void MostrarError(string mensaje)
        {
            MessageBox.Show(
                mensaje,
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }

        private void MostrarAdvertencia(string mensaje)
        {
            MessageBox.Show(
                mensaje,
                "Advertencia",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );
        }

        private void MostrarExito(string mensaje)
        {
            MessageBox.Show(
                mensaje,
                "Éxito",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        // ------------------- EVENTOS DEL FORMULARIO -------------------

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Los datos se guardan automáticamente en cada operación
            // pero podemos agregar una confirmación si hay cambios sin guardar
            base.OnFormClosing(e);
        }
    }
}
