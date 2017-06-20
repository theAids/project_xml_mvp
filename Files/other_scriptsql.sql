USE [AEOIDB]
GO

--DELETE all content except CountryList table and CRS table
DELETE FROM dbo.Account
DELETE FROM dbo.Address
--DELETE FROM dbo.AeoiProfile
DELETE FROM dbo.ControllingPerson
DELETE FROM dbo.DocSpec
DELETE FROM dbo.Entity
DELETE FROM dbo.Entity_tbl
DELETE FROM dbo.Individual_tbl
DELETE FROM dbo.INType
DELETE FROM dbo.MessageSpec
DELETE FROM dbo.Payment
DELETE FROM dbo.Person
DELETE FROM dbo.PersonAcctHolder
DELETE FROM dbo.ResCountryCode

--Sample AEOI Profile
INSERT INTO dbo.AeoiProfile([AeoiId],[FIName])
VALUES('AU66356','Industrial and Commercial Bank of China (Asia) Limited')

--CRS104 for non-reportable
INSERT INTO CRS(CrsCode,CrsDescription)
VALUES('CRS104','Non-Reportable Person')

DELETE FROM dbo.CRS
SELECT * FROM dbo.Person
SELECT * FROM dbo.Entity

