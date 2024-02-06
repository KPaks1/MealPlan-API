-- Drop all tables in dependency order:
DROP TABLE IF EXISTS dbo.MealIngredient;
GO
DROP TABLE IF EXISTS dbo.Meal;
GO
DROP TABLE IF EXISTS dbo.Ingredient;
GO
DROP TABLE IF EXISTS dbo.IngredientType;
GO

-- Create tables
CREATE TABLE dbo.IngredientType (
	IngredientTypeId INT IDENTITY (1,1) NOT NULL PRIMARY KEY,
	[Name] VARCHAR(MAX) NOT NULL,
	[Description] VARCHAR(MAX) NOT NULL,
	CreateTimestamp DATETIME2 (7) DEFAULT(sysutcdatetime()) NOT NULL,
	UpdateTimestamp DATETIME2 (7) DEFAULT(sysutcdatetime()) NOT NULL
);
GO

-- Update trigger
DROP TRIGGER IF EXISTS TRG_IngredientType_UpdateTimestamp;
GO

CREATE TRIGGER dbo.TRG_IngredientType_UpdateTimestamp ON dbo.IngredientType
AFTER UPDATE
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE dbo.IngredientType
	SET UpdateTimestamp = sysutcdatetime()
	FROM dbo.IngredientType AS d
	WHERE EXISTS (SELECT 1 FROM inserted i WHERE i.IngredientTypeId = d.IngredientTypeId)
END
GO

/*
* SETUP - Ingredient
*/

-- Table
CREATE TABLE dbo.Ingredient (
	IngredientId INT IDENTITY (1,1) NOT NULL PRIMARY KEY,
	[Name] VARCHAR(255) NOT NULL,
	[Description] VARCHAR(MAX) NULL,
	Cost MONEY DEFAULT(0) NOT NULL,
	IngredientTypeId INT NOT NULL FOREIGN KEY REFERENCES dbo.IngredientType(IngredientTypeId),
	CreateTimestamp DATETIME2 (7) DEFAULT(sysutcdatetime()) NOT NULL,
	UpdateTimestamp DATETIME2 (7) DEFAULT(sysutcdatetime()) NOT NULL
);

-- Trigger
DROP TRIGGER IF EXISTS TRG_Ingredient_UpdateTimestamp;
GO

CREATE TRIGGER dbo.TRG_Ingredient_UpdateTimestamp ON dbo.Ingredient
AFTER UPDATE
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE dbo.Ingredient
	SET UpdateTimestamp = sysutcdatetime()
	FROM dbo.Ingredient AS d
	WHERE EXISTS (SELECT 1 FROM inserted i WHERE i.IngredientId = d.IngredientId)
END
GO
/*
* SETUP - Meal
*/

-- Table

CREATE TABLE Meal (
    MealId          INT           IDENTITY (1, 1) NOT NULL PRIMARY KEY,
    Title           VARCHAR (128) NOT NULL,
    [Description]   VARCHAR (MAX) NOT NULL,
    ImageUrl        VARCHAR (MAX) NULL,
    CreateTimestamp DATETIME2 (7) DEFAULT (sysutcdatetime()) NOT NULL,
    UpdateTimestamp DATETIME2 (7) DEFAULT (sysutcdatetime()) NOT NULL
);
GO

-- Trigger
DROP TRIGGER IF EXISTS TRG_Meal_UpdateTimestamp;
GO

CREATE TRIGGER dbo.TRG_Meal_UpdateTimestamp ON dbo.Meal
AFTER UPDATE
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE dbo.Meal
	SET UpdateTimestamp = sysutcdatetime()
	FROM dbo.Meal AS d
	WHERE EXISTS (SELECT 1 FROM inserted i WHERE i.MealId = d.MealId)
END
GO

/*
*	SETUP - Meal Ingredient - Mapping for 1 meal -> many ingredients
*/
CREATE TABLE dbo.MealIngredient (
    IngredientId INT NOT NULL,
    MealId       INT NOT NULL,
    FOREIGN KEY (IngredientId) REFERENCES dbo.Ingredient (IngredientId),
    FOREIGN KEY (MealId) REFERENCES dbo.Meal (MealId)
);
GO

SELECT 'Tables in MealPlan db', * FROM sys.Tables;
GO