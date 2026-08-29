/*
    Estructura y operaciones para asignar una asignatura modular al plan
    financiero de un estudiante. Ejecutar en la misma base donde existen
    AFPlanes, AFPlanEstudiante, AFDetallePlanEstudiante y CALAsignaturaEnCurso.
*/

IF COL_LENGTH(N'dbo.AFPlanEstudiante', N'Id_Matricula') IS NULL
BEGIN
    ALTER TABLE dbo.AFPlanEstudiante ADD Id_Matricula INT NULL;
END
GO

IF COL_LENGTH(N'dbo.AFPlanEstudiante', N'Activo') IS NULL
BEGIN
    ALTER TABLE dbo.AFPlanEstudiante
    ADD Activo BIT NOT NULL
        CONSTRAINT DF_AFPlanEstudiante_Activo DEFAULT (1) WITH VALUES;
END
GO

IF COL_LENGTH(N'dbo.AFPlanEstudiante', N'Usuario') IS NULL
BEGIN
    ALTER TABLE dbo.AFPlanEstudiante ADD Usuario NVARCHAR(100) NULL;
END
GO

IF OBJECT_ID(N'dbo.AFPlanEstudianteModulo', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.AFPlanEstudianteModulo
    (
        Id_PlanEstudianteModulo INT IDENTITY(1,1) NOT NULL,
        Id_PlanEstudiante INT NOT NULL,
        Id_Modulo INT NOT NULL,
        Id_TarifaAsignaturaModular INT NOT NULL,
        ValorOriginal INT NOT NULL,
        ValorDescuento INT NOT NULL CONSTRAINT DF_AFPlanEstudianteModulo_Descuento DEFAULT (0),
        ValorFinal INT NOT NULL,
        Activo BIT NOT NULL CONSTRAINT DF_AFPlanEstudianteModulo_Activo DEFAULT (1),
        Fecha DATETIME NOT NULL CONSTRAINT DF_AFPlanEstudianteModulo_Fecha DEFAULT (GETDATE()),
        Usuario NVARCHAR(100) NULL,
        Id_AsigCurso INT NULL,

        CONSTRAINT PK_AFPlanEstudianteModulo
            PRIMARY KEY (Id_PlanEstudianteModulo),
        CONSTRAINT FK_AFPlanEstudianteModulo_PlanEstudiante
            FOREIGN KEY (Id_PlanEstudiante)
            REFERENCES dbo.AFPlanEstudiante (Id_PlanEstudiante),
        CONSTRAINT FK_AFPlanEstudianteModulo_TarifaModulo
            FOREIGN KEY (Id_TarifaAsignaturaModular)
            REFERENCES dbo.AFTarifaAsignaturaModular (Id_TarifaAsignaturaModular)
    );
END
GO

IF COL_LENGTH(N'dbo.AFDetallePlanEstudiante', N'Id_PlanEstudianteModulo') IS NULL
BEGIN
    ALTER TABLE dbo.AFDetallePlanEstudiante ADD Id_PlanEstudianteModulo INT NULL;
END
GO

IF COL_LENGTH(N'dbo.AFDetallePlanEstudiante', N'Id_DetallePlan') IS NULL
BEGIN
    ALTER TABLE dbo.AFDetallePlanEstudiante ADD Id_DetallePlan INT NULL;
END
GO

IF NOT EXISTS
(
    SELECT 1
    FROM sys.indexes
    WHERE object_id = OBJECT_ID(N'dbo.AFPlanEstudianteModulo')
      AND name = N'IX_AFPlanEstudianteModulo_PlanModulo'
)
BEGIN
    CREATE INDEX IX_AFPlanEstudianteModulo_PlanModulo
        ON dbo.AFPlanEstudianteModulo (Id_PlanEstudiante, Id_Modulo, Activo);
END
GO

IF OBJECT_ID(N'dbo.AFMatriculaAsignaturaModular', N'P') IS NULL
BEGIN
    EXEC(N'
        CREATE PROCEDURE dbo.AFMatriculaAsignaturaModular
        AS
        BEGIN
            SET NOCOUNT ON;
        END');
END
GO

ALTER PROCEDURE dbo.AFMatriculaAsignaturaModular
    @Id_Tipo NVARCHAR(2),
    @Id_Estudiante INT = NULL,
    @Id_DocEstudiante VARCHAR(16) = NULL,
    @Id_Matricula INT = NULL,
    @Id_Programa INT = NULL,
    @Id_Modulo INT = NULL,
    @Id_TarifaAsignaturaModular INT = NULL,
    @Id_Tarifa INT = NULL,
    @Id_Periodo INT = NULL,
    @Semestre INT = NULL,
    @Nivel INT = NULL,
    @Id_Docente INT = NULL,
    @Usuario NVARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Id_PlanEstudiante INT;
    DECLARE @Id_PlanEstudianteModulo INT;
    DECLARE @Id_AsigCurso INT;
    DECLARE @ValorOriginal INT;
    DECLARE @ValorFinal INT;

    SET @Id_Tipo = UPPER(LTRIM(RTRIM(ISNULL(@Id_Tipo, N''))));
    SET @Usuario = ISNULL(NULLIF(LTRIM(RTRIM(@Usuario)), N''), LEFT(SUSER_SNAME(), 100));

    /* B: busca el estudiante por documento y su matrícula modular del período. */
    IF @Id_Tipo = N'B'
    BEGIN
        IF NULLIF(LTRIM(RTRIM(@Id_DocEstudiante)), '') IS NULL OR @Id_Periodo IS NULL
        BEGIN
            RAISERROR('Se requieren Id_DocEstudiante e Id_Periodo.', 16, 1);
            RETURN;
        END;

        SELECT TOP (1)
            E.Id_Estudiante,
            E.Id_DocEstudiante,
            LTRIM(RTRIM(ISNULL(E.Nombres, ''))) + ' ' +
            LTRIM(RTRIM(ISNULL(E.PrimerApellido, ''))) + ' ' +
            LTRIM(RTRIM(ISNULL(E.SegundoApellido, ''))) AS Estudiante,
            M.Id_Matricula,
            M.Id_Periodo,
            M.Nivel,
            M.EsModular,
            CASE WHEN M.Id_Matricula IS NULL THEN 0 ELSE 1 END AS TieneMatriculaModular
        FROM dbo.GEEstudiante AS E
        OUTER APPLY
        (
            SELECT TOP (1)
                GM.Id_Matricula,
                GM.Id_Periodo,
                GM.Nivel,
                GM.EsModular
            FROM dbo.GEMatricula AS GM
            WHERE GM.Id_Estudiante = E.Id_Estudiante
              AND GM.Id_Periodo = @Id_Periodo
              AND ISNULL(GM.EsModular, 0) = 1
            ORDER BY GM.Id_Matricula DESC
        ) AS M
        WHERE E.Id_DocEstudiante = @Id_DocEstudiante;

        RETURN;
    END;

    /* S: asignaturas financieras activas del estudiante en el período. */
    IF @Id_Tipo = N'S'
    BEGIN
        IF @Id_Estudiante IS NULL OR @Id_Periodo IS NULL
        BEGIN
            RAISERROR('Se requieren Id_Estudiante e Id_Periodo.', 16, 1);
            RETURN;
        END;

        SELECT
            PEM.Id_PlanEstudianteModulo,
            PE.Id_PlanEstudiante,
            PEM.Id_Modulo,
            PEM.Id_TarifaAsignaturaModular,
            PEM.Id_AsigCurso,
            P.Id_Plan AS Id_Tarifa,
            P.NombrePlan,
            PEM.ValorOriginal,
            PEM.ValorDescuento,
            PEM.ValorFinal,
            PEM.Activo,
            PEM.Fecha
        FROM dbo.AFPlanEstudianteModulo AS PEM
        INNER JOIN dbo.AFPlanEstudiante AS PE
            ON PE.Id_PlanEstudiante = PEM.Id_PlanEstudiante
        INNER JOIN dbo.AFPlanes AS P
            ON P.Id_Plan = PE.Id_Plan
        WHERE PE.Id_Estudiante = @Id_Estudiante
          AND PE.Id_Periodo = @Id_Periodo
          AND (@Id_Programa IS NULL OR PE.Id_Programa = @Id_Programa)
          AND PEM.Activo = 1
          AND PE.Activo = 1
        ORDER BY PEM.Fecha DESC, PEM.Id_PlanEstudianteModulo DESC;

        RETURN;
    END;

    /* I: crea la relación financiera, las cuotas y CALAsignaturaEnCurso. */
    IF @Id_Tipo = N'I'
    BEGIN
        IF @Id_Estudiante IS NULL OR @Id_Matricula IS NULL OR @Id_Programa IS NULL
           OR @Id_Modulo IS NULL OR @Id_TarifaAsignaturaModular IS NULL
           OR @Id_Tarifa IS NULL OR @Id_Periodo IS NULL OR @Id_Docente IS NULL
        BEGIN
            RAISERROR('Faltan datos para asignar la tarifa y crear la asignatura en curso.', 16, 1);
            RETURN;
        END;

        SELECT @Nivel = Nivel
        FROM dbo.GEMatricula
        WHERE Id_Matricula = @Id_Matricula
          AND Id_Estudiante = @Id_Estudiante
          AND Id_Periodo = @Id_Periodo
          AND ISNULL(EsModular, 0) = 1;

        IF @Nivel IS NULL
        BEGIN
            RAISERROR('La matrícula modular no existe o no tiene Nivel definido para el período.', 16, 1);
            RETURN;
        END;

        SET @Semestre = ISNULL(@Semestre, @Nivel);

        IF NOT EXISTS
        (
            SELECT 1
            FROM dbo.AFTarifaAsignaturaModular
            WHERE Id_TarifaAsignaturaModular = @Id_TarifaAsignaturaModular
              AND Id_Tarifa = @Id_Tarifa
              AND Id_Modulo = @Id_Modulo
              AND Id_Periodo = @Id_Periodo
              AND Activo = 1
        )
        BEGIN
            RAISERROR('La tarifa no está habilitada para la asignatura modular y período seleccionados.', 16, 1);
            RETURN;
        END;

        IF EXISTS
        (
            SELECT 1
            FROM dbo.AFPlanEstudianteModulo AS PEM
            INNER JOIN dbo.AFPlanEstudiante AS PE
                ON PE.Id_PlanEstudiante = PEM.Id_PlanEstudiante
            WHERE PE.Id_Estudiante = @Id_Estudiante
              AND PE.Id_Periodo = @Id_Periodo
              AND PEM.Id_Modulo = @Id_Modulo
              AND PE.Activo = 1
              AND PEM.Activo = 1
        )
        BEGIN
            RAISERROR('El estudiante ya tiene esta asignatura modular activa en el período.', 16, 1);
            RETURN;
        END;

        SELECT
            @ValorOriginal = ISNULL(ValorOrdinaria, 0),
            @ValorFinal = ISNULL(ValorOrdinaria, 0)
        FROM dbo.AFPlanes
        WHERE Id_Plan = @Id_Tarifa
          AND Id_Periodo = @Id_Periodo;

        IF @ValorOriginal IS NULL
        BEGIN
            RAISERROR('No se encontró la tarifa en AFPlanes para el período indicado.', 16, 1);
            RETURN;
        END;

        BEGIN TRY
            BEGIN TRANSACTION;

            SELECT TOP (1)
                @Id_PlanEstudiante = Id_PlanEstudiante
            FROM dbo.AFPlanEstudiante
            WHERE Id_Plan = @Id_Tarifa
              AND Id_Programa = @Id_Programa
              AND Id_Estudiante = @Id_Estudiante
              AND Id_Periodo = @Id_Periodo
              AND Activo = 1
            ORDER BY Id_PlanEstudiante DESC;

            IF @Id_PlanEstudiante IS NULL
            BEGIN
                INSERT INTO dbo.AFPlanEstudiante
                    (Id_Plan, Id_Programa, Semestre, Fecha, Id_Estudiante, Id_Periodo,
                     Id_Matricula, Activo, Usuario)
                VALUES
                    (@Id_Tarifa, @Id_Programa, @Semestre, GETDATE(), @Id_Estudiante,
                     @Id_Periodo, @Id_Matricula, 1, @Usuario);

                SET @Id_PlanEstudiante = SCOPE_IDENTITY();
            END;

            SELECT TOP (1)
                @Id_AsigCurso = Id_AsigCurso
            FROM dbo.CALAsignaturaEnCurso
            WHERE Id_Matricula = @Id_Matricula
              AND Id_AsignaturaPlan = @Id_Modulo
              AND Id_Periodo = @Id_Periodo
            ORDER BY Id_AsigCurso DESC;

            IF @Id_AsigCurso IS NULL
            BEGIN
                INSERT INTO dbo.CALAsignaturaEnCurso
                    (Id_Matricula, Nivel, Id_Programa, Id_AsignaturaPlan, Id_Docente, Id_Periodo)
                VALUES
                    (@Id_Matricula, @Nivel, @Id_Programa, @Id_Modulo, @Id_Docente, @Id_Periodo);

                SET @Id_AsigCurso = SCOPE_IDENTITY();
            END;

            INSERT INTO dbo.AFPlanEstudianteModulo
                (Id_PlanEstudiante, Id_Modulo, Id_TarifaAsignaturaModular,
                 ValorOriginal, ValorDescuento, ValorFinal, Activo, Fecha, Usuario, Id_AsigCurso)
            VALUES
                (@Id_PlanEstudiante, @Id_Modulo, @Id_TarifaAsignaturaModular,
                 @ValorOriginal, 0, @ValorFinal, 1, GETDATE(), @Usuario, @Id_AsigCurso);

            SET @Id_PlanEstudianteModulo = SCOPE_IDENTITY();

            INSERT INTO dbo.AFDetallePlanEstudiante
                (Id_PlanEstudiante, Id_PlanEstudianteModulo, Id_DetallePlan,
                 Concepto, FechaPago, Valor)
            SELECT
                @Id_PlanEstudiante,
                @Id_PlanEstudianteModulo,
                DP.Id_DetallePlan,
                DP.Concepto,
                DP.FechaPago,
                ISNULL(DP.Valor, 0)
            FROM dbo.AFDetallePlan AS DP
            WHERE DP.Id_Plan = @Id_Tarifa;

            COMMIT TRANSACTION;
        END TRY
        BEGIN CATCH
            IF @@TRANCOUNT > 0
                ROLLBACK TRANSACTION;

            THROW;
        END CATCH;

        SELECT
            @Id_PlanEstudiante AS Id_PlanEstudiante,
            @Id_PlanEstudianteModulo AS Id_PlanEstudianteModulo,
            @Id_AsigCurso AS Id_AsigCurso;

        RETURN;
    END;

    RAISERROR('Tipo inválido. Use B para buscar, S para consultar o I para asignar.', 16, 1);
END
GO
