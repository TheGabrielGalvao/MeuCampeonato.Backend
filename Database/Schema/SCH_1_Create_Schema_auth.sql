IF NOT EXISTS (
    SELECT schema_name
    FROM information_schema.schemata
    WHERE schema_name = 'auth'
)
BEGIN
    EXEC('CREATE SCHEMA auth');
END;
