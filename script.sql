USE [superheroisdb]
GO
/****** Object:  Table [dbo].[Herois]    Script Date: 19/01/2024 05:43:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Herois](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](120) NOT NULL,
	[NomeHeroi] [nvarchar](120) NOT NULL,
	[DataNascimento] [datetime2](7) NULL,
	[Altura] [float] NOT NULL,
	[Peso] [float] NOT NULL,
 CONSTRAINT [PK_Herois] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HeroiSuperPoderes]    Script Date: 19/01/2024 05:43:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HeroiSuperPoderes](
	[HeroiId] [int] NOT NULL,
	[SuperPoderesId] [int] NOT NULL,
 CONSTRAINT [PK_HeroiSuperPoderes] PRIMARY KEY CLUSTERED 
(
	[HeroiId] ASC,
	[SuperPoderesId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SuperPoderes]    Script Date: 19/01/2024 05:43:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SuperPoderes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SuperPoder] [nvarchar](50) NOT NULL,
	[Descricao] [nvarchar](250) NULL,
 CONSTRAINT [PK_SuperPoderes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[HeroiSuperPoderes]  WITH CHECK ADD  CONSTRAINT [FK_HeroiSuperPoderes_Herois_HeroiId] FOREIGN KEY([HeroiId])
REFERENCES [dbo].[Herois] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[HeroiSuperPoderes] CHECK CONSTRAINT [FK_HeroiSuperPoderes_Herois_HeroiId]
GO
ALTER TABLE [dbo].[HeroiSuperPoderes]  WITH CHECK ADD  CONSTRAINT [FK_HeroiSuperPoderes_SuperPoderes_SuperPoderesId] FOREIGN KEY([SuperPoderesId])
REFERENCES [dbo].[SuperPoderes] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[HeroiSuperPoderes] CHECK CONSTRAINT [FK_HeroiSuperPoderes_SuperPoderes_SuperPoderesId]
GO
