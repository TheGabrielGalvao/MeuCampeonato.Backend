IF NOT EXISTS (
    SELECT *
    FROM information_schema.tables 
    WHERE table_name = 'Team'
)
BEGIN
    CREATE TABLE Team (
        Id INT PRIMARY KEY IDENTITY(1,1),
        Uuid UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
        Name VARCHAR(45) NOT NULL
    );
END;
