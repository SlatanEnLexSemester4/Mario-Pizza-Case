--CREATE Database MARIO;
CREATE TABLE "Address" (
 ID INT PRIMARY KEY IDENTITY,
 Streetname VARCHAR(100),
 Number VARCHAR(10),
 Zipcode VARCHAR(10),
 City VARCHAR(100),
 CountryCode VARCHAR(10)
);

CREATE TABLE PaymentMethod (
 ID INT PRIMARY KEY IDENTITY,
 "Name" VARCHAR(100) NOT NULL UNIQUE,
);

CREATE TABLE Municipality (
 MunicipalityCode DECIMAL PRIMARY KEY,
 "Name" VARCHAR(100)
);

CREATE TABLE Zipcode (
 Zipcode VARCHAR(10) PRIMARY KEY,
 SeriesIndex VARCHAR(100),
 BreakpointFrom DECIMAL,
 BreakpointTo DECIMAL,
 City VARCHAR(100),
 Streetname VARCHAR(100),
 MunicipalityCode DECIMAL REFERENCES Municipality(MunicipalityCode)
);

CREATE TABLE Store (
 ID INT PRIMARY KEY IDENTITY,
 Name VARCHAR(100),
 Streetname VARCHAR(100),
 Number VARCHAR(10),
 City VARCHAR(100),
 CountryCode VARCHAR(10),
 Zipcode VARCHAR(10) REFERENCES Zipcode(Zipcode),
 PhoneNumber VARCHAR(50)
);

CREATE TABLE Customer (
 ID INT PRIMARY KEY IDENTITY,
 Email VARCHAR(100) NOT NULL UNIQUE,
 "Name" VARCHAR(100),
 PhoneNumber VARCHAR(30),
 PaymentMethodID INT REFERENCES PaymentMethod(ID),
 StoreID INT REFERENCES Store(ID),
 "Password" VARCHAR(100),
 Newsletter BIT,
 Sms BIT
);

CREATE TABLE AddressCustomer (
 ID INT PRIMARY KEY IDENTITY,
 AddressID INT REFERENCES "Address"(ID),
 CustomerID INT REFERENCES Customer(CustomerID),
);

CREATE TABLE Voucher (
 ID INT PRIMARY KEY IDENTITY,
 VoucherCode VARCHAR(100),
 "Description" VARCHAR(500),
 IssueDate DATE,
 ExpirationDate DATE
);

CREATE TABLE "Order" (
 ID INT PRIMARY KEY IDENTITY,
 CustomerID INT REFERENCES Customer(ID),
 StoreID INT REFERENCES Store(ID),
 Delivery BIT,
 AddressID INT REFERENCES Address(ID),
 DeliveryTime DATETIME,
 DeliveryCost SMALLMONEY,
 VoucherID INT REFERENCES Voucher(ID),
 Tip SMALLMONEY,
 TotalPrice SMALLMONEY,
 PaymentMethodID INT REFERENCES PaymentMethod(ID)
);

CREATE TABLE OrderItem (
 ID INT PRIMARY KEY IDENTITY,
 OrderID INT REFERENCES "Order"(ID),
 Quantity INT,
 Price SMALLMONEY
);

CREATE TABLE PizzaCrust (
 ID INT PRIMARY KEY IDENTITY,
 "Name" VARCHAR(100),
 Diamater VARCHAR(10),
 "Description" VARCHAR(500),
 Fee SMALLMONEY,
 "Availability" BIT
);

CREATE TABLE IngredientCategory (
 ID INT PRIMARY KEY IDENTITY,
 "Name" VARCHAR(100)
);

CREATE TABLE Ingredient (
 ID INT PRIMARY KEY IDENTITY,
 IngredientCategoryID INT REFERENCES IngredientCategory(ID),
 "Name" VARCHAR(100),
 "Description" VARCHAR(500),
 Price SMALLMONEY,
 Spicy BIT,
 Vegetarian BIT
);

CREATE TABLE ProductCategory (
 ID INT PRIMARY KEY IDENTITY,
  "Name" VARCHAR(100)
);

CREATE TABLE Product (
 ID INT PRIMARY KEY IDENTITY,
 ProductCategoryID INT REFERENCES ProductCategory(ID),
 "Name" VARCHAR(100),
 "Description" VARCHAR(500),
 Price SMALLMONEY,
 Spicy BIT,
 Vegetarian BIT
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
 IngredientID INT REFERENCES Ingredient(ID),
 PizzaCrustID INT REFERENCES PizzaCrust(ID)
);
