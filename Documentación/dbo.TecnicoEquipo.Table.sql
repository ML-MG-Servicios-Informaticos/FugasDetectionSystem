/****** Object:  Table [dbo].[TecnicoEquipo]    Script Date: 24/03/2024 10:05:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TecnicoEquipo](
	[TecnicoID] [int] NOT NULL,
	[EquipoID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TecnicoID] ASC,
	[EquipoID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TecnicoEquipo]  WITH CHECK ADD FOREIGN KEY([EquipoID])
REFERENCES [dbo].[Equipos] ([EquipoID])
GO
ALTER TABLE [dbo].[TecnicoEquipo]  WITH CHECK ADD FOREIGN KEY([TecnicoID])
REFERENCES [dbo].[Tecnicos] ([TecnicoID])
GO
