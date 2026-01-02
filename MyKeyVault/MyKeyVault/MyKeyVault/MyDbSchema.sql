-- MyDbSchema.sql (recreate tables)
DROP TABLE IF EXISTS DbUsers;
DROP TABLE IF EXISTS People;

CREATE TABLE DbUsers (
                         UserId   INTEGER PRIMARY KEY AUTOINCREMENT,
                         Name     TEXT NOT NULL UNIQUE,
                         Password TEXT NOT NULL,
                         Role     TEXT NOT NULL
                             CHECK (Role IN ('Admin', 'Reader', 'Guest'))
);

CREATE TABLE People (
                        UserId   INTEGER PRIMARY KEY AUTOINCREMENT,
                        Name     TEXT NOT NULL,
                        Password TEXT NOT NULL
);

INSERT INTO DbUsers (Name, Password, Role) VALUES ('admin', 'Admin123', 'Admin');
INSERT INTO DbUsers (Name, Password, Role) VALUES ('reader', 'Reader123', 'Reader');
INSERT INTO DbUsers (Name, Password, Role) VALUES ('guest', 'Guest123', 'Guest');

INSERT INTO People (Name, Password) VALUES ('JohnDoe', 'password_pizza');
INSERT INTO People (Name, Password) VALUES ('JaneSmith', 'password_coffee');
