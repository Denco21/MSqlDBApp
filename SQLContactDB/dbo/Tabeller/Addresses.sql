﻿CREATE TABLE [dbo].[Addresses]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY (1,1),
	[PersonId] INT NOT NULL
, [Street] NVARCHAR(50) NOT NULL
, [City] NVARCHAR(50) NOT NULL
, [PostalCode] NVARCHAR(50) NOT NULL
, [Country] NVARCHAR(50) NOT NULL
)
