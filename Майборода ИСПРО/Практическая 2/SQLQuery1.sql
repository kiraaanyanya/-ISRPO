CREATE TABLE products (
    id INT PRIMARY KEY,
    name NVARCHAR(100),
    stillage INT,
    cell INT,
    quantity INT
);

INSERT INTO products (id, name, stillage, cell, quantity) VALUES (1, 'Хлеб', 2, 3, 20);
INSERT INTO products (id, name, stillage, cell, quantity) VALUES (2, 'Вода', 1, 2, 15);
INSERT INTO products (id, name, stillage, cell, quantity) VALUES (3, 'Сладости', 2, 1, 100);
INSERT INTO products (id, name, stillage, cell, quantity) VALUES (4, 'Газировка', 20, 100, 15); -- Изменён id на 4