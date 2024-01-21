IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.VIEWS WHERE TABLE_NAME = 'VW_ChampionshipDetails')
BEGIN
    DROP VIEW VW_ChampionshipDetails
END;
GO
CREATE VIEW VW_ChampionshipDetails AS (
        SELECT 
             U.Uuid AS UserUuid,
             U.Username,
             CH.Uuid AS ChampionshipUuid, 
             CH.Name, 
             M.Uuid AS MatchUuid,
             M.ChampionshipStage,
             HT.Uuid AS HomeTeamUuid,
             HT.Name AS HomeTeamName,
             M.HomeTeamNormalTimeScore,
             M.HomeTeamPenaltyScore,
             AWT.Uuid AS AwayTeamUuid,
             AWT.Name AS AwayTeamName,
             M.AwayTeamNormalTimeScore,
             M.AwayTeamPenaltyScore,
		     MW.Uuid AS MatchWinnerUuid,
             MW.Name AS MatchWinnerName
     
         FROM Championship CH 
             INNER JOIN Match M ON M.ChampionshipId = CH.Id
             INNER JOIN Team HT ON HT.Id = M.HomeTeamId
             INNER JOIN Team AWT ON AWT.Id = M.AwayTeamId
             INNER JOIN Team MW ON MW.Id = M.MatchWinnerId
             INNER JOIN AUTH.Users U ON U.Id = CH.UserId
    );
