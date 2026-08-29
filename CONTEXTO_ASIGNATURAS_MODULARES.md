# Contexto: asignación financiera de asignaturas modulares

Fecha de consolidación: 29 de agosto de 2026  
Proyecto: `AsignaturasModularizadas`  
Base de datos de trabajo: `DBSIIE_2025A`

## Objetivo funcional

El sistema vende y matrícula **asignaturas modulares individuales**. El valor no se asigna directamente a la asignatura ni al programa: proviene de un **plan de pago** creado previamente en `AFPlanes`.

La relación que habilita un plan para una asignatura modular se guarda en `AFTarifaAsignaturaModular`. Así, una misma asignatura puede ofrecer varios planes (contado, cuotas, cortes, descuentos, entre otros) durante un período.

El programa se usa para localizar las asignaturas ofertadas y contextualizar la matrícula, pero no es el elemento al que se le asigna el valor.

## Principios acordados

1. Contabilidad configura los planes y habilita cuáles aplican a cada asignatura modular.
2. El precio oficial viene de `AFPlanes` y sus cuotas de `AFDetallePlan`.
3. `PrecioAleatorio` no interviene en la liquidación financiera y se oculta de la grilla de asignaturas ofertadas.
4. La asignación financiera debe crear la fila de `CALAsignaturaEnCurso` en la misma operación.
5. Si el estudiante no tiene plan financiero, se crea `AFPlanEstudiante` al confirmar la primera asignatura y tarifa.
6. Una matrícula modular (`GEMatricula`) es un requisito académico previo; no equivale a tener un plan financiero.

## Tablas del flujo

### Planes y cuotas

| Tabla | Propósito | Campos relevantes |
|---|---|---|
| `AFPlanes` | Maestro de planes de pago. | `Id_Plan`, `NombrePlan`, `TipoPlan`, `ValorOrdinaria`, `ValorExtraordinaria`, `ValorDescuento`, `Id_Periodo`, `FechaPago`. |
| `AFDetallePlan` | Cuotas, porcentajes y fechas de corte de un plan. | `Id_DetallePlan`, `Id_Plan`, `Concepto`, `Porcentaje`, `Valor`, `ValorExtr`, `FechaPago`. |
| `AFTarifas` | Tarifas históricas o por programa/nivel. No debe confundirse su `Id_Tarifa` con el identificador de `AFPlanes`. | `Id_Tarifa`, valores, `Id_Programa`, `Id_Periodo`, `Id_Plan`. |

### Habilitación de planes para módulos

`AFTarifaAsignaturaModular` contiene la configuración comercial por asignatura y período:

| Campo | Uso |
|---|---|
| `Id_TarifaAsignaturaModular` | Llave primaria de la relación. Debe generarse automáticamente, por `IDENTITY` o por una secuencia/default. |
| `Id_Tarifa` | En el diseño actual, guarda el **`AFPlanes.Id_Plan`**. Aunque su nombre dice tarifa, el procedimiento la trata como identificador de plan. |
| `Id_Modulo` | `Id_AsignaturaPlan` de la asignatura modular ofertada. |
| `Id_Periodo` | Período para el cual se habilita. |
| `Activo` | Indica si la asociación está disponible. |
| `Usuario` | Auditoría de la configuración. |

Recomendación: mantener una sola asociación por combinación `Id_Tarifa`, `Id_Modulo`, `Id_Periodo`.

> Importante: en una fila de `AFTarifas`, `Id_Tarifa = 2831` y `Id_Plan = 541` son identificadores diferentes. Para el procedimiento actual se envía `541`, porque este busca `AFPlanes.Id_Plan`.

### Matrícula académica y financiera del estudiante

| Tabla | Propósito | Campos relevantes |
|---|---|---|
| `GEEstudiante` | Identificación del estudiante. | `Id_Estudiante`, `Id_DocEstudiante`, nombres y apellidos. |
| `GEMatricula` | Matrícula académica. | `Id_Matricula`, `Id_Estudiante`, `Id_Periodo`, `Nivel`, `FechadeMatricula`, `EsModular`. |
| `CALAsignaturaEnCurso` | Matrícula del estudiante en una asignatura ofertada. | `Id_Matricula`, `Nivel`, `Id_Programa`, `Id_AsignaturaPlan`, `Id_Docente`, `Id_Periodo`. |
| `AFPlanEstudiante` | Encabezado financiero por estudiante, plan, programa y período. | `Id_PlanEstudiante`, `Id_Plan`, `Id_Programa`, `Semestre`, `Fecha`, `Id_Estudiante`, `Id_Periodo`. |
| `AFPlanEstudianteModulo` | Detalle financiero de cada asignatura modular asignada. | `Id_PlanEstudianteModulo`, `Id_PlanEstudiante`, `Id_Modulo`, `Id_TarifaAsignaturaModular`, valores, `Estado`, `FechaRegistro`, `Usuario`, `Id_AsigCurso`. |
| `AFDetallePlanEstudiante` | Copia de las cuotas exigibles al estudiante. | `Id_DetallePlanEst`, `Id_PlanEstudiante`, `Concepto`, `FechaPago`, `Valor`; se recomienda agregar la referencia al detalle modular y al detalle fuente del plan. |

La estructura existente de `AFPlanEstudianteModulo` usa los nombres **`Estado`** y **`FechaRegistro`**. El procedimiento no debe usar `Activo` ni `Fecha` para esta tabla.

## Flujo completo

### A. Preparación académica

1. El programa y la asignatura deben estar ofertados: la consulta `HOHorariosModular` devuelve el `Id_AsignaturaPlan`, docente, horario, fechas y nivel.
2. Se crea o valida la matrícula modular del estudiante en `GEMatricula`:
   - `Id_Estudiante`
   - `Id_Periodo`
   - `Nivel` correspondiente a la asignatura a matricular
   - `FechadeMatricula`
   - `EsModular = 1`
3. La fila de `GEMatricula` genera el `Id_Matricula` que usará `CALAsignaturaEnCurso`.

No se debe crear una asignatura nueva desde el formulario financiero: allí se selecciona una asignatura ya ofertada. Tampoco se recomienda que contabilidad cree automáticamente una matrícula académica, pues es una decisión propia del proceso académico.

### B. Preparación comercial

1. Crear el plan en `AFPlanes` para el período.
2. Crear sus cuotas y cortes en `AFDetallePlan`.
3. Habilitar el plan para la asignatura con `AFGestionTarifaAsignaturaModular`, tipo `I`.
4. Validar la configuración con el tipo `H`.

Ejemplo de habilitación:

```sql
EXEC dbo.AFGestionTarifaAsignaturaModular
    @Id_Tipo = N'I',
    @Id_Modulo = <Id_AsignaturaPlan>,
    @Id_Tarifa = <AFPlanes.Id_Plan>,
    @Id_Periodo = 125,
    @Usuario = N'CONTABILIDAD';
```

### C. Asignación al estudiante

1. Contabilidad digita el documento del estudiante.
2. El sistema resuelve `GEEstudiante.Id_Estudiante` y la matrícula modular del período.
3. Se seleccionan programa, asignatura ofertada y un plan habilitado.
4. El sistema muestra las cuotas y el total del plan.
5. Al confirmar, se valida la relación plan–módulo activa y se ejecuta una transacción:
   - Reutiliza o crea `AFPlanEstudiante`.
   - Inserta `AFPlanEstudianteModulo`.
   - Copia `AFDetallePlan` a `AFDetallePlanEstudiante`.
   - Reutiliza o crea `CALAsignaturaEnCurso`.

Si el estudiante no tiene plan, la grilla de asignaciones puede estar vacía: esto es correcto. Lo que bloquea la operación es que no exista una matrícula modular activa para el período.

## Procedimiento `AFGestionTarifaAsignaturaModular`

Firma esperada:

```sql
@Id_Tipo NVARCHAR(2),
@Id_Modulo INT = NULL,
@Id_Tarifa INT = NULL,
@Id_Periodo INT = NULL,
@Usuario NVARCHAR(100) = NULL
```

| Tipo | Acción | Parámetros requeridos |
|---|---|---|
| `H` | Consulta planes activos habilitados para una asignatura. Lo usa el formulario financiero. | `Id_Modulo`, `Id_Periodo` |
| `S` | Lista todos los planes del período e informa si están disponibles, asociados o inactivos. | `Id_Modulo`, `Id_Periodo` |
| `D` | Consulta cuotas y fechas del plan. | `Id_Tarifa` |
| `I` | Inserta o reactiva una relación plan–asignatura. | `Id_Modulo`, `Id_Tarifa`, `Id_Periodo` |
| `E` | Desactiva una relación, sin borrar historial. | `Id_Modulo`, `Id_Tarifa`, `Id_Periodo` |

Consulta de validación:

```sql
EXEC dbo.AFGestionTarifaAsignaturaModular
    @Id_Tipo = N'H',
    @Id_Modulo = <Id_AsignaturaPlan>,
    @Id_Periodo = 125;
```

## Procedimiento `AFMatriculaAsignaturaModular`

Firma esperada:

```sql
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
```

| Tipo | Acción | Parámetros requeridos |
|---|---|---|
| `B` | Busca estudiante por documento y su matrícula modular del período. | `Id_DocEstudiante`, `Id_Periodo` |
| `S` | Lista las asignaturas financieras activas del estudiante. | `Id_Estudiante`, `Id_Periodo`; `Id_Programa` opcional |
| `I` | Asigna tarifa y crea los registros académicos/financieros en una transacción. | Estudiante, matrícula, programa, módulo, relación tarifa–módulo, plan, período y docente |

En este procedimiento, el tipo `S` consulta por estudiante y período; **no** debe requerir `Id_Modulo`.

## Formulario `FrmAsignacionFinancieraModular`

Ubicación: `AsignaturasModularizadas/FrmAsignacionFinancieraModular.cs`.

Componentes principales:

- Búsqueda por número de documento.
- Grilla de programas.
- Grilla de asignaturas modulares ofertadas.
- Grilla de planes habilitados para la asignatura.
- Grilla de cuotas y fechas de corte.
- Grilla de asignaturas financieras ya asignadas al estudiante.
- Resumen de cantidad de asignaturas y total.

La grilla de asignaturas oculta `Id_AsignaturaPlan`, `Id_Docente` y `PrecioAleatorio`. El identificador y docente permanecen disponibles internamente para realizar la asignación, pero no se muestran al usuario.

## Incidencias registradas y corrección

| Mensaje o síntoma | Causa | Corrección |
|---|---|---|
| `@Id_Estudiante no es un parámetro` | Se desplegó una versión anterior del procedimiento financiero. | Actualizar la firma de `AFMatriculaAsignaturaModular`. |
| `Se requieren Id_Modulo e Id_Periodo` al usar el tipo `S` financiero | Se copió al procedimiento financiero una lógica propia de tarifas. | El tipo `S` de `AFMatriculaAsignaturaModular` debe requerir estudiante y período. |
| Columnas `Activo` o `Fecha` inválidas | La tabla real usa `Estado` y `FechaRegistro`; el encabezado de plan no tiene `Activo`. | Adaptar el procedimiento a los nombres reales. |
| Estudiante encontrado sin matrícula modular | No existe `GEMatricula` con `EsModular = 1` para ese estudiante y período. | Completar matrícula académica antes de asignar tarifa. |
| `La tarifa no pertenece al período indicado` | Se envió `AFTarifas.Id_Tarifa` cuando el procedimiento busca `AFPlanes.Id_Plan`. | Enviar `Id_Plan` de `AFPlanes`. |
| No se puede insertar `NULL` en `Id_TarifaAsignaturaModular` | La llave de la tabla no es `IDENTITY` ni posee default. | Configurar `IDENTITY` al crear la tabla o una secuencia con `DEFAULT (NEXT VALUE FOR ...)`. |
| Grilla de planes habilitados vacía | No hay una relación activa módulo–plan para el período. | Ejecutar tipo `I` de `AFGestionTarifaAsignaturaModular` y verificar con tipo `H`. |

## Pendientes técnicos antes de pasar a producción

1. Desplegar ambos procedimientos en la misma base que usa la aplicación.
2. Resolver la generación automática de `AFTarifaAsignaturaModular.Id_TarifaAsignaturaModular`.
3. Confirmar que `HOHorariosModular` devuelve los campos `Id_AsignaturaPlan` e `Id_Docente`; el formulario necesita ambos para crear `CALAsignaturaEnCurso`.
4. Verificar que `AFDetallePlanEstudiante` incluya `Id_PlanEstudianteModulo` e `Id_DetallePlan` si se requiere trazabilidad completa de las cuotas copiadas.
5. Probar los escenarios: estudiante sin plan, estudiante con plan existente, módulo sin tarifa, tarifa inactiva, asignatura ya asignada y estudiante sin matrícula modular.
6. Definir la regla para estudiantes que cursan módulos de distintos niveles en el mismo período. `GEMatricula` guarda un único `Nivel` por registro; la selección debe identificar la matrícula correspondiente al nivel de la asignatura.

## Archivos relevantes

- `BaseDatos/AFGestionTarifaAsignaturaModular.sql`
- `BaseDatos/AFMatriculaAsignaturaModular.sql`
- `AsignaturasModularizadas/FrmAsignacionFinancieraModular.cs`
- `AsignaturasModularizadas/FrmAsignacionFinancieraModular.Designer.cs`
- `ANALISIS_PROYECTO.md`

