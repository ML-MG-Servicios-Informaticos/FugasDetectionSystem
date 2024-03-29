/****** Object:  StoredProcedure [dbo].[ActualizarDisponibilidadTecnico]    Script Date: 24/03/2024 10:05:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Marlen Leyva
-- Fecha de Creación: 2024-03-12
-- Descripción: Este procedimiento almacenado actualiza la disponibilidad de un técnico 
--              en la tabla DisponibilidadTecnico.
--              Devuelve 1 si la actualización fue exitosa y 0 si no lo fue.
-- =============================================
CREATE PROCEDURE [dbo].[ActualizarDisponibilidadTecnico]
    @DisponibilidadID INT,
    @TecnicoID INT,
    @Fecha DATE,
    @HoraInicio TIME,
    @HoraFin TIME
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Resultado INT;
    
    BEGIN TRY
        UPDATE [dbo].[DisponibilidadTecnico]
        SET TecnicoID = @TecnicoID,
		    Fecha = @Fecha,
			HoraInicio = @HoraInicio, 
			HoraFin = @HoraFin
        WHERE DisponibilidadID = @DisponibilidadID;
        
        -- Comprueba si se afectó alguna fila
        IF @@ROWCOUNT > 0
            SET @Resultado = 1; -- Actualización exitosa
        ELSE
            SET @Resultado = 0; -- Ninguna fila afectada, actualización fallida
    END TRY
    BEGIN CATCH
        SET @Resultado = 0; -- Error durante la actualización
    END CATCH
    
    SELECT @Resultado AS EstadoActualizacion;
END;
GO
