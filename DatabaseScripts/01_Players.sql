USE [LimitBreakPugs]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Players](
	[ID] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Battle_Tag] [nvarchar](50) NULL,
	[Discord_Tag] [nvarchar](50) NULL,
	[Player_Rank] [int] NULL,
	[Player_Team] [nvarchar](50) NULL,
	[Created_On] [datetime] NULL,
	[Modified_On] [datetime] NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Players] ADD  CONSTRAINT [DF_Players_ID]  DEFAULT (newid()) FOR [ID]
GO

ALTER TABLE [dbo].[Players] ADD  CONSTRAINT [DF_Players_Created_On]  DEFAULT (getdate()) FOR [Created_On]
GO


