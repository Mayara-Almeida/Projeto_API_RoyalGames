
--CRIANDO BANCO

CREATE DATABASE RoyalGames;
GO

USE Royalgames;
GO

CREATE TABLE Usuario(
	UsuarioID INT PRIMARY KEY IDENTITY,
	Nome VARCHAR(60) NOT NULL,
	Email VARCHAR(150) UNIQUE NOT NULL,
	Senha VARBINARY(32) NOT NULL,
	StatusUsuario BIT DEFAULT 1
);
GO

CREATE TABLE ClassificacaoIndicativa(
	 ClassificacaoIndicativaID INT PRIMARY KEY IDENTITY,
	 Classificacao VARCHAR(50) UNIQUE NOT NULL
);
GO

CREATE TABLE Jogo(
	JogoID INT PRIMARY KEY IDENTITY,
	Nome VARCHAR(150) UNIQUE NOT NULL, 
	Preco DECIMAL(10, 2) NOT NULL,
	Descricao NVARCHAR(MAX) NOT NULL,
	Imagem VARBINARY(MAX) NOT NULL,
	StatusJogo BIT DEFAULT 1,

	ClassificacaoIndicativaID INT FOREIGN KEY REFERENCES ClassificacaoIndicativa(ClassificacaoIndicativaID),
	UsuarioID INT FOREIGN KEY REFERENCES Usuario(UsuarioID)
);
GO

CREATE TABLE Plataforma(
	PlataformaID INT PRIMARY KEY IDENTITY,
	Nome VARCHAR(50) UNIQUE NOT NULL
);         
GO

CREATE TABLE JogoPlataforma(
	JogoID INT NOT NULL,
	PlataformaID INT NOT NULL,

	CONSTRAINT PK_JogoPlataforma PRIMARY KEY (JogoID, PlataformaID),
	CONSTRAINT FK_JogoPlataforma_Jogo FOREIGN KEY (JogoId) REFERENCES Jogo(JogoID) ON DELETE CASCADE,
	CONSTRAINT FK_JogoPlataforma_Plataforma FOREIGN KEY (PlataformaID) REFERENCES Plataforma(PlataformaID) ON DELETE CASCADE,
);
GO 

CREATE TABLE Genero(
	GeneroID INT PRIMARY KEY IDENTITY,
	Nome VARCHAR(50) UNIQUE  NOT NULL
 );
 GO

CREATE TABLE JogoGenero(
	JogoID INT NOT NUll,
	GeneroID INT NOT NULL, 

	CONSTRAINT PK_JogoGenero PRIMARY KEY(JogoID, GeneroID),
	CONSTRAINT FK_JogoGenero_Jogo FOREIGN KEY (JogoID) REFERENCES Jogo(JogoID) ON DELETE CASCADE,
	CONSTRAINT FK_JogoGenero_Genero FOREIGN KEY (GeneroID) REFERENCES Genero(GeneroID) ON DELETE CASCADE
);
GO

CREATE TABLE Log_AlteracaoJogo(
	Log_AlteracaJogoID INT PRIMARY KEY IDENTITY,
	DataAlteracao DATETIME2(0) NOT NULL,
	NomeAnterior VARCHAR(100),
	Precoanterior DECIMAL(10, 2),

	JogoID INT FOREIGN KEY REFERENCES Jogo(JogoID)
GO

--TRIGGERS

-- Inativar usuário
CREATE TRIGGER trg_ExclusaoUsuario
ON Usuario
INSTEAD OF DELETE
AS 
BEGIN
	UPDATE u SET StatusUsuario = 0 -- Deixa o usuário como false(inativado)
		FROM Usuario u 
		INNER JOIN deleted d 
			ON d.UsuarioID = u.UsuarioID;
		END
		GO

-- Salvar alterações feitas em jogo na tabela log
CREATE TRIGGER trg_AlteracaoJogo
ON Jogo
AFTER UPDATE
AS
BEGIN
	INSERT INTO Log_AlteracaoJogo(DataAlteracao, JogoID, NomeAnterior, PrecoAnterior) 
	SELECT GETDATE(), JogoID, Nome, Preco FROM deleted
	END
	GO

-- Inativar jogo
CREATE TRIGGER trg_ExclusaoJogo
		ON Jogo
		INSTEAD OF DELETE 
		AS 
		BEGIN
			UPDATE j SET StatusJogo = 0 -- Deixa o jogo como false(inativado)
			FROM Jogo j
			INNER JOIN deleted d 
			ON d.JogoID = j.JogoID;
		END
		GO

-- INSERINDO REGISTROS

INSERT INTO Usuario(Nome, Email, Senha) 
	VALUES
	('Mayara Almeida', 'mayara@royalgames.com', HASHBYTES('SHA2_256', 'admin@123'))
GO

INSERT INTO ClassificacaoIndicativa(Classificacao)
	VALUES
	('+18 anos'),
	('+16 anos'),
	('Livre')
GO

INSERT INTO Jogo(Nome, Preco, Descricao, Imagem, UsuarioID, ClassificacaoIndicativaID) 
	VALUES
	('Minecraft', 29.90, 'Explore e crie livremente em um mundo totalmente aberto.', CONVERT(VARBINARY(MAX), 'imagem aleatoria'), 1, 3),
	('Red Dead Redemption 2', 120.50, 'O fim da era do velho oeste se aproxima, e os xerifes caçam as últimas gangues fora da lei.', CONVERT(VARBINARY(MAX), 'imagem aleatoria'), 1, 1),
	('Valorant', 69.90, 'Personagens marcantes, mecânica de tiro precisa e habilidades únicas!', CONVERT(VARBINARY(MAX), 'imagem aleatoria'), 1, 2)
GO

INSERT INTO Plataforma(Nome)
	VALUES
	('PlayStation')
GO

INSERT INTO JogoPlataforma(JogoID, PlataformaID)
	VALUES
	(1, 1),
	(2, 1),
	(3, 1)
GO

INSERT INTO Genero(Nome)
	VALUES
	('Aventura'),
	('Ação'),
	('Tiro tático')
GO

INSERT INTO JogoGenero(JogoID, GeneroID)
	VALUES
	(1, 1),
	(2, 2),
	(3, 3)
GO

INSERT INTO Usuario(Nome, Email, Senha) 
	VALUES
	('Maria Clara', 'mariaClara@royalgames.com', HASHBYTES('SHA2_256', 'admin@123'))
GO

SELECT * FROM Usuario;
SELECT * FROM Jogo;
SELECT * FROM ClassificacaoIndicativa;
SELECT * FROM Plataforma;
SELECT * FROM Genero;
SELECT * FROM JogoGenero;
SELECT * FROM JogoPlataforma;
SELECT * FROM Log_AlteracaoJogo;