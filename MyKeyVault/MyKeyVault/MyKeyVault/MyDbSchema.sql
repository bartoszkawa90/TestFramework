CREATE TABLE DbUsers (
                         UserId INTEGER PRIMARY KEY AUTOINCREMENT,
                         Name TEXT NOT NULL UNIQUE,
                         Secret TEXT NOT NULL
);

CREATE TABLE People (
                        UserId INTEGER PRIMARY KEY AUTOINCREMENT,
                        Name TEXT NOT NULL,
                        Password TEXT NOT NULL
);

INSERT INTO DbUsers (Name, Secret) VALUES ('Admin', 'admin_secret_123');
INSERT INTO People (Name, Password) VALUES ('JohnDoe', 'password_pizza');
INSERT INTO People (Name, Password) VALUES ('JaneSmith', 'password_coffee');