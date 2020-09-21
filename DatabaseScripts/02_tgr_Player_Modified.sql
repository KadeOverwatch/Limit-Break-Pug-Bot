USE [LimitBreakPugs]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER TRIGGER [dbo].[tgr_player_modified]
ON [dbo].[Players]
AFTER UPDATE AS
	UPDATE dbo.Players
	SET Modified_On = GETDATE()
	WHERE ID IN (SELECT DISTINCT ID FROM inserted)