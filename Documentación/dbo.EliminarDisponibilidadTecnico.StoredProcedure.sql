/****** Object:  StoredProcedure [dbo].[EliminarDisponibilidadTecnico]    Script Date: 24/03/2024 10:05:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Marlen Leyva
-- Fecha de Creación: 2024-03-12
-- Descripción: Este procedimiento almacenado elimina una entrada específica de la tabla DisponibilidadTecnico
--              basándose en el DisponibilidadID proporcionado. Devuelve 1 si la eliminación fue exitosa
--              y 0 si no lo fue.
-- =============================================
CREATE PROCEDURE [dbo].[EliminarDisponibilidadTecnico]
    @DisponibilidadID INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Resultado INT;
    
    BEGIN TRY
        DELETE FROM [dbo].[DisponibilidadTecnico]
        WHERE DisponibilidadID = @DisponibilidadID;
        
        -- Comprueba si se afectó alguna fila
        IF @@ROWCOUNT > 0
            SET @Resultado = 1; -- Eliminación exitosa
        ELSE
            SET @Resultado = 0; -- Ninguna fila afectada, eliminación fallida
    END TRY
    BEGIN CATCH
        SET @Resultado = 0; -- Error durante la eliminación
    END CATCH
    
    SELECT @Resultado AS EstadoEliminacion;
END;
GO
