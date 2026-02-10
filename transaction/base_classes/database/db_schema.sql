-- SQL Server용으로 변환한 메모 테이블 스키마 및 샘플 데이터 (트리거 없음)

IF OBJECT_ID('dbo.memo_data', 'U') IS NOT NULL
    DROP TABLE dbo.memo_data;
GO

CREATE TABLE dbo.memo_data (
    id INT IDENTITY(1,1) PRIMARY KEY,
    title VARCHAR(255) NOT NULL,
    content VARCHAR(2048) NULL,
    created_at DATETIME2 NOT NULL CONSTRAINT DF_memo_data_created_at DEFAULT (SYSUTCDATETIME()),
    updated_at DATETIME2 NOT NULL CONSTRAINT DF_memo_data_updated_at DEFAULT (SYSUTCDATETIME())
);
GO

-- 샘플 데이터 삽입
INSERT INTO dbo.memo_data (title, content) VALUES
('memo #1', '이것은 첫 번째 메모의 내용입니다.'),
('memo #2', '이것은 두 번째 메모의 내용입니다.'),
('memo #3', '이것은 세 번째 메모의 내용입니다.');
GO