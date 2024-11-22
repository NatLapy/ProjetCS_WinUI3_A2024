
/*Projet de session 
William Lafrance et Louka Beaudoin 
2024-11-22*/


/*Création des tables*/

CREATE TABLE Categories(
    idCategorie INT NOT NULL,
    nom VARCHAR(30),
    CONSTRAINT PK_Categories PRIMARY KEY (idCategorie)
);

CREATE TABLE Activites (
    idActivite INT NOT NULL ,
    nom VARCHAR(30) NOT NULL ,
    coutOrganisation INT NOT NULL,
    prixDeVente INT NOT NULL,
    idAdmin INT NOT NULL ,
    idCategorie INT NOT NULL,
    CONSTRAINT PK_Activite PRIMARY KEY (idActivite),
    CONSTRAINT FK_activites_administrateurs  FOREIGN KEY (idAdmin) REFERENCES administrateurs (idAdmin),
    CONSTRAINT FK_activites_categories FOREIGN KEY (idCategorie) REFERENCES Categories (idCategorie)
);

CREATE TABLE Seances (
    idSeances INT NOT NULL ,
    dateOrganisation DATE NOT NULL ,
    nbPlaceDispo INT NOT NULL ,
    idActivite INT NOT NULL ,
    CONSTRAINT PK_Seance PRIMARY KEY (idSeances),
    CONSTRAINT FK_Seances_activites FOREIGN KEY (idActivite) REFERENCES Activites (idActivite)

);

CREATE TABLE Administrateurs (
idAdmin int AUTO_INCREMENT,
nomUtilisateur varchar(50) NOT NULL,
motDePasse varchar(50) NOT NULL,
PRIMARY KEY (idAdmin)
);
 
 
CREATE TABLE Adherents (
noIdentification varchar(50) NOT NULL,
nom varchar(50) NOT NULL,
prenom varchar(50) NOT NULL,
adresse varchar(100) NOT NULL,
dateNaissance date NOT NULL,
age int NOT NULL,
idAdmin int NOT NULL,
PRIMARY KEY (noIdentification),
FOREIGN KEY (idAdmin) REFERENCES Administrateurs(idAdmin)
);
 
 
CREATE TABLE Inscription (
noIdentificationAdherent varchar(50) NOT NULL,
idSeance int ,
noteAppreciation int NOT NULL,
CONSTRAINT PK_Inscription PRIMARY KEY (noIdentificationAdherent,idSeance),
FOREIGN KEY (noIdentificationAdherent) REFERENCES Adherents(noIdentification),
FOREIGN KEY (idSeance) REFERENCES seances(idSeances)
);

/*Création des Triggers*/

-- Trigger pour l'id adhérent
DELIMITER //
CREATE TRIGGER Creation_IdAdherent before insert
on adherents
for each row
begin
set NEW.noIdentification=CONCAT(SUBSTRING(NEW.prenom,1,1),SUBSTRING(NEW.nom,1,1),'-',YEAR(NEW.dateNaissance),'-',ROUND(RAND()*10),ROUND(RAND()*10),ROUND(RAND()*10));
end//
delimiter ;



-- Erreur si nombre de place disponilbe 0
DELIMITER //
CREATE TRIGGER Ajout_seances_Place_Maximum before insert
on seances
for each row
begin
if NEW.nbPlaceDispo <= 0 THEN
SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT ='Il ny a plus de pace disponible pour cette séance.';
end if ;
end//
delimiter ;


--décrémente le nombre de place lors d'un inscripttion.
DELIMITER //
CREATE TRIGGER miseAJourSeance
AFTER INSERT
ON inscription
FOR EACH ROW
BEGIN
    UPDATE seances
    SET nbPlaceDispo = nbPlaceDispo -1
    WHERE idSeances = NEW.idSeance;
END //
DELIMITER ;

/*Insertion*/

INSERT INTO Administrateurs (nomUtilisateur, motDePasse) VALUES
('admin1', 'password1'),
('admin2', 'password2'),
('admin3', 'password3'),
('admin4', 'password4'),
('admin5', 'password5'),
('admin6', 'password6'),
('admin7', 'password7'),
('admin8', 'password8'),
('admin9', 'password9'),
('admin10', 'password10');

INSERT INTO Categories (idCategorie, nom) VALUES
(1, 'Escalade'),
(2, 'Raquette'),
(3, 'Cirque'),
(4, 'Tricot'),
(5, 'Rock acrobatique'),
(6, 'Parapente'),
(7, 'Yoga'),
(8, 'Danse'),
(9, 'Parkour'),
(10, 'Randonnée');

INSERT INTO Activites (idActivite, nom, coutOrganisation, prixDeVente, idAdmin, idCategorie) VALUES
(1, 'Escalade en salle', 500, 20, 1, 1),
(2, 'Raquette sur neige', 200, 30, 2, 2),
(3, 'Spectacle de cirque', 1000, 50, 3, 3),
(4, 'Cours de tricot', 150, 10, 4, 4),
(5, 'Rock acrobatique', 800, 40, 5, 5),
(6, 'Parapente dans les Alpes', 1000, 150, 6, 6),
(7, 'Yoga pour débutants', 100, 15, 7, 7),
(8, 'Danse contemporaine', 300, 25, 8, 8),
(9, 'Parkour urbain', 400, 20, 9, 9),
(10, 'Randonnée en montagne', 200, 35, 10, 10);

INSERT INTO Seances (idSeances, dateOrganisation, nbPlaceDispo, idActivite) VALUES
(1, '2024-12-01', 20, 1),
(2, '2024-12-05', 15, 2),
(3, '2024-12-10', 20, 3),
(4, '2024-12-12', 10, 4),
(5, '2024-12-15', 12, 5),
(6, '2024-12-20', 8, 6),
(7, '2024-12-22', 20, 7),
(8, '2024-12-25', 25, 8),
(9, '2024-12-30', 12, 9),
(10, '2025-01-02', 15, 10);

INSERT INTO Adherents (noIdentification, nom, prenom, adresse, dateNaissance, age, idAdmin) VALUES
('A001', 'Dupont', 'Jean', '1 rue de Paris, 75001 Paris', '1990-05-15', 34, 1),
('A002', 'Martin', 'Sophie', '2 avenue de la République, 75002 Paris', '1985-10-20', 39, 2),
('A003', 'Durand', 'Paul', '3 boulevard Saint-Germain, 75003 Paris', '1992-03-12', 32, 3),
('A004', 'Lemoine', 'Claire', '4 place de la Concorde, 75004 Paris', '1988-07-25', 36, 4),
('A005', 'Perrin', 'Luc', '5 rue de la Liberté, 75005 Paris', '1995-11-02', 29, 5),
('A006', 'Robert', 'Julie', '6 rue de l’Église, 75006 Paris', '1991-01-17', 33, 6),
('A007', 'Benoit', 'Marc', '7 rue de la République, 75007 Paris', '1993-02-28', 31, 7),
('A008', 'Gauthier', 'Eva', '8 rue du Bac, 75008 Paris', '1987-09-10', 37, 8),
('A009', 'Briand', 'Nicolas', '9 rue de la Gare, 75009 Paris', '1994-04-05', 30, 9),
('A010', 'Dufresne', 'Alice', '10 rue des Champs, 75010 Paris', '1990-12-30', 34, 10);


INSERT INTO Inscription (noIdentificationAdherent, idSeance, noteAppreciation) VALUES
('AD-1990-323', 1, 5),
('CL-1988-6100', 2, 4),
('EG-1987-845', 3, 3),
('JD-1990-451', 4, 5),
('JR-1991-265', 5, 4),
('LP-1995-205', 6, 3),
('MB-1993-642', 7, 5),
('NB-1994-641', 8, 4),
('PD-1992-620', 9, 5),
('SM-1985-109', 10, 4);


/*Procedure*/

/*permet d'Ajouter un adherent seulement si il est agé de plus de 18 ans */
DELIMITER //
create procedure p_ajout_Adherent( _nom VARCHAR(30) ,_prenom VARCHAR(30) , _adresse VARCHAR(30), _dateNaissance DATE , _age INT , _idAdmin INT)
BEGIN
IF (_age < 18 ) THEN
    SIGNAL SQLSTATE '45000' SET message_text="l'age doit etre supérieur a 18.";
END IF;
INSERT INTO adherents (nom, prenom, adresse, dateNaissance, age, idAdmin) VALUES ( _nom , _prenom , _adresse , _dateNaissance , _age , _idAdmin);
END //
DELIMITER ;

CALL p_ajout_Adherent('william' , 'lafrance' , '100' , '2000-12-12' , 17 , 2);


/*Calcul la moyenne des note d'appreciation par sceances */
DELIMITER //
create procedure p_calculMoyenneNoteAppreciationParSeance(IN _idSeance INT )
BEGIN
     SELECT ROUND(AVG(noteAppreciation))AS moyenne FROM inscription WHERE idSeance = _idSeance ;
END //
DELIMITER ;

CALL p_calculMoyenneNoteAppreciationParSeance(2);

/*Calcul le nombre de place disponible pour une séance a partir du id de la sceance.*/

DELIMITER //
CREATE PROCEDURE p_nombrePlaceDispo(IN _idSeance INT )
BEGIN
     SELECT nbPlaceDispo FROM seances WHERE idSeances = _idSeance;
END //
DELIMITER ;


call p_nombrePlaceDispo(2);

/*procedure qui permet d'ajouter une activité */

DELIMITER //
create procedure p_ajout_Activite(_idActivite INT , _nom VARCHAR(30) ,_coutOrganisation INT ,_prixVente INT, _idAdmin INT , _IDCategorie INT )
BEGIN

INSERT INTO activites VALUES (_idActivite ,_nom , _coutOrganisation , _prixVente , _idAdmin , _IDCategorie);
END //
DELIMITER ;

CALL p_ajout_Activite(11 , 'escalade en sandale' , 1000 , 1200 , 2 , 1 );
