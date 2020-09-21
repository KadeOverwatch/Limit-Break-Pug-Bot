﻿USE [LimitBreakPugs]
GO

/****** Object:  Trigger [dbo].[tgr_player_modified]    Script Date: 9/21/2020 11:23:20 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TRIGGER [dbo].[tgr_player_modified]
ON [dbo].[Players]
AFTER UPDATE AS
	UPDATE dbo.Players
	SET Modified_On = GETDATE()
	WHERE ID IN (SELECT DISTINCT ID FROM inserted)
GO

ALTER TABLE [dbo].[Players] ENABLE TRIGGER [tgr_player_modified]
GO


