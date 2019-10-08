--
-- 성능 로그 기록용 테이블
--
DROP TABLE PerformanceActivity
GO

DROP TABLE PerformanceContext
GO

CREATE TABLE PerformanceActivity(
	ActivityId varchar(50) NOT NULL,
	ActivityName nvarchar(128) NOT NULL,
	LogTimestamp datetime NOT NULL,
	ElapsedTime decimal(9, 3) NOT NULL,
	Category nvarchar(32) NOT NULL,
	MachineName nvarchar(50) NULL,
	ProcessId int NULL,
 
CONSTRAINT PK_PerformanceActivity PRIMARY KEY CLUSTERED (
	ActivityId ASC
))
GO

CREATE TABLE PerformanceContext (
	ActivityId varchar(50) NOT NULL,
	ContextId int NOT NULL,
	ContextName nvarchar(128) NOT NULL,
	LogTimestamp datetime NOT NULL,
	InclusiveTime decimal(9, 3) NOT NULL,
	ExclusiveTime decimal(9, 3) NOT NULL,
	ParentContextId int NOT NULL,
CONSTRAINT PK_PerformanceContext PRIMARY KEY CLUSTERED 
(
	ActivityId ASC,
	ContextId ASC
))
GO
