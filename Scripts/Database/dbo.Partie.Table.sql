USE [Akina_peintures]
GO
/****** Object:  Table [dbo].[Partie]    Script Date: 27/04/2017 22:47:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Partie](
	[ID_P] [int] IDENTITY(1,1) NOT NULL,
	[ID_Question] [int] NOT NULL,
	[ID_Reponse] [int] NOT NULL,
	[ID_Oeuvre] [int] NOT NULL,
 CONSTRAINT [PK_Partie] PRIMARY KEY CLUSTERED 
(
	[ID_P] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Partie]  WITH CHECK ADD  CONSTRAINT [FK_Partie_Oeuvre] FOREIGN KEY([ID_Oeuvre])
REFERENCES [dbo].[Oeuvre] ([ID_O])
GO
ALTER TABLE [dbo].[Partie] CHECK CONSTRAINT [FK_Partie_Oeuvre]
GO
ALTER TABLE [dbo].[Partie]  WITH CHECK ADD  CONSTRAINT [FK_Partie_Question] FOREIGN KEY([ID_Question])
REFERENCES [dbo].[Question] ([ID_Q])
GO
ALTER TABLE [dbo].[Partie] CHECK CONSTRAINT [FK_Partie_Question]
GO
