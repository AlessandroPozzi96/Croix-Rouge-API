DROP TABLE IF EXISTS Don;
DROP TABLE IF EXISTS Lanceralerte;
DROP TABLE IF EXISTS Jourouverture;
DROP TABLE IF EXISTS TrancheHoraire;
DROP TABLE IF EXISTS Diffuserimage;
DROP TABLE IF EXISTS Partagerimage;
DROP TABLE IF EXISTS Alerte;
DROP TABLE IF EXISTS Information;
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
	Id 					INT IDENTITY(1,1) 	NOT NULL,
	Nom 				NVARCHAR(100)		NOT NULL, 
	Contenu				NVARCHAR(500) 		NOT NULL,
	Fk_Groupesanguin	NVARCHAR(3),
	Rv					ROWVERSION			NOT NULL,
	PRIMARY KEY CLUSTERED(Id ASC), 
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

CREATE TABLE Information (
	Id 				INT 	IDENTITY(1, 1) 	NOT NULL,
	Question 		NVARCHAR(MAX) 			NOT NULL,
	Reponse			NVARCHAR(MAX)			NOT NULL,
	PRIMARY KEY CLUSTERED (Id ASC)
);


INSERT INTO [dbo].[Role]
           ([Libelle])
     VALUES
           ('ADMIN'), 
		   ('USER');
		   
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
           ('Gwynbleidd', 'POZZI', 'Alessandro', '$2a$12$a0zO2D/85/qUy8N/bPKq4.beCTNdTYwTF7Zq/LmsaGDKfZAq.45sO', 'alessandro.pozzi72@gmail.com', 473227085, '1996-07-14', 1, 0, 'ADMIN', 1, null), 
		   ('Bob', 'BRAHY', 'Sébastien', '$2a$12$n/QOzqYJRgQhAaiv6Jz5COxzy0g/hB6KeaRsqFJmeVOpczC2Tl6Hu', 'brahysebastien@hotmail.com', 473124578, '1993-01-01', 1, 0, 'ADMIN', 2, null);
--PASSWORD : Gwynbleidd → 11111111 | Bob → 12345678		   
		   
INSERT INTO [dbo].[Groupesanguin]
([Nom])
     VALUES
           ('A+'), ('A-'), ('B+'), ('B-'), ('AB+'), ('AB-'), ('O+'), ('O-');
		   
		   
INSERT INTO [dbo].[Alerte]
           ([Nom]
           ,[Contenu]
		   ,[Fk_Groupesanguin])
     VALUES
           ('Stock O- faible', 'La croix Rouge a besoin des donneurs O- !', 'O-'), 
		   ('Trop de donneurs', 'Suite aux récents attentats, vous être nombreux a vouloir donner votre sang, tout en vous remerciant pour votre solidarité nous vous prions de revenir dans quelques jours afin que nous puissions mieux nous organiser', null), 
		   ('Stock AB+ suffisant', 'La croix rouge a pour l instant assez de donneurs AB+, revenez dans quelques semaines', 'AB+');
		   
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

INSERT INTO [dbo].[Information]
           ([Question]
           ,[Reponse])
     VALUES
           ('Qui peut donner son sang ?', 'Toute personne âgée de 18 à 65 ans révolus peut donner du sang. Le don de sang est également autorisé pour toute personne âgée de 66 ans à 70 ans révolus à condition que son dernier don remonte à moins de trois ans. Le premier don de sang doit avoir eu lieu avant le jour du 66ème anniversaire. Avant chaque don, on vérifie que le donneur est en condition pour donner du sang. Le médecin contrôle entre autres le poids qui doit être supérieur à 50 kilos, et la tension artérielle. Le donneur doit également remplir un questionnaire permettant au médecin de s''assurer de son bon état de santé.'), 
		   ('Déroulement du don de sang ?', 'Le prélèvement proprement dit dure de 5 à 10 minutes. Compte tenu de l''inscription, de l''examen médical et du temps de repos après le don, il faut prévoir une demi-heure environ.'),
		   ('Qui peut recevoir du sang ?', 'Avec une seule poche de sang, nous obtenons en réalité 3 produits différents : des globules rouges, des plaquettes et du plasma. Chacun de ces produits peut être nécessaire au patient en fonction de sa maladie ou de son traitement.\n D''une manière générale, les profils suivants peuvent avoir besoin de l''un ou l''autre de ces produits : \n \n Les accidentés de la route \n Les personnes subissant une opération chirurgicale\n Les personnes atteintes d''une leucémie\n Les hémophiles\n Les grands brûlés\n'), 
		   ('Pourquoi faut-il chaque fois remplir un questionnaire médical ?', 'Grâce au questionnaire et aux questions posées oralement par le personnel qualifié, il est possible de juger si l''état de santé actuel du donneur lui permet de donner son sang. Cela aussi bien dans l''intérêt du donneur lui-même que dans celui du receveur. C''est pourquoi le questionnaire comporte aussi des questions sur une éventuelle maladie récente, une opération, sur la possibilité de comportements à risque, etc.\n Des réponses correctes, précises et honnêtes à toutes ces questions sont indispensables pour la sécurité transfusionnelle."'), 
		   ('Quelle est la fréquence des dons ?', 'Toute personne en bonne santé peut donner du sang quatre fois par an, avec un intervalle de deux mois minimum entre chaque don de sang. \n Pour les dons de plasma, le délai à respecter est de deux semaines, avec un maximum de quinze litres par an. \n Pour les dons de plaquettes, il faut un intervalle de 2 semaines, et un maximum de 24 par an. \n Le délai à respecter entre un don de sang et un don de plasma/plaquettes est de 2 semaines.'),
		   ('Quelle quantité de sang donne-t-on ?', 'La quantité varie en fonction du donneur, sans jamais dépasser 500 millilitres. Le sang est récolté dans une poche contenant un liquide anticoagulant et de conservation.\n Les poches utilisées, en plastique, sont du matériel à usage unique, y compris l''aiguille, utilisé donc une seule fois, et détruit après le don par incinération. Tout risque de contamination est donc exclu d''office.'),
		   ('Doit-on être à jeûn ?', 'Certainement pas. Une légère collation est même souhaitable. Par contre, il faut s''abstenir de tout repas copieux afin de ne pas surcharger le sang en graisses.'),
		   ('Ce qui est prélevé, n''est-ce pas trop ?', 'Un adulte a entre 4 et 6 litres de sang selon son poids. Le prélèvement adapté au poids du donneur ne présente aucun inconvénient pour un adulte pesant au moins 50 kilos, et la quantité de sang prélevée est remplacée par l''organisme de manière à la compenser parfaitement.'),
		   ('Qu''est-ce que l''autotransfusion ?', 'Il s''agit de donner son sang pour soi-même, en prévision d''une opération programmée. Dans ce cas, veuillez en parler au médecin prescripteur (chirurgien, anesthésiste) ; si l''autotransfusion est une option éventuelle pour vous, il vous informera de la façon de procéder. Le médecin du Centre de Transfusion reste néanmoins seul juge de l''aptitude ou non du futur opéré à prédonner avant son intervention. La quantité de sang pouvant être donnée est cependant limitée. Si elle ne suffit pas lors de l''intervention, le médecin devra malgré tout recourir au sang d''un autre donneur.\n Depuis quelques années, l''autotransfusion a perdu de son engouement par suite d''une grande sécurité transfusionnelle et par les progrès des techniques chirurgicales permettant la diminution des besoins transfusionnels dans bon nombre d''interventions.'),
		   ('Quelles sont les analyses pratiquées ?', 'Tout d''abord, une série d''analyses qui concernent la recherche de maladies transmissibles par transfusion : hépatite B, hépatite C, SIDA et syphilis. S''y ajoutent la détermination du groupe sanguin, la numération des globules rouges, des globules blancs et des plaquettes sanguines.\n La recherche d''anticorps antimalariques est réalisée chez toute personne ayant séjourné en zone impaludée, après la mise en quarantaine de 6 mois.'),
		   ('Suis-je prévenu si une analyse n''est pas bonne ?', 'Lorsque les résultats sont normaux, il n''y a pas de notification au donneur. Par contre, en cas d''anomalie, le donneur est prévenu par téléphone ou par courrier. Dans certains cas plus particuliers, le donneur peut éventuellement être convoqué par le médecin du Centre de Transfusion pour lui en parler.'),
		   ('Est-ce que cela fait mal quand je donne du sang ?', 'Au moment de la piqûre, on sent à peine l''aiguille entrer dans la veine. Quant à la prise de sang elle-même, elle est tout à fait indolore.'),
		   ('Combien de temps faut-il au corps humain pour remplacer le sang prélevé ?', 'La perte de liquide est compensée en un jour. Il n''en faut pas beaucoup plus pour les globules blancs. Pour les globules rouges, la reconstitution est plus lente. Au bout de 6 à 8 semaines, le corps aura remplacé tout le sang donné. Mais on ne ressent rien de particulier pendant cette période.'),
		   ('Est-ce que le don influence la pression sanguine ?', 'La pression sanguine ou tension artérielle baisse très légèrement après un prélèvement, mais elle remonte et se stabilise vite.\n Le don de sang n''est donc pas un traitement de l''hypertension.'),
		   ('Est-ce que le prélèvement abime les veines ?', 'Non, tout au plus il peut y avoir une petite cicatrice à l''endroit où l''on pique habituellement.'),
		   ('Que devient le sang que je donne en cas d''anomalie ?', 'Si les analyses pratiquées révèlent un résultat anormal, le sang est détruit par incinération.'),
		   ('Quand utilise-t-on le sang ?', 'On a surtout besoin de sang pour les personnes qui en ont perdu beaucoup, en cas d''accident ou d''opération grave.\n Il faut également disposer de produits sanguins pour traiter les maladies qui affectent la fabrication du sang, par exemple dans le cas des leucémies.'),
		   ('Puis-je contracter une maladie en donnant du sang ?', 'NON. D''une part, avant chaque don, on vérifie soigneusement l''aptitude du donneur à faire don de son sang. D''autre part, il n''y a aucun risque de contamination puisqu''on utilise pour chaque don un matériel stérile et à usage unique, détruit par incinération après utilisation. Il en va de même pour les dons de plasma et de plaquettes.');