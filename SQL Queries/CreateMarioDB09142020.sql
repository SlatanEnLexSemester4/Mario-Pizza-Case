--CREATE Database MARIO;
CREATE TABLE "Address" (
 AddressID INT PRIMARY KEY,
 Streetname VARCHAR,
 Number VARCHAR,
 Zipcode VARCHAR,
 City VARCHAR,
 CountryCode VARCHAR
);CREATE TABLE PaymentMethod (
 PaymentMethodID INT PRIMARY KEY,
 "Name" VARCHAR NOT NULL UNIQUE,
);
CREATE TABLE Municipality (
 MunicipalityCode DECIMAL PRIMARY KEY,
 "Name" VARCHAR
);

CREATE TABLE Zipcode (
 Zipcode VARCHAR PRIMARY KEY,
 SeriesIndex VARCHAR,
 BreakpointFrom DECIMAL,
 BreakpointTo DECIMAL,
 City VARCHAR,
 Streetname VARCHAR,
 MunicipalityCode DECIMAL REFERENCES Municipality(MunicipalityCode)
);CREATE TABLE Store (
 StoreID INT PRIMARY KEY,
 Name VARCHAR,
 Streetname VARCHAR,
 Number VARCHAR,
 City VARCHAR,
 CountryCode CHAR(2),
 Zipcode VARCHAR REFERENCES Zipcode(Zipcode),
 PhoneNumber VARCHAR
);CREATE TABLE Customer (
 CustomerID INT PRIMARY KEY,
 Email VARCHAR NOT NULL UNIQUE,
 "Name" VARCHAR,
 PhoneNumber VARCHAR,
 PaymentMethodID INT REFERENCES PaymentMethod(PaymentMethodID),
 StoreID INT REFERENCES Store(StoreID),
 "Password" CHAR(64),
 Newsletter BIT,
 Sms BIT
);CREATE TABLE AddressCustomer (
 AddressCustomerID INT PRIMARY KEY,
 AddressID INT REFERENCES "Address"(AddressID),
 CustomerID INT REFERENCES Customer(CustomerID),
);CREATE TABLE Voucher (
 VoucherID INT PRIMARY KEY,
 VoucherCode VARCHAR,
 "Description" VARCHAR,
 IssueDate DATE,
 ExpirationDate DATE
);CREATE TABLE "Order" (
 OrderID INT PRIMARY KEY,
 CustomerID INT REFERENCES Customer(CustomerID),
 StoreID INT REFERENCES Store(StoreID),
 Delivery BIT,
 AddressID INT REFERENCES Address(AddressID),
 DeliveryTime DATETIME,
 DeliveryCost SMALLMONEY,
 VoucherID INT REFERENCES Voucher(VoucherID),
 Tip SMALLMONEY,
 TotalPrice SMALLMONEY,
 PaymentMethodID INT REFERENCES PaymentMethod(PaymentMethodID)
);CREATE TABLE OrderItem (
 OrderItemID INT PRIMARY KEY,
 OrderID INT REFERENCES "Order"(OrderID),
 Quantity INT,
 Price SMALLMONEY
);CREATE TABLE PizzaCrust (
 PizzaCrustID INT PRIMARY KEY,
 "Name" VARCHAR,
 Diamater VARCHAR,
 "Description" VARCHAR,
 Fee SMALLMONEY,
 "Availability" BIT
);CREATE TABLE IngredientCategory (
 IngredientCategoryID INT PRIMARY KEY,
 "Name" VARCHAR
);CREATE TABLE Ingredient (
 IngredientID INT PRIMARY KEY,
 IngredientCategoryID INT REFERENCES IngredientCategory(IngredientCategoryID),
 "Name" VARCHAR,
 "Description" VARCHAR,
 Price SMALLMONEY,
 Spicy BIT,
 Vegetarian BIT
);CREATE TABLE ProductCategory (
 ProductCategoryID INT PRIMARY KEY,
  "Name" VARCHAR
);CREATE TABLE Product (
 ProductID INT PRIMARY KEY,
 ProductCategoryID INT REFERENCES ProductCategory(ProductCategoryID),
 "Name" VARCHAR,
 "Description" VARCHAR,
 Price SMALLMONEY,
 Spicy BIT,
 Vegetarian BIT
);CREATE TABLE PizzaRecipe (
 PizzaRecipeID INT PRIMARY KEY,
 ProductID INT REFERENCES Product(ProductID),
 IngredientID INT REFERENCES Ingredient(IngredientID)
);CREATE TABLE Item (
 ItemID INT PRIMARY KEY,
 OrderItemID INT REFERENCES OrderItem(OrderItemID),
 ProductID INT REFERENCES Product(ProductID),
 IngredientID INT REFERENCES Ingredient(IngredientID),
 PizzaCrustID INT REFERENCES PizzaCrust(PizzaCrustID)
);
