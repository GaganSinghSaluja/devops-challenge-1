CREATE DATABASE IF NOT EXISTS product;

Use product;

CREATE TABLE IF NOT EXISTS ProductData (

sku INT,

description VARCHAR(255) NOT NULL,

category VARCHAR(255) NOT NULL,

price DOUBLE(10,2),

location VARCHAR(255) NOT NULL,

l3 VARCHAR(20) NOT NULL,

qty TINYINT NOT NULL,

PRIMARY KEY (sku)

);

CREATE TABLE IF NOT EXISTS TransmissionsummaryData (

Id VARCHAR(255),

qtysum TINYINT NOT NULL,

recordcount TINYINT NOT NULL,

PRIMARY KEY (Id)

);
