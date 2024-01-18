IF NOT EXISTS (
    SELECT *
    FROM information_schema.tables 
    WHERE table_name = 'Match'
)
BEGIN
    CREATE TABLE Match (
        Id INT PRIMARY KEY IDENTITY(1,1),
        Uuid UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
        HomeTeamId INT FOREIGN KEY REFERENCES Team(Id) ON DELETE NO ACTION ON UPDATE NO ACTION,
        AwayTeamId INT FOREIGN KEY REFERENCES Team(Id) ON DELETE NO ACTION ON UPDATE NO ACTION,
        HomeTeamNormalTimeScore INT NOT NULL DEFAULT 0,
        AwayTeamNormalTimeScore INT NOT NULL DEFAULT 0,
        HomeTeamPenaltyScore INT NULL,
        AwayTeamPenaltyScore INT NULL,
        MatchWinnerId INT NOT NULL,
        ChampionshipStage INT NOT NULL,
        ChampionshipId INT FOREIGN KEY REFERENCES Championship(Id) ON DELETE NO ACTION ON UPDATE NO ACTION,
    );
END;
