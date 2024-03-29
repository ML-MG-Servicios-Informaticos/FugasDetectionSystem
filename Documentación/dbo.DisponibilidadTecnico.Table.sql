/****** Object:  Table [dbo].[DisponibilidadTecnico]    Script Date: 24/03/2024 10:05:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DisponibilidadTecnico](
	[DisponibilidadID] [int] IDENTITY(1,1) NOT NULL,
	[TecnicoID] [int] NULL,
	[DiaSemana] [tinyint] NULL,
	[HoraInicio] [time](7) NULL,
	[HoraFin] [time](7) NULL,
 CONSTRAINT [PK__Disponib__0300F3D4EE208946] PRIMARY KEY CLUSTERED 
(
	[DisponibilidadID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[DisponibilidadTecnico]  WITH CHECK ADD  CONSTRAINT [FK__Disponibi__Tecni__72C60C4A] FOREIGN KEY([TecnicoID])
REFERENCES [dbo].[Tecnicos] ([TecnicoID])
GO
ALTER TABLE [dbo].[DisponibilidadTecnico] CHECK CONSTRAINT [FK__Disponibi__Tecni__72C60C4A]
GO
