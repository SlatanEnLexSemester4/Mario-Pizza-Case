ALTER PROCEDURE spImportIntoMarioDB
AS
BEGIN
SET IDENTITY_INSERT Municipality ON
--SET IDENTITY_INSERT Zipcode ON
------------------------------------------------------
	INSERT INTO dbo.Municipality
	(
	MunicipalityCode,
	"Name"
	)
	SELECT Kode, Naam
	FROM ShadowDB.dbo.Gemeenten
------------------------------------------------------
	INSERT INTO dbo.Zipcode
	(
	Zipcode,
	SeriesIndex,
	BreakpointFrom,
	BreakpointTo,
	City,
	Streetname,
	MunicipalityCode
	)
	SELECT Postcode, Reeksind, Breekpunt_van, Breekpunt_tem, Woonplaats,Straatnaam,Gemeentecode
	FROM ShadowDB.dbo.Postcodes
------------------------------------------------------
	INSERT INTO dbo.VoucherType
	(
	"Name"
	)
	SELECT GebruikteCoupon
	FROM ShadowDB.dbo.MarioOrderData
------------------------------------------------------
	INSERT INTO dbo.ProductCategory
	(
	"Name"
	)
	SELECT Categorie
	FROM ShadowDB.dbo.PizzaIngredienten
------------------------------------------------------
	INSERT INTO dbo.Voucher
	(
	Amount
	)
	SELECT CouponKorting
	FROM ShadowDB.dbo.MarioOrderData
------------------------------------------------------
	INSERT INTO dbo.PizzaCrustProperties
	(
	Diameter,
	"Description",
	Fee,
	"Availability"
	)
	SELECT Diameter, Omschrijving, Toeslag, Beschikbaar
	FROM ShadowDB.dbo.PizzaBodems
------------------------------------------------------
	INSERT INTO PizzaCrust
	(
	"Name"
	)
	SELECT Naam
	FROM ShadowDB.dbo.PizzaBodems
------------------------------------------------------
	INSERT INTO IngredientProperties
	(
	Price
	)
	SELECT ExtraPrice
	FROM ShadowDB.dbo.ExtraIngredienten
	INNER JOIN ShadowDB.dbo.PizzaIngredienten ON ShadowDB.dbo.ExtraIngredienten.Ingredient = ShadowDB.dbo.PizzaIngredienten.Productnaam
------------------------------------------------------
	INSERT INTO IngredientProperties
	(
	Spicy,
	Vegetarian
	)
	SELECT Spicy, Vegetarisch
	FROM ShadowDB.dbo.ExtraIngredienten
	INNER JOIN ShadowDB.dbo.PizzaIngredienten ON ShadowDB.dbo.ExtraIngredienten.Ingredient = ShadowDB.dbo.PizzaIngredienten.Productnaam
------------------------------------------------------
	INSERT INTO Ingredient
	(
	"Name"
	)
	SELECT Ingredient
	FROM ShadowDB.dbo.ExtraIngredienten
------------------------------------------------------
	INSERT INTO Customer
	(
	Email,
	"Name",
	PhoneNumber
	)
	SELECT Email, Klantnaam, Telefoon
	FROM ShadowDB.dbo.MarioOrderData
------------------------------------------------------
	INSERT INTO "Order"
	(
	OrderTime,
	Delivery,
	DeliveryTime,
	DeliveryCost
	)
	SELECT Besteldatum, AfleverType, AfleverDatum, AfleverMoment, Bezorgkosten
	FROM ShadowDB.dbo.MarioOrderData
-----------------------------------------------------
	INSERT INTO ProductProperties
	(
	"Description",
	Price,
	Spicy,
	Vegetarian
	)
	SELECT Productomschrijving, Prijs, Spicy, Vegetarisch
	FROM ShadowDB.dbo.OverigeProducten
-----------------------------------------------------
	INSERT INTO ProductProperties
	(
	"Description",
	Price,
	Spicy,
	Vegetarian
	)
	SELECT Productomschrijving, Prijs, Spicy, Vegetarisch
	FROM ShadowDB.dbo.PizzaIngredienten
-----------------------------------------------------
	INSERT INTO "Address"
	(
	Streetname,
	Number,
	City
	)
	SELECT TRIM('0123456789' FROM Adres), dbo.udf_GetNumeric(Adres), Woonplaats
	FROM ShadowDB.dbo.MarioOrderData
-----------------------------------------------------
	INSERT INTO StoreAddress
	(
	StreetName,
	Number,
	Zipcode,
	City,
	CountryCode
	)
	SELECT Straat, Nummer, Postcode, Plaats, LandKode
	FROM ShadowDB.dbo.Winkels
-----------------------------------------------------
	INSERT INTO Store
	(
	"Name",
	PhoneNumber
	)
	SELECT Naam, Telefoon
	FROM ShadowDB.dbo.Winkels
-----------------------------------------------------

SET IDENTITY_INSERT Municipality ON
--SET IDENTITY_INSERT Zipcode ON
END;