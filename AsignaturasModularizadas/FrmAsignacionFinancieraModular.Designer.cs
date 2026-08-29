namespace AsignaturasModularizadas
{
    partial class FrmAsignacionFinancieraModular
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelTitulo = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblPeriodo = new System.Windows.Forms.Label();
            this.splitContainerPrincipal = new System.Windows.Forms.SplitContainer();
            this.panelSeleccion = new System.Windows.Forms.Panel();
            this.lblDocumento = new System.Windows.Forms.Label();
            this.txtDocumento = new System.Windows.Forms.TextBox();
            this.btnBuscarEstudiante = new System.Windows.Forms.Button();
            this.lblPrograma = new System.Windows.Forms.Label();
            this.txtPrograma = new System.Windows.Forms.TextBox();
            this.lblEstudiante = new System.Windows.Forms.Label();
            this.txtEstudiante = new System.Windows.Forms.TextBox();
            this.tableLayoutPanelIzquierda = new System.Windows.Forms.TableLayoutPanel();
            this.ultraGridProgramas = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.ultraGridEstudiantes = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.panelContexto = new System.Windows.Forms.Panel();
            this.lblAsignatura = new System.Windows.Forms.Label();
            this.txtAsignatura = new System.Windows.Forms.TextBox();
            this.lblPlan = new System.Windows.Forms.Label();
            this.txtPlan = new System.Windows.Forms.TextBox();
            this.btnAsignarTarifa = new System.Windows.Forms.Button();
            this.lblTotalPlan = new System.Windows.Forms.Label();
            this.tableLayoutPanelDerecha = new System.Windows.Forms.TableLayoutPanel();
            this.ultraGridAsignaturas = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.splitContainerPlanes = new System.Windows.Forms.SplitContainer();
            this.ultraGridTarifas = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.ultraGridDetallePlan = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.panelAsignaciones = new System.Windows.Forms.Panel();
            this.ultraGridAsignaciones = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.panelResumen = new System.Windows.Forms.Panel();
            this.lblCantidadAsignaturas = new System.Windows.Forms.Label();
            this.lblTotalPagar = new System.Windows.Forms.Label();
            this.panelTitulo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerPrincipal)).BeginInit();
            this.splitContainerPrincipal.Panel1.SuspendLayout();
            this.splitContainerPrincipal.Panel2.SuspendLayout();
            this.splitContainerPrincipal.SuspendLayout();
            this.panelSeleccion.SuspendLayout();
            this.tableLayoutPanelIzquierda.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridProgramas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridEstudiantes)).BeginInit();
            this.panelContexto.SuspendLayout();
            this.tableLayoutPanelDerecha.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridAsignaturas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerPlanes)).BeginInit();
            this.splitContainerPlanes.Panel1.SuspendLayout();
            this.splitContainerPlanes.Panel2.SuspendLayout();
            this.splitContainerPlanes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridTarifas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridDetallePlan)).BeginInit();
            this.panelAsignaciones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridAsignaciones)).BeginInit();
            this.panelResumen.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTitulo
            // 
            this.panelTitulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(126)))), ((int)(((byte)(179)))));
            this.panelTitulo.Controls.Add(this.lblPeriodo);
            this.panelTitulo.Controls.Add(this.lblTitulo);
            this.panelTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitulo.Location = new System.Drawing.Point(0, 0);
            this.panelTitulo.Name = "panelTitulo";
            this.panelTitulo.Size = new System.Drawing.Size(1364, 44);
            this.panelTitulo.TabIndex = 0;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(12, 10);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(322, 21);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Asignación financiera de asignaturas modulares";
            // 
            // lblPeriodo
            // 
            this.lblPeriodo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPeriodo.AutoSize = true;
            this.lblPeriodo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPeriodo.ForeColor = System.Drawing.Color.White;
            this.lblPeriodo.Location = new System.Drawing.Point(1234, 15);
            this.lblPeriodo.Name = "lblPeriodo";
            this.lblPeriodo.Size = new System.Drawing.Size(95, 15);
            this.lblPeriodo.TabIndex = 1;
            this.lblPeriodo.Text = "Período: --";
            // 
            // splitContainerPrincipal
            // 
            this.splitContainerPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerPrincipal.Location = new System.Drawing.Point(0, 44);
            this.splitContainerPrincipal.Name = "splitContainerPrincipal";
            // 
            // splitContainerPrincipal.Panel1
            // 
            this.splitContainerPrincipal.Panel1.Controls.Add(this.tableLayoutPanelIzquierda);
            this.splitContainerPrincipal.Panel1.Controls.Add(this.panelSeleccion);
            // 
            // splitContainerPrincipal.Panel2
            // 
            this.splitContainerPrincipal.Panel2.Controls.Add(this.tableLayoutPanelDerecha);
            this.splitContainerPrincipal.Panel2.Controls.Add(this.panelContexto);
            this.splitContainerPrincipal.Size = new System.Drawing.Size(1364, 725);
            this.splitContainerPrincipal.SplitterDistance = 380;
            this.splitContainerPrincipal.TabIndex = 1;
            // 
            // panelSeleccion
            // 
            this.panelSeleccion.BackColor = System.Drawing.Color.White;
            this.panelSeleccion.Controls.Add(this.btnBuscarEstudiante);
            this.panelSeleccion.Controls.Add(this.txtDocumento);
            this.panelSeleccion.Controls.Add(this.lblDocumento);
            this.panelSeleccion.Controls.Add(this.txtEstudiante);
            this.panelSeleccion.Controls.Add(this.lblEstudiante);
            this.panelSeleccion.Controls.Add(this.txtPrograma);
            this.panelSeleccion.Controls.Add(this.lblPrograma);
            this.panelSeleccion.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSeleccion.Location = new System.Drawing.Point(0, 0);
            this.panelSeleccion.Name = "panelSeleccion";
            this.panelSeleccion.Size = new System.Drawing.Size(380, 116);
            this.panelSeleccion.TabIndex = 0;
            // 
            // lblDocumento
            // 
            this.lblDocumento.AutoSize = true;
            this.lblDocumento.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDocumento.Location = new System.Drawing.Point(10, 13);
            this.lblDocumento.Name = "lblDocumento";
            this.lblDocumento.Size = new System.Drawing.Size(72, 15);
            this.lblDocumento.TabIndex = 0;
            this.lblDocumento.Text = "Documento:";
            // 
            // txtDocumento
            // 
            this.txtDocumento.Location = new System.Drawing.Point(88, 10);
            this.txtDocumento.MaxLength = 16;
            this.txtDocumento.Name = "txtDocumento";
            this.txtDocumento.Size = new System.Drawing.Size(160, 20);
            this.txtDocumento.TabIndex = 1;
            this.txtDocumento.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDocumento_KeyDown);
            // 
            // btnBuscarEstudiante
            // 
            this.btnBuscarEstudiante.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(126)))), ((int)(((byte)(179)))));
            this.btnBuscarEstudiante.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscarEstudiante.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscarEstudiante.ForeColor = System.Drawing.Color.White;
            this.btnBuscarEstudiante.Location = new System.Drawing.Point(258, 7);
            this.btnBuscarEstudiante.Name = "btnBuscarEstudiante";
            this.btnBuscarEstudiante.Size = new System.Drawing.Size(106, 27);
            this.btnBuscarEstudiante.TabIndex = 2;
            this.btnBuscarEstudiante.Text = "Buscar";
            this.btnBuscarEstudiante.UseVisualStyleBackColor = false;
            this.btnBuscarEstudiante.Click += new System.EventHandler(this.btnBuscarEstudiante_Click);
            // 
            // lblPrograma
            // 
            this.lblPrograma.AutoSize = true;
            this.lblPrograma.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrograma.Location = new System.Drawing.Point(10, 47);
            this.lblPrograma.Name = "lblPrograma";
            this.lblPrograma.Size = new System.Drawing.Size(65, 15);
            this.lblPrograma.TabIndex = 3;
            this.lblPrograma.Text = "Programa:";
            // 
            // txtPrograma
            // 
            this.txtPrograma.BackColor = System.Drawing.Color.White;
            this.txtPrograma.Location = new System.Drawing.Point(82, 44);
            this.txtPrograma.Name = "txtPrograma";
            this.txtPrograma.ReadOnly = true;
            this.txtPrograma.Size = new System.Drawing.Size(282, 20);
            this.txtPrograma.TabIndex = 4;
            // 
            // lblEstudiante
            // 
            this.lblEstudiante.AutoSize = true;
            this.lblEstudiante.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEstudiante.Location = new System.Drawing.Point(10, 81);
            this.lblEstudiante.Name = "lblEstudiante";
            this.lblEstudiante.Size = new System.Drawing.Size(65, 15);
            this.lblEstudiante.TabIndex = 5;
            this.lblEstudiante.Text = "Estudiante:";
            // 
            // txtEstudiante
            // 
            this.txtEstudiante.BackColor = System.Drawing.Color.White;
            this.txtEstudiante.Location = new System.Drawing.Point(82, 78);
            this.txtEstudiante.Name = "txtEstudiante";
            this.txtEstudiante.ReadOnly = true;
            this.txtEstudiante.Size = new System.Drawing.Size(282, 20);
            this.txtEstudiante.TabIndex = 6;
            // 
            // tableLayoutPanelIzquierda
            // 
            this.tableLayoutPanelIzquierda.ColumnCount = 1;
            this.tableLayoutPanelIzquierda.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelIzquierda.Controls.Add(this.ultraGridProgramas, 0, 0);
            this.tableLayoutPanelIzquierda.Controls.Add(this.ultraGridEstudiantes, 0, 1);
            this.tableLayoutPanelIzquierda.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelIzquierda.Location = new System.Drawing.Point(0, 116);
            this.tableLayoutPanelIzquierda.Name = "tableLayoutPanelIzquierda";
            this.tableLayoutPanelIzquierda.RowCount = 2;
            this.tableLayoutPanelIzquierda.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 48F));
            this.tableLayoutPanelIzquierda.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 52F));
            this.tableLayoutPanelIzquierda.Size = new System.Drawing.Size(380, 609);
            this.tableLayoutPanelIzquierda.TabIndex = 1;
            // 
            // ultraGridProgramas
            // 
            this.ultraGridProgramas.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ExtendLastColumn;
            this.ultraGridProgramas.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.True;
            this.ultraGridProgramas.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            this.ultraGridProgramas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGridProgramas.Location = new System.Drawing.Point(3, 3);
            this.ultraGridProgramas.Name = "ultraGridProgramas";
            this.ultraGridProgramas.Size = new System.Drawing.Size(374, 286);
            this.ultraGridProgramas.TabIndex = 0;
            this.ultraGridProgramas.Text = "Programas";
            this.ultraGridProgramas.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridProgramas_InitializeLayout);
            this.ultraGridProgramas.ClickCell += new Infragistics.Win.UltraWinGrid.ClickCellEventHandler(this.ultraGridProgramas_ClickCell);
            // 
            // ultraGridEstudiantes
            // 
            this.ultraGridEstudiantes.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ExtendLastColumn;
            this.ultraGridEstudiantes.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.True;
            this.ultraGridEstudiantes.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            this.ultraGridEstudiantes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGridEstudiantes.Location = new System.Drawing.Point(3, 295);
            this.ultraGridEstudiantes.Name = "ultraGridEstudiantes";
            this.ultraGridEstudiantes.Size = new System.Drawing.Size(374, 311);
            this.ultraGridEstudiantes.TabIndex = 1;
            this.ultraGridEstudiantes.Text = "Estudiantes con matrícula modular";
            this.ultraGridEstudiantes.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridEstudiantes_InitializeLayout);
            this.ultraGridEstudiantes.ClickCell += new Infragistics.Win.UltraWinGrid.ClickCellEventHandler(this.ultraGridEstudiantes_ClickCell);
            // 
            // panelContexto
            // 
            this.panelContexto.BackColor = System.Drawing.Color.White;
            this.panelContexto.Controls.Add(this.lblTotalPlan);
            this.panelContexto.Controls.Add(this.btnAsignarTarifa);
            this.panelContexto.Controls.Add(this.txtPlan);
            this.panelContexto.Controls.Add(this.lblPlan);
            this.panelContexto.Controls.Add(this.txtAsignatura);
            this.panelContexto.Controls.Add(this.lblAsignatura);
            this.panelContexto.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelContexto.Location = new System.Drawing.Point(0, 0);
            this.panelContexto.Name = "panelContexto";
            this.panelContexto.Size = new System.Drawing.Size(980, 82);
            this.panelContexto.TabIndex = 0;
            // 
            // lblAsignatura
            // 
            this.lblAsignatura.AutoSize = true;
            this.lblAsignatura.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAsignatura.Location = new System.Drawing.Point(12, 13);
            this.lblAsignatura.Name = "lblAsignatura";
            this.lblAsignatura.Size = new System.Drawing.Size(71, 15);
            this.lblAsignatura.TabIndex = 0;
            this.lblAsignatura.Text = "Asignatura:";
            // 
            // txtAsignatura
            // 
            this.txtAsignatura.BackColor = System.Drawing.Color.White;
            this.txtAsignatura.Location = new System.Drawing.Point(90, 10);
            this.txtAsignatura.Name = "txtAsignatura";
            this.txtAsignatura.ReadOnly = true;
            this.txtAsignatura.Size = new System.Drawing.Size(390, 20);
            this.txtAsignatura.TabIndex = 1;
            // 
            // lblPlan
            // 
            this.lblPlan.AutoSize = true;
            this.lblPlan.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlan.Location = new System.Drawing.Point(12, 47);
            this.lblPlan.Name = "lblPlan";
            this.lblPlan.Size = new System.Drawing.Size(33, 15);
            this.lblPlan.TabIndex = 2;
            this.lblPlan.Text = "Plan:";
            // 
            // txtPlan
            // 
            this.txtPlan.BackColor = System.Drawing.Color.White;
            this.txtPlan.Location = new System.Drawing.Point(90, 44);
            this.txtPlan.Name = "txtPlan";
            this.txtPlan.ReadOnly = true;
            this.txtPlan.Size = new System.Drawing.Size(390, 20);
            this.txtPlan.TabIndex = 3;
            // 
            // btnAsignarTarifa
            // 
            this.btnAsignarTarifa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(126)))), ((int)(((byte)(179)))));
            this.btnAsignarTarifa.Enabled = false;
            this.btnAsignarTarifa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAsignarTarifa.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAsignarTarifa.ForeColor = System.Drawing.Color.White;
            this.btnAsignarTarifa.Location = new System.Drawing.Point(732, 39);
            this.btnAsignarTarifa.Name = "btnAsignarTarifa";
            this.btnAsignarTarifa.Size = new System.Drawing.Size(168, 28);
            this.btnAsignarTarifa.TabIndex = 4;
            this.btnAsignarTarifa.Text = "Asignar tarifa y matrícula";
            this.btnAsignarTarifa.UseVisualStyleBackColor = false;
            this.btnAsignarTarifa.Click += new System.EventHandler(this.btnAsignarTarifa_Click);
            // 
            // lblTotalPlan
            // 
            this.lblTotalPlan.AutoSize = true;
            this.lblTotalPlan.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalPlan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(126)))), ((int)(((byte)(179)))));
            this.lblTotalPlan.Location = new System.Drawing.Point(520, 14);
            this.lblTotalPlan.Name = "lblTotalPlan";
            this.lblTotalPlan.Size = new System.Drawing.Size(112, 15);
            this.lblTotalPlan.TabIndex = 5;
            this.lblTotalPlan.Text = "Total del plan: 0";
            // 
            // tableLayoutPanelDerecha
            // 
            this.tableLayoutPanelDerecha.ColumnCount = 1;
            this.tableLayoutPanelDerecha.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelDerecha.Controls.Add(this.ultraGridAsignaturas, 0, 0);
            this.tableLayoutPanelDerecha.Controls.Add(this.splitContainerPlanes, 0, 1);
            this.tableLayoutPanelDerecha.Controls.Add(this.panelAsignaciones, 0, 2);
            this.tableLayoutPanelDerecha.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelDerecha.Location = new System.Drawing.Point(0, 82);
            this.tableLayoutPanelDerecha.Name = "tableLayoutPanelDerecha";
            this.tableLayoutPanelDerecha.RowCount = 3;
            this.tableLayoutPanelDerecha.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 180F));
            this.tableLayoutPanelDerecha.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 205F));
            this.tableLayoutPanelDerecha.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelDerecha.Size = new System.Drawing.Size(980, 643);
            this.tableLayoutPanelDerecha.TabIndex = 1;
            // 
            // ultraGridAsignaturas
            // 
            this.ultraGridAsignaturas.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ExtendLastColumn;
            this.ultraGridAsignaturas.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.True;
            this.ultraGridAsignaturas.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            this.ultraGridAsignaturas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGridAsignaturas.Location = new System.Drawing.Point(3, 3);
            this.ultraGridAsignaturas.Name = "ultraGridAsignaturas";
            this.ultraGridAsignaturas.Size = new System.Drawing.Size(974, 174);
            this.ultraGridAsignaturas.TabIndex = 0;
            this.ultraGridAsignaturas.Text = "Asignaturas modulares ofertadas";
            this.ultraGridAsignaturas.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridAsignaturas_InitializeLayout);
            this.ultraGridAsignaturas.ClickCell += new Infragistics.Win.UltraWinGrid.ClickCellEventHandler(this.ultraGridAsignaturas_ClickCell);
            // 
            // splitContainerPlanes
            // 
            this.splitContainerPlanes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerPlanes.Location = new System.Drawing.Point(3, 183);
            this.splitContainerPlanes.Name = "splitContainerPlanes";
            // 
            // splitContainerPlanes.Panel1
            // 
            this.splitContainerPlanes.Panel1.Controls.Add(this.ultraGridTarifas);
            // 
            // splitContainerPlanes.Panel2
            // 
            this.splitContainerPlanes.Panel2.Controls.Add(this.ultraGridDetallePlan);
            this.splitContainerPlanes.Size = new System.Drawing.Size(974, 199);
            this.splitContainerPlanes.SplitterDistance = 490;
            this.splitContainerPlanes.TabIndex = 1;
            // 
            // ultraGridTarifas
            // 
            this.ultraGridTarifas.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ResizeAllColumns;
            this.ultraGridTarifas.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.True;
            this.ultraGridTarifas.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            this.ultraGridTarifas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGridTarifas.Location = new System.Drawing.Point(0, 0);
            this.ultraGridTarifas.Name = "ultraGridTarifas";
            this.ultraGridTarifas.Size = new System.Drawing.Size(490, 199);
            this.ultraGridTarifas.TabIndex = 0;
            this.ultraGridTarifas.Text = "Planes habilitados para la asignatura";
            this.ultraGridTarifas.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridTarifas_InitializeLayout);
            this.ultraGridTarifas.ClickCell += new Infragistics.Win.UltraWinGrid.ClickCellEventHandler(this.ultraGridTarifas_ClickCell);
            // 
            // ultraGridDetallePlan
            // 
            this.ultraGridDetallePlan.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ResizeAllColumns;
            this.ultraGridDetallePlan.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.True;
            this.ultraGridDetallePlan.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            this.ultraGridDetallePlan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGridDetallePlan.Location = new System.Drawing.Point(0, 0);
            this.ultraGridDetallePlan.Name = "ultraGridDetallePlan";
            this.ultraGridDetallePlan.Size = new System.Drawing.Size(480, 199);
            this.ultraGridDetallePlan.TabIndex = 0;
            this.ultraGridDetallePlan.Text = "Cuotas y fechas de corte";
            this.ultraGridDetallePlan.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridDetallePlan_InitializeLayout);
            // 
            // panelAsignaciones
            // 
            this.panelAsignaciones.Controls.Add(this.ultraGridAsignaciones);
            this.panelAsignaciones.Controls.Add(this.panelResumen);
            this.panelAsignaciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelAsignaciones.Location = new System.Drawing.Point(3, 388);
            this.panelAsignaciones.Name = "panelAsignaciones";
            this.panelAsignaciones.Size = new System.Drawing.Size(974, 252);
            this.panelAsignaciones.TabIndex = 2;
            // 
            // ultraGridAsignaciones
            // 
            this.ultraGridAsignaciones.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ResizeAllColumns;
            this.ultraGridAsignaciones.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.True;
            this.ultraGridAsignaciones.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            this.ultraGridAsignaciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGridAsignaciones.Location = new System.Drawing.Point(0, 0);
            this.ultraGridAsignaciones.Name = "ultraGridAsignaciones";
            this.ultraGridAsignaciones.Size = new System.Drawing.Size(974, 200);
            this.ultraGridAsignaciones.TabIndex = 0;
            this.ultraGridAsignaciones.Text = "Asignaturas financieras del estudiante";
            this.ultraGridAsignaciones.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridAsignaciones_InitializeLayout);
            // 
            // panelResumen
            // 
            this.panelResumen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(244)))), ((int)(((byte)(251)))));
            this.panelResumen.Controls.Add(this.lblTotalPagar);
            this.panelResumen.Controls.Add(this.lblCantidadAsignaturas);
            this.panelResumen.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelResumen.Location = new System.Drawing.Point(0, 200);
            this.panelResumen.Name = "panelResumen";
            this.panelResumen.Size = new System.Drawing.Size(974, 52);
            this.panelResumen.TabIndex = 1;
            // 
            // lblCantidadAsignaturas
            // 
            this.lblCantidadAsignaturas.AutoSize = true;
            this.lblCantidadAsignaturas.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCantidadAsignaturas.Location = new System.Drawing.Point(14, 17);
            this.lblCantidadAsignaturas.Name = "lblCantidadAsignaturas";
            this.lblCantidadAsignaturas.Size = new System.Drawing.Size(191, 19);
            this.lblCantidadAsignaturas.TabIndex = 0;
            this.lblCantidadAsignaturas.Text = "Asignaturas matriculadas: 0";
            // 
            // lblTotalPagar
            // 
            this.lblTotalPagar.AutoSize = true;
            this.lblTotalPagar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalPagar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(113)))), ((int)(((byte)(66)))));
            this.lblTotalPagar.Location = new System.Drawing.Point(420, 17);
            this.lblTotalPagar.Name = "lblTotalPagar";
            this.lblTotalPagar.Size = new System.Drawing.Size(113, 19);
            this.lblTotalPagar.TabIndex = 1;
            this.lblTotalPagar.Text = "Total a pagar: 0";
            // 
            // FrmAsignacionFinancieraModular
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1364, 769);
            this.Controls.Add(this.splitContainerPrincipal);
            this.Controls.Add(this.panelTitulo);
            this.MinimumSize = new System.Drawing.Size(1100, 650);
            this.Name = "FrmAsignacionFinancieraModular";
            this.Text = "Asignación financiera modular";
            this.Load += new System.EventHandler(this.FrmAsignacionFinancieraModular_Load);
            this.panelTitulo.ResumeLayout(false);
            this.panelTitulo.PerformLayout();
            this.splitContainerPrincipal.Panel1.ResumeLayout(false);
            this.splitContainerPrincipal.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerPrincipal)).EndInit();
            this.splitContainerPrincipal.ResumeLayout(false);
            this.panelSeleccion.ResumeLayout(false);
            this.panelSeleccion.PerformLayout();
            this.tableLayoutPanelIzquierda.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridProgramas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridEstudiantes)).EndInit();
            this.panelContexto.ResumeLayout(false);
            this.panelContexto.PerformLayout();
            this.tableLayoutPanelDerecha.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridAsignaturas)).EndInit();
            this.splitContainerPlanes.Panel1.ResumeLayout(false);
            this.splitContainerPlanes.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerPlanes)).EndInit();
            this.splitContainerPlanes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridTarifas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridDetallePlan)).EndInit();
            this.panelAsignaciones.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridAsignaciones)).EndInit();
            this.panelResumen.ResumeLayout(false);
            this.panelResumen.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel panelTitulo;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblPeriodo;
        private System.Windows.Forms.SplitContainer splitContainerPrincipal;
        private System.Windows.Forms.Panel panelSeleccion;
        private System.Windows.Forms.Label lblDocumento;
        private System.Windows.Forms.TextBox txtDocumento;
        private System.Windows.Forms.Button btnBuscarEstudiante;
        private System.Windows.Forms.Label lblPrograma;
        private System.Windows.Forms.TextBox txtPrograma;
        private System.Windows.Forms.Label lblEstudiante;
        private System.Windows.Forms.TextBox txtEstudiante;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelIzquierda;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridProgramas;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridEstudiantes;
        private System.Windows.Forms.Panel panelContexto;
        private System.Windows.Forms.Label lblAsignatura;
        private System.Windows.Forms.TextBox txtAsignatura;
        private System.Windows.Forms.Label lblPlan;
        private System.Windows.Forms.TextBox txtPlan;
        private System.Windows.Forms.Button btnAsignarTarifa;
        private System.Windows.Forms.Label lblTotalPlan;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelDerecha;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridAsignaturas;
        private System.Windows.Forms.SplitContainer splitContainerPlanes;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridTarifas;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridDetallePlan;
        private System.Windows.Forms.Panel panelAsignaciones;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridAsignaciones;
        private System.Windows.Forms.Panel panelResumen;
        private System.Windows.Forms.Label lblCantidadAsignaturas;
        private System.Windows.Forms.Label lblTotalPagar;
    }
}
