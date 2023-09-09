CREATE TABLE [dbo].[ContactPeopleAddresses]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    [PersonId] INT NOT NULL, 
    [AddressId] INT NOT NULL
 
)
