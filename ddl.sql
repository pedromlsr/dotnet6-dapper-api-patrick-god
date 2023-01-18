CREATE TABLE super_heroes (
	super_hero_id int IDENTITY(1,1) PRIMARY KEY,
	codename varchar(100) NOT NULL,
	first_name varchar(100) NOT NULL,
	last_name varchar(100) NOT NULL,
	place varchar(100) NOT NULL
);

SET IDENTITY_INSERT super_heroes ON

INSERT INTO super_heroes (super_hero_id, codename, first_name, last_name, place)
VALUES (
	1, 'Spider-Man', 'Peter', 'Parker', 'New York City'
);

SET IDENTITY_INSERT super_heroes OFF