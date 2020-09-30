--CREATE DATABASE MarioDB;

CREATE TABLE "Address" (
 ID INT PRIMARY KEY IDENTITY,
 Streetname VARCHAR(100),
 "Number" VARCHAR(10),
 Zipcode VARCHAR(10),
 City VARCHAR(100),
 CountryCode VARCHAR(5)
);

CREATE TABLE PaymentMethod (
 ID INT PRIMARY KEY IDENTITY,
 "Name" VARCHAR(50) NOT NULL UNIQUE,
);

CREATE TABLE Municipality (
 MunicipalityCode INT PRIMARY KEY IDENTITY,
 "Name" VARCHAR(50)
);

CREATE TABLE Zipcode (
 Zipcode VARCHAR(10) PRIMARY KEY,
 SeriesIndex VARCHAR(10),
 BreakpointFrom DECIMAL,
 BreakpointTo DECIMAL,
 City VARCHAR(100),
 Streetname VARCHAR(100),
 MunicipalityCode INT REFERENCES Municipality(MunicipalityCode)
);

CREATE TABLE Store (
 ID INT PRIMARY KEY IDENTITY,
 "Name" VARCHAR(50),
 Streetname VARCHAR(100),
 Number VARCHAR(10),
 City VARCHAR(100),
 CountryCode VARCHAR(5),
 Zipcode VARCHAR(10) REFERENCES Zipcode(Zipcode),
 PhoneNumber VARCHAR(20)
);

CREATE TABLE Customer (
 ID INT PRIMARY KEY IDENTITY,
 Email VARCHAR(50) NOT NULL UNIQUE,
 "Name" VARCHAR(50),
 PhoneNumber VARCHAR(20),
 PaymentMethodID INT REFERENCES PaymentMethod(ID),
 StoreID INT REFERENCES Store(ID),
 "Password" CHAR(64),
 Newsletter BIT,
 Sms BIT
);

CREATE TABLE AddressCustomer (
 ID INT PRIMARY KEY IDENTITY,
 AddressID INT REFERENCES "Address"(ID),
 CustomerID INT REFERENCES Customer(ID),
);

CREATE TABLE ProductCategory (
 ID INT PRIMARY KEY IDENTITY,
  "Name" VARCHAR(50)
);

CREATE TABLE VoucherType (
 ID INT PRIMARY KEY IDENTITY,
 "Name" VARCHAR(50),
 "Description" VARCHAR(500)
);

CREATE TABLE Voucher (
 ID INT PRIMARY KEY IDENTITY,
 VoucherCode VARCHAR(50),
 "Type" INT REFERENCES VoucherType(ID),
 Amount DECIMAL,
 Category INT REFERENCES ProductCategory(ID),
 MinimumOrderAmount INT,
 IssueDate DATE,
 ExpirationDate DATE
);

CREATE TABLE Tax (
 ID INT PRIMARY KEY IDENTITY,
 Rate DECIMAL,
 ValidFrom DATE,
 ValidTill DATE
);

CREATE TABLE "Order" (
 ID INT PRIMARY KEY IDENTITY,
 CustomerID INT REFERENCES Customer(ID),
 StoreID INT REFERENCES Store(ID),
 OrderTime DATE,
 Delivery BIT,
 AddressID INT REFERENCES Address(ID),
 DeliveryTime DATETIME,
 DeliveryCost DECIMAL,
 VoucherID INT REFERENCES Voucher(ID),
 Tip DECIMAL,
 TotalPrice DECIMAL,
 PaymentMethodID INT REFERENCES PaymentMethod(ID),
 TaxID INT REFERENCES Tax(ID)
);

CREATE TABLE OrderItem (
 ID INT PRIMARY KEY IDENTITY,
 OrderID INT REFERENCES "Order"(ID),
 Quantity INT,
 Price DECIMAL
);

CREATE TABLE PizzaCrustProperties (
 ID INT PRIMARY KEY IDENTITY,
 Diameter VARCHAR(10),
 "Description" VARCHAR(500),
 Fee DECIMAL,
 "Availability" BIT,
 VersionValidFrom DATE,
 VersionValidTill DATE
);

CREATE TABLE PizzaCrust (
 ID INT PRIMARY KEY IDENTITY,
 PizzaCrustPropertiesID INT REFERENCES PizzaCrustProperties(ID),
 "Name" VARCHAR(50)
);

CREATE TABLE IngredientCategory (
 ID INT PRIMARY KEY IDENTITY,
 "Name" VARCHAR(100)
);

CREATE TABLE IngredientProperties (
 ID INT PRIMARY KEY IDENTITY,
 IngredientCategoryID INT REFERENCES IngredientCategory(ID),
 "Description" VARCHAR(500),
 Price DECIMAL,
 Spicy BIT,
 Vegetarian BIT,
 ValidFrom DATE,
 ValidTill DATE
);

CREATE TABLE Ingredient (
 ID INT PRIMARY KEY IDENTITY,
 IngredientPropertiesID INT REFERENCES IngredientProperties(ID),
 "Name" VARCHAR(50)
);

CREATE TABLE ProductProperties (
 ID INT PRIMARY KEY IDENTITY,
 ProductCategoryID INT REFERENCES ProductCategory(ID),
 "Description" VARCHAR(500),
 Price DECIMAL,
 Spicy BIT,
 Vegetarian BIT,
 ValidFrom DATE,
 ValidTill DATE
);

CREATE TABLE Product (
 ID INT PRIMARY KEY IDENTITY,
 ProductPropertiesID INT REFERENCES ProductProperties(ID),
 "Name" VARCHAR(50),
);

CREATE TABLE PizzaRecipe (
 ID INT PRIMARY KEY IDENTITY,
 ProductID INT REFERENCES Product(ID),
 IngredientID INT REFERENCES Ingredient(ID)
);

CREATE TABLE Item (
 ID INT PRIMARY KEY IDENTITY,
 OrderItemID INT REFERENCES OrderItem(ID),
 ProductID INT REFERENCES Product(ID),
 ProductPropertiesID INT REFERENCES ProductProperties(ID),
 IngredientID INT REFERENCES Ingredient(ID),
 IngredientPropertiesID INT REFERENCES IngredientProperties(ID),
 PizzaCrustID INT REFERENCES PizzaCrust(ID),
 PizzaCrustPropertiesID INT REFERENCES PizzaCrustProperties(ID)
);
