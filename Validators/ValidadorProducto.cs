using System;
using CRUD_Inventario.Models;

namespace CRUD_Inventario.Validators
{
    /// <summary>
    /// Clase para validar productos antes de agregarlos o actualizarlos
    /// </summary>
    public static class ValidadorProducto
    {
        /// <summary>
        /// Valida que un producto tenga todos los datos correctos
        /// </summary>
        /// <param name="producto">Producto a validar</param>
        /// <param name="validarIDUnico">Función para validar si el ID es único (opcional)</param>
        /// <returns>Tupla con (esValido, mensajeError)</returns>
        public static (bool esValido, string mensajeError) Validar(Producto producto, Func<int, bool>? validarIDUnico = null)
        {
            // Validar ID
            if (producto.ID <= 0)
            {
                return (false, "El ID debe ser un número mayor a cero.");
            }

            // Validar ID único si se proporciona la función
            if (validarIDUnico != null && !validarIDUnico(producto.ID))
            {
                return (false, "El ID ya existe en el inventario. Por favor, use un ID diferente.");
            }

            // Validar Nombre
            if (string.IsNullOrWhiteSpace(producto.Nombre))
            {
                return (false, "El nombre del producto es obligatorio.");
            }

            if (producto.Nombre.Length > 100)
            {
                return (false, "El nombre del producto no puede exceder 100 caracteres.");
            }

            // Validar Cantidad
            if (producto.Cantidad < 0)
            {
                return (false, "La cantidad no puede ser negativa.");
            }

            if (producto.Cantidad > 1000000)
            {
                return (false, "La cantidad no puede exceder 1,000,000 unidades.");
            }

            // Validar Precio
            if (producto.Precio < 0)
            {
                return (false, "El precio no puede ser negativo.");
            }

            if (producto.Precio > 999999.99m)
            {
                return (false, "El precio no puede exceder $999,999.99.");
            }

            return (true, string.Empty);
        }

        /// <summary>
        /// Valida que los campos del formulario puedan convertirse a los tipos correctos
        /// </summary>
        public static (bool esValido, string mensajeError, Producto? producto) ValidarCamposFormulario(
            string idTexto, string nombre, string cantidadTexto, string precioTexto)
        {
            // Validar ID
            if (string.IsNullOrWhiteSpace(idTexto))
            {
                return (false, "El campo ID es obligatorio.", null);
            }

            if (!int.TryParse(idTexto, out int id) || id <= 0)
            {
                return (false, "El ID debe ser un número entero mayor a cero.", null);
            }

            // Validar Nombre
            if (string.IsNullOrWhiteSpace(nombre))
            {
                return (false, "El campo Nombre es obligatorio.", null);
            }

            // Validar Cantidad
            if (string.IsNullOrWhiteSpace(cantidadTexto))
            {
                return (false, "El campo Cantidad es obligatorio.", null);
            }

            if (!int.TryParse(cantidadTexto, out int cantidad))
            {
                return (false, "La cantidad debe ser un número entero válido.", null);
            }

            // Validar Precio
            if (string.IsNullOrWhiteSpace(precioTexto))
            {
                return (false, "El campo Precio es obligatorio.", null);
            }

            if (!decimal.TryParse(precioTexto, out decimal precio))
            {
                return (false, "El precio debe ser un número válido (ejemplo: 10.50).", null);
            }

            // Crear producto temporal para validación
            var producto = new Producto
            {
                ID = id,
                Nombre = nombre.Trim(),
                Cantidad = cantidad,
                Precio = precio
            };

            return (true, string.Empty, producto);
        }
    }
}
