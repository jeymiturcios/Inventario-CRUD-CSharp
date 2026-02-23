using System;

namespace CRUD_Inventario.Models
{
    /// <summary>
    /// Representa un producto en el inventario
    /// </summary>
    public class Producto
    {
        public int ID { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }

        /// <summary>
        /// Calcula el valor total del producto (Cantidad * Precio)
        /// </summary>
        public decimal ValorTotal => Cantidad * Precio;

        public override string ToString()
        {
            return $"{ID} - {Nombre} (Cantidad: {Cantidad}, Precio: {Precio:C})";
        }
    }
}
