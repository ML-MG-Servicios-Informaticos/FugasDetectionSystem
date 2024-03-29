/****** Object:  Table [dbo].[Visitas]    Script Date: 24/03/2024 10:05:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Visitas](
	[VisitaID] [int] IDENTITY(1,1) NOT NULL,
	[TecnicoID] [int] NULL,
	[ClienteID] [int] NULL,
	[FechaHora] [datetime] NULL,
	[Direccion] [text] NULL,
PRIMARY KEY CLUSTERED 
(
	[VisitaID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Visitas]  WITH CHECK ADD FOREIGN KEY([ClienteID])
REFERENCES [dbo].[Clientes] ([ClienteID])
GO
ALTER TABLE [dbo].[Visitas]  WITH CHECK ADD FOREIGN KEY([TecnicoID])
REFERENCES [dbo].[Tecnicos] ([TecnicoID])
GO
