/****** Object:  Table [dbo].[Tecnicos]    Script Date: 24/03/2024 10:05:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tecnicos](
	[TecnicoID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](100) NULL,
	[Apellido] [nvarchar](100) NULL,
	[Especialidad] [nvarchar](100) NULL,
	[CorreoElectronico] [nvarchar](255) NULL,
	[Telefono] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[TecnicoID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
