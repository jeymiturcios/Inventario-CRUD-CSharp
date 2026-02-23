using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using CRUD_Inventario.Models;

namespace CRUD_Inventario.Services
{
    /// <summary>
    /// Servicio para guardar y cargar datos del inventario en formato JSON
    /// </summary>
    public class PersistenciaService
    {
        private readonly string _rutaArchivo;
        private readonly JsonSerializerOptions _opcionesJson;

        public PersistenciaService(string nombreArchivo = "inventario.json")
        {
            // Guardar en la carpeta de datos de la aplicación
            string carpetaDatos = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "InventarioCRUD"
            );

            // Crear carpeta si no existe
            if (!Directory.Exists(carpetaDatos))
            {
                Directory.CreateDirectory(carpetaDatos);
            }

            _rutaArchivo = Path.Combine(carpetaDatos, nombreArchivo);

            // Configurar opciones de serialización JSON
            _opcionesJson = new JsonSerializerOptions
            {
                WriteIndented = true, // Formato legible
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
        }

        /// <summary>
        /// Guarda la lista de productos en un archivo JSON
        /// </summary>
        /// <param name="productos">Lista de productos a guardar</param>
        /// <returns>True si se guardó correctamente, False en caso contrario</returns>
        public bool Guardar(List<Producto> productos)
        {
            try
            {
                string json = JsonSerializer.Serialize(productos, _opcionesJson);
                File.WriteAllText(_rutaArchivo, json);
                return true;
            }
            catch (Exception ex)
            {
                // En una aplicación real, aquí se podría usar un logger
                System.Diagnostics.Debug.WriteLine($"Error al guardar: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Carga la lista de productos desde un archivo JSON
        /// </summary>
        /// <returns>Lista de productos cargados, o lista vacía si no existe el archivo o hay error</returns>
        public List<Producto> Cargar()
        {
            try
            {
                if (!File.Exists(_rutaArchivo))
                {
                    return new List<Producto>();
                }

                string json = File.ReadAllText(_rutaArchivo);
                
                if (string.IsNullOrWhiteSpace(json))
                {
                    return new List<Producto>();
                }

                var productos = JsonSerializer.Deserialize<List<Producto>>(json, _opcionesJson);
                return productos ?? new List<Producto>();
            }
            catch (JsonException ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al deserializar JSON: {ex.Message}");
                // Si el archivo está corrupto, devolver lista vacía
                return new List<Producto>();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al cargar: {ex.Message}");
                return new List<Producto>();
            }
        }

        /// <summary>
        /// Obtiene la ruta del archivo de datos
        /// </summary>
        public string ObtenerRutaArchivo() => _rutaArchivo;

        /// <summary>
        /// Verifica si existe el archivo de datos
        /// </summary>
        public bool ExisteArchivo() => File.Exists(_rutaArchivo);
    }
}
