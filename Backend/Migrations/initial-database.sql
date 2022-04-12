DROP TABLE [todo]

IF NOT EXISTS( SELECT TOP 1 1 FROM sys.tables WHERE [name] = 'todo' )
BEGIN
	CREATE TABLE [todo] (
		[id_todo] INT IDENTITY(1,1) CONSTRAINT PK_todo PRIMARY KEY,
		[title] NVARCHAR(MAX) NOT NULL,
		[is_done] BIT NOT NULL
	)
END

INSERT INTO [todo] 
VALUES 
	( 'Test', 0 ),
	( 'Test2', 1)

INSERT INTO [todo] VALUES ('test3', 0) SELECT SCOPE_IDENTITY()

UPDATE [todo] SET [title]='test_2', [is_done]=0 WHERE [id_todo] = 20

SELECT * FROM [todo]