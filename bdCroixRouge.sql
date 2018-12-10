DROP TABLE IF EXISTS Don;
DROP TABLE IF EXISTS Lanceralerte;
DROP TABLE IF EXISTS Jourouverture;
DROP TABLE IF EXISTS TrancheHoraire;
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
	Rv		ROWVERSION 			NOT NULL,
	PRIMARY KEY CLUSTERED(Id ASC)
);

CREATE TABLE Utilisateur (
	Login 				NVARCHAR(50)	NOT NULL, 
	Nom 				NVARCHAR(100), 
	Prenom				NVARCHAR(100), 
	Password 			NVARCHAR(200)	NOT NULL, 
	Mail				NVARCHAR(320)	NOT NULL, 
	NumGSM				INT, 
	DateNaissance		DATE, 
	IsMale				BIT,
	Score				INT				NOT NULL,
	Fk_Role				NVARCHAR(50)	NOT NULL,
	Fk_Adresse			INT, 
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
	Rv				ROWVERSION			NOT NULL,
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
	Latitude	DECIMAL(9, 6)			NOT NULL, 
	Longitude	DECIMAL(9, 6) 			NOT NULL, 
	Telephone	INT,
	Rv			ROWVERSION				NOT NULL,
	PRIMARY KEY CLUSTERED (Id ASC)
);

CREATE TABLE TrancheHoraire (
	Id 		INT IDENTITY(1, 1) 			NOT NULL, 
	HeureDebut		TIME 				NOT NULL, 
	HeureFin		TIME 				NOT NULL, 
	PRIMARY KEY CLUSTERED (Id ASC)
);

CREATE TABLE Jourouverture (
	Id 					INT 	IDENTITY(1, 1) 	NOT NULL,
	LibelleJour 		NVARCHAR(8), 
	Date 				DATE,
	Fk_Collecte			INT 					NOT NULL,
	Fk_TrancheHoraire	INT 					NOT NULL,
	PRIMARY KEY CLUSTERED (Id ASC), 
	FOREIGN KEY (Fk_Collecte) REFERENCES Collecte(Id), 
	FOREIGN KEY (Fk_TrancheHoraire) REFERENCES TrancheHoraire(Id)
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
           ('Dinant', 'Place du Baty', '6B'), 
		   ('Libramont', 'Rue de Libramont', '77A');
		   
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
           ('Gwynbleidd', 'POZZI', 'Alessandro', 'MotDePasseNonHashé', 'alessandro.pozzi72@gmail.com', 473227085, '1996-07-14', 1, 0, 'ADMIN', 1, null), 
		   ('Bob', 'BRAHY', 'Sébastien', '12345678', 'brahysebastien@hotmail.com', 473124578, '1993-01-01', 1, 0, 'ADMIN', 2, null);
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
		   
INSERT INTO [dbo].[Collecte]
           ([Nom]
           ,[Latitude]
           ,[Longitude]
           ,[Telephone])
     VALUES
           ('Site de prélèvement Namur', 50.467260, 4.866500, 081221010), 
		   ('Bois de Villers - Salle communal', 50.389900, 4.823320, null), 
		   ('Courriere - Salle cecilia', 50.392790, 4.990760, null), 
		   ('Emines - Ecole communal', 50.512530, 4.840690, null), 
		   ('Namur - Car devant l''église saint-joseph', 50.466410, 4.865420, null), 
		   ('Saint Denis Bovesse - Salle la Ruche', 50.535290, 4.784890, null), 
		   ('Sclayn - Salle communale (Près du terrain de foot)', 50.493090, 5.035540, null), 
		   ('Spy - École fondamentale autonome', 50.480150, 4.702640, null), 
		   ('Site de prélèvement Huy', 50.510810, 5.240360, 085277588);
		   
INSERT INTO [dbo].[TrancheHoraire]
           ([HeureDebut]
           ,[HeureFin])
     VALUES
           ('08:00', '19:30'), 
		   ('08:00', '12:30'), 
		   ('08:00', '16:30'), 
		   ('16:00', '19:30'), 
		   ('15:30', '19:30'), 
		   ('15:00', '18:30'), 
		   ('10:00', '17:00'), 
		   ('08:00', '14:00'), 
		   ('11:00', '17:00'), 
		   ('11:00', '16:30'), 
		   ('13:00', '19:30'), 
		   ('09:00', '13:00');

INSERT INTO [dbo].[Jourouverture]
           ([LibelleJour]
           ,[Date]
           ,[Fk_Collecte]
           ,[Fk_TrancheHoraire])
     VALUES
           ('Lundi', null, 1, 1), ('Mardi', null, 1, 2), ('Mercredi', null, 1, 3), ('Jeudi', null, 1, 1), ('Vendredi', null, 1, 3), (null, '2018-12-15', 1, 7),(null, '2018-12-24', 1, 8),(null, '2018-12-29', 1, 8),(null, '2018-12-31', 1, 8),
		   (null, '2018-12-20', 2, 4), (null, '2019-03-21', 2, 4), (null, '2019-06-20', 2, 4), (null, '2019-09-19', 2, 4), (null, '2019-12-19', 2, 4), 
		   (null, '2019-02-15', 3, 5), (null, '2019-05-17', 3, 5), (null, '2019-08-16', 3, 5), (null, '2019-11-05', 3, 5), 
		   (null, '2019-01-14', 4, 6), (null, '2019-04-01', 4, 6), (null, '2019-07-01', 4, 6), (null, '2019-10-14', 4, 6), 
		   (null, '2018-12-26', 5, 9), (null, '2018-12-27', 5, 9), (null, '2018-12-28', 5, 10), 
		   (null, '2019-02-04', 6, 6), (null, '2019-05-16', 6, 6), (null, '2019-08-08', 6, 6), (null, '2019-11-14', 6, 6), 
		   (null, '2019-01-22', 7, 6), (null, '2019-04-23', 7, 6), (null, '2019-07-23', 7, 6), (null, '2019-10-22', 7, 6), 
		   (null, '2019-02-25', 8, 6), (null, '2019-05-27', 8, 6), (null, '2019-08-26', 8, 6), (null, '2019-11-25', 8, 6), 
		   ('Mardi', null, 9, 11), ('Mercredi', null, 9, 12), ('Jeudi', null, 9, 11);