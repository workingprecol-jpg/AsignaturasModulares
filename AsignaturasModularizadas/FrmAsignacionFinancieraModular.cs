using Buscador.ServicioSQL;
using Infragistics.Win.UltraWinGrid;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Validador;

namespace AsignaturasModularizadas
{
    public partial class FrmAsignacionFinancieraModular : Form
    {
        private readonly Clave validar = new Clave();
        private SQLClient sw = new SQLClient();

        private string sConexion;
        private string strClave;
        private string strSql;

        private string idProgramaSeleccionado = "";
        private string idEstudianteSeleccionado = "";
        private string idMatriculaSeleccionada = "";
        private string idModuloSeleccionado = "";
        private string idDocenteSeleccionado = "";
        private string idTarifaSeleccionada = "";
        private string idTarifaAsignaturaModularSeleccionada = "";

        private const int IdPeriodoModular = 125;

        public FrmAsignacionFinancieraModular()
        {
            InitializeComponent();
        }

        private void FrmAsignacionFinancieraModular_Load(object sender, EventArgs e)
        {
            try
            {
                if (Parent is null)
                {
                    sConexion = validar.Encriptar(General.ConexionDesarrollo);
                    General.Ini.WebService = General.WebServiceDesarrollo;
                }
                else
                {
                    sConexion = validar.Encriptar(General.Ini.Conexion());
                }

                sw = new SQLClient("BasicHttpBinding_ISQL", General.Ini.WebService);
                lblPeriodo.Text = "Período: " + IdPeriodoModular;
                CargarProgramas();
            }
            catch (Exception ex)
            {
                MostrarError(ex, "Asignación financiera modular");
            }
        }

        private void CargarProgramas()
        {
            try
            {
                strSql = $"HOHorariosModular null,{IdPeriodoModular},'S',null";
                ultraGridProgramas.DataSource = EjecutarConsulta(strSql).Tables[0];
            }
            catch (Exception ex)
            {
                MostrarError(ex, "Programas");
            }
        }

        private void CargarEstudiantes()
        {
            try
            {
                string jsonParam = $"'{{\"Id_Prog\":{idProgramaSeleccionado}}}'";
                strSql = $"HOHorariosModular null, null, 'S2', {jsonParam}";
                ultraGridEstudiantes.DataSource = EjecutarConsulta(strSql).Tables[0];
            }
            catch (Exception ex)
            {
                MostrarError(ex, "Estudiantes modulares");
            }
        }

        private void CargarAsignaturas()
        {
            try
            {
                strSql = $"HOHorariosModular {idProgramaSeleccionado},{IdPeriodoModular},'S1',null";
                ultraGridAsignaturas.DataSource = EjecutarConsulta(strSql).Tables[0];
            }
            catch (Exception ex)
            {
                MostrarError(ex, "Asignaturas modulares");
            }
        }

        private void CargarTarifasAsignatura()
        {
            try
            {
                LimpiarTarifaSeleccionada();
                ultraGridDetallePlan.DataSource = null;

                if (idModuloSeleccionado == "")
                {
                    ultraGridTarifas.DataSource = null;
                    return;
                }

                strSql = $@"EXEC dbo.AFGestionTarifaAsignaturaModular
                                @Id_Tipo = 'H',
                                @Id_Modulo = {idModuloSeleccionado},
                                @Id_Periodo = {IdPeriodoModular}";

                DataTable tarifas = EjecutarConsulta(strSql).Tables[0];
                ultraGridTarifas.DataSource = tarifas;
            }
            catch (Exception ex)
            {
                MostrarError(ex, "Tarifas de la asignatura");
            }
        }

        private void CargarDetallePlan()
        {
            try
            {
                if (idTarifaSeleccionada == "")
                {
                    ultraGridDetallePlan.DataSource = null;
                    return;
                }

                strSql = $@"EXEC dbo.AFGestionTarifaAsignaturaModular
                                @Id_Tipo = 'D',
                                @Id_Tarifa = {idTarifaSeleccionada}";

                DataTable detalle = EjecutarConsulta(strSql).Tables[0];
                ultraGridDetallePlan.DataSource = detalle;
                lblTotalPlan.Text = "Total del plan: " + CalcularTotalPlan(detalle).ToString("N0");
            }
            catch (Exception ex)
            {
                MostrarError(ex, "Detalle del plan");
            }
        }

        private void CargarAsignacionesEstudiante()
        {
            try
            {
                if (idEstudianteSeleccionado == "")
                {
                    ultraGridAsignaciones.DataSource = null;
                    ActualizarResumen(null);
                    return;
                }

                string filtroPrograma = idProgramaSeleccionado == ""
                    ? ""
                    : ", @Id_Programa = " + idProgramaSeleccionado;

                strSql = $@"EXEC dbo.AFMatriculaAsignaturaModular
                                @Id_Tipo = 'S',
                                @Id_Estudiante = {idEstudianteSeleccionado},
                                @Id_Periodo = {IdPeriodoModular}
                                {filtroPrograma}";

                DataTable asignaciones = EjecutarConsulta(strSql).Tables[0];
                ultraGridAsignaciones.DataSource = asignaciones;
                ActualizarResumen(asignaciones);
            }
            catch (Exception ex)
            {
                MostrarError(ex, "Asignaturas asignadas");
            }
        }

        private void ultraGridProgramas_ClickCell(object sender, ClickCellEventArgs e)
        {
            try
            {
                if (e.Cell == null || e.Cell.Row == null)
                {
                    return;
                }

                idProgramaSeleccionado = ObtenerValor(e.Cell.Row, "Id_Programa", "Id_PrOfrecido");
                if (idProgramaSeleccionado == "")
                {
                    return;
                }

                txtPrograma.Text = ObtenerValor(e.Cell.Row, "NombrePrograma", "Programa", "Nombre");
                LimpiarAsignaturaSeleccionada();
                CargarEstudiantes();
                CargarAsignaturas();

                if (idEstudianteSeleccionado != "")
                {
                    CargarAsignacionesEstudiante();
                }
            }
            catch (Exception ex)
            {
                MostrarError(ex, "Programas");
            }
        }

        private void ultraGridEstudiantes_ClickCell(object sender, ClickCellEventArgs e)
        {
            try
            {
                if (e.Cell == null || e.Cell.Row == null)
                {
                    return;
                }

                string idEstudiante = ObtenerValor(e.Cell.Row, "Id_Estudiante");
                string documento = ObtenerValor(e.Cell.Row, "Id_DocEstudiante");

                if (idEstudiante == "" && documento == "")
                {
                    throw new Exception("La consulta de estudiantes debe incluir Id_Estudiante o Id_DocEstudiante.");
                }

                if (documento != "")
                {
                    txtDocumento.Text = documento;
                    BuscarEstudiantePorDocumento();
                    return;
                }

                idEstudianteSeleccionado = idEstudiante;
                idMatriculaSeleccionada = ObtenerValor(e.Cell.Row, "Id_Matricula");
                txtEstudiante.Text = ObtenerValor(e.Cell.Row, "Estudiante", "NombreEstudiante", "Nombre");
                CargarAsignacionesEstudiante();
                ActualizarEstadoAsignacion();
            }
            catch (Exception ex)
            {
                MostrarError(ex, "Estudiantes");
            }
        }

        private void ultraGridAsignaturas_ClickCell(object sender, ClickCellEventArgs e)
        {
            try
            {
                if (e.Cell == null || e.Cell.Row == null)
                {
                    return;
                }

                idModuloSeleccionado = ObtenerValor(e.Cell.Row, "Id_AsignaturaPlan", "Id_DetallePlan");
                idDocenteSeleccionado = ObtenerValor(e.Cell.Row, "Id_Docente");

                if (idModuloSeleccionado == "")
                {
                    throw new Exception("La consulta de asignaturas debe incluir Id_AsignaturaPlan.");
                }

                txtAsignatura.Text = ObtenerValor(e.Cell.Row, "NombreAsignatura", "Asignatura", "Nombre");
                if (txtAsignatura.Text == "")
                {
                    txtAsignatura.Text = "Asignatura " + idModuloSeleccionado;
                }

                CargarTarifasAsignatura();
            }
            catch (Exception ex)
            {
                MostrarError(ex, "Asignaturas modulares");
            }
        }

        private void ultraGridTarifas_ClickCell(object sender, ClickCellEventArgs e)
        {
            try
            {
                if (e.Cell == null || e.Cell.Row == null)
                {
                    return;
                }

                idTarifaSeleccionada = ObtenerValor(e.Cell.Row, "Id_Tarifa", "Id_Plan");
                idTarifaAsignaturaModularSeleccionada = ObtenerValor(e.Cell.Row, "Id_TarifaAsignaturaModular");

                if (idTarifaSeleccionada == "" || idTarifaAsignaturaModularSeleccionada == "")
                {
                    return;
                }

                txtPlan.Text = ObtenerValor(e.Cell.Row, "NombrePlan", "Plan", "Nombre");
                CargarDetallePlan();
                ActualizarEstadoAsignacion();
            }
            catch (Exception ex)
            {
                MostrarError(ex, "Planes de pago");
            }
        }

        private void btnAsignarTarifa_Click(object sender, EventArgs e)
        {
            try
            {
                ValidarDatosAsignacion();

                string usuario = Environment.UserName.Replace("'", "''");
                strSql = $@"EXEC dbo.AFMatriculaAsignaturaModular
                                @Id_Tipo = 'I',
                                @Id_Estudiante = {idEstudianteSeleccionado},
                                @Id_Matricula = {idMatriculaSeleccionada},
                                @Id_Programa = {idProgramaSeleccionado},
                                @Id_Modulo = {idModuloSeleccionado},
                                @Id_TarifaAsignaturaModular = {idTarifaAsignaturaModularSeleccionada},
                                @Id_Tarifa = {idTarifaSeleccionada},
                                @Id_Periodo = {IdPeriodoModular},
                                @Id_Docente = {idDocenteSeleccionado},
                                @Usuario = N'{usuario}'";

                EjecutarComando(strSql);
                CargarAsignacionesEstudiante();
                SIIEMessageBox.Clases.SIIEMessageBox.Show(
                    "La tarifa fue asignada. También se creó la fila de CALAsignaturaEnCurso.",
                    "Asignación financiera modular",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MostrarError(ex, "Asignación financiera modular");
            }
        }

        private void btnBuscarEstudiante_Click(object sender, EventArgs e)
        {
            try
            {
                BuscarEstudiantePorDocumento();
            }
            catch (Exception ex)
            {
                MostrarError(ex, "Búsqueda de estudiante");
            }
        }

        private void txtDocumento_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnBuscarEstudiante_Click(sender, EventArgs.Empty);
            }
        }

        private void BuscarEstudiantePorDocumento()
        {
            string documento = txtDocumento.Text.Trim().Replace("'", "''");

            if (documento == "")
            {
                throw new Exception("Ingrese el número de documento del estudiante.");
            }

            strSql = $@"EXEC dbo.AFMatriculaAsignaturaModular
                            @Id_Tipo = 'B',
                            @Id_DocEstudiante = '{documento}',
                            @Id_Periodo = {IdPeriodoModular}";

            DataTable estudiante = EjecutarConsulta(strSql).Tables[0];

            if (estudiante.Rows.Count == 0)
            {
                throw new Exception("No se encontró un estudiante con ese número de documento.");
            }

            DataRow fila = estudiante.Rows[0];
            idEstudianteSeleccionado = fila["Id_Estudiante"].ToString();
            idMatriculaSeleccionada = fila["Id_Matricula"] == DBNull.Value ? "" : fila["Id_Matricula"].ToString();
            txtEstudiante.Text = fila["Estudiante"].ToString();

            CargarAsignacionesEstudiante();
            ActualizarEstadoAsignacion();

            if (idMatriculaSeleccionada == "")
            {
                SIIEMessageBox.Clases.SIIEMessageBox.Show(
                    "El estudiante fue encontrado. El plan financiero se creará al asignar la tarifa, " +
                    "pero antes debe tener una matrícula modular activa para este período.",
                    "Búsqueda de estudiante",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void ValidarDatosAsignacion()
        {
            if (idProgramaSeleccionado == "")
            {
                throw new Exception("Seleccione un programa antes de asignar la tarifa.");
            }

            if (idEstudianteSeleccionado == "")
            {
                throw new Exception("Busque y seleccione el estudiante antes de asignar la tarifa.");
            }

            if (idMatriculaSeleccionada == "")
            {
                throw new Exception("El estudiante no tiene matrícula modular activa para este período.");
            }

            if (idModuloSeleccionado == "" || idTarifaSeleccionada == "" || idTarifaAsignaturaModularSeleccionada == "")
            {
                throw new Exception("Seleccione asignatura modular y plan de pago.");
            }

            if (idDocenteSeleccionado == "")
            {
                throw new Exception("La asignatura debe tener Id_Docente para crear CALAsignaturaEnCurso.");
            }
        }

        private void ActualizarEstadoAsignacion()
        {
            btnAsignarTarifa.Enabled =
                idProgramaSeleccionado != "" &&
                idEstudianteSeleccionado != "" &&
                idMatriculaSeleccionada != "" &&
                idModuloSeleccionado != "" &&
                idDocenteSeleccionado != "" &&
                idTarifaSeleccionada != "" &&
                idTarifaAsignaturaModularSeleccionada != "";
        }

        private DataSet EjecutarConsulta(string consulta)
        {
            strClave = validar.Crear(consulta);
            SQLRespuesta respuesta = sw.SQLCargarDts(consulta, sConexion, strClave);

            if (respuesta.MensajeError != "")
            {
                throw new Exception(respuesta.MensajeError);
            }

            return respuesta.Dts;
        }

        private void EjecutarComando(string comando)
        {
            strClave = validar.Crear(comando);
            SQLRespuesta respuesta = sw.SQLEjecutar(comando, sConexion, strClave);

            if (respuesta.MensajeError != "")
            {
                throw new Exception(respuesta.MensajeError);
            }
        }

        private static string ObtenerValor(UltraGridRow fila, params string[] columnas)
        {
            foreach (string columna in columnas)
            {
                if (fila.Band.Columns.Exists(columna) && fila.Cells[columna].Value != DBNull.Value)
                {
                    return fila.Cells[columna].Value.ToString();
                }
            }

            return "";
        }

        private static decimal CalcularTotalPlan(DataTable detalle)
        {
            decimal total = 0;

            if (!detalle.Columns.Contains("Valor"))
            {
                return total;
            }

            foreach (DataRow fila in detalle.Rows)
            {
                if (fila["Valor"] != DBNull.Value)
                {
                    total += Convert.ToDecimal(fila["Valor"]);
                }
            }

            return total;
        }

        private void ActualizarResumen(DataTable asignaciones)
        {
            int cantidad = asignaciones == null ? 0 : asignaciones.Rows.Count;
            decimal total = 0;

            if (asignaciones != null && asignaciones.Columns.Contains("ValorFinal"))
            {
                foreach (DataRow fila in asignaciones.Rows)
                {
                    if (fila["ValorFinal"] != DBNull.Value)
                    {
                        total += Convert.ToDecimal(fila["ValorFinal"]);
                    }
                }
            }

            lblCantidadAsignaturas.Text = "Asignaturas matriculadas: " + cantidad;
            lblTotalPagar.Text = "Total a pagar: " + total.ToString("N0");
        }

        private void LimpiarEstudianteSeleccionado()
        {
            idEstudianteSeleccionado = "";
            idMatriculaSeleccionada = "";
            txtEstudiante.Text = "";
            ultraGridEstudiantes.DataSource = null;
            ultraGridAsignaciones.DataSource = null;
            ActualizarResumen(null);
        }

        private void LimpiarAsignaturaSeleccionada()
        {
            idModuloSeleccionado = "";
            idDocenteSeleccionado = "";
            txtAsignatura.Text = "";
            ultraGridAsignaturas.DataSource = null;
            LimpiarTarifaSeleccionada();
            ultraGridTarifas.DataSource = null;
            ultraGridDetallePlan.DataSource = null;
        }

        private void LimpiarTarifaSeleccionada()
        {
            idTarifaSeleccionada = "";
            idTarifaAsignaturaModularSeleccionada = "";
            txtPlan.Text = "";
            lblTotalPlan.Text = "Total del plan: 0";
            btnAsignarTarifa.Enabled = false;
        }

        private static void MostrarError(Exception ex, string titulo)
        {
            SIIEMessageBox.Clases.SIIEMessageBox.Show(ex.Message, titulo, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ultraGridProgramas_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            OcultarColumna(e, "Id_Programa");
            OcultarColumna(e, "Id_PrOfrecido");
        }

        private void ultraGridEstudiantes_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            OcultarColumna(e, "Id_Matricula");
            OcultarColumna(e, "Id_Estudiante");
        }

        private void ultraGridAsignaturas_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            OcultarColumna(e, "Id_AsignaturaPlan");
            OcultarColumna(e, "Id_Docente");
            OcultarColumna(e, "PrecioAleatorio");
        }

        private void ultraGridTarifas_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            OcultarColumna(e, "Id_TarifaAsignaturaModular");
            OcultarColumna(e, "Id_Periodo");
            FormatearNumero(e, "ValorOrdinaria");
            FormatearNumero(e, "ValorExtraordinaria");
            FormatearNumero(e, "ValorDescuento");
        }

        private void ultraGridDetallePlan_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            OcultarColumna(e, "Id_DetallePlan");
            OcultarColumna(e, "Id_Tarifa");
            FormatearNumero(e, "Valor");
            FormatearNumero(e, "ValorExtr");
            FormatearFecha(e, "FechaPago");
        }

        private void ultraGridAsignaciones_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            OcultarColumna(e, "Id_PlanEstudianteModulo");
            OcultarColumna(e, "Id_PlanEstudiante");
            OcultarColumna(e, "Id_TarifaAsignaturaModular");
            OcultarColumna(e, "Id_AsigCurso");
            FormatearNumero(e, "ValorOriginal");
            FormatearNumero(e, "ValorDescuento");
            FormatearNumero(e, "ValorFinal");
            FormatearFecha(e, "Fecha");
        }

        private static void OcultarColumna(InitializeLayoutEventArgs e, string nombre)
        {
            if (e.Layout.Bands.Count > 0 && e.Layout.Bands[0].Columns.Exists(nombre))
            {
                e.Layout.Bands[0].Columns[nombre].Hidden = true;
            }
        }

        private static void FormatearNumero(InitializeLayoutEventArgs e, string nombre)
        {
            if (e.Layout.Bands.Count > 0 && e.Layout.Bands[0].Columns.Exists(nombre))
            {
                e.Layout.Bands[0].Columns[nombre].Format = "N0";
            }
        }

        private static void FormatearFecha(InitializeLayoutEventArgs e, string nombre)
        {
            if (e.Layout.Bands.Count > 0 && e.Layout.Bands[0].Columns.Exists(nombre))
            {
                e.Layout.Bands[0].Columns[nombre].Format = "dd/MM/yyyy";
            }
        }
    }
}
