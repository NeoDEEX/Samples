--
-- 예제를 위한 PostgreSQL 데이터베이스 스키마
--

-- 메모 데이터 테이블
CREATE TABLE memo_data (
    id INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    title VARCHAR(255) NOT NULL,
    content VARCHAR(2048),
    created_at TIMESTAMP NOT NULL DEFAULT now(),
    updated_at TIMESTAMP NOT NULL DEFAULT now()
);

-- 샘플 데이터 삽입
INSERT INTO memo_data (title, content) VALUES
('memo #1', '이것은 첫 번째 메모의 내용입니다.'),
('memo #2', '이것은 두 번째 메모의 내용입니다.'),
('memo #3', '이것은 세 번째 메모의 내용입니다.');