using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using CRUD_Inventario.Models;
using CRUD_Inventario.Validators;

namespace CRUD_Inventario.Services
{
    /// <summary>
    /// Servicio que maneja la lógica de negocio del inventario
    /// </summary>
    public class InventarioService
    {
        private readonly BindingList<Producto> _inventario;
        private readonly PersistenciaService _persistenciaService;

        public InventarioService()
        {
            _inventario = new BindingList<Producto>();
            _persistenciaService = new PersistenciaService();
            
            // Cargar datos al inicializar
            CargarDatos();
        }

        /// <summary>
        /// Obtiene el inventario como BindingList para enlazar con DataGridView
        /// </summary>
        public BindingList<Producto> ObtenerInventario() => _inventario;

        /// <summary>
        /// Obtiene el inventario como lista
        /// </summary>
        public List<Producto> ObtenerListaInventario() => _inventario.ToList();

        /// <summary>
        /// Agrega un nuevo producto al inventario
        /// </summary>
        /// <param name="producto">Producto a agregar</param>
        /// <returns>Tupla con (exito, mensajeError)</returns>
        public (bool exito, string mensajeError) AgregarProducto(Producto producto)
        {
            // Validar producto
            var (esValido, mensajeError) = ValidadorProducto.Validar(
                producto, 
                id => !ExisteID(id) // Validar que el ID sea único
            );

            if (!esValido)
            {
                return (false, mensajeError);
            }

            try
            {
                _inventario.Add(producto);
                GuardarDatos();
                return (true, string.Empty);
            }
            catch (Exception ex)
            {
                return (false, $"Error al agregar producto: {ex.Message}");
            }
        }

        /// <summary>
        /// Actualiza un producto existente en el inventario
        /// </summary>
        /// <param name="productoOriginal">Producto original a actualizar</param>
        /// <param name="productoActualizado">Producto con los datos actualizados</param>
        /// <returns>Tupla con (exito, mensajeError)</returns>
        public (bool exito, string mensajeError) ActualizarProducto(Producto productoOriginal, Producto productoActualizado)
        {
            // Si el ID cambió, validar que el nuevo ID no exista
            if (productoOriginal.ID != productoActualizado.ID)
            {
                if (ExisteID(productoActualizado.ID))
                {
                    return (false, "El nuevo ID ya existe en el inventario.");
                }
            }

            // Validar producto actualizado
            var (esValido, mensajeError) = ValidadorProducto.Validar(
                productoActualizado,
                id => id == productoOriginal.ID || !ExisteID(id) // Permitir el ID original o uno nuevo único
            );

            if (!esValido)
            {
                return (false, mensajeError);
            }

            try
            {
                // Actualizar propiedades
                productoOriginal.ID = productoActualizado.ID;
                productoOriginal.Nombre = productoActualizado.Nombre;
                productoOriginal.Cantidad = productoActualizado.Cantidad;
                productoOriginal.Precio = productoActualizado.Precio;

                // Notificar cambios (BindingList se actualiza automáticamente)
                _inventario.ResetBindings();
                GuardarDatos();
                return (true, string.Empty);
            }
            catch (Exception ex)
            {
                return (false, $"Error al actualizar producto: {ex.Message}");
            }
        }

        /// <summary>
        /// Elimina un producto del inventario
        /// </summary>
        /// <param name="producto">Producto a eliminar</param>
        /// <returns>True si se eliminó correctamente</returns>
        public bool EliminarProducto(Producto producto)
        {
            try
            {
                bool removido = _inventario.Remove(producto);
                if (removido)
                {
                    GuardarDatos();
                }
                return removido;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al eliminar: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Verifica si existe un producto con el ID especificado
        /// </summary>
        public bool ExisteID(int id)
        {
            return _inventario.Any(p => p.ID == id);
        }

        /// <summary>
        /// Busca un producto por ID
        /// </summary>
        public Producto? BuscarPorID(int id)
        {
            return _inventario.FirstOrDefault(p => p.ID == id);
        }

        /// <summary>
        /// Obtiene estadísticas del inventario
        /// </summary>
        public (int totalProductos, int totalCantidad, decimal valorTotal) ObtenerEstadisticas()
        {
            int totalProductos = _inventario.Count;
            int totalCantidad = _inventario.Sum(p => p.Cantidad);
            decimal valorTotal = _inventario.Sum(p => p.ValorTotal);

            return (totalProductos, totalCantidad, valorTotal);
        }

        /// <summary>
        /// Carga los datos desde el archivo
        /// </summary>
        private void CargarDatos()
        {
            try
            {
                var productos = _persistenciaService.Cargar();
                _inventario.Clear();
                foreach (var producto in productos)
                {
                    _inventario.Add(producto);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al cargar datos: {ex.Message}");
            }
        }

        /// <summary>
        /// Guarda los datos en el archivo
        /// </summary>
        private void GuardarDatos()
        {
            try
            {
                var lista = _inventario.ToList();
                _persistenciaService.Guardar(lista);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al guardar datos: {ex.Message}");
                // Mostrar mensaje al usuario
                System.Windows.Forms.MessageBox.Show(
                    "Error al guardar los datos. Los cambios podrían perderse al cerrar la aplicación.",
                    "Error de Guardado",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Warning
                );
            }
        }
    }
}
