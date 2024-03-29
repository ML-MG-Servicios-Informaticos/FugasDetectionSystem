/****** Object:  StoredProcedure [dbo].[ConsultarDisponibilidadesTecnico]    Script Date: 24/03/2024 10:05:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Marlen Leyva
-- Fecha de Creación: 2024-03-12
-- Descripción: Este procedimiento almacenado consulta y devuelve todos los registros de la tabla DisponibilidadTecnico.
-- =============================================
CREATE PROCEDURE [dbo].[ConsultarDisponibilidadesTecnico]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT DisponibilidadID, TecnicoID, Fecha, HoraInicio, HoraFin
    FROM [dbo].[DisponibilidadTecnico];
END;
GO
