/****** Object:  StoredProcedure [dbo].[ConsultarDisponibilidadTecnicoPorID]    Script Date: 24/03/2024 10:05:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Marlen Leyva
-- Fecha de Creación: 2024-03-12
-- Descripción: Este procedimiento almacenado consulta y devuelve los datos de una disponibilidad
--              específica en la tabla DisponibilidadTecnico, basándose en el DisponibilidadID
--              proporcionado como parámetro.
-- =============================================
CREATE PROCEDURE [dbo].[ConsultarDisponibilidadTecnicoPorID]
    @DisponibilidadID INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT DisponibilidadID, TecnicoID, Fecha, HoraInicio, HoraFin
    FROM [dbo].[DisponibilidadTecnico]
    WHERE DisponibilidadID = @DisponibilidadID;
END;
GO
