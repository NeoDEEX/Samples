--
-- DB Profile 기록용 테이블
--
CREATE TABLE DbProfile
(
	LogId			varchar(42)		NOT NULL,
	LogTimestamp	DateTime		NOT NULL,
	UserId			varchar(16),
	FoxQueryId		varchar(256),
	ExecutionType	varchar(32),
	ExecutionTime	Numeric(12, 3),
	QueryParameters	nvarchar(2000),
	QueryText		nvarchar(4000),
	ResultString	nvarchar(256),
	InlineQuery		nvarchar(4000),
	CallerName		nvarchar(256),
	ExceptionType	nvarchar(256),
	ErrorCode		INT,
	ErrorMessage	nvarchar(2000)
	CONSTRAINT "PK_LogId" PRIMARY KEY  CLUSTERED 
	(
		LogId
	)
)
GO

CREATE PROCEDURE InsertDbProfile(
	@LogId				VARCHAR(42),
	@LogTimestamp		DATETIME,
	@UserId				VARCHAR(16),
	@FoxQueryId			VARCHAR(256),
	@ExecutionType		VARCHAR(32),
	@ExecutionTime		NUMERIC(12, 3),
	@QueryParameters	NVARCHAR(2000),
	@QueryText			NVARCHAR(4000),
	@ResultString		NVARCHAR(256),
	@InlineQuery		NVARCHAR(4000),
	@CallerName			NVARCHAR(256),
	@ExceptionType		NVARCHAR(256),
	@ErrorCode			INT,
	@ErrorMessage		NVARCHAR(2000)
)
AS
BEGIN
        INSERT INTO DbProfile (LogId, LogTimestamp, UserId, FoxQueryId, ExecutionType, ExecutionTime, QueryParameters, QueryText, ResultString, InlineQuery, CallerName, ExceptionType, ErrorCode, ErrorMessage)
        VALUES (@LogId, @LogTimestamp, @UserId, @FoxQueryId, @ExecutionType, @ExecutionTime, @QueryParameters, @QueryText, @ResultString, @InlineQuery, @CallerName, @ExceptionType, @ErrorCode, @ErrorMessage)
END
GO