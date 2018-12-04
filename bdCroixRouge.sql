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
DROP TABLE IF EXISTS Addresse;
DROP TABLE IF EXISTS Imagepromotion;


CREATE TABLE Groupesanguin (
    Nom          NVARCHAR(3) NOT NULL,
    PRIMARY KEY CLUSTERED (Nom ASC)
);

CREATE TABLE Imagepromotion (
	Url		NVARCHAR(900)	NOT NULL, 
	PRIMARY KEY CLUSTERED (Url ASC)
);

CREATE TABLE Role (
	Libelle	NVARCHAR(50) NOT NULL,
	PRIMARY KEY CLUSTERED (Libelle ASC)
);

CREATE TABLE Addresse (
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
	Score				INT				NOT NULL,
	Fk_Libelle			NVARCHAR(50)	NOT NULL,
	Fk_Addresse			INT 			NOT NULL, 
	Fk_Groupesanguin	NVARCHAR(3),
	PRIMARY KEY CLUSTERED (Login), 
	FOREIGN KEY (Fk_Libelle) REFERENCES Role(Libelle), 
	FOREIGN KEY (Fk_Addresse) REFERENCES Addresse(Id), 
	FOREIGN KEY (Fk_Groupesanguin) REFERENCES Groupesanguin(Nom)
);

CREATE TABLE Partagerimage (
	Id 				INT 	IDENTITY(1, 1) 	NOT NULL,
	Fk_Image		NVARCHAR(900)			NOT NULL, 
	Fk_Utilisateur	NVARCHAR(50)			NOT NULL
	PRIMARY KEY CLUSTERED (Id ASC), 
	FOREIGN KEY (Fk_Image) REFERENCES Imagepromotion(Url), 
	FOREIGN KEY (Fk_Utilisateur) REFERENCES Utilisateur(Login)
);

CREATE TABLE Diffuserimage (
	Id 				INT 	IDENTITY(1, 1) 	NOT NULL,
	Fk_Image		NVARCHAR(900)			NOT NULL, 
	Fk_Utilisateur	NVARCHAR(50)			NOT NULL
	PRIMARY KEY CLUSTERED (Id ASC), 
	FOREIGN KEY (Fk_Image) REFERENCES Imagepromotion(Url), 
	FOREIGN KEY (Fk_Utilisateur) REFERENCES Utilisateur(Login)
);

CREATE TABLE Alerte (
	Id 				INT IDENTITY(1,1) 	NOT NULL,
	Nom 			NVARCHAR(100)		NOT NULL, 
	Contenu			NVARCHAR(500) 		NOT NULL,
	Fk_Utilisateur 	NVARCHAR(50)		NOT NULL,
	PRIMARY KEY CLUSTERED(Id ASC), 
	FOREIGN KEY (Fk_Utilisateur) REFERENCES Utilisateur(Login)
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
	Latitude	GEOGRAPHY				NOT NULL, 
	Longitude	GEOGRAPHY 				NOT NULL,
	Fk_Addresse	INT 					NOT NULL,
	PRIMARY KEY CLUSTERED (Id ASC), 
	FOREIGN KEY (Fk_Addresse) REFERENCES Addresse(Id)
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
		   
INSERT INTO [dbo].[Addresse]
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
           ,[Score]
           ,[Fk_Libelle]
           ,[Fk_Addresse]
           ,[Fk_Groupesanguin])
     VALUES
           ('Gwynbleidd', 'POZZI', 'Alessandro', 'MotDePasseNonHashé', 'aless@gmail.com', 473227085, '1996-07-14', 0, 'ADMIN', 1, null);


