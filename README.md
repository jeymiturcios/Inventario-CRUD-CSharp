# 📦 Sistema de Gestión de Inventario

<div align="center">

![.NET Version](https://img.shields.io/badge/.NET-6.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![Windows Forms](https://img.shields.io/badge/Windows%20Forms-0078D4?style=for-the-badge&logo=windows&logoColor=white)
![License](https://img.shields.io/badge/License-MIT-green?style=for-the-badge)
![Status](https://img.shields.io/badge/Status-Active-success?style=for-the-badge)

**Un sistema completo de gestión de inventario desarrollado con C# y Windows Forms**

*Diseñado para demostrar habilidades en desarrollo de aplicaciones de escritorio para Windows*

[Características](#-características) • [Instalación](#-instalación) • [Uso](#-uso) • [Estructura](#-estructura-del-proyecto) • [Contribuir](#-contribuciones)

</div>

---

## 📑 Tabla de Contenidos

- [Características](#-características)
- [Tecnologías Utilizadas](#-tecnologías-utilizadas)
- [Requisitos del Sistema](#-requisitos-del-sistema)
- [Instalación](#-instalación)
- [Estructura del Proyecto](#-estructura-del-proyecto)
- [Uso](#-uso)
- [Ejemplos de Código](#-ejemplos-de-código)
- [Arquitectura](#-arquitectura)
- [Características Técnicas](#-características-técnicas-destacadas)
- [Roadmap](#-roadmap)
- [Contribuciones](#-contribuciones)
- [Licencia](#-licencia)

## Características

###  Funcionalidades Principales

- **CRUD Completo**: Crear, Leer, Actualizar y Eliminar productos del inventario
- ** Persistencia Automática**: Los datos se guardan automáticamente en formato JSON
- ** Carga Automática**: Los datos se cargan automáticamente al iniciar la aplicación
- ** Validaciones Completas**: Validación robusta de todos los campos (ID único, valores positivos, formatos)
- ** Manejo Robusto de Errores**: Prevención de crashes con manejo completo de excepciones
- **Interfaz Gráfica Intuitiva**: Formulario con campos claros y tabla de visualización
- **Selección Interactiva**: Haz clic en cualquier fila de la tabla para cargar los datos en el formulario
- **Limpieza de Campos**: Botón para limpiar rápidamente todos los campos del formulario
- **Vista Tabular**: Visualización organizada de todos los productos en un DataGridView

### 🎨 Diseño y UX

- **Interfaz Windows Nativa**: Diseño familiar y consistente con el sistema operativo Windows
- **DataGridView Configurado**: Tabla de solo lectura con selección de fila completa
- **Formulario Organizado**: Campos etiquetados para ID, Nombre, Cantidad y Precio
- **Botones de Acción**: Controles claramente identificados para cada operación
- **Mensajes de Error**: Feedback visual mediante MessageBox para operaciones fallidas

### 📋 Campos de Producto

- **ID** (obligatorio): Identificador único del producto (debe ser > 0 y único)
- **Nombre** (obligatorio): Nombre del producto (máximo 100 caracteres)
- **Cantidad** (obligatorio): Cantidad disponible en inventario (>= 0, máximo 1,000,000)
- **Precio** (obligatorio): Precio unitario del producto (>= 0, máximo $999,999.99)

### 🔒 Validaciones Implementadas

-  **ID único**: No se permiten IDs duplicados
-  **Valores positivos**: ID, Cantidad y Precio deben ser >= 0
-  **Validación de tipos**: Verificación de formatos numéricos correctos
-  **Campos obligatorios**: Todos los campos son requeridos
-  **Límites razonables**: Validación de rangos máximos para prevenir errores

## 🚀 Tecnologías Utilizadas

- **C#**: Lenguaje de programación orientado a objetos
- **.NET 6.0**: Framework moderno y multiplataforma
- **Windows Forms**: Biblioteca para crear aplicaciones de escritorio Windows
- **System.Text.Json**: Serialización JSON para persistencia de datos
- **System.ComponentModel**: BindingList para enlace de datos bidireccional
- **System.Collections.Generic**: Para manejo de listas y colecciones

## 📦 Instalación

### Requisitos Previos

- Windows 10 o superior
- .NET 6.0 SDK o superior
- Visual Studio 2022 (recomendado) o Visual Studio Code

### Pasos de Instalación

1. **Clona el repositorio o descarga los archivos**
   ```bash
   git clone [url-del-repositorio]
   cd Inventario-CRUD-CSharp
   ```

2. **Abre el proyecto en Visual Studio**
   - Abre `Inventario-CRUD-CSharp.csproj` en Visual Studio
   - O ejecuta: `dotnet restore` en la terminal

3. **Compila el proyecto**
   ```bash
   dotnet build
   ```

4. **Ejecuta la aplicación**
   ```bash
   dotnet run
   ```
   O presiona `F5` en Visual Studio

## 🏗️ Estructura del Proyecto

```
Inventario-CRUD-CSharp/
├── Models/
│   └── Producto.cs              # Modelo de datos con propiedades y métodos
├── Services/
│   ├── InventarioService.cs     # Lógica de negocio y operaciones CRUD
│   └── PersistenciaService.cs   # Guardado y carga de datos en JSON
├── Validators/
│   └── ValidadorProducto.cs     # Validaciones completas de productos
├── Form1.cs                      # Interfaz de usuario (solo UI y eventos)
├── Form1.Designer.cs             # Diseño y configuración de controles
├── Programs.cs                   # Punto de entrada de la aplicación
├── Inventario-CRUD-CSharp.csproj # Configuración del proyecto
├── bin/                          # Archivos compilados (Debug/Release)
└── obj/                          # Archivos temporales de compilación
```

### 📂 Descripción de Carpetas

- **Models/**: Contiene las clases de modelo de datos
- **Services/**: Contiene la lógica de negocio y servicios
- **Validators/**: Contiene las clases de validación
- **Raíz**: Contiene los formularios y archivos de configuración

## 💡 Características Técnicas Destacadas para Portafolio

### 1. Arquitectura en Capas
-  **Separación de Responsabilidades**: Modelos, Servicios, Validadores y UI separados
-  **Patrón de Servicios**: Lógica de negocio encapsulada en servicios reutilizables
-  **Single Responsibility**: Cada clase tiene una responsabilidad única
-  **Código Mantenible**: Fácil de extender y modificar

### 2. Persistencia de Datos
-  **Serialización JSON**: Guardado automático en formato JSON legible
-  **Carga Automática**: Datos persistentes entre sesiones
-  **Manejo de Errores**: Recuperación segura de datos corruptos
-  **Ubicación Estándar**: Archivo en `%AppData%\InventarioCRUD\inventario.json`

### 3. Validaciones Robustas
-  **Validación de Tipos**: Verificación de formatos numéricos
-  **Validación de Negocio**: ID único, valores positivos, límites
-  **Mensajes Claros**: Feedback específico para cada error
-  **Prevención de Errores**: Validación antes de procesar datos

### 4. Manejo de Excepciones
-  **Try-Catch Completo**: Manejo de errores en todos los métodos críticos
-  **TryParse**: Uso consistente para prevenir crashes
-  **Mensajes Informativos**: Errores claros para el usuario
-  **Aplicación Robusta**: No crashea con entrada inválida

### 5. Programación Orientada a Objetos
-  **Clases y Propiedades**: Modelo de datos bien definido
-  **Encapsulación**: Datos protegidos con validaciones
-  **Métodos de Utilidad**: Propiedades calculadas (ValorTotal)
-  **Namespaces Organizados**: Estructura clara y profesional

### 6. Enlace de Datos (Data Binding)
-  **BindingList**: Actualización automática del DataGridView
-  **Two-Way Binding**: Sincronización bidireccional
-  **Eventos Optimizados**: Selección y actualización eficiente

### 7. Código Limpio y Profesional
-  **Comentarios XML**: Documentación en código
-  **Nombres Descriptivos**: Variables y métodos claros
-  **Métodos Pequeños**: Responsabilidades específicas
-  **Buenas Prácticas**: Convenciones de C# y .NET

## 🎯 Uso

### Agregar un Producto
1. Completa los campos del formulario:
   - Ingresa un **ID numérico mayor a cero** (debe ser único)
   - Escribe el **nombre del producto** (obligatorio)
   - Ingresa la **cantidad disponible** (número entero >= 0)
   - Especifica el **precio unitario** (número decimal >= 0)
2. Haz clic en el botón **"Agregar"**
3. El producto aparecerá en la tabla de inventario
4. **Los datos se guardan automáticamente** en formato JSON

> ⚠️ **Nota**: Si el ID ya existe o hay errores de validación, verás un mensaje informativo.

### Editar un Producto
1. Haz clic en la fila del producto que deseas editar en la tabla
2. Los datos se cargarán automáticamente en el formulario
3. Modifica los campos deseados
4. Haz clic en el botón **"Editar"**
5. Los cambios se reflejarán inmediatamente en la tabla

### Eliminar un Producto
1. Haz clic en la fila del producto que deseas eliminar
2. Haz clic en el botón **"Eliminar"**
3. Confirma la eliminación en el diálogo de confirmación
4. El producto será removido del inventario
5. **Los cambios se guardan automáticamente**

### Limpiar Campos
- Haz clic en el botón **"Limpiar"** para vaciar todos los campos del formulario

## 🔧 Configuración del DataGridView

El DataGridView está configurado con las siguientes características:
- **Selección de fila completa**: Al hacer clic, se selecciona toda la fila
- **Solo lectura**: Los datos no se pueden editar directamente en la tabla
- **Una sola selección**: No se permite selección múltiple
- **Sin filas vacías**: No se muestran filas adicionales para agregar datos
- **AutoSizeColumnsMode**: Las columnas se ajustan automáticamente
- **BindingList**: Actualización automática sin necesidad de refrescar manualmente

## 💾 Persistencia de Datos

### Ubicación del Archivo
Los datos se guardan automáticamente en:
```
%AppData%\InventarioCRUD\inventario.json
```

### Características
-  **Guardado Automático**: Después de cada operación CRUD
-  **Carga Automática**: Al iniciar la aplicación
-  **Formato JSON**: Legible y fácil de editar manualmente si es necesario
-  **Manejo de Errores**: Si el archivo está corrupto, se crea uno nuevo
-  **Sin Pérdida de Datos**: Los datos persisten entre sesiones

### Ejemplo de Estructura JSON
```json
[
  {
    "ID": 1,
    "Nombre": "Producto Ejemplo",
    "Cantidad": 10,
    "Precio": 25.50
  }
]
```

## 🏛️ Arquitectura del Proyecto

### Capas de la Aplicación

```
┌─────────────────────────────────────┐
│         Capa de Presentación        │
│         (Form1.cs - UI)             │
└──────────────┬──────────────────────┘
               │
┌──────────────▼──────────────────────┐
│      Capa de Servicios              │
│  (InventarioService.cs)             │
└──────────────┬──────────────────────┘
               │
    ┌──────────┴──────────┐
    │                     │
┌───▼────────┐    ┌───────▼──────────┐
│ Validators │    │  Persistencia    │
│ (Validación)│    │  (JSON Service)  │
└────────────┘    └──────────────────┘
    │                     │
    └──────────┬──────────┘
               │
┌──────────────▼──────────────────────┐
│         Capa de Modelos             │
│         (Producto.cs)                │
└─────────────────────────────────────┘
```

### Principios Aplicados
-  **Separación de Responsabilidades**: Cada capa tiene un propósito específico
-  **Inversión de Dependencias**: La UI depende de servicios, no de implementaciones
-  **Single Responsibility**: Cada clase tiene una única responsabilidad
-  **DRY (Don't Repeat Yourself)**: Código reutilizable en servicios

## 🔮 Posibles Mejoras Futuras

### Funcionalidades Adicionales
-  **Búsqueda y Filtrado**: Buscar productos por nombre o filtrar por rango de precios
-  **Exportar Datos**: Exportar inventario a Excel o CSV
-  **Importar Datos**: Cargar productos desde archivo CSV/Excel
-  **Dashboard de Estadísticas**: Visualizar totales, valor del inventario, gráficos
-  **Notificaciones**: Alertas de stock bajo o productos próximos a agotarse

### Mejoras Técnicas
-  **SQLite**: Migrar de JSON a base de datos SQLite para mejor rendimiento
-  **Tests Unitarios**: Agregar pruebas con xUnit o NUnit
-  **Logging**: Implementar sistema de logs con Serilog o NLog
-  **Inyección de Dependencias**: Usar Microsoft.Extensions.DependencyInjection
-  **Temas y Personalización**: Modo oscuro/claro, colores personalizables
-  **Historial de Cambios**: Registrar modificaciones y eliminaciones con timestamps
-  **Autenticación**: Sistema de usuarios y permisos (opcional)
-  **Integración con API**: Conectar con backend para almacenamiento remoto

## 📝 Ejemplos de Código

### Agregar un Producto (Servicio)
```csharp
var producto = new Producto
{
    ID = 1,
    Nombre = "Producto Ejemplo",
    Cantidad = 10,
    Precio = 25.50m
};

var (exito, mensajeError) = _inventarioService.AgregarProducto(producto);
if (!exito)
{
    MessageBox.Show(mensajeError, "Error");
}
```

### Validar un Producto
```csharp
var (esValido, mensajeError) = ValidadorProducto.Validar(
    producto,
    id => !_inventarioService.ExisteID(id)
);
```

### Guardar Datos
```csharp
var persistenciaService = new PersistenciaService();
persistenciaService.Guardar(listaProductos);
```

## 🎓 Aprendizajes y Buenas Prácticas

Este proyecto demuestra:
-  Arquitectura en capas para aplicaciones de escritorio
-  Separación de responsabilidades (SRP)
-  Manejo robusto de excepciones
-  Validaciones completas de datos
-  Persistencia de datos con JSON
-  Data Binding con BindingList
-  Código limpio y mantenible
-  Buenas prácticas de C# y .NET

## 📄 Licencia

Este proyecto es de código abierto y está disponible para uso en portafolios personales.

---

## 📚 Documentación Adicional

- 📄 [Análisis del Proyecto](./ANALISIS_PROYECTO.md) - Análisis detallado de problemas y soluciones
-  [Mejoras Implementadas](./MEJORAS_IMPLEMENTADAS.md) - Resumen completo de todas las mejoras

---

Desarrollado c usando C# y Windows Forms
