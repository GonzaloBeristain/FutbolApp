CREATE DATABASE DBFUTBOL;
USE DBFUTBOL;

-- Crear tabla LIGA
CREATE TABLE LIGA(
	IdLiga INT PRIMARY KEY IDENTITY(1,1),
	NombreLiga VARCHAR(50) UNIQUE,
	PaisLiga VARCHAR(50)
);

-- Crear tabla POSICION
CREATE TABLE POSICION(
	IdPosicion INT PRIMARY KEY IDENTITY(1,1),
	NombrePosicion VARCHAR(50) UNIQUE
);

-- Crear tabla EQUIPO
CREATE TABLE EQUIPO(
	IdEquipo INT PRIMARY KEY IDENTITY(1,1),
	NombreEquipo VARCHAR(50) UNIQUE,
	PaisEquipo VARCHAR(50),
	IdLiga INT,
	CONSTRAINT FK_Liga FOREIGN KEY (IdLiga) REFERENCES LIGA(IdLiga) ON DELETE SET NULL
);

-- Crear tabla JUGADOR
CREATE TABLE JUGADOR(
	IdJugador INT PRIMARY KEY IDENTITY(1,1),
	NombreJugador VARCHAR(50) NOT NULL,
	PaisJugador VARCHAR(50) NOT NULL,
	IdPosicion INT NOT NULL,
	IdEquipo INT NOT NULL,
	FechaNacimiento DATE NOT NULL,
	Imagen VARCHAR(255),
	CONSTRAINT FK_Posicion FOREIGN KEY (IdPosicion) REFERENCES POSICION(IdPosicion),
	CONSTRAINT FK_Equipo FOREIGN KEY (IdEquipo) REFERENCES EQUIPO(IdEquipo),
	CONSTRAINT UQ_Jugador UNIQUE (NombreJugador, IdEquipo) -- Asegura que un jugador con el mismo nombre no pueda estar en el mismo equipo más de una vez
);

-- Crear índices en nombre JUGADOR
CREATE INDEX IDX_NombreJugador
ON JUGADOR (NombreJugador);

-- Insertar datos en cada tabla
--Liga
INSERT INTO LIGA (NombreLiga, PaisLiga) 
VALUES ('La Liga', 'España');
INSERT INTO LIGA (NombreLiga, PaisLiga) 
VALUES ('Premier League', 'Inglaterra');

--Posicion
INSERT INTO POSICION (NombrePosicion) 
VALUES ('Delantero');
INSERT INTO POSICION (NombrePosicion) 
VALUES ('Volante');

--Equipo
INSERT INTO EQUIPO (NombreEquipo, PaisEquipo, IdLiga) 
VALUES ('FC Barcelona', 'España', 1);
INSERT INTO EQUIPO (NombreEquipo, PaisEquipo, IdLiga) 
VALUES ('Manchester City', 'Inglaterra', 2);
INSERT INTO EQUIPO (NombreEquipo, PaisEquipo, IdLiga) 
VALUES ('Manchester United', 'Inglaterra', 2);

--Jugadores
INSERT INTO JUGADOR (NombreJugador, PaisJugador, IdPosicion, IdEquipo, FechaNacimiento) 
VALUES ('Lionel Messi', 'Argentina', 1, 1, '1987-06-24');
INSERT INTO JUGADOR (NombreJugador, PaisJugador, IdPosicion, IdEquipo, FechaNacimiento) 
VALUES ('Sergio Aguero', 'Argentina', 1, 2, '1988-06-02');
INSERT INTO JUGADOR (NombreJugador, PaisJugador, IdPosicion, IdEquipo, FechaNacimiento) 
VALUES ('Garmnacho', 'Argentina', 1, 1002, '1999-06-02');

-- Consultas de cada tabla
SELECT * FROM LIGA;

SELECT * FROM POSICION;

SELECT * FROM EQUIPO;

SELECT * FROM JUGADOR;

SELECT * FROM JUGADOR WHERE NombreJugador = 'Lionel Messi'; -- Consulta por nombre

-- Eliminar datos
DELETE FROM JUGADOR WHERE IdJugador = 1;