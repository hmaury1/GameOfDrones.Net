USE [SamApp]
GO

DROP TABLE IF EXISTS [dbo].[rounds];
GO
DROP TABLE IF EXISTS [dbo].[games];
GO
DROP TABLE IF EXISTS [dbo].[moves];
GO
DROP TABLE IF EXISTS [dbo].[players];
GO

CREATE TABLE [dbo].[players](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](MAX) NULL
 CONSTRAINT [PK_players] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[moves](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](MAX) NOT NULL,
	[beatMoveId] [int] NULL,
	[iconClass] [varchar](MAX) NULL
 CONSTRAINT [PK_moves] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[moves]  WITH CHECK ADD  CONSTRAINT [FK_moves_beat_moves] FOREIGN KEY([beatMoveId])
REFERENCES [dbo].[moves] ([id])
GO

CREATE TABLE [dbo].[games](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[playerOneId] [int] NOT NULL,
	[playerTwoId] [int] NOT NULL,
	[gameWinnerId] [int] NULL,
	[createdOn] [datetime] NULL DEFAULT (getdate())
 CONSTRAINT [PK_games] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[games]  WITH CHECK ADD  CONSTRAINT [FK_players_games_players1] FOREIGN KEY([playerOneId])
REFERENCES [dbo].[players] ([id])
GO
ALTER TABLE [dbo].[games]  WITH CHECK ADD  CONSTRAINT [FK_players_games_players2] FOREIGN KEY([playerTwoId])
REFERENCES [dbo].[players] ([id])
GO
ALTER TABLE [dbo].[games]  WITH CHECK ADD  CONSTRAINT [FK_players_games_winner] FOREIGN KEY([gameWinnerId])
REFERENCES [dbo].[players] ([id])
GO

CREATE TABLE [dbo].[rounds](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[gameId] [int] NOT NULL,
	[roundWinnerId] [int] NULL,
	[playerOneMoveId] [int] NOT NULL,
	[playerTwoMoveId] [int] NOT NULL
 CONSTRAINT [PK_rounds] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[rounds]  WITH CHECK ADD  CONSTRAINT [FK_games_rounds_games] FOREIGN KEY([gameId])
REFERENCES [dbo].[games] ([id])
GO
ALTER TABLE [dbo].[rounds]  WITH CHECK ADD  CONSTRAINT [FK_players_rounds_players_winner] FOREIGN KEY([roundWinnerId])
REFERENCES [dbo].[players] ([id])
GO
ALTER TABLE [dbo].[rounds]  WITH CHECK ADD  CONSTRAINT [FK_moves_games_moves1] FOREIGN KEY([playerOneMoveId])
REFERENCES [dbo].[moves] ([id])
GO
ALTER TABLE [dbo].[rounds]  WITH CHECK ADD  CONSTRAINT [FK_moves_games_moves2] FOREIGN KEY([playerTwoMoveId])
REFERENCES [dbo].[moves] ([id])
GO

INSERT INTO [dbo].[moves] ([name] ,[beatMoveId] ,[iconClass]) VALUES
('Paper' ,NULL ,'hand-paper'),
('Rock' ,NULL ,'hand-rock'),
('Scissors' ,NULL ,'hand-scissors');
GO

UPDATE [dbo].[moves] SET [beatMoveId] = (SELECT TOP 1 id FROM [dbo].[moves] WHERE name = 'Rock') WHERE name = 'Paper';
GO
UPDATE [dbo].[moves] SET [beatMoveId] = (SELECT TOP 1 id FROM [dbo].[moves] WHERE name = 'Scissors') WHERE name = 'Rock';
GO
UPDATE [dbo].[moves] SET [beatMoveId] = (SELECT TOP 1 id FROM [dbo].[moves] WHERE name = 'Paper') WHERE name = 'Scissors';
GO