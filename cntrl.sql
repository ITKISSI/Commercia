create database commercia

use commercia

create table client
(
Num_C smallint primary key identity,
Nom varchar(50),
Prenom varchar(50),
Telephone varchar(20),
Email varchar(50),
Ville varchar(20),
Adresse varchar(50)
)

create table fournisseur
(
Num_F smallint primary key identity,
Nom_Entreprise varchar(50),
Telephone varchar(20),
Email varchar(50),
Ville varchar(20),
Adresse varchar(50)
)

create table type_produit
(
Libelle varchar(50) primary key
)

create table produit
(
Num_P smallint primary key identity,
Libelle varchar(50),
Type_P varchar(50) foreign key references type_produit(Libelle),
Prix float,
Image_Nom varchar(20),
Image_P image,
Description_P varchar(max),
Quantite int
)

create table utilisateur
(
num_utl smallint identity primary key,
Nom varchar(50),
Prenom varchar(50),
Email varchar(50),
Mot_De_Passe varchar(50),
Type_U varchar(50) check( Type_U='gérant' or Type_U='vendeur' or Type_U='magasinier' )
)

create table commande_vente
(
Num_C_V smallint primary key identity,
Date_C datetime,
Client smallint foreign key references client(Num_C)
)

create table ligne_commande_vente
(
Commande_V smallint foreign key references commande_vente(Num_C_V),
Produit smallint foreign key references produit(Num_P),
Quantite int,
primary key(Commande_V,Produit)
)

create table commande_achat
(
Num_C_A smallint primary key identity,
Date_C datetime,
Fournisseur smallint foreign key references fournisseur(Num_F)
)

create table ligne_commande_achat
(
Commande_A smallint foreign key references commande_achat(Num_C_A),
Produit smallint foreign key references produit(Num_P),
Quantite int
primary key(Commande_A,Produit)
)


--client
select * from client  
insert into client values ('kissi','abdelaziz','06XXXXXXX','kissi@XXXX.com','oujda','hay el mohammadi'),
                          ('tahiri','rajae','06XXXXXXX','tahiri@XXXX.com','oujda','Qods'),
                          ('nouri','houssam','06XXXXXXX','nouri@XXXX.com','nador','hay el mohammadi')

 

--type_produit
select * from type_produit  
insert into type_produit values ('smart phone'),('ordinateur'),('smart TV'),('smart speaker'),('tablette')

 

--produit
select * from produit  
insert into produit values ('samsunge','smart phone',1000,'samsunge.jpeg','xxxxx',10),
                           ('HP','ordinateur',4000,'hp.png','xxxxx',5),
                           ('LG','smart TV',1500,'lg.jpg','xxxxx',9),
                           ('JBL','smart speaker',700,'jbl.jpg','xxxxx',20),
                           ('samsungetab','tablette',2000,'samsungetab.jpg','xxxxx',10)

 


--fournisseur
select * from fournisseur
insert into fournisseur values('F1','XXXXXX','F1XXX@.COM','NY','USA'),
                              ('F2','XXXXXX','F2XXX@.COM','LA','USA'),
                              ('F3','XXXXXX','F3XXX@.COM','Paris','FRANCE')

 

--commande_achat
select * from commande_achat
insert into commande_achat values('05/02/2021',1),
                                 ('05/03/2021',4),
                                 ('05/05/2021',3)
--ligne commande_achat
select * from ligne_commande_achat
insert into ligne_commande_achat values(5,1,10),
                                       (6,4,20),
                                        (7,5,10)

 

--commande_vente
select * from commande_vente
insert into commande_vente values('15/02/2021',4),
                                 ('25/03/2021',5),
                                 ('30/05/2021',6)

 

--ligne commande_vente
select * from ligne_commande_vente
insert into ligne_commande_vente values(4,1,1),
                                       (5,2,3),
                                       (6,3,2)

go
create or alter proc bon_livraison(@id int)as
begin
select cl.Nom,cl.Prenom,cl.Telephone,cl.Adresse,cl.Ville,fr.Nom_Entreprise,fr.Adresse_F,
fr.Ville_F,pr.Num_P,cv.Date_C,cv.Num_C_V,cv.Client,pr.Libelle,pr.Description_P,lcv.Quantite from client cl 
join commande_vente cv on cv.Client=cl.Num_C 
join ligne_commande_vente lcv on lcv.Commande_V=cv.Num_C_V 
join produit pr on pr.Num_P=lcv.Produit
join ligne_commande_achat lca on lca.Produit=pr.Num_P
join commande_achat ca on ca.Num_C_A=lca.Commande_A
join fournisseur fr on fr.Num_F=ca.Fournisseur
where cl.Num_C=@id
end
go
create or alter proc bon_command(@id int)as
begin
select fr.Nom_Entreprise,fr.Email_F,fr.Telephone_F,fr.Adresse_F,lca.Quantite*pr.prix as 'Montan',
fr.Ville_F,pr.Libelle,pr.Description_P,lca.Quantite,pr.prix from  produit pr 
join ligne_commande_achat lca on lca.Produit=pr.Num_P
join commande_achat ca on ca.Num_C_A=lca.Commande_A
join fournisseur fr on fr.Num_F=ca.Fournisseur
where fr.Num_F=@id
end
go
create or alter proc bon_FACTURE(@id int)as
begin
select cl.Nom,cl.Prenom,cl.Telephone,cl.Adresse,cl.Ville,cl.Email,pr.Num_P,pr.Libelle,pr.Description_P,lcv.Quantite,pr.prix,(lcv.Quantite*pr.prix) as 'Montan' from client cl 
join commande_vente cv on cv.Client=cl.Num_C 
join ligne_commande_vente lcv on lcv.Commande_V=cv.Num_C_V 
join produit pr on pr.Num_P=lcv.Produit

where cl.Num_C=@id
end
exec bon_command Num_F

--trigger for Incrémentation automatique du stock produit après chaque commande d’achat
go
create or alter trigger Incrémentation on ligne_commande_achat for insert , update 
as
begin
declare  @id int=(select produit from inserted)
declare  @qt int=(select Quantite from inserted)
update produit set Quantite+=@qt where Num_P=@id
end
-- trigger for décrémentation après chaque commande de vente
go
create or alter trigger décrémentation on ligne_commande_vente for insert , update 
as
begin
declare  @id int=(select produit from inserted)
declare  @qt int=(select Quantite from inserted)
update produit set Quantite-=@qt where Num_P=@id
end

--trigger pour supprimer tous les command achte une fois supprimer la lign
go
create or alter trigger supprimer_tous_les_command on commande_achat instead of delete
as
begin
declare  @id int=(select Num_C_A from deleted)
delete from ligne_commande_achat where Commande_A=@id
delete from commande_achat where Num_C_A=@id
end