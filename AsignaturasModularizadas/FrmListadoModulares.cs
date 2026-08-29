using Buscador.ServicioSQL;
using Infragistics.Win.UltraWinGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Validador;

namespace AsignaturasModularizadas
{
    public partial class Modular : Form
    {
        SQLClient sw = new SQLClient();
        SQLRespuesta rSQLProgramas = new SQLRespuesta();

        Clave validar = new Clave();

        string strClave, sConexion, strSql;

        List<string> listaResultados = new List<string>();
        HashSet<string> listaProgramasComparar = new HashSet<string>();

        DataTable tablaPlanesAsignatura = new DataTable();
        DataTable tablaDetallePlan = new DataTable();
        DataTable tablaDetallePlanEstudiante = new DataTable();

        string idAsignaturaPlanSeleccionada = "";
        string nombreAsignaturaSeleccionada = "";
        string idPlanSeleccionado = "";
        const int IdPeriodoModular = 125;

        public Modular()
        {
            InitializeComponent();
            InicializarGrillasPlanes();
        }

        private void InicializarGrillasPlanes()
        {
            tablaPlanesAsignatura.Columns.Add("Id_TarifaAsignaturaModular", typeof(int));
            tablaPlanesAsignatura.Columns.Add("Id_Tarifa", typeof(int));
            tablaPlanesAsignatura.Columns.Add("NombrePlan", typeof(string));
            tablaPlanesAsignatura.Columns.Add("TipoPlan", typeof(string));
            tablaPlanesAsignatura.Columns.Add("ValorOrdinaria", typeof(decimal));
            tablaPlanesAsignatura.Columns.Add("ValorExtraordinaria", typeof(decimal));
            tablaPlanesAsignatura.Columns.Add("ValorDescuento", typeof(decimal));
            tablaPlanesAsignatura.Columns.Add("Id_Periodo", typeof(int));
            tablaPlanesAsignatura.Columns.Add("Activo", typeof(bool));
            tablaPlanesAsignatura.Columns.Add("Estado", typeof(string));

            tablaDetallePlan.Columns.Add("Id_DetallePlan", typeof(int));
            tablaDetallePlan.Columns.Add("Id_Tarifa", typeof(int));
            tablaDetallePlan.Columns.Add("Concepto", typeof(string));
            tablaDetallePlan.Columns.Add("Porcentaje", typeof(decimal));
            tablaDetallePlan.Columns.Add("Valor", typeof(decimal));
            tablaDetallePlan.Columns.Add("ValorExtr", typeof(decimal));
            tablaDetallePlan.Columns.Add("FechaPago", typeof(DateTime));

            tablaDetallePlanEstudiante.Columns.Add("Id_DetallePlanEst", typeof(int));
            tablaDetallePlanEstudiante.Columns.Add("Id_PlanEstudiante", typeof(int));
            tablaDetallePlanEstudiante.Columns.Add("Concepto", typeof(string));
            tablaDetallePlanEstudiante.Columns.Add("FechaPago", typeof(DateTime));
            tablaDetallePlanEstudiante.Columns.Add("Valor", typeof(decimal));

            ultraGridPlanesAsignatura.DataSource = tablaPlanesAsignatura;
            ultraGridDetallePlan.DataSource = tablaDetallePlan;
            ultraGridDetallePlanEstudiante.DataSource = tablaDetallePlanEstudiante;
        }

        private void ultraGridPlanesAsignatura_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            try
            {
                if (e.Layout.Bands[0].Columns.Exists("Id_TarifaAsignaturaModular"))
                {
                    e.Layout.Bands[0].Columns["Id_TarifaAsignaturaModular"].Hidden = true;
                }

                if (e.Layout.Bands[0].Columns.Exists("Id_Periodo"))
                {
                    e.Layout.Bands[0].Columns["Id_Periodo"].Hidden = true;
                }

                if (e.Layout.Bands[0].Columns.Exists("ValorOrdinaria"))
                {
                    e.Layout.Bands[0].Columns["ValorOrdinaria"].Format = "N0";
                }

                if (e.Layout.Bands[0].Columns.Exists("ValorExtraordinaria"))
                {
                    e.Layout.Bands[0].Columns["ValorExtraordinaria"].Format = "N0";
                }

                if (e.Layout.Bands[0].Columns.Exists("ValorDescuento"))
                {
                    e.Layout.Bands[0].Columns["ValorDescuento"].Format = "N0";
                }
            }
            catch (Exception ex)
            {
                SIIEMessageBox.Clases.SIIEMessageBox.Show(ex.Message, "Inconsistencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ultraGridDetallePlan_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            try
            {
                if (e.Layout.Bands[0].Columns.Exists("Id_DetallePlan"))
                {
                    e.Layout.Bands[0].Columns["Id_DetallePlan"].Hidden = true;
                }

                if (e.Layout.Bands[0].Columns.Exists("Id_Tarifa"))
                {
                    e.Layout.Bands[0].Columns["Id_Tarifa"].Hidden = true;
                }

                if (e.Layout.Bands[0].Columns.Exists("Porcentaje"))
                {
                    e.Layout.Bands[0].Columns["Porcentaje"].Format = "N2";
                }

                if (e.Layout.Bands[0].Columns.Exists("Valor"))
                {
                    e.Layout.Bands[0].Columns["Valor"].Format = "N0";
                }

                if (e.Layout.Bands[0].Columns.Exists("ValorExtr"))
                {
                    e.Layout.Bands[0].Columns["ValorExtr"].Format = "N0";
                }

                if (e.Layout.Bands[0].Columns.Exists("FechaPago"))
                {
                    e.Layout.Bands[0].Columns["FechaPago"].Format = "dd/MM/yyyy";
                }
            }
            catch (Exception ex)
            {
                SIIEMessageBox.Clases.SIIEMessageBox.Show(ex.Message, "Inconsistencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ultraGridDetallePlanEstudiante_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            try
            {
                if (e.Layout.Bands[0].Columns.Exists("Id_DetallePlanEst"))
                {
                    e.Layout.Bands[0].Columns["Id_DetallePlanEst"].Hidden = true;
                }

                if (e.Layout.Bands[0].Columns.Exists("Id_PlanEstudiante"))
                {
                    e.Layout.Bands[0].Columns["Id_PlanEstudiante"].Hidden = true;
                }

                if (e.Layout.Bands[0].Columns.Exists("FechaPago"))
                {
                    e.Layout.Bands[0].Columns["FechaPago"].Format = "dd/MM/yyyy";
                }

                if (e.Layout.Bands[0].Columns.Exists("Valor"))
                {
                    e.Layout.Bands[0].Columns["Valor"].Format = "N0";
                }
            }
            catch (Exception ex)
            {
                SIIEMessageBox.Clases.SIIEMessageBox.Show(ex.Message, "Inconsistencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Modular_Load(object sender, EventArgs e)
        {
            if (this.Parent is null)
            {
                sConexion = validar.Encriptar(General.ConexionDesarrollo);
                General.Ini.WebService = General.WebServiceDesarrollo;
            }
            else
            {
                sConexion = validar.Encriptar(General.Ini.Conexion());
            }
            sw = new SQLClient("BasicHttpBinding_ISQL", General.Ini.WebService);

            barraSIIE1.SIIEEstadoAuto = false;
            barraSIIE1.SIIEActivarBoton(true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false);

            MatriculasModulares();
            ListadoProgramasParaComparar();

            ListadoProgramas();
        }

        private void ListadoProgramas()
        {
            try
            {
                strSql = $"HOHorariosModular null,{IdPeriodoModular},'S',null";
                strClave = validar.Crear(strSql);
                rSQLProgramas = sw.SQLCargarDts(strSql, sConexion, strClave);
                if (rSQLProgramas.MensajeError != "")
                {
                    throw new Exception(rSQLProgramas.MensajeError);
                }
                ultraGridProgramasModulares.DataSource = rSQLProgramas.Dts.Tables[0];
                ultraGridProgramas2.DataSource = rSQLProgramas.Dts.Tables[0];

                foreach (Infragistics.Win.UltraWinGrid.UltraGridRow filaGrid in ultraGridProgramas2.Rows)
                {
                    // Obtenemos el Id_Programa de la fila actual del Grid
                    if (filaGrid.Cells["Id_Programa"].Value != DBNull.Value)
                    {
                        int idProgramaGrid = Convert.ToInt32(filaGrid.Cells["Id_Programa"].Value);

                        // COMPARACIÓN
                        // Verificamos si este ID existe en la lista que cargamos con S5
                        if (listaProgramasComparar.Contains(idProgramaGrid.ToString()))
                        {
                            filaGrid.Appearance.BackColor = Color.LightGreen;
                            filaGrid.Appearance.ForeColor = Color.Black;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                SIIEMessageBox.Clases.SIIEMessageBox.Show(ex.Message, "Inconsistencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ListadoEstudiantesModulares(string Id_Prog) 
        {
            try
            {
                string jsonParam = $"'{{\"Id_Prog\":{Id_Prog}}}'";

                // LA CADENA SQL:
                // Orden params: @Id_ProgOfre(null), @Id_Periodo(null), @Tipo('S2'), @JSON(jsonParam), @Id_Prog(null)
                strSql = $"HOHorariosModular null, null, 'S2', {jsonParam}";

                strClave = validar.Crear(strSql);
                rSQLProgramas = sw.SQLCargarDts(strSql, sConexion, strClave);

                if (rSQLProgramas.MensajeError != "")
                {
                    throw new Exception(rSQLProgramas.MensajeError);
                }
                ultraGridEstudiantesModular.DataSource = rSQLProgramas.Dts.Tables[0];
            }
            catch (Exception ex)
            {
                SIIEMessageBox.Clases.SIIEMessageBox.Show(ex.Message, "Inconsistencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void ultraGridProgramas2_ClickCellButton(object sender, Infragistics.Win.UltraWinGrid.CellEventArgs e)
        {
            try
            {
                string IdProg = e.Cell.Row.Cells["Id_Programa"].Value.ToString();
                ListadoEstudiantesModulares(IdProg);
            }
            catch (Exception ex)
            {
                SIIEMessageBox.Clases.SIIEMessageBox.Show(ex.Message, "Inconsistencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ultraGridProgramas2_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            try
            {
                Infragistics.Win.UltraWinGrid.UltraGridColumn ugc = ultraGridProgramas2.DisplayLayout.Bands[0].Columns["NombrePrograma"];
                ugc.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Button;
                this.ultraGridProgramas2.DisplayLayout.Bands[0].Columns["Id_Programa"].Hidden = true;
            }
            catch (Exception ex)
            {
                SIIEMessageBox.Clases.SIIEMessageBox.Show(ex.Message, "Inconsistencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ultraGridEstudiantesModular_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            try
            {
                Infragistics.Win.UltraWinGrid.UltraGridColumn ugc = ultraGridEstudiantesModular.DisplayLayout.Bands[0].Columns["Id_DocEstudiante"];
                ugc.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Button;
            }
            catch (Exception ex)
            {
                SIIEMessageBox.Clases.SIIEMessageBox.Show(ex.Message, "Inconsistencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ultraGridEstudiantesModular_ClickCellButton(object sender, Infragistics.Win.UltraWinGrid.CellEventArgs e)
        {
            try
            {
                string IdMatricula = e.Cell.Row.Cells["Id_Matricula"].Value.ToString();
                CargarAsignaturasPorMatricula(IdMatricula);
            }
            catch (Exception ex)
            {
                SIIEMessageBox.Clases.SIIEMessageBox.Show(ex.Message, "Inconsistencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarAsignaturasPorMatricula(string idMatricula)
        {
            try
            {
                tablaDetallePlanEstudiante.Clear();
                // CONSTRUCCIÓN DEL JSON:
                // Resultado esperado: '{"Id_Matricula":12345}'
                string jsonParam = $"'{{\"Id_Matricula\":{idMatricula}}}'";

                // LA CADENA SQL:
                // Usamos 'S3' y pasamos el JSON en la posición 4
                strSql = $"HOHorariosModular null, null, 'S3', {jsonParam}";

                strClave = validar.Crear(strSql);
                rSQLProgramas = sw.SQLCargarDts(strSql, sConexion, strClave);

                if (rSQLProgramas.MensajeError != "")
                {
                    throw new Exception(rSQLProgramas.MensajeError);
                }

                // Asignar al Grid correspondiente
                ultraGridAsignaturas.DataSource = rSQLProgramas.Dts.Tables[0];
            }
            catch (Exception ex)
            {
                SIIEMessageBox.Clases.SIIEMessageBox.Show(ex.Message, "Inconsistencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ultraGridAsignaturas_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            try
            {
                this.ultraGridAsignaturas.DisplayLayout.Bands[0].Columns["Id_AsigCurso"].Hidden = true;
            }
            catch (Exception ex)
            {
                SIIEMessageBox.Clases.SIIEMessageBox.Show(ex.Message, "Inconsistencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ultraGridProgramasModulares_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            try
            {
                Infragistics.Win.UltraWinGrid.UltraGridColumn ugc = ultraGridProgramasModulares.DisplayLayout.Bands[0].Columns["NombrePrograma"];
                ugc.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Button;

                this.ultraGridProgramasModulares.DisplayLayout.Bands[0].Columns["Id_Programa"].Hidden = true;
            }
            catch (Exception ex)
            {
                SIIEMessageBox.Clases.SIIEMessageBox.Show(ex.Message, "Inconsistencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ultraGridAsignaturaModulares_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            try
            {
                this.ultraGridAsignaturaModulares.DisplayLayout.Bands[0].Columns["Id_AsignaturaPlan"].Hidden = true;
            }
            catch (Exception ex)
            {
                SIIEMessageBox.Clases.SIIEMessageBox.Show(ex.Message, "Inconsistencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ultraGridProgramasModulares_ClickCellButton(object sender, Infragistics.Win.UltraWinGrid.CellEventArgs e)
        {
            try
            {
                string IdProgOfre = e.Cell.Row.Cells["Id_Programa"].Value.ToString();
                AsignaturasModulares(IdProgOfre);
            }
            catch (Exception ex)
            {
                SIIEMessageBox.Clases.SIIEMessageBox.Show(ex.Message, "Inconsistencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ultraGridAsignaturaModulares_ClickCell(object sender, Infragistics.Win.UltraWinGrid.ClickCellEventArgs e)
        {
            try
            {
                if (e.Cell == null || e.Cell.Row == null)
                {
                    return;
                }

                idAsignaturaPlanSeleccionada = ObtenerValorFila(e.Cell.Row, "Id_AsignaturaPlan", "Id_DetallePlan");
                nombreAsignaturaSeleccionada = ObtenerValorFila(e.Cell.Row, "NombreAsignatura", "Asignatura", "Nombre");

                if (idAsignaturaPlanSeleccionada == "")
                {
                    return;
                }

                if (nombreAsignaturaSeleccionada == "")
                {
                    nombreAsignaturaSeleccionada = "Asignatura del plan " + idAsignaturaPlanSeleccionada;
                }

                txtAsignaturaSeleccionada.Text = nombreAsignaturaSeleccionada;
                CargarPlanesAsignaturaSeleccionada();
            }
            catch (Exception ex)
            {
                SIIEMessageBox.Clases.SIIEMessageBox.Show(ex.Message, "Inconsistencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string ObtenerValorFila(Infragistics.Win.UltraWinGrid.UltraGridRow fila, params string[] nombresColumnas)
        {
            foreach (string nombreColumna in nombresColumnas)
            {
                if (fila.Band.Columns.Exists(nombreColumna) && fila.Cells[nombreColumna].Value != DBNull.Value)
                {
                    return fila.Cells[nombreColumna].Value.ToString();
                }
            }

            return "";
        }

        private void CargarPlanesAsignaturaSeleccionada()
        {
            try
            {
                idPlanSeleccionado = "";
                txtPlanSeleccionado.Text = "";
                btnAsociarPlan.Enabled = false;
                tablaDetallePlan.Clear();

                if (idAsignaturaPlanSeleccionada == "")
                {
                    tablaPlanesAsignatura.Clear();
                    return;
                }

                strSql = $@"SELECT
                                TAM.Id_TarifaAsignaturaModular,
                                P.Id_Plan AS Id_Tarifa,
                                P.NombrePlan,
                                P.TipoPlan,
                                P.ValorOrdinaria,
                                P.ValorExtraordinaria,
                                P.ValorDescuento,
                                P.Id_Periodo,
                                ISNULL(TAM.Activo, CAST(0 AS bit)) AS Activo,
                                CASE
                                    WHEN TAM.Id_TarifaAsignaturaModular IS NULL THEN 'Disponible'
                                    WHEN ISNULL(TAM.Activo, 0) = 1 THEN 'Asociado'
                                    ELSE 'Inactivo'
                                END AS Estado
                            FROM dbo.AFPlan AS P
                            LEFT JOIN dbo.AFTarifaAsignaturaModular AS TAM
                                ON TAM.Id_Tarifa = P.Id_Plan
                                AND TAM.Id_Modulo = {idAsignaturaPlanSeleccionada}
                                AND TAM.Id_Periodo = {IdPeriodoModular}
                            WHERE P.Id_Periodo = {IdPeriodoModular}
                            ORDER BY P.NombrePlan";

                strClave = validar.Crear(strSql);
                SQLRespuesta respuesta = sw.SQLCargarDts(strSql, sConexion, strClave);

                if (respuesta.MensajeError != "")
                {
                    throw new Exception(respuesta.MensajeError);
                }

                ultraGridPlanesAsignatura.DataSource = respuesta.Dts.Tables[0];
            }
            catch (Exception ex)
            {
                SIIEMessageBox.Clases.SIIEMessageBox.Show(ex.Message, "Planes de pago", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ultraGridPlanesAsignatura_ClickCell(object sender, ClickCellEventArgs e)
        {
            try
            {
                if (e.Cell == null || e.Cell.Row == null)
                {
                    return;
                }

                idPlanSeleccionado = ObtenerValorFila(e.Cell.Row, "Id_Tarifa", "Id_Plan");
                string nombrePlan = ObtenerValorFila(e.Cell.Row, "NombrePlan", "Plan", "Nombre");
                string estadoPlan = ObtenerValorFila(e.Cell.Row, "Estado");

                if (idPlanSeleccionado == "")
                {
                    return;
                }

                txtPlanSeleccionado.Text = nombrePlan;
                btnAsociarPlan.Enabled = idAsignaturaPlanSeleccionada != "" && estadoPlan != "Asociado";
                CargarDetallePlanSeleccionado();
            }
            catch (Exception ex)
            {
                SIIEMessageBox.Clases.SIIEMessageBox.Show(ex.Message, "Inconsistencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarDetallePlanSeleccionado()
        {
            try
            {
                tablaDetallePlan.Clear();

                if (idPlanSeleccionado == "")
                {
                    return;
                }

                strSql = $@"SELECT
                                Id_DetallePlan,
                                Id_Plan AS Id_Tarifa,
                                Concepto,
                                Porcentaje,
                                Valor,
                                ValorExtr,
                                FechaPago
                            FROM dbo.AFDetallePlan
                            WHERE Id_Plan = {idPlanSeleccionado}
                            ORDER BY FechaPago, Id_DetallePlan";

                strClave = validar.Crear(strSql);
                SQLRespuesta respuesta = sw.SQLCargarDts(strSql, sConexion, strClave);

                if (respuesta.MensajeError != "")
                {
                    throw new Exception(respuesta.MensajeError);
                }

                ultraGridDetallePlan.DataSource = respuesta.Dts.Tables[0];
            }
            catch (Exception ex)
            {
                SIIEMessageBox.Clases.SIIEMessageBox.Show(ex.Message, "Detalle del plan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAsociarPlan_Click(object sender, EventArgs e)
        {
            try
            {
                if (idAsignaturaPlanSeleccionada == "")
                {
                    SIIEMessageBox.Clases.SIIEMessageBox.Show("Seleccione una asignatura modular antes de asociar un plan.", "Planes de pago", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (idPlanSeleccionado == "")
                {
                    SIIEMessageBox.Clases.SIIEMessageBox.Show("Seleccione un plan de pago para la asignatura modular.", "Planes de pago", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string usuario = Environment.UserName.Replace("'", "''");
                strSql = $@"IF EXISTS
                            (
                                SELECT 1
                                FROM dbo.AFTarifaAsignaturaModular
                                WHERE Id_Tarifa = {idPlanSeleccionado}
                                  AND Id_Modulo = {idAsignaturaPlanSeleccionada}
                                  AND Id_Periodo = {IdPeriodoModular}
                            )
                            BEGIN
                                UPDATE dbo.AFTarifaAsignaturaModular
                                SET Activo = 1,
                                    Usuario = N'{usuario}'
                                WHERE Id_Tarifa = {idPlanSeleccionado}
                                  AND Id_Modulo = {idAsignaturaPlanSeleccionada}
                                  AND Id_Periodo = {IdPeriodoModular}
                            END
                            ELSE
                            BEGIN
                                INSERT INTO dbo.AFTarifaAsignaturaModular
                                    (Id_Tarifa, Id_Modulo, Id_Periodo, Activo, Usuario)
                                VALUES
                                    ({idPlanSeleccionado}, {idAsignaturaPlanSeleccionada}, {IdPeriodoModular}, 1, N'{usuario}')
                            END";

                strClave = validar.Crear(strSql);
                SQLRespuesta respuesta = sw.SQLEjecutar(strSql, sConexion, strClave);

                if (respuesta.MensajeError != "")
                {
                    throw new Exception(respuesta.MensajeError);
                }

                CargarPlanesAsignaturaSeleccionada();
                SIIEMessageBox.Clases.SIIEMessageBox.Show("El plan de pago fue asociado a la asignatura modular.", "Planes de pago", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                SIIEMessageBox.Clases.SIIEMessageBox.Show(ex.Message, "Inconsistencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void ultraGridProgramasModulares_InitializeRow(object sender, InitializeRowEventArgs e)
        {
            try
            {
                // Verificamos que la columna exista para evitar errores
                if (e.Row.Band.Columns.Exists("ModularesDisponibles"))
                {
                    // Obtenemos el valor de la base de datos
                    int cantidad = 0;
                    if (e.Row.Cells["ModularesDisponibles"].Value != DBNull.Value)
                    {
                        cantidad = Convert.ToInt32(e.Row.Cells["ModularesDisponibles"].Value);
                    }

                    // APLICAMOS LA LÓGICA VISUAL
                    if (cantidad > 0)
                    {
                        // SI TIENE MODULARES: Verde y Negrita
                        e.Row.Appearance.BackColor = Color.LightGreen;
                        e.Row.Cells["NombrePrograma"].Appearance.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
                    }
                    else
                    {
                        // SI ESTÁ VACÍO: Gris suave (Deshabilitado visualmente)
                        e.Row.Appearance.ForeColor = Color.Gray;
                    }
                }
            }
            catch (Exception ex)
            {
                SIIEMessageBox.Clases.SIIEMessageBox.Show(ex.Message, "Inconsistencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AsignaturasModulares(string IdProgOfre)
        {
            try
            {
                strSql = $"HOHorariosModular {IdProgOfre},{IdPeriodoModular},'S1',null";
                strClave = validar.Crear(strSql);
                rSQLProgramas = sw.SQLCargarDts(strSql, sConexion, strClave);
                if (rSQLProgramas.MensajeError != "")
                {
                    throw new Exception(rSQLProgramas.MensajeError);
                }
                ultraGridAsignaturaModulares.DataSource = rSQLProgramas.Dts.Tables[0];
            }
            catch (Exception ex)
            {
                SIIEMessageBox.Clases.SIIEMessageBox.Show(ex.Message, "Inconsistencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MatriculasModulares()
        {
            try
            {
                strSql = $"HOHorariosModular null,null,'S4',null";
                strClave = validar.Crear(strSql);
                rSQLProgramas = sw.SQLCargarDts(strSql, sConexion, strClave);
                if (rSQLProgramas.MensajeError != "")
                {
                    throw new Exception(rSQLProgramas.MensajeError);
                }

                foreach (DataRow fila in rSQLProgramas.Dts.Tables[0].Rows)
                {
                    if (int.TryParse(fila["Id_Matricula"].ToString(), out int valorEntero))
                    {
                        listaResultados.Add(valorEntero.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                SIIEMessageBox.Clases.SIIEMessageBox.Show(ex.Message, "Inconsistencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ListadoProgramasParaComparar()
        {
            try
            {
                string jsonParaSQL = Newtonsoft.Json.JsonConvert.SerializeObject(listaResultados);
                strSql = $"HOHorariosModular null,null,'S5','{jsonParaSQL}'";
                strClave = validar.Crear(strSql);
                rSQLProgramas = sw.SQLCargarDts(strSql,sConexion,strClave);
                if (rSQLProgramas.MensajeError != "") 
                {
                    throw new Exception(rSQLProgramas.MensajeError);
                }

                foreach (DataRow fila in rSQLProgramas.Dts.Tables[0].Rows)
                {
                    if (int.TryParse(fila["Id_Programa"].ToString(), out int valorEntero))
                    {
                        listaProgramasComparar.Add(valorEntero.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                SIIEMessageBox.Clases.SIIEMessageBox.Show(ex.Message, "Inconsistencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
