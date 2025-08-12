-- PostgreSQL FoxDbProfile 관련 DB 스크립트

-- {"infoId":"DB:cbf7f5fd-7d8b-4da4-859f-ab14f41da9e7","timestamp":"2025-08-12T05:56:41.9200026Z","executionType":1,"commandText":"SELECT * FROM products WHERE product_id \u003C :p0","parametersString":"p0=3","resultString":"2","executionTime":592.5531000000001,"queryString":"SELECT * FROM products WHERE product_id \u003C 3","caller":"SimpleDbProfile","parameterInfos":{"p0":"3"}}

-- 테이블 생성
create table dbprofile(
    log_id varchar(64) not null primary key,
    log_time timestamp not null,
    -- log_user varchar(64) null,
    info jsonb not null
);

-- 시간에 따른 내림차순 정렬용 인덱스 (최근 우선)
CREATE INDEX idx_dbprofile_log_time_desc
ON dbprofile (log_time DESC);

-- 수행 시간 인덱스
CREATE INDEX idx_dbprofile_execution_time_float
ON dbprofile ((info->>'executionTime')::float8);

-- executionTime이 3초 이상인 로그 조회 예제
SELECT * FROM dbprofile
WHERE (info->>'executionTime')::float8 >= 3000
ORDER BY log_time DESC
LIMIT 100;