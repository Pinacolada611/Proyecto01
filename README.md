# GoXela Delivery

Sistema de gestión de entregas desarrollado en **C#** con **.NET 10**, utilizando Programación Orientada a Objetos (POO).

## Descripción

GoXela Delivery es una aplicación de consola para gestionar las operaciones de una empresa de entregas en **Quetzaltenango y municipios cercanos**.

El sistema permite administrar:

* Clientes
* Repartidores
* Vehículos
* Paquetes
* Entregas
* Incidencias
* Reportes

También realiza validaciones, asignación de recursos y cálculo automático de tarifas.

## Tecnologías

* **C#**
* **.NET 10**
* **Aplicación de consola**
* **Programación Orientada a Objetos**
* **Visual Studio**

## Funcionalidades principales

* Registro y consulta de clientes.
* Gestión de repartidores y disponibilidad.
* Gestión de vehículos y capacidades.
* Gestión de paquetes según su tipo.
* Creación y seguimiento de entregas.
* Asignación de repartidores y vehículos.
* Cálculo automático de tarifas.
* Registro de incidencias.
* Finalización, cancelación y reprogramación de entregas.
* Generación de reportes.
* Matriz de distancias entre municipios.
* Validaciones y manejo de excepciones.

## POO implementada

El proyecto aplica los siguientes conceptos:

* Encapsulamiento
* Herencia
* Polimorfismo
* Sobrecarga de métodos
* Sobrescritura de métodos
* Relaciones entre clases
* Recursividad
* Manejo de excepciones
* Estructuras de datos

Principales jerarquías:

```text
Persona
├── Cliente
└── Repartidor

Vehiculo
├── Automovil
├── Motocicleta
└── Bicicleta

Paquete
├── Documento
├── PaqueteEstandar
├── PaqueteFragil
└── ProductoRefrigerado
```

## Cálculo de tarifas

La tarifa se determina considerando factores como:

* Distancia
* Peso
* Tipo de paquete
* Tipo de vehículo
* Tipo de servicio
* Recargos
* Descuentos

Los servicios disponibles son **Normal, Prioritario y Urgente**.

## Ejecución

Clonar el repositorio:

```bash
git clone URL_DEL_REPOSITORIO
```

Ingresar al proyecto:

```bash
cd GoXela-Delivery
```

Ejecutar:

```bash
dotnet run
```

Para compilar:

```bash
dotnet build
```

## Almacenamiento

Actualmente, los datos se almacenan **en memoria mediante listas**, por lo que la información se pierde al cerrar la aplicación.

## Estado del proyecto

**Versión:** 1.0
**Estado:** Proyecto académico funcional.

## Mejoras futuras

* Implementación de base de datos.
* Interfaz gráfica o aplicación web.
* Autenticación de usuarios.
* Integración de mapas y geolocalización.
* Seguimiento de entregas en tiempo real.
* Notificaciones.
* API para aplicaciones móviles.

## Licencia

Proyecto desarrollado con fines **académicos y educativos**.
