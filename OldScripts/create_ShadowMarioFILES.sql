--create database ShadowMarioFILES;

CREATE TABLE ExtraIngredienten
(
Ingredient VARCHAR(50),
ExtraPrice DECIMAL
);

CREATE TABLE OverigeProducten
(
Categorie VARCHAR(50),
Subcategorie VARCHAR(50),
Productnaam VARCHAR(50),
Productomschrijving VARCHAR(500),
Prijs DECIMAL,
Spicy BIT,
Vegetarisch BIT
);

CREATE TABLE PizzaIngredienten
(
Categorie VARCHAR(50),
Subcategorie VARCHAR(50),
Productnaam VARCHAR(50),
Productomschrijving VARCHAR(500),
Prijs DECIMAL,
Bezorgtoeslag DECIMAL,
Spicy BIT,
Vegetarisch BIT,
Beschikbaar BIT,
AantaalkeerIngredient INT,
Ingredientnaam VARCHAR(50),
PizzasausStandaard VARCHAR(50)
);

CREATE TABLE PizzaBodems
(
Naam VARCHAR(50),
Diameter VARCHAR(10),
Omschrijving VARCHAR(500),
Toeslag DECIMAL,
Beschikbaar BIT
);

CREATE TABLE Postcodes
(
Postcode VARCHAR(10),
Reeksind VARCHAR(10),
Breekpunt_van DECIMAL,
Breekpunt_tem DECIMAL,
Woonplaats VARCHAR(100),
Straatnaam VARCHAR(100),
Gemeentecode INT
);

CREATE TABLE Gemeenten
(
Kode INT,
Naam VARCHAR(50)
);

CREATE TABLE Winkels
(
Naam VARCHAR(100),
Straat VARCHAR(100),
Nummer VARCHAR(10),
Plaats VARCHAR(100),
LandKode VARCHAR(5),
Postcode VARCHAR(10),
Telefoon VARCHAR(20)
);

CREATE TABLE MarioOrderData
(
Winkelnaam VARCHAR(100),
Klantnaam VARCHAR(50),
Telefoon VARCHAR(20),
Email VARCHAR(50),
Adres VARCHAR(100),
Woonplaats VARCHAR(100),
Besteldatum DATE,
AfleverType BIT,
AfleverDatum DATE,
AfleverMoment DATETIME,
Product VARCHAR(50),
PizzaBodem VARCHAR(50),
PizzaSaus VARCHAR(50),
Prijs DECIMAL,
Bezorgkosten DECIMAL,
Aantal INT,
ExtraIngredienten VARCHAR(100),
PrijsExtraIngredient DECIMAL,
Regelprijs DECIMAL,
Totaalprijs DECIMAL,
GebruikteCoupon VARCHAR(100),
CouponKorting DECIMAL,
TeBetalen DECIMAL
);


