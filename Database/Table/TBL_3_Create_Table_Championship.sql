IF NOT EXISTS (
    SELECT *
    FROM information_schema.tables 
    WHERE table_name = 'Championship'
)
BEGIN
    CREATE TABLE Championship (
        Id INT PRIMARY KEY IDENTITY(1,1),
        Uuid UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
        Name VARCHAR(45) NOT NULL,
        UserId INT FOREIGN KEY REFERENCES [auth].Users(Id) ON DELETE NO ACTION ON UPDATE NO ACTION
    );
END;
