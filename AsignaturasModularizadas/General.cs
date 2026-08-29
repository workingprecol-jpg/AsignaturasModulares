using Conector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsignaturasModularizadas
{
    public static class General
    {
        public static Inicio Ini = new Inicio();
        public static int CodigoEmpresa;
        public static string CadenaConexion;
        public static string IndicadorNulo = "";
        public static string FormatoFecha = "dd/MM/yyyy HH:mm";
        public static string FormatoSoloFecha = "dd/MM/yyyy";
        public static string FormatoFechaEspecial = "dd MMM yyyy HH:mm";
        public static string FormatoSoloFechaEspecial = "dd MMM yyyy";
        public static string SeparadorMiles = ",";
        public static string SeparadorDecimal = ".";

        public static string ConexionDesarrollo = "Data Source=142-180-62-50;Initial Catalog=DBSIIE_2025A;User ID=JFERNANDEZ;pwd=javier454";
        public static string WebServiceDesarrollo = "http://50.62.180.142/ServicioSIIE/sql.svc";
    }
}
