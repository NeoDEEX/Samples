--
-- Sqlite script for creating products table & inserting sample data
--

CREATE TABLE IF NOT EXISTS t_products (
	product_id text PRIMARY KEY,
	product_name text not null,
	unit_price real not null
);

DELETE FROM t_products;

INSERT INTO t_products (product_id, product_name, unit_price) VALUES
	('P001', 'Product 1', 10.0),
	('P002', 'Product 2', 20.0),
	('P003', 'Product 3', 30.0);
