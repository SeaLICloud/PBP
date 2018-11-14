CREATE TRIGGER [dbo].[Accounts_UPDATE] ON [dbo].[Accounts]
    AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
                  
    IF ((SELECT TRIGGER_NESTLEVEL()) > 1) RETURN;
    
    DECLARE @Id uniqueidentifier
        
    SELECT @Id = INSERTED.Guid
    FROM INSERTED
          
    UPDATE dbo.Accounts
    SET UpdateTime = GETDATE()
    WHERE Guid = @Id
END