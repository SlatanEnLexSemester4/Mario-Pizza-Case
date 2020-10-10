create PROCEDURE spImportIntoMarioDB
AS
BEGIN
--SET IDENTITY_INSERT Municipality ON

------------------------------------------------------
	INSERT INTO dbo.Municipality
	(
	MunicipalityCode,
	"Name"
	)
	SELECT Kode = cast(Kode as int), Naam
	FROM ShadowDB.dbo.Gemeenten
------------------------------------------------------
	INSERT INTO dbo.Zipcode
	(
	Zipcode,
	SeriesIndex,
	BreakpointFrom ,
	BreakpointTo,
	City,
	Streetname,
	MunicipalityCode
	)
	SELECT Postcode, Reeksind = cast(Reeksind as int), 
	Breekpunt_van = cast(Breekpunt_van as int), 
	Breekpunt_tem = cast(Breekpunt_tem as int), 
	Woonplaats,
	Straatnaam, 
	Gemeeentecode = cast(Gemeeentecode as int)
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
	SELECT CouponKorting = TRY_PARSE(CouponKorting AS decimal(10,2) USING 'El-GR')
	FROM ShadowDB.dbo.MarioOrderData
------------------------------------------------------
	INSERT INTO dbo.PizzaCrustProperties
	(
	Diameter,
	"Description",
	Fee,
	"Availability"
	)
	SELECT Diameter, Omschrijving, Toeslag = cast(Toeslag as decimal(10,2)), CASE WHEN Beschikbaar = 'True' THEN CAST('True' as bit) ELSE CAST('False' as bit) END
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
	SELECT ExtraPrice = cast(ExtraPrice as decimal(10,2))
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
	FROM ShadowDB.dbo.MarioOrderData WHERE Email != ShadowDB.dbo.MarioOrderData.Email
------------------------------------------------------
	INSERT INTO "Order"
	(
	OrderTime,
	Delivery,
	DeliveryDate,
	--DeliveryTime,
	DeliveryCost
	)
	SELECT Besteldatum, /*= convert(date, Besteldatum, 3),*/ CASE WHEN AfleverType = '0' THEN CAST('0' as bit) ELSE CAST('1' as bit) END, 
	AfleverDatum /*= convert(date, AfleverDatum, 3)*/, 
	--AfleverMoment = try_convert(time(0), AfleverMoment, 0),
	Bezorgkosten = try_parse(Bezorgkosten as decimal(10,2) USING 'El-GR')
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
	HouseNumber,
	City,
	CountryCode
	)
	SELECT TRIM('0123456789' FROM Adres), dbo.udf_GetNumeric(Adres), Woonplaats, 'NL' 
	FROM ShadowDB.dbo.MarioOrderData
	WHERE ShadowDB.dbo.MarioOrderData.Woonplaats != ' '
-----------------------------------------------------
	
	INSERT INTO "Address"
	(
	Zipcode
	)
	SELECT ID =
	
	CASE
	WHEN (cast((select HouseNumber from "Address") as int) % 2 <> 0)
		THEN
		(
		SELECT ID FROM dbo.Zipcode 
		WHERE
		(
		(City = (select City from dbo.Zipcode) AND Streetname = (select Streetname from dbo.Zipcode))
		AND (dbo.Zipcode.BreakpointFrom % 2 <> 0
		AND (cast((select HouseNumber from "Address") as int) >= (select BreakpointFrom from Zipcode) AND cast((select HouseNumber from "Address") as int) <= (select BreakpointTo from Zipcode)))
		)
		)
	ELSE 
		(
		SELECT ID FROM dbo.Zipcode 
		WHERE 		
		(
		(City = (select City from dbo.Zipcode) AND Streetname = (select Streetname from dbo.Zipcode))
		AND (dbo.Zipcode.BreakpointFrom % 2 = 0
		AND (cast((select HouseNumber from "Address") as int) >= (select BreakpointFrom from Zipcode) AND cast((select HouseNumber from "Address") as int) <= (select BreakpointTo from Zipcode)))
		)
		)
	END	
	FROM dbo.Zipcode

-----------------------------------------------------
	INSERT INTO StoreAddress
	(
	StreetName,
	Number,
	City,
	CountryCode
	)
	SELECT Straat, Nummer, 
	Plaats, LandCode
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

--SET IDENTITY_INSERT Municipality ON
--SET IDENTITY_INSERT Zipcode ON
END;