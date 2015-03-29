CREATE PROCEDURE write_message 
	@message nvarchar(4000)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @InitDlgHandle UNIQUEIDENTIFIER;

	BEGIN TRANSACTION;
	BEGIN DIALOG @InitDlgHandle
		 FROM SERVICE
		  [//acme.com/ServiceBrokerIntegration/SourceService]
		 TO SERVICE
		  N'//acme.com/ServiceBrokerIntegration/DestinationService'
		 ON CONTRACT
		  [//acme.com/ServiceBrokerIntegration/Contract]
		 WITH
			 ENCRYPTION = OFF;

	SEND ON CONVERSATION @InitDlgHandle
		 MESSAGE TYPE 
		 [//acme.com/ServiceBrokerIntegration/RequestMessage]
		 (@message);

	SELECT @message AS SentRequestMsg;

	COMMIT TRANSACTION;

END