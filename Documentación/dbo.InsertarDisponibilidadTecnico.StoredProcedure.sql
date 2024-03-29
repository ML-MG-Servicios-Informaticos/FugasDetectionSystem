/****** Object:  StoredProcedure [dbo].[InsertarDisponibilidadTecnico]    Script Date: 24/03/2024 10:05:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Marlen Leyva
-- Fecha de Creación: 2024-03-12
-- Descripción: Este procedimiento almacenado inserta un nueva disponibilidad de un técnico en la tabla DisponibilidadTecnico.
--              Devuelve 1 si la inserción fue exitosa y 0 si no lo fue.
-- =============================================
CREATE PROCEDURE [dbo].[InsertarDisponibilidadTecnico]
    @TecnicoID INT,
    @Fecha DATE,
    @HoraInicio TIME,
    @HoraFin TIME
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Resultado INT;
    
    BEGIN TRY
        INSERT INTO [dbo].[DisponibilidadTecnico] (TecnicoID, Fecha, HoraInicio, HoraFin)
        VALUES (@TecnicoID, @Fecha, @HoraInicio, @HoraFin);
        
        SET @Resultado = 1; -- Operación exitosa
    END TRY
    BEGIN CATCH
        SET @Resultado = 0; -- Operación fallida
    END CATCH
    
    SELECT @Resultado AS EstadoInsercion;
END
GO
