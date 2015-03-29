CREATE PROCEDURE [dbo].[read_message]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @RecvReqDlgHandle UNIQUEIDENTIFIER;
	DECLARE @RecvReqMsg NVARCHAR(4000);
	DECLARE @RecvReqMsgName sysname;

	BEGIN TRANSACTION;
	RECEIVE TOP(1)
		@RecvReqDlgHandle = conversation_handle,
		@RecvReqMsg = message_body,
		@RecvReqMsgName = message_type_name
	FROM DestinationQueue;

	IF @RecvReqDlgHandle IS NOT NULL
		END CONVERSATION @RecvReqDlgHandle;

	SELECT @RecvReqMsg AS ReceivedRequestMsg;

	COMMIT TRANSACTION;

END