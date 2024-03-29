/****** Object:  Table [dbo].[Interacciones]    Script Date: 24/03/2024 10:05:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Interacciones](
	[InteraccionID] [int] IDENTITY(1,1) NOT NULL,
	[ClienteID] [int] NULL,
	[CanalID] [int] NULL,
	[FechaHora] [datetime] NULL,
	[Mensaje] [text] NULL,
	[Tipo] [nvarchar](50) NULL,
	[AtendidoPor] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[InteraccionID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Interacciones]  WITH CHECK ADD FOREIGN KEY([AtendidoPor])
REFERENCES [dbo].[Operadores] ([OperadorID])
GO
ALTER TABLE [dbo].[Interacciones]  WITH CHECK ADD FOREIGN KEY([CanalID])
REFERENCES [dbo].[CanalesDeComunicacion] ([CanalID])
GO
ALTER TABLE [dbo].[Interacciones]  WITH CHECK ADD FOREIGN KEY([ClienteID])
REFERENCES [dbo].[Clientes] ([ClienteID])
GO
