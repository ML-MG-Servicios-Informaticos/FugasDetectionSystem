/****** Object:  Table [dbo].[Pagos]    Script Date: 24/03/2024 10:05:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pagos](
	[PagoID] [int] IDENTITY(1,1) NOT NULL,
	[ServicioID] [int] NULL,
	[Fecha] [date] NULL,
	[Monto] [decimal](10, 2) NULL,
	[MetodoPago] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[PagoID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Pagos]  WITH CHECK ADD FOREIGN KEY([ServicioID])
REFERENCES [dbo].[Servicios] ([ServicioID])
GO
