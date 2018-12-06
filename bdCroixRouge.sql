DROP TABLE IF EXISTS Don;
DROP TABLE IF EXISTS Lanceralerte;
DROP TABLE IF EXISTS Jourouverture;
DROP TABLE IF EXISTS Diffuserimage;
DROP TABLE IF EXISTS Partagerimage;
DROP TABLE IF EXISTS Concerne;
DROP TABLE IF EXISTS Alerte;
DROP TABLE IF EXISTS Utilisateur;
DROP TABLE IF EXISTS Groupesanguin;
DROP TABLE IF EXISTS Role;
DROP TABLE IF EXISTS Collecte;
DROP TABLE IF EXISTS Adresse;
DROP TABLE IF EXISTS Imagepromotion;


CREATE TABLE Groupesanguin (
    Nom          NVARCHAR(3) NOT NULL,
    PRIMARY KEY CLUSTERED (Nom ASC)
);

CREATE TABLE Imagepromotion (
	Url		NVARCHAR(400)	NOT NULL, 
	PRIMARY KEY (Url)
);

CREATE TABLE Role (
	Libelle	NVARCHAR(50) NOT NULL,
	PRIMARY KEY CLUSTERED (Libelle ASC)
);

CREATE TABLE Adresse (
	Id 		INT IDENTITY(1,1) 	NOT NULL,
	Ville	NVARCHAR(100) 		NOT NULL, 
	Rue		NVARCHAR(100) 		NOT NULL, 
	Numero 	NVARCHAR(4)			NOT NULL, 
	PRIMARY KEY CLUSTERED(Id ASC)
);

CREATE TABLE Utilisateur (
	Login 				NVARCHAR(50)	NOT NULL, 
	Nom 				NVARCHAR(100)	NOT NULL, 
	Prenom				NVARCHAR(100)	NOT NULL, 
	Password 			NVARCHAR(200)	NOT NULL, 
	Mail				NVARCHAR(320)	NOT NULL, 
	NumGSM				INT				NOT NULL, 
	DateNaissance		DATE			NOT NULL, 
	IsMale				BIT				NOT NULL,
	Score				INT				NOT NULL,
	Fk_Role				NVARCHAR(50)	NOT NULL,
	Fk_Adresse			INT 			NOT NULL, 
	Fk_Groupesanguin	NVARCHAR(3),
	Rv					ROWVERSION		NOT NULL,
	PRIMARY KEY CLUSTERED (Login), 
	FOREIGN KEY (Fk_Role) REFERENCES Role(Libelle), 
	FOREIGN KEY (Fk_Adresse) REFERENCES Adresse(Id), 
	FOREIGN KEY (Fk_Groupesanguin) REFERENCES Groupesanguin(Nom)
);

CREATE TABLE Partagerimage (
	Id 				INT 	IDENTITY(1, 1) 	NOT NULL,
	Fk_Image		NVARCHAR(400)			NOT NULL, 
	Fk_Utilisateur	NVARCHAR(50)			NOT NULL
	PRIMARY KEY CLUSTERED (Id ASC), 
	FOREIGN KEY (Fk_Image) REFERENCES Imagepromotion(Url), 
	FOREIGN KEY (Fk_Utilisateur) REFERENCES Utilisateur(Login)
);

CREATE TABLE Diffuserimage (
	Id 				INT 	IDENTITY(1, 1) 	NOT NULL,
	Fk_Image		NVARCHAR(400)			NOT NULL, 
	Fk_Utilisateur	NVARCHAR(50)			NOT NULL
	PRIMARY KEY CLUSTERED (Id ASC), 
	FOREIGN KEY (Fk_Image) REFERENCES Imagepromotion(Url), 
	FOREIGN KEY (Fk_Utilisateur) REFERENCES Utilisateur(Login)
);

CREATE TABLE Alerte (
	Id 				INT IDENTITY(1,1) 	NOT NULL,
	Nom 			NVARCHAR(100)		NOT NULL, 
	Contenu			NVARCHAR(500) 		NOT NULL,
	Rv				rowversion			NOT NULL,
	PRIMARY KEY CLUSTERED(Id ASC)
);

CREATE TABLE Concerne (
	Id 					INT 	IDENTITY(1, 1) 	NOT NULL, 
	Fk_Alerte			INT 					NOT NULL, 
	Fk_Groupesanguin 	NVARCHAR(3) 			NOT NULL, 
	PRIMARY KEY CLUSTERED (Id ASC), 
	FOREIGN KEY (Fk_Alerte) REFERENCES Alerte(Id), 
	FOREIGN KEY (Fk_Groupesanguin) REFERENCES Groupesanguin(Nom)
);

CREATE TABLE Collecte ( 
	Id 			INT 	IDENTITY(1, 1) 	NOT NULL,
	Nom 		NVARCHAR(200)			NOT NULL, 
	DateDebut	DATE 					NOT NULL, 
	DateFin		DATE, 
	Latitude	DECIMAL(9, 6)			NOT NULL, 
	Longitude	DECIMAL(9, 6) 			NOT NULL,
	Fk_Adresse	INT 					NOT NULL,
	PRIMARY KEY CLUSTERED (Id ASC), 
	FOREIGN KEY (Fk_Adresse) REFERENCES Adresse(Id)
);

CREATE TABLE Jourouverture (
	Id 			INT 	IDENTITY(1, 1) 	NOT NULL,
	Nom 		NVARCHAR(8) 			NOT NULL, 
	HeureDebut	TIME 					NOT NULL, 
	HeureFin	TIME 					NOT NULL, 
	Fk_Collecte	INT 					NOT NULL,
	PRIMARY KEY CLUSTERED (Id ASC), 
	FOREIGN KEY (Fk_Collecte) REFERENCES Collecte(Id)
);

CREATE TABLE Don (
	Id 				INT 	IDENTITY(1, 1) 	NOT NULL,
	Date 			DATE 					NOT NULL, 
	Fk_Collecte		INT 					NOT NULL, 
	Fk_Utilisateur	NVARCHAR(50) 			NOT NULL,
	PRIMARY KEY CLUSTERED (Id ASC), 
	FOREIGN KEY (Fk_Collecte) REFERENCES Collecte(Id), 
	FOREIGN KEY (Fk_Utilisateur) REFERENCES Utilisateur(Login)
);

CREATE TABLE Lanceralerte (
	Id 				INT 	IDENTITY(1, 1) 	NOT NULL,
	Fk_Alerte		INT 					NOT NULL, 
	Fk_Utilisateur	NVARCHAR(50)			NOT NULL, 
	PRIMARY KEY CLUSTERED (Id ASC), 
	FOREIGN KEY (Fk_Alerte) REFERENCES Alerte(Id), 
	FOREIGN KEY (Fk_Utilisateur) REFERENCES Utilisateur(Login)
);


INSERT INTO [dbo].[Role]
           ([Libelle])
     VALUES
           ('ADMIN'), 
		   ('USER'), 
		   ('SUPERUSER');
		   
INSERT INTO [dbo].[Adresse]
           ([Ville]
           ,[Rue]
           ,[Numero])
     VALUES
           ('Dinant', 'Place du Baty', '6B');
		   
INSERT INTO [dbo].[Utilisateur]
           ([Login]
           ,[Nom]
           ,[Prenom]
           ,[Password]
           ,[Mail]
           ,[NumGSM]
           ,[DateNaissance]
		   ,[IsMale]
           ,[Score]
           ,[Fk_Role]
           ,[Fk_Adresse]
           ,[Fk_Groupesanguin])
     VALUES
           ('Gwynbleidd', 'POZZI', 'Alessandro', 'MotDePasseNonHashé', 'aless@gmail.com', 473227085, '1996-07-14', 1, 0, 'ADMIN', 1, null);
INSERT INTO [dbo].[Alerte]
           ([Nom]
           ,[Contenu])
     VALUES
           ('Stock O- faible', 'La croix Rouge a besoin des donneurs O- !'), 
		   ('Trop de donneurs', 'Suite aux récents attentats, vous être nombreux a vouloir donner votre sang, tout en vous remerciant pour votre solidarité nous vous prions de revenir dans quelques jours afin que nous puissions mieux nous organiser'), 
		   ('Stock AB+ suffisant', 'La croix rouge a pour l instant assez de donneurs AB+, revenez dans quelques semaines');
		   
INSERT INTO [dbo].[Groupesanguin]
           ([Nom])
     VALUES
           ('A+'), ('A-'), ('B+'), ('B-'), ('AB+'), ('AB-'), ('O+'), ('O-');


