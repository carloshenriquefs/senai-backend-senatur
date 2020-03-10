CREATE DATABASE DB_Senatur
GO

USE DB_Senatur
GO

CREATE TABLE TBL_Pacote(
    IdPacote INT PRIMARY KEY IDENTITY (1,1),
    NomePacote VARCHAR(200) NOT NULL,
    Descricao   TEXT NOT NULL,
    DataIda DATE NOT NULL,
    DataVolta DATE NOT NULL,
    Valor DECIMAL NOT NULL,
    Ativo BINARY NOT NULL,
    NomeCidade VARCHAR(200)
);

CREATE TABLE TBL_TipoUsuario(
    IdTipoUsuario INT PRIMARY KEY IDENTITY (1,1),
    TituloTipoUsuario VARCHAR(200) NOT NULL
);

CREATE TABLE TBL_Usuario(
    IdUsuario INT PRIMARY KEY IDENTITY (1,1),
    Email VARCHAR(200) NOT NULL,
    Senha VARCHAR(20) NOT NULL,
    FK_IdTipoUsuario INT FOREIGN KEY REFERENCES TBL_TipoUsuario(IdTipoUsuario)
);

ALTER TABLE TBL_Pacote
ALTER COLUMN Descricao TEXT