/*
    Gestión de planes de pago para asignaturas modulares.

    Relación usada:
      AFPlanes.Id_Plan -> AFTarifaAsignaturaModular.Id_Tarifa
      Id_AsignaturaPlan -> AFTarifaAsignaturaModular.Id_Modulo
      Período vigente -> AFTarifaAsignaturaModular.Id_Periodo
*/

IF OBJECT_ID(N'dbo.AFGestionTarifaAsignaturaModular', N'P') IS NULL
BEGIN
    EXEC(N'CREATE PROCEDURE dbo.AFGestionTarifaAsignaturaModular AS BEGIN SET NOCOUNT ON; END');
END
GO

ALTER PROCEDURE dbo.AFGestionTarifaAsignaturaModular
    @Id_Tipo NVARCHAR(2),
    @Id_Modulo INT = NULL,
    @Id_Tarifa INT = NULL,
    @Id_Periodo INT = NULL,
    @Usuario NVARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SET @Id_Tipo = UPPER(LTRIM(RTRIM(ISNULL(@Id_Tipo, N''))));
    SET @Usuario = ISNULL(NULLIF(LTRIM(RTRIM(@Usuario)), N''), LEFT(SUSER_SNAME(), 100));

    /* H: devuelve únicamente las tarifas activas habilitadas para el módulo. */
    IF @Id_Tipo = N'H'
    BEGIN
        IF @Id_Modulo IS NULL OR @Id_Periodo IS NULL
        BEGIN
            RAISERROR('Para consultar tarifas habilitadas se requieren Id_Modulo e Id_Periodo.', 16, 1);
            RETURN;
        END

        SELECT
            TAM.Id_TarifaAsignaturaModular,
            P.Id_Plan AS Id_Tarifa,
            P.NombrePlan,
            P.TipoPlan,
            P.ValorOrdinaria,
            P.ValorExtraordinaria,
            P.ValorDescuento,
            P.Id_Periodo,
            TAM.Activo,
            'Habilitado' AS Estado
        FROM dbo.AFTarifaAsignaturaModular AS TAM
        INNER JOIN dbo.AFPlanes AS P
            ON P.Id_Plan = TAM.Id_Tarifa
           AND P.Id_Periodo = TAM.Id_Periodo
        WHERE TAM.Id_Modulo = @Id_Modulo
          AND TAM.Id_Periodo = @Id_Periodo
          AND ISNULL(TAM.Activo, 0) = 1
        ORDER BY P.NombrePlan;

        RETURN;
    END

    /* S: lista todos los planes del período e indica su estado para el módulo. */
    IF @Id_Tipo = N'S'
    BEGIN
        IF @Id_Modulo IS NULL OR @Id_Periodo IS NULL
        BEGIN
            RAISERROR('Para listar los planes se requieren Id_Modulo e Id_Periodo.', 16, 1);
            RETURN;
        END

        SELECT
            TAM.Id_TarifaAsignaturaModular,
            P.Id_Plan AS Id_Tarifa,
            P.NombrePlan,
            P.TipoPlan,
            P.ValorOrdinaria,
            P.ValorExtraordinaria,
            P.ValorDescuento,
            P.Id_Periodo,
            ISNULL(TAM.Activo, CONVERT(BIT, 0)) AS Activo,
            CASE
                WHEN TAM.Id_TarifaAsignaturaModular IS NULL THEN 'Disponible'
                WHEN ISNULL(TAM.Activo, 0) = 1 THEN 'Asociado'
                ELSE 'Inactivo'
            END AS Estado
        FROM dbo.AFPlanes AS P
        LEFT JOIN dbo.AFTarifaAsignaturaModular AS TAM
            ON TAM.Id_Tarifa = P.Id_Plan
            AND TAM.Id_Modulo = @Id_Modulo
            AND TAM.Id_Periodo = @Id_Periodo
        WHERE P.Id_Periodo = @Id_Periodo
        ORDER BY P.NombrePlan;

        RETURN;
    END

    /* D: devuelve las cuotas o conceptos del plan seleccionado. */
    IF @Id_Tipo = N'D'
    BEGIN
        IF @Id_Tarifa IS NULL
        BEGIN
            RAISERROR('Para consultar el detalle se requiere Id_Tarifa.', 16, 1);
            RETURN;
        END

        SELECT
            DP.Id_DetallePlan,
            DP.Id_Plan AS Id_Tarifa,
            DP.Concepto,
            DP.Porcentaje,
            DP.Valor,
            DP.ValorExtr,
            DP.FechaPago
        FROM dbo.AFDetallePlan AS DP
        WHERE DP.Id_Plan = @Id_Tarifa
        ORDER BY DP.FechaPago, DP.Id_DetallePlan;

        RETURN;
    END

    /* I: inserta la asociación; si existe pero está inactiva, la reactiva. */
    IF @Id_Tipo = N'I'
    BEGIN
        IF @Id_Modulo IS NULL OR @Id_Tarifa IS NULL OR @Id_Periodo IS NULL
        BEGIN
            RAISERROR('Para asociar se requieren Id_Modulo, Id_Tarifa e Id_Periodo.', 16, 1);
            RETURN;
        END

        IF NOT EXISTS
        (
            SELECT 1
            FROM dbo.AFPlanes
            WHERE Id_Plan = @Id_Tarifa
              AND Id_Periodo = @Id_Periodo
        )
        BEGIN
            RAISERROR('La tarifa seleccionada no pertenece al período indicado.', 16, 1);
            RETURN;
        END

        BEGIN TRY
            BEGIN TRANSACTION;

            UPDATE dbo.AFTarifaAsignaturaModular
            SET Activo = 1,
                Usuario = @Usuario
            WHERE Id_Tarifa = @Id_Tarifa
              AND Id_Modulo = @Id_Modulo
              AND Id_Periodo = @Id_Periodo;

            IF @@ROWCOUNT = 0
            BEGIN
                INSERT INTO dbo.AFTarifaAsignaturaModular
                    (Id_Tarifa, Id_Modulo, Id_Periodo, Activo, Usuario)
                VALUES
                    (@Id_Tarifa, @Id_Modulo, @Id_Periodo, 1, @Usuario);
            END

            COMMIT TRANSACTION;
        END TRY
        BEGIN CATCH
            IF XACT_STATE() <> 0
            BEGIN
                ROLLBACK TRANSACTION;
            END

            DECLARE @MensajeError NVARCHAR(2048) = ERROR_MESSAGE();
            RAISERROR('%s', 16, 1, @MensajeError);
            RETURN;
        END CATCH

        RETURN;
    END

    /* E: conserva el historial y deja de ofrecer el plan para esa asignatura. */
    IF @Id_Tipo = N'E'
    BEGIN
        IF @Id_Modulo IS NULL OR @Id_Tarifa IS NULL OR @Id_Periodo IS NULL
        BEGIN
            RAISERROR('Para desactivar se requieren Id_Modulo, Id_Tarifa e Id_Periodo.', 16, 1);
            RETURN;
        END

        UPDATE dbo.AFTarifaAsignaturaModular
        SET Activo = 0,
            Usuario = @Usuario
        WHERE Id_Tarifa = @Id_Tarifa
          AND Id_Modulo = @Id_Modulo
          AND Id_Periodo = @Id_Periodo;

        RETURN;
    END

    RAISERROR('Tipo no válido. Use H (habilitadas), S (selección), D (detalle), I (insertar/reactivar) o E (desactivar).', 16, 1);
END
GO

/* Prueba de la consulta de planes para el módulo 1330 del período 125.
   EXEC dbo.AFGestionTarifaAsignaturaModular
        @Id_Tipo = N'S',
        @Id_Modulo = 1330,
        @Id_Periodo = 125;
*/

/*
    Recomendado: ejecutar una sola vez, después de revisar que no existan duplicados.

    CREATE UNIQUE NONCLUSTERED INDEX UX_AFTarifaAsignaturaModular_TarifaModuloPeriodo
        ON dbo.AFTarifaAsignaturaModular (Id_Tarifa, Id_Modulo, Id_Periodo);
*/
