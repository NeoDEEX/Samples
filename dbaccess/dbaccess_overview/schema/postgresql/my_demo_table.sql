CREATE TABLE my_demo_table (
	col_id		integer NOT NULL PRIMARY KEY,
	col_str		varchar(40),
	col_int		integer
);

INSERT INTO my_demo_table VALUES(1, 'str1', 1);
INSERT INTO my_demo_table VALUES(2, 'str2', 2);
INSERT INTO my_demo_table VALUES(3, 'str3', 3);