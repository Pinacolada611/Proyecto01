using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

class Persona
{
    private int codigo;
    private string nombreCompleto;
    private string telefono;

    public Persona(int codigo, string nombreCompleto, string telefono)
    {
        Codigo = codigo;
        NombreCompleto = nombreCompleto;
        Telefono = telefono;
    }

    public int Codigo
    {
        get
        {
            return codigo;
        }
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value), "El código no puede ser negativo.");
            codigo = value;
        }
    }

    public string NombreCompleto
    {
        get { return nombreCompleto; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("El nombre no puede estar vacío.");
            nombreCompleto = value.Trim();
        }
    }

    public string Telefono
    {
        get { return telefono; }
        set
        {
            if (string.IsNullOrWhiteSpace(value) || !Regex.IsMatch(value, @"^\d{8}$"))
                throw new FormatException("El teléfono debe tener exactamente 8 dígitos.");
            telefono = value;
        }
    }
}

class Cliente : Persona
{
    private string correoElectronico;
    private string direccion;
    private int cantidadSolicitudes;

    public static readonly List<string> MunicipiosXela = new List<string>
    {
        "Quetzaltenango",
        "Almolonga",
        "Cabricán",
        "Cajolá",
        "Cantel",
        "Coatepeque",
        "Colomba Costa Cuca",
        "Concepción Chiquirichapa",
        "El Palmar",
        "Flores Costa Cuca",
        "Génova",
        "Huitán",
        "La Esperanza",
        "Olintepeque",
        "Ostuncalco",
        "Palestina de los Altos",
        "Salcajá",
        "San Carlos Sija",
        "San Francisco La Unión",
        "San Martín Sacatepéquez",
        "San Mateo",
        "San Miguel Sigüilá",
        "Sibilia",
        "Zunil"
    };

    public Cliente(int codigo, string nombreCompleto, string telefono,
        string correoElectronico, string direccion, int cantidadSolicitudes = 0)
        : base(codigo, nombreCompleto, telefono)
    {
        CorreoElectronico = correoElectronico;
        Direccion = direccion;
        CantidadSolicitudes = cantidadSolicitudes;
    }

    public string CorreoElectronico
    {
        get { return correoElectronico; }
        set
        {
            if (string.IsNullOrWhiteSpace(value) ||
                !Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                throw new FormatException("Correo electrónico inválido.");
            }

            correoElectronico = value.Trim();
        }
    }

    public string Direccion
    {
        get { return direccion; }
        set
        {
            string opcion = value?.Trim();

            bool valido = MunicipiosXela.Exists(
                m => string.Equals(m, opcion, StringComparison.OrdinalIgnoreCase));

            if (!valido)
                throw new ArgumentException(
                    "El municipio no pertenece al departamento de Quetzaltenango.");

            direccion = MunicipiosXela.Find(
                m => string.Equals(m, opcion, StringComparison.OrdinalIgnoreCase));
        }
    }

    public int CantidadSolicitudes
    {
        get { return cantidadSolicitudes; }
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(
                    nameof(value), "La cantidad no puede ser negativa.");

            cantidadSolicitudes = value;
        }
    }
}

class Repartidor : Persona
{
    private string numeroLicencia;
    private string tipoLicencia;
    private string estadoDisponible;
    private int cantidadEntregas;
    private double calificacionPromedio;
    private int totalCalificaciones;
    private string municipio;

    public Repartidor(int codigo, string nombreCompleto, string telefono,
        string numeroLicencia, string tipoLicencia, string municipio,
        string estadoDisponible = "Disponible",
        int cantidadEntregas = 0,
        double calificacionPromedio = 0,
        int totalCalificaciones = 0)
        : base(codigo, nombreCompleto, telefono)
    {
        NumeroLicencia = numeroLicencia;
        TipoLicencia = tipoLicencia;
        Municipio = municipio;
        EstadoDisponible = estadoDisponible;
        CantidadEntregas = cantidadEntregas;
        CalificacionPromedio = calificacionPromedio;
        TotalCalificaciones = totalCalificaciones;
    }

    public string NumeroLicencia
    {
        get { return numeroLicencia; }
        set
        {
            if (string.IsNullOrWhiteSpace(value) ||
                !Regex.IsMatch(value, @"^\d{8}$"))
            {
                throw new FormatException(
                    "La licencia debe tener exactamente 8 dígitos.");
            }

            numeroLicencia = value;
        }
    }

    public string TipoLicencia
    {
        get { return tipoLicencia; }
        set
        {
            if (value != "C" && value != "M")
                throw new ArgumentException(
                    "La licencia debe ser C o M.");

            tipoLicencia = value;
        }
    }

    public string Municipio
    {
        get { return municipio; }
        set
        {
            bool valido = Cliente.MunicipiosXela.Exists(
                m => string.Equals(m, value, StringComparison.OrdinalIgnoreCase));

            if (!valido)
                throw new ArgumentException("Municipio inválido.");

            municipio = Cliente.MunicipiosXela.Find(
                m => string.Equals(m, value, StringComparison.OrdinalIgnoreCase));
        }
    }

    public string EstadoDisponible
    {
        get { return estadoDisponible; }
        set
        {
            if (value != "Disponible" &&
                value != "Asignado" &&
                value != "Fuera de servicio")
            {
                throw new ArgumentException("Estado de repartidor inválido.");
            }

            estadoDisponible = value;
        }
    }

    public int CantidadEntregas
    {
        get { return cantidadEntregas; }
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));

            cantidadEntregas = value;
        }
    }

    public double CalificacionPromedio
    {
        get { return calificacionPromedio; }
        set
        {
            if (value < 0 || value > 5)
                throw new ArgumentOutOfRangeException(
                    nameof(value), "La calificación debe ser de 0 a 5.");

            calificacionPromedio = value;
        }
    }

    public int TotalCalificaciones
    {
        get { return totalCalificaciones; }
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));

            totalCalificaciones = value;
        }
    }

    public void AgregarCalificacion(double calificacion)
    {
        if (calificacion < 1 || calificacion > 5)
            throw new ArgumentOutOfRangeException(
                nameof(calificacion), "La calificación debe estar entre 1 y 5.");

        calificacionPromedio =
            ((calificacionPromedio * totalCalificaciones) + calificacion)
            / (totalCalificaciones + 1);

        totalCalificaciones++;
    }
}

class Vehiculo
{
    private int codigoVehiculo;
    private string placa;
    private string marca;
    private string modelo;
    private double capacidadMaxima;
    private string estadoVehiculo;
    private double costoOperativo;

    public Vehiculo(int codigoVehiculo, string placa, string marca,
        string modelo, double capacidadMaxima, string estadoVehiculo,
        double costoOperativo)
    {
        CodigoVehiculo = codigoVehiculo;
        Placa = placa;
        Marca = marca;
        Modelo = modelo;
        CapacidadMaxima = capacidadMaxima;
        EstadoVehiculo = estadoVehiculo;
        CostoOperativo = costoOperativo;
    }

    public int CodigoVehiculo
    {
        get { return codigoVehiculo; }
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));

            codigoVehiculo = value;
        }
    }

    public string Placa
    {
        get { return placa; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("La placa no puede estar vacía.");

            placa = value.Trim();
        }
    }

    public string Marca
    {
        get { return marca; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("La marca no puede estar vacía.");

            marca = value.Trim();
        }
    }

    public string Modelo
    {
        get { return modelo; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("El modelo no puede estar vacío.");

            modelo = value.Trim();
        }
    }

    public double CapacidadMaxima
    {
        get { return capacidadMaxima; }
        set
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(
                    nameof(value), "La capacidad debe ser mayor a 0.");

            capacidadMaxima = value;
        }
    }

    public string EstadoVehiculo
    {
        get { return estadoVehiculo; }
        set
        {
            if (value != "Disponible" &&
                value != "Asignado" &&
                value != "Fuera de servicio")
            {
                throw new ArgumentException("Estado de vehículo inválido.");
            }

            estadoVehiculo = value;
        }
    }

    public double CostoOperativo
    {
        get { return costoOperativo; }
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(
                    nameof(value), "El costo no puede ser negativo.");

            costoOperativo = value;
        }
    }

    public virtual bool CompatibleConPaquete(Paquete paquete)
    {
        return paquete.Peso <= CapacidadMaxima;
    }

    public virtual double FactorVehiculo()
    {
        return 1.00;
    }

    public virtual string TipoVehiculo()
    {
        return "Vehículo";
    }
}

class Automovil : Vehiculo
{
    private int cantidadDePuertas;

    public Automovil(int codigoVehiculo, string placa, string marca,
        string modelo, double capacidadMaxima, string estadoVehiculo,
        double costoOperativo, int cantidadDePuertas)
        : base(codigoVehiculo, placa, marca, modelo, capacidadMaxima,
              estadoVehiculo, costoOperativo)
    {
        CantidadDePuertas = cantidadDePuertas;
    }

    public int CantidadDePuertas
    {
        get { return cantidadDePuertas; }
        set
        {
            if (value != 2 && value != 4)
                throw new ArgumentOutOfRangeException(
                    nameof(value), "La cantidad de puertas debe ser 2 o 4.");

            cantidadDePuertas = value;
        }
    }

    public override bool CompatibleConPaquete(Paquete paquete)
    {
        return paquete.Peso <= CapacidadMaxima;
    }

    public override double FactorVehiculo()
    {
        return 1.25;
    }

    public override string TipoVehiculo()
    {
        return "Automóvil";
    }
}

class Motocicleta : Vehiculo
{
    private bool topCase;

    public Motocicleta(int codigoVehiculo, string placa, string marca,
        string modelo, double capacidadMaxima, string estadoVehiculo,
        double costoOperativo, bool topCase)
        : base(codigoVehiculo, placa, marca, modelo, capacidadMaxima,
              estadoVehiculo, costoOperativo)
    {
        TopCase = topCase;
    }

    public bool TopCase
    {
        get { return topCase; }
        set { topCase = value; }
    }

    public override bool CompatibleConPaquete(Paquete paquete)
    {
        return paquete.Peso <= CapacidadMaxima && TopCase;
    }

    public override double FactorVehiculo()
    {
        return 1.10;
    }

    public override string TipoVehiculo()
    {
        return "Motocicleta";
    }
}

class Bicicleta : Vehiculo
{
    private bool tieneCanasta;

    public Bicicleta(int codigoVehiculo, string placa, string marca,
        string modelo, double capacidadMaxima, string estadoVehiculo,
        double costoOperativo, bool tieneCanasta)
        : base(codigoVehiculo, placa, marca, modelo, capacidadMaxima,
              estadoVehiculo, costoOperativo)
    {
        TieneCanasta = tieneCanasta;
    }

    public bool TieneCanasta
    {
        get { return tieneCanasta; }
        set { tieneCanasta = value; }
    }

    public override bool CompatibleConPaquete(Paquete paquete)
    {
        return paquete.Peso <= CapacidadMaxima && TieneCanasta;
    }

    public override double FactorVehiculo()
    {
        return 0.90;
    }

    public override string TipoVehiculo()
    {
        return "Bicicleta";
    }
}

class Paquete
{
    private int codigoPaquete;
    private string descripcion;
    private double peso;
    private double valorDeclarado;
    private string direccionOrigen;
    private string direccionDestino;
    private string estadoPaquete;
    private bool estadoAsignado;

    public Paquete(int codigoPaquete, string descripcion, double peso,
        double valorDeclarado, string direccionOrigen, string direccionDestino,
        string estadoPaquete = "Pendiente", bool estadoAsignado = false)
    {
        CodigoPaquete = codigoPaquete;
        Descripcion = descripcion;
        Peso = peso;
        ValorDeclarado = valorDeclarado;
        DireccionOrigen = direccionOrigen;
        DireccionDestino = direccionDestino;
        EstadoPaquete = estadoPaquete;
        EstadoAsignado = estadoAsignado;
    }

    public int CodigoPaquete
    {
        get { return codigoPaquete; }
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));

            codigoPaquete = value;
        }
    }

    public string Descripcion
    {
        get { return descripcion; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("La descripción no puede estar vacía.");

            descripcion = value.Trim();
        }
    }

    public double Peso
    {
        get { return peso; }
        set
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(
                    nameof(value), "El peso debe ser mayor a 0.");

            peso = value;
        }
    }

    public double ValorDeclarado
    {
        get { return valorDeclarado; }
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(
                    nameof(value), "El valor no puede ser negativo.");

            valorDeclarado = value;
        }
    }

    public string DireccionOrigen
    {
        get { return direccionOrigen; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("El origen no puede estar vacío.");

            direccionOrigen = value.Trim();
        }
    }

    public string DireccionDestino
    {
        get { return direccionDestino; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("El destino no puede estar vacío.");

            direccionDestino = value.Trim();
        }
    }

    public string EstadoPaquete
    {
        get { return estadoPaquete; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("El estado no puede estar vacío.");

            estadoPaquete = value;
        }
    }

    public bool EstadoAsignado
    {
        get { return estadoAsignado; }
        set { estadoAsignado = value; }
    }

    public virtual double FactorPaquete()
    {
        return 1.00;
    }

    public virtual string TipoPaquete()
    {
        return "Estándar";
    }
}

class Documento : Paquete
{
    private string tipoDocumento;
    private string tamañoDocumento;
    private int cantidadDocumentos;

    public Documento(int codigoPaquete, string descripcion, double peso,
        double valorDeclarado, string direccionOrigen, string direccionDestino,
        string tipoDocumento, string tamañoDocumento, int cantidadDocumentos)
        : base(codigoPaquete, descripcion, peso, valorDeclarado,
              direccionOrigen, direccionDestino)
    {
        TipoDocumento = tipoDocumento;
        TamañoDocumento = tamañoDocumento;
        CantidadDocumentos = cantidadDocumentos;
    }

    public string TipoDocumento
    {
        get { return tipoDocumento; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("El tipo de documento no puede estar vacío.");

            tipoDocumento = value;
        }
    }

    public string TamañoDocumento
    {
        get { return tamañoDocumento; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("El tamaño no puede estar vacío.");

            tamañoDocumento = value;
        }
    }

    public int CantidadDocumentos
    {
        get { return cantidadDocumentos; }
        set
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(
                    nameof(value), "La cantidad debe ser mayor a 0.");

            cantidadDocumentos = value;
        }
    }

    public override double FactorPaquete()
    {
        return 0.95;
    }

    public override string TipoPaquete()
    {
        return "Documento";
    }
}

class PaqueteEstandar : Paquete
{
    private string tipoContenido;

    public PaqueteEstandar(int codigoPaquete, string descripcion, double peso,
        double valorDeclarado, string direccionOrigen, string direccionDestino,
        string tipoContenido)
        : base(codigoPaquete, descripcion, peso, valorDeclarado,
              direccionOrigen, direccionDestino)
    {
        TipoContenido = tipoContenido;
    }

    public string TipoContenido
    {
        get { return tipoContenido; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("El contenido no puede estar vacío.");

            tipoContenido = value;
        }
    }

    public override double FactorPaquete()
    {
        return 1.00;
    }

    public override string TipoPaquete()
    {
        return "Paquete estándar";
    }
}

class PaqueteFragil : Paquete
{
    private string nivelFragilidad;
    private bool requiereManipulacionEspecial;

    public PaqueteFragil(int codigoPaquete, string descripcion, double peso,
        double valorDeclarado, string direccionOrigen, string direccionDestino,
        string nivelFragilidad, bool requiereManipulacionEspecial)
        : base(codigoPaquete, descripcion, peso, valorDeclarado,
              direccionOrigen, direccionDestino)
    {
        NivelFragilidad = nivelFragilidad;
        RequiereManipulacionEspecial = requiereManipulacionEspecial;
    }

    public string NivelFragilidad
    {
        get { return nivelFragilidad; }
        set
        {
            if (value != "Baja" && value != "Media" && value != "Alta")
                throw new ArgumentException(
                    "La fragilidad debe ser Baja, Media o Alta.");

            nivelFragilidad = value;
        }
    }

    public bool RequiereManipulacionEspecial
    {
        get { return requiereManipulacionEspecial; }
        set { requiereManipulacionEspecial = value; }
    }

    public override double FactorPaquete()
    {
        return 1.20;
    }

    public override string TipoPaquete()
    {
        return "Paquete frágil";
    }
}

class ProductoRefrigerado : Paquete
{
    private double temperaturaMinima;
    private double temperaturaMaxima;

    public ProductoRefrigerado(int codigoPaquete, string descripcion, double peso,
        double valorDeclarado, string direccionOrigen, string direccionDestino,
        double temperaturaMinima, double temperaturaMaxima)
        : base(codigoPaquete, descripcion, peso, valorDeclarado,
              direccionOrigen, direccionDestino)
    {
        TemperaturaMinima = temperaturaMinima;
        TemperaturaMaxima = temperaturaMaxima;
    }

    public double TemperaturaMinima
    {
        get { return temperaturaMinima; }
        set { temperaturaMinima = value; }
    }

    public double TemperaturaMaxima
    {
        get { return temperaturaMaxima; }
        set
        {
            if (value < temperaturaMinima)
                throw new ArgumentException(
                    "La temperatura máxima no puede ser menor que la mínima.");

            temperaturaMaxima = value;
        }
    }

    public override double FactorPaquete()
    {
        return 1.35;
    }

    public override string TipoPaquete()
    {
        return "Producto refrigerado";
    }
}

class Incidencia
{
    private int codigoIncidencia;
    private string tipoIncidencia;
    private string descripcion;
    private DateTime fecha;
    private string estadoIncidencia;
    private string accionTomada;

    public Incidencia(int codigoIncidencia, string tipoIncidencia,
        string descripcion, string estadoIncidencia = "Abierta",
        string accionTomada = "Pendiente")
    {
        CodigoIncidencia = codigoIncidencia;
        TipoIncidencia = tipoIncidencia;
        Descripcion = descripcion;
        Fecha = DateTime.Now;
        EstadoIncidencia = estadoIncidencia;
        AccionTomada = accionTomada;
    }

    public int CodigoIncidencia
    {
        get { return codigoIncidencia; }
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));

            codigoIncidencia = value;
        }
    }

    public string TipoIncidencia
    {
        get { return tipoIncidencia; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("El tipo no puede estar vacío.");

            tipoIncidencia = value;
        }
    }

    public string Descripcion
    {
        get { return descripcion; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("La descripción no puede estar vacía.");

            descripcion = value;
        }
    }

    public DateTime Fecha
    {
        get { return fecha; }
        set { fecha = value; }
    }

    public string EstadoIncidencia
    {
        get { return estadoIncidencia; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("El estado no puede estar vacío.");

            estadoIncidencia = value;
        }
    }

    public string AccionTomada
    {
        get { return accionTomada; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("La acción no puede estar vacía.");

            accionTomada = value;
        }
    }
}

class Entrega
{
    private int codigoEntrega;
    private Cliente cliente;
    private Paquete paquete;
    private Repartidor repartidor;
    private Vehiculo vehiculo;
    private List<Incidencia> incidencias = new List<Incidencia>();
    private DateTime fechaSolicitud;
    private double distanciaEstimada;
    private string municipioDestino;
    private string tipoServicio;
    private string estadoEntrega;
    private double tarifaBase;
    private double recargos;
    private double descuentos;
    private double total;

    public Entrega(int codigoEntrega, Cliente cliente, Paquete paquete,
        Repartidor repartidor, Vehiculo vehiculo, string municipioDestino,
        double distanciaEstimada, string tipoServicio)
    {
        if (cliente == null || paquete == null ||
            repartidor == null || vehiculo == null)
        {
            throw new ArgumentException(
                "La entrega necesita cliente, paquete, repartidor y vehículo.");
        }

        CodigoEntrega = codigoEntrega;
        Cliente = cliente;
        Paquete = paquete;
        Repartidor = repartidor;
        Vehiculo = vehiculo;
        MunicipioDestino = municipioDestino;
        DistanciaEstimada = distanciaEstimada;
        TipoServicio = tipoServicio;
        FechaSolicitud = DateTime.Now;
        EstadoEntrega = "Solicitada";

        CalcularTarifa();
    }

    public int CodigoEntrega
    {
        get { return codigoEntrega; }
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));

            codigoEntrega = value;
        }
    }

    public Cliente Cliente
    {
        get { return cliente; }
        set
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            cliente = value;
        }
    }

    public Paquete Paquete
    {
        get { return paquete; }
        set
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            paquete = value;
        }
    }

    public Repartidor Repartidor
    {
        get { return repartidor; }
        set
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            repartidor = value;
        }
    }

    public Vehiculo Vehiculo
    {
        get { return vehiculo; }
        set
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            vehiculo = value;
        }
    }

    public List<Incidencia> Incidencias
    {
        get { return incidencias; }
    }

    public DateTime FechaSolicitud
    {
        get { return fechaSolicitud; }
        set { fechaSolicitud = value; }
    }

    public double DistanciaEstimada
    {
        get { return distanciaEstimada; }
        set
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(
                    nameof(value), "La distancia debe ser mayor a 0.");

            distanciaEstimada = value;
        }
    }

    public string MunicipioDestino
    {
        get { return municipioDestino; }
        set
        {
            bool valido = Cliente.MunicipiosXela.Exists(
                m => string.Equals(m, value, StringComparison.OrdinalIgnoreCase));

            if (!valido)
                throw new ArgumentException("Municipio inválido.");

            municipioDestino = Cliente.MunicipiosXela.Find(
                m => string.Equals(m, value, StringComparison.OrdinalIgnoreCase));
        }
    }

    public string TipoServicio
    {
        get { return tipoServicio; }
        set
        {
            if (value != "Normal" &&
                value != "Prioritario" &&
                value != "Urgente")
            {
                throw new ArgumentException(
                    "El servicio debe ser Normal, Prioritario o Urgente.");
            }

            tipoServicio = value;
        }
    }

    public string EstadoEntrega
    {
        get { return estadoEntrega; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Estado inválido.");

            estadoEntrega = value;
        }
    }

    public double TarifaBase
    {
        get { return tarifaBase; }
        private set { tarifaBase = value; }
    }

    public double Recargos
    {
        get { return recargos; }
        private set { recargos = value; }
    }

    public double Descuentos
    {
        get { return descuentos; }
        private set { descuentos = value; }
    }

    public double Total
    {
        get { return total; }
        private set { total = value; }
    }

    public void CalcularTarifa()
    {
        double baseCalculo =
            15 + (DistanciaEstimada * 3.00) + (Paquete.Peso * 2.00);

        TarifaBase =
            baseCalculo *
            Paquete.FactorPaquete() *
            Vehiculo.FactorVehiculo();

        Recargos = 0;
        Descuentos = 0;

        if (TipoServicio == "Prioritario")
            Recargos += TarifaBase * 0.15;

        if (TipoServicio == "Urgente")
            Recargos += TarifaBase * 0.30;

        if (Paquete is PaqueteFragil)
            Recargos += TarifaBase * 0.10;

        if (Paquete is ProductoRefrigerado)
            Recargos += TarifaBase * 0.20;

        if (DistanciaEstimada <= 5)
            Descuentos += TarifaBase * 0.05;

        Total = TarifaBase + Recargos - Descuentos;
    }

    public void CalcularTarifa(double descuentoManual)
    {
        if (descuentoManual < 0 || descuentoManual > 100)
            throw new ArgumentOutOfRangeException(
                nameof(descuentoManual),
                "El descuento debe estar entre 0 y 100.");

        CalcularTarifa();

        Descuentos += TarifaBase * (descuentoManual / 100);
        Total = TarifaBase + Recargos - Descuentos;
    }

    public void AgregarIncidencia(Incidencia incidencia)
    {
        if (incidencia == null)
            throw new ArgumentNullException(nameof(incidencia));

        incidencias.Add(incidencia);
    }
}

class Progeam
{
    static List<Cliente> clientes = new List<Cliente>();
    static List<Repartidor> repartidores = new List<Repartidor>();
    static List<Vehiculo> vehiculos = new List<Vehiculo>();
    static List<Paquete> paquetes = new List<Paquete>();
    static List<Entrega> entregas = new List<Entrega>();
    static List<Incidencia> incidencias = new List<Incidencia>();

    static double[,] matrizDistancias = new double[24, 24];

    static void Main()
    {
        InicializarSistema();
        MenuPrincipal();
    }

    static void InicializarSistema()
    {
        for (int i = 0; i < Cliente.MunicipiosXela.Count; i++)
        {
            for (int j = 0; j < Cliente.MunicipiosXela.Count; j++)
            {
                if (i == j)
                    matrizDistancias[i, j] = 0;
                else
                    matrizDistancias[i, j] =
                        Math.Round(Math.Abs(i - j) * 2.5 + 5, 1);
            }
        }

        int codigoRepartidor = 1;

        for (int i = 0; i < Cliente.MunicipiosXela.Count; i++)
        {
            int cantidadRepartidores = i == 0 ? 4 : 2;

            for (int j = 1; j <= cantidadRepartidores; j++)
            {
                string tipoLicencia = j % 2 == 0 ? "M" : "C";

                repartidores.Add(
                    new Repartidor(
                        codigoRepartidor,
                        "Repartidor " + codigoRepartidor,
                        "12345678",
                        (10000000 + codigoRepartidor).ToString(),
                        tipoLicencia,
                        Cliente.MunicipiosXela[i]
                    )
                );

                codigoRepartidor++;
            }
        }

        vehiculos.Add(
            new Automovil(
                1, "P001ABC", "Toyota", "Yaris",
                1000, "Disponible", 20, 4
            )
        );

        vehiculos.Add(
            new Motocicleta(
                2, "M002ABC", "Honda", "CB125",
                25, "Disponible", 10, true
            )
        );

        vehiculos.Add(
            new Bicicleta(
                3, "B003ABC", "GW", "Urbana",
                10, "Disponible", 5, true
            )
        );

        vehiculos.Add(
            new Automovil(
                4, "P004ABC", "Kia", "Picanto",
                700, "Disponible", 18, 4
            )
        );
    }

    static void MenuPrincipal()
    {
        Console.Clear();

        Console.WriteLine("========================================");
        Console.WriteLine("             GOXELA DELIVERY            ");
        Console.WriteLine("========================================");
        Console.WriteLine("1. Gestión de clientes");
        Console.WriteLine("2. Gestión de repartidores");
        Console.WriteLine("3. Gestión de vehículos");
        Console.WriteLine("4. Gestión de paquetes");
        Console.WriteLine("5. Gestión de entregas");
        Console.WriteLine("6. Gestión de incidencias");
        Console.WriteLine("7. Reportes");
        Console.WriteLine("8. Salir");
        Console.Write("Seleccione una opción: ");

        string opcion = Console.ReadLine();

        switch (opcion)
        {
            case "1":
                MenuClientes();
                break;

            case "2":
                MenuRepartidores();
                break;

            case "3":
                MenuVehiculos();
                break;

            case "4":
                MenuPaquetes();
                break;

            case "5":
                MenuEntregas();
                break;

            case "6":
                MenuIncidencias();
                break;

            case "7":
                MenuReportes();
                break;

            case "8":
                return;

            default:
                Console.WriteLine("Opción inválida.");
                Pausar();
                break;
        }

        MenuPrincipal();
    }

    static void MenuClientes()
    {
        Console.Clear();

        Console.WriteLine("--- GESTIÓN DE CLIENTES ---");
        Console.WriteLine("1. Registrar cliente");
        Console.WriteLine("2. Consultar clientes");
        Console.WriteLine("3. Volver");
        Console.Write("Opción: ");

        switch (Console.ReadLine())
        {
            case "1":
                RegistrarCliente();
                break;

            case "2":
                MostrarClientes();
                break;

            case "3":
                return;

            default:
                Console.WriteLine("Opción inválida.");
                break;
        }

        Pausar();
        MenuClientes();
    }

    static void RegistrarCliente()
    {
        try
        {
            int codigo = LeerEntero("Código: ");

            if (clientes.Exists(c => c.Codigo == codigo))
                throw new ArgumentException("Ese código ya existe.");

            string nombre = LeerTexto("Nombre completo: ");
            string telefono = LeerTexto("Teléfono: ");
            string correo = LeerTexto("Correo electrónico: ");

            Console.WriteLine("Municipios disponibles:");
            MostrarMunicipios();

            string municipio = LeerTexto("Municipio: ");

            clientes.Add(
                new Cliente(
                    codigo,
                    nombre,
                    telefono,
                    correo,
                    municipio
                )
            );

            Console.WriteLine("Cliente registrado correctamente.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }

    static void MostrarClientes()
    {
        if (clientes.Count == 0)
        {
            Console.WriteLine("No hay clientes registrados.");
            return;
        }

        foreach (Cliente cliente in clientes)
        {
            Console.WriteLine(
                $"Código: {cliente.Codigo} | " +
                $"Nombre: {cliente.NombreCompleto} | " +
                $"Teléfono: {cliente.Telefono} | " +
                $"Municipio: {cliente.Direccion}"
            );
        }
    }

    static void MenuRepartidores()
    {
        Console.Clear();

        Console.WriteLine("--- GESTIÓN DE REPARTIDORES ---");
        Console.WriteLine("1. Consultar repartidores");
        Console.WriteLine("2. Calificar repartidor");
        Console.WriteLine("3. Volver");
        Console.Write("Opción: ");

        switch (Console.ReadLine())
        {
            case "1":
                MostrarRepartidores();
                break;

            case "2":
                CalificarRepartidor();
                break;

            case "3":
                return;

            default:
                Console.WriteLine("Opción inválida.");
                break;
        }

        Pausar();
        MenuRepartidores();
    }

    static void MostrarRepartidores()
    {
        foreach (Repartidor repartidor in repartidores)
        {
            Console.WriteLine(
                $"{repartidor.Codigo} | " +
                $"{repartidor.NombreCompleto} | " +
                $"{repartidor.Municipio} | " +
                $"Licencia: {repartidor.TipoLicencia} | " +
                $"Estado: {repartidor.EstadoDisponible} | " +
                $"Entregas: {repartidor.CantidadEntregas} | " +
                $"Calificación: {repartidor.CalificacionPromedio:F2}"
            );
        }
    }

    static void CalificarRepartidor()
    {
        try
        {
            int codigo = LeerEntero("Código del repartidor: ");

            Repartidor repartidor =
                repartidores.Find(r => r.Codigo == codigo);

            if (repartidor == null)
                throw new ArgumentException("Repartidor no encontrado.");

            bool tieneEntregaFinalizada =
                entregas.Exists(
                    e => e.Repartidor == repartidor &&
                         e.EstadoEntrega == "Finalizada"
                );

            if (!tieneEntregaFinalizada)
                throw new InvalidOperationException(
                    "Solo se puede calificar después de una entrega finalizada."
                );

            double nota = LeerDouble("Calificación de 1 a 5: ");

            repartidor.AgregarCalificacion(nota);

            Console.WriteLine("Calificación registrada.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }

    static void MenuVehiculos()
    {
        Console.Clear();

        Console.WriteLine("--- GESTIÓN DE VEHÍCULOS ---");
        Console.WriteLine("1. Consultar vehículos");
        Console.WriteLine("2. Registrar vehículo");
        Console.WriteLine("3. Volver");
        Console.Write("Opción: ");

        switch (Console.ReadLine())
        {
            case "1":
                MostrarVehiculos();
                break;

            case "2":
                RegistrarVehiculo();
                break;

            case "3":
                return;

            default:
                Console.WriteLine("Opción inválida.");
                break;
        }

        Pausar();
        MenuVehiculos();
    }

    static void MostrarVehiculos()
    {
        if (vehiculos.Count == 0)
        {
            Console.WriteLine("No hay vehículos registrados.");
            return;
        }

        foreach (Vehiculo vehiculo in vehiculos)
        {
            Console.WriteLine(
                $"{vehiculo.CodigoVehiculo} | " +
                $"{vehiculo.TipoVehiculo()} | " +
                $"Placa: {vehiculo.Placa} | " +
                $"Capacidad: {vehiculo.CapacidadMaxima} kg | " +
                $"Estado: {vehiculo.EstadoVehiculo}"
            );
        }
    }

    static void RegistrarVehiculo()
    {
        try
        {
            int codigo = LeerEntero("Código: ");

            if (vehiculos.Exists(v => v.CodigoVehiculo == codigo))
                throw new ArgumentException("Ese código ya existe.");

            Console.WriteLine("1. Automóvil");
            Console.WriteLine("2. Motocicleta");
            Console.WriteLine("3. Bicicleta");

            string tipo = LeerTexto("Tipo: ");

            string placa = LeerTexto("Placa: ");
            string marca = LeerTexto("Marca: ");
            string modelo = LeerTexto("Modelo: ");
            double capacidad = LeerDouble("Capacidad máxima en kg: ");
            double costo = LeerDouble("Costo operativo: ");

            if (tipo == "1")
            {
                int puertas = LeerEntero("Cantidad de puertas (2 o 4): ");

                vehiculos.Add(
                    new Automovil(
                        codigo, placa, marca, modelo,
                        capacidad, "Disponible",
                        costo, puertas
                    )
                );
            }
            else if (tipo == "2")
            {
                bool topCase =
                    LeerTexto("¿Tiene Top Case? S/N: ").ToUpper() == "S";

                vehiculos.Add(
                    new Motocicleta(
                        codigo, placa, marca, modelo,
                        capacidad, "Disponible",
                        costo, topCase
                    )
                );
            }
            else if (tipo == "3")
            {
                bool canasta =
                    LeerTexto("¿Tiene canasta? S/N: ").ToUpper() == "S";

                vehiculos.Add(
                    new Bicicleta(
                        codigo, placa, marca, modelo,
                        capacidad, "Disponible",
                        costo, canasta
                    )
                );
            }
            else
            {
                throw new ArgumentException("Tipo de vehículo inválido.");
            }

            Console.WriteLine("Vehículo registrado correctamente.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }

    static void MenuPaquetes()
    {
        Console.Clear();

        Console.WriteLine("--- GESTIÓN DE PAQUETES ---");
        Console.WriteLine("1. Registrar paquete");
        Console.WriteLine("2. Consultar paquetes");
        Console.WriteLine("3. Volver");
        Console.Write("Opción: ");

        switch (Console.ReadLine())
        {
            case "1":
                RegistrarPaquete();
                break;

            case "2":
                MostrarPaquetes();
                break;

            case "3":
                return;

            default:
                Console.WriteLine("Opción inválida.");
                break;
        }

        Pausar();
        MenuPaquetes();
    }

    static void RegistrarPaquete()
    {
        try
        {
            int codigo = LeerEntero("Código del paquete: ");

            if (paquetes.Exists(p => p.CodigoPaquete == codigo))
                throw new ArgumentException("Ese código ya existe.");

            string descripcion = LeerTexto("Descripción: ");
            double peso = LeerDouble("Peso en kg: ");
            double valor = LeerDouble("Valor declarado: ");
            string origen = LeerTexto("Dirección de origen: ");

            Console.WriteLine("Municipios de destino:");
            MostrarMunicipios();

            string destino = LeerTexto("Municipio destino: ");

            Console.WriteLine("1. Documento");
            Console.WriteLine("2. Paquete estándar");
            Console.WriteLine("3. Paquete frágil");
            Console.WriteLine("4. Producto refrigerado");

            string tipo = LeerTexto("Tipo de paquete: ");

            Paquete paquete;

            if (tipo == "1")
            {
                paquete = new Documento(
                    codigo,
                    descripcion,
                    peso,
                    valor,
                    origen,
                    destino,
                    LeerTexto("Tipo de documento: "),
                    LeerTexto("Tamaño del documento: "),
                    LeerEntero("Cantidad de documentos: ")
                );
            }
            else if (tipo == "2")
            {
                paquete = new PaqueteEstandar(
                    codigo,
                    descripcion,
                    peso,
                    valor,
                    origen,
                    destino,
                    LeerTexto("Tipo de contenido: ")
                );
            }
            else if (tipo == "3")
            {
                paquete = new PaqueteFragil(
                    codigo,
                    descripcion,
                    peso,
                    valor,
                    origen,
                    destino,
                    LeerTexto("Nivel de fragilidad (Baja/Media/Alta): "),
                    LeerTexto("¿Requiere manipulación especial? S/N: ")
                        .ToUpper() == "S"
                );
            }
            else if (tipo == "4")
            {
                paquete = new ProductoRefrigerado(
                    codigo,
                    descripcion,
                    peso,
                    valor,
                    origen,
                    destino,
                    LeerDouble("Temperatura mínima: "),
                    LeerDouble("Temperatura máxima: ")
                );
            }
            else
            {
                throw new ArgumentException("Tipo de paquete inválido.");
            }

            paquetes.Add(paquete);

            Console.WriteLine("Paquete registrado correctamente.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }

    static void MostrarPaquetes()
    {
        if (paquetes.Count == 0)
        {
            Console.WriteLine("No hay paquetes registrados.");
            return;
        }

        foreach (Paquete paquete in paquetes)
        {
            Console.WriteLine(
                $"{paquete.CodigoPaquete} | " +
                $"{paquete.TipoPaquete()} | " +
                $"{paquete.Descripcion} | " +
                $"{paquete.Peso} kg | " +
                $"Destino: {paquete.DireccionDestino} | " +
                $"Asignado: {paquete.EstadoAsignado}"
            );
        }
    }

    static void MenuEntregas()
    {
        Console.Clear();

        Console.WriteLine("--- GESTIÓN DE ENTREGAS ---");
        Console.WriteLine("1. Crear entrega");
        Console.WriteLine("2. Consultar entregas activas");
        Console.WriteLine("3. Finalizar entrega");
        Console.WriteLine("4. Cancelar entrega");
        Console.WriteLine("5. Reprogramar entrega");
        Console.WriteLine("6. Volver");
        Console.Write("Opción: ");

        switch (Console.ReadLine())
        {
            case "1":
                CrearEntrega();
                break;

            case "2":
                MostrarActivas();
                break;

            case "3":
                CambiarEstadoEntrega("Finalizada");
                break;

            case "4":
                CambiarEstadoEntrega("Cancelada");
                break;

            case "5":
                ReprogramarEntrega();
                break;

            case "6":
                return;

            default:
                Console.WriteLine("Opción inválida.");
                break;
        }

        Pausar();
        MenuEntregas();
    }

    static void CrearEntrega()
    {
        try
        {
            if (clientes.Count == 0)
                throw new InvalidOperationException(
                    "Primero debe registrar un cliente.");

            if (paquetes.Count == 0)
                throw new InvalidOperationException(
                    "Primero debe registrar un paquete.");

            Console.WriteLine("CLIENTES");
            MostrarClientes();

            int codigoCliente = LeerEntero("Código del cliente: ");

            Cliente cliente =
                clientes.Find(c => c.Codigo == codigoCliente);

            if (cliente == null)
                throw new ArgumentException("Cliente no encontrado.");

            Console.WriteLine();
            Console.WriteLine("PAQUETES");
            MostrarPaquetes();

            int codigoPaquete = LeerEntero("Código del paquete: ");

            Paquete paquete =
                paquetes.Find(p => p.CodigoPaquete == codigoPaquete);

            if (paquete == null)
                throw new ArgumentException("Paquete no encontrado.");

            if (paquete.EstadoAsignado)
                throw new InvalidOperationException(
                    "El paquete ya está asignado a una entrega.");

            Console.WriteLine();
            Console.WriteLine("MUNICIPIOS DE DESTINO");
            MostrarMunicipios();

            int indiceDestino = LeerIndiceMunicipio();
            string municipioDestino =
                Cliente.MunicipiosXela[indiceDestino];

            int indiceOrigen =
                BuscarMunicipio(cliente.Direccion);

            double distancia =
                matrizDistancias[indiceOrigen, indiceDestino];

            Vehiculo vehiculo =
                BuscarVehiculoCompatible(paquete);

            if (vehiculo == null)
                throw new InvalidOperationException(
                    "No existe un vehículo disponible y compatible.");

            Repartidor repartidor =
                BuscarRepartidorDisponible(
                    municipioDestino,
                    paquete.Peso,
                    vehiculo
                );

            if (repartidor == null)
                throw new InvalidOperationException(
                    "No existe un repartidor disponible en ese municipio con licencia compatible.");

            string servicio = LeerServicio();

            int codigoEntrega = entregas.Count + 1;

            Entrega entrega =
                new Entrega(
                    codigoEntrega,
                    cliente,
                    paquete,
                    repartidor,
                    vehiculo,
                    municipioDestino,
                    distancia,
                    servicio
                );

            entregas.Add(entrega);

            paquete.EstadoAsignado = true;
            paquete.EstadoPaquete = "En tránsito";

            repartidor.EstadoDisponible = "Asignado";
            repartidor.CantidadEntregas++;

            vehiculo.EstadoVehiculo = "Asignado";

            cliente.CantidadSolicitudes++;

            Console.WriteLine();
            Console.WriteLine("========================================");
            Console.WriteLine("         ENTREGA CREADA                 ");
            Console.WriteLine("========================================");
            Console.WriteLine("Municipio destino: " + municipioDestino);
            Console.WriteLine("Distancia: " + distancia.ToString("F1") + " km");
            Console.WriteLine("Repartidor: " + repartidor.NombreCompleto);
            Console.WriteLine("Vehículo: " + vehiculo.TipoVehiculo());
            Console.WriteLine("Servicio: " + servicio);
            Console.WriteLine("Tarifa base: Q" + entrega.TarifaBase.ToString("F2"));
            Console.WriteLine("Recargos: Q" + entrega.Recargos.ToString("F2"));
            Console.WriteLine("Descuentos: Q" + entrega.Descuentos.ToString("F2"));
            Console.WriteLine("TOTAL: Q" + entrega.Total.ToString("F2"));
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }

    static Repartidor BuscarRepartidorDisponible(
        string municipio,
        double peso,
        Vehiculo vehiculo)
    {
        foreach (Repartidor repartidor in repartidores)
        {
            if (repartidor.Municipio != municipio)
                continue;

            if (repartidor.EstadoDisponible != "Disponible")
                continue;

            if (vehiculo is Automovil &&
                repartidor.TipoLicencia != "C")
                continue;

            if (vehiculo is Motocicleta &&
                repartidor.TipoLicencia != "M")
                continue;

            if (peso <= 0)
                continue;

            return repartidor;
        }

        return null;
    }

    static Vehiculo BuscarVehiculoCompatible(Paquete paquete)
    {
        foreach (Vehiculo vehiculo in vehiculos)
        {
            if (vehiculo.EstadoVehiculo != "Disponible")
                continue;

            if (vehiculo.CompatibleConPaquete(paquete))
                return vehiculo;
        }

        return null;
    }

    static void MostrarActivas()
    {
        bool encontro = false;

        foreach (Entrega entrega in entregas)
        {
            if (entrega.EstadoEntrega != "Finalizada" &&
                entrega.EstadoEntrega != "Cancelada")
            {
                MostrarEntrega(entrega);
                encontro = true;
            }
        }

        if (!encontro)
            Console.WriteLine("No hay entregas activas.");
    }

    static void MostrarEntrega(Entrega entrega)
    {
        Console.WriteLine(
            $"Entrega {entrega.CodigoEntrega} | " +
            $"Cliente: {entrega.Cliente.NombreCompleto} | " +
            $"Paquete: {entrega.Paquete.TipoPaquete()} | " +
            $"Destino: {entrega.MunicipioDestino} | " +
            $"Distancia: {entrega.DistanciaEstimada:F1} km | " +
            $"Servicio: {entrega.TipoServicio} | " +
            $"Estado: {entrega.EstadoEntrega} | " +
            $"Total: Q{entrega.Total:F2}"
        );
    }

    static void CambiarEstadoEntrega(string nuevoEstado)
    {
        try
        {
            int codigo = LeerEntero("Código de entrega: ");

            Entrega entrega =
                entregas.Find(e => e.CodigoEntrega == codigo);

            if (entrega == null)
                throw new ArgumentException("Entrega no encontrada.");

            if (entrega.EstadoEntrega == "Finalizada" ||
                entrega.EstadoEntrega == "Cancelada")
            {
                throw new InvalidOperationException(
                    "La entrega ya terminó y no puede modificarse.");
            }

            if (nuevoEstado == "Finalizada")
            {
                entrega.EstadoEntrega = "Finalizada";
                entrega.Paquete.EstadoPaquete = "Entregado";

                LiberarRecursos(entrega);

                Console.WriteLine("Entrega finalizada correctamente.");
            }
            else
            {
                entrega.EstadoEntrega = "Cancelada";
                entrega.Paquete.EstadoPaquete = "Cancelado";

                LiberarRecursos(entrega);

                entrega.Paquete.EstadoAsignado = false;

                Console.WriteLine("Entrega cancelada correctamente.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }

    static void LiberarRecursos(Entrega entrega)
    {
        entrega.Repartidor.EstadoDisponible = "Disponible";
        entrega.Vehiculo.EstadoVehiculo = "Disponible";
    }

    static void ReprogramarEntrega()
    {
        try
        {
            int codigo = LeerEntero("Código de entrega: ");

            Entrega entrega =
                entregas.Find(e => e.CodigoEntrega == codigo);

            if (entrega == null)
                throw new ArgumentException("Entrega no encontrada.");

            if (entrega.EstadoEntrega == "Finalizada" ||
                entrega.EstadoEntrega == "Cancelada")
            {
                throw new InvalidOperationException(
                    "No se puede reprogramar una entrega terminada.");
            }

            entrega.FechaSolicitud =
                DateTime.Now.AddDays(1);

            entrega.EstadoEntrega = "Reprogramada";

            Console.WriteLine(
                "Entrega reprogramada para mañana.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }

    static void MenuIncidencias()
    {
        Console.Clear();

        Console.WriteLine("--- GESTIÓN DE INCIDENCIAS ---");
        Console.WriteLine("1. Registrar incidencia");
        Console.WriteLine("2. Consultar incidencias");
        Console.WriteLine("3. Volver");
        Console.Write("Opción: ");

        switch (Console.ReadLine())
        {
            case "1":
                RegistrarIncidencia();
                break;

            case "2":
                MostrarIncidencias();
                break;

            case "3":
                return;

            default:
                Console.WriteLine("Opción inválida.");
                break;
        }

        Pausar();
        MenuIncidencias();
    }

    static void RegistrarIncidencia()
    {
        try
        {
            int codigoEntrega =
                LeerEntero("Código de entrega: ");

            Entrega entrega =
                entregas.Find(e => e.CodigoEntrega == codigoEntrega);

            if (entrega == null)
                throw new ArgumentException(
                    "La entrega no existe.");

            if (entrega.EstadoEntrega == "Finalizada" ||
                entrega.EstadoEntrega == "Cancelada")
            {
                throw new InvalidOperationException(
                    "No se puede agregar una incidencia a una entrega terminada.");
            }

            Console.WriteLine("1. Cliente ausente");
            Console.WriteLine("2. Dirección incorrecta");
            Console.WriteLine("3. Paquete dañado");
            Console.WriteLine("4. Falla de vehículo");
            Console.WriteLine("5. Retraso");
            Console.WriteLine("6. Condiciones climáticas");
            Console.WriteLine("7. Rechazo de recepción");

            int opcion = LeerEntero("Tipo de incidencia: ");

            string[] tipos =
            {
                "Cliente ausente",
                "Dirección incorrecta",
                "Paquete dañado",
                "Falla de vehículo",
                "Retraso",
                "Condiciones climáticas",
                "Rechazo de recepción"
            };

            if (opcion < 1 || opcion > tipos.Length)
                throw new ArgumentException(
                    "Tipo de incidencia inválido.");

            int codigoIncidencia =
                incidencias.Count + 1;

            string descripcion =
                LeerTexto("Descripción: ");

            Incidencia incidencia =
                new Incidencia(
                    codigoIncidencia,
                    tipos[opcion - 1],
                    descripcion
                );

            incidencias.Add(incidencia);
            entrega.AgregarIncidencia(incidencia);

            Console.WriteLine(
                "Incidencia registrada correctamente.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }

    static void MostrarIncidencias()
    {
        if (incidencias.Count == 0)
        {
            Console.WriteLine("No hay incidencias.");
            return;
        }

        foreach (Incidencia incidencia in incidencias)
        {
            Console.WriteLine(
                $"{incidencia.CodigoIncidencia} | " +
                $"{incidencia.TipoIncidencia} | " +
                $"{incidencia.Descripcion} | " +
                $"{incidencia.Fecha}"
            );
        }
    }

    static void MenuReportes()
    {
        Console.Clear();

        Console.WriteLine("--- REPORTES ---");
        Console.WriteLine("1. Entregas activas");
        Console.WriteLine("2. Entregas finalizadas");
        Console.WriteLine("3. Entregas canceladas");
        Console.WriteLine("4. Entregas con incidencias");
        Console.WriteLine("5. Repartidores disponibles");
        Console.WriteLine("6. Repartidor con más entregas");
        Console.WriteLine("7. Vehículo más utilizado");
        Console.WriteLine("8. Paquetes por tipo");
        Console.WriteLine("9. Ingreso total");
        Console.WriteLine("10. Entrega de mayor costo");
        Console.WriteLine("11. Matriz de distancias");
        Console.WriteLine("12. Volver");
        Console.Write("Opción: ");

        switch (Console.ReadLine())
        {
            case "1":
                ReporteActivas();
                break;

            case "2":
                ReporteFinalizadas();
                break;

            case "3":
                ReporteCanceladas();
                break;

            case "4":
                ReporteIncidencias();
                break;

            case "5":
                ReporteRepartidoresDisponibles();
                break;

            case "6":
                MostrarRepartidorMasEntregas();
                break;

            case "7":
                MostrarVehiculoMasUsado();
                break;

            case "8":
                ReportePaquetesTipo();
                break;

            case "9":
                Console.WriteLine(
                    "Ingreso total: Q" +
                    IngresoTotal().ToString("F2")
                );
                break;

            case "10":
                MostrarEntregaMayorCosto();
                break;

            case "11":
                MostrarMatriz();
                break;

            case "12":
                return;

            default:
                Console.WriteLine("Opción inválida.");
                break;
        }

        Pausar();
        MenuReportes();
    }

    static void ReporteActivas()
    {
        foreach (Entrega entrega in entregas)
        {
            if (entrega.EstadoEntrega != "Finalizada" &&
                entrega.EstadoEntrega != "Cancelada")
            {
                MostrarEntrega(entrega);
            }
        }
    }

    static void ReporteFinalizadas()
    {
        foreach (Entrega entrega in entregas)
        {
            if (entrega.EstadoEntrega == "Finalizada")
                MostrarEntrega(entrega);
        }
    }

    static void ReporteCanceladas()
    {
        foreach (Entrega entrega in entregas)
        {
            if (entrega.EstadoEntrega == "Cancelada")
                MostrarEntrega(entrega);
        }
    }

    static void ReporteIncidencias()
    {
        foreach (Entrega entrega in entregas)
        {
            if (entrega.Incidencias.Count > 0)
                MostrarEntrega(entrega);
        }
    }

    static void ReporteRepartidoresDisponibles()
    {
        foreach (Repartidor repartidor in repartidores)
        {
            if (repartidor.EstadoDisponible == "Disponible")
            {
                Console.WriteLine(
                    $"{repartidor.Codigo} | " +
                    $"{repartidor.NombreCompleto} | " +
                    $"{repartidor.Municipio} | " +
                    $"Licencia: {repartidor.TipoLicencia}"
                );
            }
        }
    }

    static void MostrarRepartidorMasEntregas()
    {
        if (repartidores.Count == 0)
            return;

        Repartidor mayor = repartidores[0];

        foreach (Repartidor repartidor in repartidores)
        {
            if (repartidor.CantidadEntregas >
                mayor.CantidadEntregas)
            {
                mayor = repartidor;
            }
        }

        Console.WriteLine(
            "Repartidor con más entregas: " +
            mayor.NombreCompleto
        );

        Console.WriteLine(
            "Cantidad: " +
            mayor.CantidadEntregas
        );
    }

    static void MostrarVehiculoMasUsado()
    {
        if (vehiculos.Count == 0)
            return;

        Vehiculo mayor = vehiculos[0];
        int cantidadMayor = 0;

        foreach (Vehiculo vehiculo in vehiculos)
        {
            int cantidad = 0;

            foreach (Entrega entrega in entregas)
            {
                if (entrega.Vehiculo == vehiculo)
                    cantidad++;
            }

            if (cantidad > cantidadMayor)
            {
                cantidadMayor = cantidad;
                mayor = vehiculo;
            }
        }

        Console.WriteLine(
            "Vehículo más utilizado: " +
            mayor.TipoVehiculo() +
            " | Placa: " +
            mayor.Placa
        );

        Console.WriteLine(
            "Cantidad de usos: " +
            cantidadMayor
        );
    }

    static void ReportePaquetesTipo()
    {
        string[] tipos =
        {
            "Documento",
            "Paquete estándar",
            "Paquete frágil",
            "Producto refrigerado"
        };

        foreach (string tipo in tipos)
        {
            int cantidad = 0;

            foreach (Paquete paquete in paquetes)
            {
                if (paquete.TipoPaquete() == tipo)
                    cantidad++;
            }

            Console.WriteLine(
                tipo + ": " + cantidad
            );
        }
    }

    static double IngresoTotal()
    {
        double total = 0;

        foreach (Entrega entrega in entregas)
        {
            if (entrega.EstadoEntrega == "Finalizada")
                total += entrega.Total;
        }

        return total;
    }

    static void MostrarEntregaMayorCosto()
    {
        if (entregas.Count == 0)
        {
            Console.WriteLine("No hay entregas.");
            return;
        }

        Entrega mayor = entregas[0];

        foreach (Entrega entrega in entregas)
        {
            if (entrega.Total > mayor.Total)
                mayor = entrega;
        }

        MostrarEntrega(mayor);
    }

    static void MostrarMatriz()
    {
        Console.WriteLine();
        Console.WriteLine(
            "MATRIZ DE DISTANCIAS EN KM"
        );

        Console.WriteLine(
            "Las filas y columnas corresponden al número de municipio."
        );

        Console.Write("     ");

        for (int i = 0;
             i < Cliente.MunicipiosXela.Count;
             i++)
        {
            Console.Write(
                (i + 1).ToString().PadLeft(6)
            );
        }

        Console.WriteLine();

        for (int i = 0;
             i < Cliente.MunicipiosXela.Count;
             i++)
        {
            Console.Write(
                (i + 1).ToString().PadLeft(5)
            );

            for (int j = 0;
                 j < Cliente.MunicipiosXela.Count;
                 j++)
            {
                Console.Write(
                    matrizDistancias[i, j]
                        .ToString("0.0")
                        .PadLeft(6)
                );
            }

            Console.WriteLine();
        }
    }

    static void MostrarMunicipios()
    {
        for (int i = 0;
             i < Cliente.MunicipiosXela.Count;
             i++)
        {
            Console.WriteLine(
                (i + 1) + ". " +
                Cliente.MunicipiosXela[i]
            );
        }
    }

    static int LeerIndiceMunicipio()
    {
        int numero =
            LeerEntero("Número del municipio: ");

        if (numero < 1 ||
            numero > Cliente.MunicipiosXela.Count)
        {
            throw new ArgumentOutOfRangeException(
                nameof(numero),
                "Municipio inválido."
            );
        }

        return numero - 1;
    }

    static int BuscarMunicipio(string nombre)
    {
        for (int i = 0;
             i < Cliente.MunicipiosXela.Count;
             i++)
        {
            if (string.Equals(
                Cliente.MunicipiosXela[i],
                nombre,
                StringComparison.OrdinalIgnoreCase))
            {
                return i;
            }
        }

        return -1;
    }

    static string LeerServicio()
    {
        Console.WriteLine("1. Normal");
        Console.WriteLine("2. Prioritario");
        Console.WriteLine("3. Urgente");

        string opcion =
            LeerTexto("Servicio: ");

        if (opcion == "1")
            return "Normal";

        if (opcion == "2")
            return "Prioritario";

        if (opcion == "3")
            return "Urgente";

        throw new ArgumentException(
            "Servicio inválido."
        );
    }

    static int LeerEntero(string mensaje)
    {
        Console.Write(mensaje);

        if (!int.TryParse(
            Console.ReadLine(),
            out int valor))
        {
            throw new FormatException(
                "Debe ingresar un número entero."
            );
        }

        return valor;
    }

    static double LeerDouble(string mensaje)
    {
        Console.Write(mensaje);

        if (!double.TryParse(
            Console.ReadLine(),
            out double valor))
        {
            throw new FormatException(
                "Debe ingresar un número."
            );
        }

        return valor;
    }

    static string LeerTexto(string mensaje)
    {
        Console.Write(mensaje);

        string texto = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(texto))
            throw new ArgumentException(
                "El campo no puede estar vacío."
            );

        return texto.Trim();
    }

    static void Pausar()
    {
        Console.WriteLine();
        Console.WriteLine(
            "Presione ENTER para continuar..."
        );

        Console.ReadLine();
    }
}
```
