--
-- SQL Server Database Script
--

DROP TABLE IF EXISTS t_products;

CREATE TABLE t_products
(
	product_id VARCHAR(16) PRIMARY KEY,
	product_name VARCHAR(64) NOT NULL,
	unit_price DECIMAL(18, 2) NOT NULL
);

DROP TABLE IF EXISTS t_orders;

CREATE TABLE t_orders
(
	order_id INT PRIMARY KEY IDENTITY(1,1),
	customer_id VARCHAR(16) NOT NULL,
	employee_id VARCHAR(16),
	order_date DATETIME,
	shipped_date DATETIME,
	ship_address VARCHAR(255)
);

DROP TABLE IF EXISTS t_order_details;

CREATE TABLE t_order_details
(
	order_id INT NOT NULL,
	product_id VARCHAR(16) NOT NULL,
	quantity INT NOT NULL,
	unit_price DECIMAL(18, 2) NOT NULL
);

INSERT INTO t_products (product_id, product_name, unit_price)
VALUES ('PROD001', 'Product 1', 1.1),
	   ('PROD002', 'Product 2', 11.11),
	   ('PROD003', 'Product 3', 3.3),
	   ('PROD004', 'Product 4', 4.4),
	   ('PROD005', 'Product 5', 5.5);

INSERT INTO t_orders (customer_id, employee_id, order_date, shipped_date, ship_address)
VALUES ('CUST001', 'EMP001', '2023-01-01', '2023-01-05', 'Addr #1'),
       ('CUST002', 'EMP002', '2023-01-02', '2023-01-06', 'Addr #2'),
       ('CUST003', 'EMP003', '2023-01-03', '2023-01-07', 'Addr #3'),
	   ('CUST004', 'EMP004', '2023-01-04', null, 'Addr #4'),
	   ('CUST005', 'EMP005', '2023-01-05', null, 'Addr #5');

INSERT INTO t_order_details (order_id, product_id, quantity, unit_price)
VALUES (1, 'PROD001', 1, 1.1),
       (1, 'PROD002', 1, 11.11),
       (2, 'PROD001', 2, 2.2),
       (2, 'PROD002', 2, 22.22),
       (3, 'PROD003', 3, 3.3),
       (4, 'PROD004', 4, 4.4),
       (5, 'PROD005', 5, 5.5);
