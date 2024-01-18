IF NOT EXISTS (
    SELECT *
    FROM information_schema.tables 
    WHERE table_schema = 'auth' 
    AND table_name = 'Users'
)
BEGIN
    CREATE TABLE auth.Users (
        Id INT PRIMARY KEY IDENTITY(1,1),
        Uuid UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
        Username VARCHAR(45) NOT NULL,
        UserPass VARCHAR(255) NOT NULL
    );
END;
