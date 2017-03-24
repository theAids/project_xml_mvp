USE [AEOIDB]
GO

--Get All Accounts

SELECT A.AcctNumber, t2.Name, t2.AcctHolderId
FROM Account A
LEFT JOIN(SELECT A.AcctNumber, E.Name, EntityId AS AcctHolderId FROM Account A, Entity E WHERE A.AcctNumber=E.AcctNumber
			UNION
			SELECT A.AcctNumber, P.LastName+', '+P.FirstName AS Name, P.PId AS AcctHolderId
				FROM Account A, Person P, PersonAcctHolder PH
				WHERE A.AcctNumber=PH.AcctNumber AND P.PId=PH.PId)
			t2 ON t2.AcctNumber=A.AcctNumber
ORDER BY t2.Name ASC

--FIAccountNumber_Type, AccountBalance
SELECT AcctNumber, AcctNumberType, isUndocumented, isClosed, isDormant, AccountBalance, CurrCode
FROM Account 
WHERE AcctNumber='123-444-567890'

--PaymentType
SELECT PaymentType, Amount, P.CurrCode
FROM Payment P, Account A
WHERE P.AcctNumber = A.AcctNumber AND A.AcctNumber='123-444-567890'

--check if acctholder is entity or person (acctHolderId)
SELECT CAST(CASE
		WHEN (SELECT EntityId FROM Entity WHERE EntityId=1001)IS NOT NULL
		THEN 1
		ELSE 0
		END AS BIT) AS isEntity

--get Person ResCountryCodes
SELECT CountryCode
FROM Person P, ResCountryCode R
WHERE P.PId=R.P_Ent_Id AND P.PId=1002  AND isReportable=1

--get Entity ResCountryCodes
SELECT CountryCode
FROM Entity, ResCountryCode
WHERE EntityId=P_Ent_Id AND EntityId=2001

-- get Entity acctholder ID and rescountry Code
SELECT EntityId, CountryCode
FROM Entity E, ResCountryCode R, Account A
WHERE E.AcctNumber=A.AcctNumber AND E.EntityId=R.P_Ent_Id AND A.AcctNumber='123-444-567890'

-- get Person acctholder ID and rescountry Code
SELECT PId, CountryCode
FROM Person P, ResCountryCode R, Account A
WHERE E.AcctNumber=A.AcctNumber AND E.EntityId=R.P_Ent_Id AND A.AcctNumber='123-444-567890'

--Entity Details
SELECT EntityId,Name,NameType,AcctHolderType,
	STUFF((SELECT ';'+Value+','+ISNULL(CountryCode,'')+','+IType 
			FROM INType, Entity
			WHERE P_Ent_Id=EntityId AND EntityId=2001
			FOR XML PATH('')),1,1,'') AS INVal
FROM Entity E, ResCountryCode
WHERE EntityId=P_Ent_Id AND EntityId=2001 AND CountryCode='HK'--@acctId

--Person Details
SELECT Person.*,
	STUFF((SELECT ';'+Value+','+ISNULL(CountryCode,'')+','+IType 
			FROM INType, Person WHERE P_Ent_Id=PId AND PId=1001
			FOR XML PATH('')),1,1,'') AS INVal
FROM Person WHERE PId=1001--@acctId FROM Person --WHERE PId=@acctId

-- Enity Address
SELECT A.* FROM Address A, Entity WHERE P_Ent_Id=EntityId AND EntityId=2001 
-- Person Address
SELECT A.* FROM Address A, Person WHERE A.P_Ent_Id=PId AND PId=1001 


--Entity Controlling Person IDs
SELECT P.PId
FROM Person P, ControllingPerson C, Account A, ResCountryCode R, Entity E
WHERE C.PId=P.PId AND A.AcctNumber=C.AcctNumber AND P.PId=R.P_Ent_Id AND E.AcctNumber=A.AcctNumber 
		AND R.CountryCode='HK' AND A.AcctNumber='123-444-567890' AND EntityId=2001

--Individual Controlling Person IDs
SELECT P.PId
FROM Person P, ControllingPerson C, Account A, ResCountryCode R, PersonAcctHolder PH
WHERE C.PId=P.PId AND A.AcctNumber=C.AcctNumber AND P.PId=R.P_Ent_Id AND A.AcctNumber=PH.AcctNumber
		AND R.CountryCode='HK' AND A.AcctNumber='097-444-567890' AND P.PId=1002

--Controlling Person Details
SELECT P.*,STUFF((SELECT ';'+Value+','+ISNULL(CountryCode,'')+','+IType 
			FROM INType, Person WHERE P_Ent_Id=PId AND PId=1001
			FOR XML PATH('')),1,1,'') AS INVal,
			CtrlPersonType
FROM Person P, ControllingPerson C, Account A
WHERE C.PId=P.PId AND C.AcctNumber=A.AcctNumber AND A.AcctNumber='123-444-567890' AND P.PId=1001


--Check if MessageRefId exists
SELECT CAST(CASE
		WHEN (SELECT MessageRefId FROM MessageSpec WHERE MessageRefid='')IS NOT NULL
		THEN 1
		ELSE 0
		END AS BIT) AS isEntity
		
--------------------------Import Scripts------------------------------------------

--------------Individual Mapping------------------------
USE [AEOIDB]
GO

--translate all country info to corresponding country codes
UPDATE Individual_tbl SET
	[Jurisdiction 1] = ISNULL((SELECT CountryCode FROM CountryList WHERE CountryName=UPPER([Jurisdiction 1]) OR CountryCode=UPPER([Jurisdiction 1])),NULL),
	[Jurisdiction 2] = ISNULL((SELECT CountryCode FROM CountryList WHERE CountryName=UPPER([Jurisdiction 2]) OR CountryCode=UPPER([Jurisdiction 2])),NULL),
	[Jurisdiction 3] = ISNULL((SELECT CountryCode FROM CountryList WHERE CountryName=UPPER([Jurisdiction 3]) OR CountryCode=UPPER([Jurisdiction 3])),NULL),
	[Country] = ISNULL((SELECT CountryCode FROM CountryList WHERE CountryName=UPPER([Country]) OR CountryCode=UPPER([Country])),NULL),
	[Place of birth] = ISNULL((SELECT CountryCode FROM CountryList WHERE CountryName=UPPER([Place of birth]) OR CountryCode=UPPER([Place of birth])),NULL),
	[TIN 1 issuedBy ] = ISNULL((SELECT CountryCode FROM CountryList WHERE CountryName=UPPER([TIN 1 issuedBy ]) OR CountryCode=UPPER([TIN 1 issuedBy ])),NULL),
	[TIN 2 issuedBy ] = ISNULL((SELECT CountryCode FROM CountryList WHERE CountryName=UPPER([TIN 2 issuedBy ]) OR CountryCode=UPPER([TIN 2 issuedBy ])),NULL),
	[TIN 3 issuedBy ] = ISNULL((SELECT CountryCode FROM CountryList WHERE CountryName=UPPER([TIN 3 issuedBy ]) OR CountryCode=UPPER([TIN 3 issuedBy ])),NULL)



--Cursor for Individual Account holder
DECLARE @FirstName nvarchar(255) 
DECLARE @LastName nvarchar(255) 
DECLARE @AddressFree nvarchar(750)
DECLARE @City nvarchar(255) 
DECLARE @Country nvarchar(255) 
DECLARE @BirthDate nvarchar(255)
DECLARE @BirthPlace nvarchar(255)
DECLARE @Jurisdiction1 nvarchar(255)
DECLARE @Jurisdiction2 nvarchar(255)
DECLARE @Jurisdiction3 nvarchar(255)
DECLARE @TIN1 nvarchar(255)
DECLARE @TIN1Ctry nvarchar(255)
DECLARE @TIN2 nvarchar(255)
DECLARE @TIN2Ctry  nvarchar(255)
DECLARE @TIN3  nvarchar(255)
DECLARE @TIN3Ctry nvarchar(255)
DECLARE @AccountNumber nvarchar(255) 
DECLARE @CurrencyCode nvarchar(255) 
DECLARE @AccountBalance decimal(16, 2) 
DECLARE @Interest decimal(16, 2)
DECLARE @Dividend decimal(16, 2)
DECLARE @OtherIncome decimal(16, 2)
DECLARE @Proceeds decimal(16, 2)

DECLARE @PId int


DECLARE indiv_cursor CURSOR
FOR SELECT * FROM Individual_tbl

OPEN indiv_cursor
FETCH NEXT FROM indiv_cursor
INTO @FirstName,@LastName,@AddressFree,@City,@Country,@BirthDate,
@BirthPlace,@Jurisdiction1,@Jurisdiction2,@Jurisdiction3,@TIN1,@TIN1Ctry,@TIN2,@TIN2Ctry,@TIN3,@TIN3Ctry,@AccountNumber,
@CurrencyCode,@AccountBalance,@Interest,@Dividend,@OtherIncome,@Proceeds

WHILE @@FETCH_STATUS = 0
BEGIN
	--Account Info
	IF(SELECT AcctNumber FROM Account WHERE AcctNumber=@AccountNumber) IS NULL
		INSERT INTO Account(AcctNumber, CurrCode, AccountBalance) 
		VALUES(@AccountNumber,@CurrencyCode, @AccountBalance)
		
	--Individual Account Holder Info
	INSERT INTO Person(FirstName, LastName, BirthDate, BirthCountry)
	VALUES(@FirstName, @LastName, @BirthDate, @BirthPlace)
	SET @PId = (SELECT SCOPE_IDENTITY()) --(SELECT PId FROM Person WHERE FirstName=@FirstName AND LastName=@LastName)
	
	--Individual Address
	INSERT INTO Address(FreeLine, City, CountryCode, P_Ent_Id)
	VALUES(@AddressFree, @City,@Country, @PId)
	
	--Individual ResCountry
	IF(@Jurisdiction1 IS NOT NULL)
		INSERT INTO ResCountryCode(P_Ent_Id,CountryCode,isReportable)
		VALUES(@PId,@Jurisdiction1,1)
	IF(@Jurisdiction2 IS NOT NULL)
		INSERT INTO ResCountryCode(P_Ent_Id,CountryCode,isReportable)
		VALUES(@PId,@Jurisdiction2,1)
	IF(@Jurisdiction3 IS NOT NULL)
		INSERT INTO ResCountryCode(P_Ent_Id,CountryCode,isReportable)
		VALUES(@PId,@Jurisdiction3,1)
	
	--Individual TIN
	IF(@TIN1 IS NOT NULL)
		INSERT INTO INType(Value,CountryCode,IType,P_Ent_Id)
		VALUES(@TIN1,@TIN1Ctry,'TIN',@PId)
	IF(@TIN2 IS NOT NULL)
		INSERT INTO INType(Value,CountryCode,IType,P_Ent_Id)
		VALUES(@TIN2,@TIN1Ctry,'TIN',@PId)
	IF(@TIN3 IS NOT NULL)
		INSERT INTO INType(Value,CountryCode,IType,P_Ent_Id)
		VALUES(@TIN3,@TIN1Ctry,'TIN',@PId)
	
	--Individual AcctHolder and Account Mapping
	INSERT INTO PersonAcctHolder(AcctNumber,PId)
	VALUES(@AccountNumber,@PId)
	
	FETCH NEXT FROM indiv_cursor
	INTO @FirstName,@LastName,@AddressFree,@City,@Country,@BirthDate,
		@BirthPlace,@Jurisdiction1,@Jurisdiction2,@Jurisdiction3,@TIN1,@TIN1Ctry,@TIN2,@TIN2Ctry,@TIN3,@TIN3Ctry,@AccountNumber,
		@CurrencyCode,@AccountBalance,@Interest,@Dividend,@OtherIncome,@Proceeds
END

CLOSE indiv_cursor
DEALLOCATE indiv_cursor
GO




--DELETE ALL
DELETE FROM Account
DELETE FROM Address
DELETE FROM ControllingPerson
DELETE FROM Entity
DELETE FROM INType
DELETE FROM Payment
DELETE FROM Person
DELETE FROM PersonAcctHolder
DELETE FROM ResCountryCode

