-- Ingredient Types
IF NOT EXISTS (SELECT 1 FROM sys.Tables WHERE Name = 'IngredientType')
BEGIN
	SELECT 'Early exit: IngredientType table is not created';
	RETURN;
END
GO

DROP TABLE IF EXISTS #IngredientTypeTable;
GO

CREATE TABLE #IngredientTypeTable
(
	[Name] VARCHAR(255),
	[Description] VARCHAR(MAX)
)
GO

INSERT INTO #IngredientTypeTable
VALUES
(
	'Vegetable', 'Items that belong in the vegetable food group'
),
(
	'Meat', 'Items that belong in the meat food group'
),
(
	'Seafood', 'Items that belong in the seafood food group'
),
(
	'Dairy', 'Items that belong in the dairy food group'
),
(
	'Spice', 'Items that belong in the spice food group'
);
GO

INSERT INTO IngredientType 
(
	Name, 
	Description
)
SELECT
	itt.Name,
	itt.Description
FROM
	#IngredientTypeTable itt
	LEFT JOIN IngredientType it
		ON it.Name = itt.Name
	WHERE
		it.Name IS NULL;
GO

SELECT 'IngredientTypes', * FROM IngredientType;
GO