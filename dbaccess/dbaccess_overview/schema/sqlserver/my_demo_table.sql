--
-- 테스트용 my_demo_table (MS SQL Server)
--
CREATE TABLE my_demo_table (
    col_id INT NOT NULL PRIMARY KEY,
    col_str VARCHAR(40),
    col_int INT
);

INSERT INTO my_demo_table VALUES(1, 'str1', 1);
INSERT INTO my_demo_table VALUES(2, 'str2', 2);
INSERT INTO my_demo_table VALUES(3, 'str3', 3);
