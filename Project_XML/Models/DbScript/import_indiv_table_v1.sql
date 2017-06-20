UPDATE Individual_tbl SET
	--Convert country name to country code
	[Jurisdiction 1] = ISNULL((SELECT CountryCode FROM CountryList WHERE CountryName=UPPER([Jurisdiction 1]) OR CountryCode=UPPER([Jurisdiction 1])),NULL),
	[Jurisdiction 2] = ISNULL((SELECT CountryCode FROM CountryList WHERE CountryName=UPPER([Jurisdiction 2]) OR CountryCode=UPPER([Jurisdiction 2])),NULL),
	[Jurisdiction 3] = ISNULL((SELECT CountryCode FROM CountryList WHERE CountryName=UPPER([Jurisdiction 3]) OR CountryCode=UPPER([Jurisdiction 3])),NULL),
	[Country] = ISNULL((SELECT CountryCode FROM CountryList WHERE CountryName=UPPER([Country]) OR CountryCode=UPPER([Country])),NULL),
	[Place of birth] = ISNULL((SELECT CountryCode FROM CountryList WHERE CountryName=UPPER([Place of birth]) OR CountryCode=UPPER([Place of birth])),NULL),
	[TIN 1 issuedBy ] = ISNULL((SELECT CountryCode FROM CountryList WHERE CountryName=UPPER([TIN 1 issuedBy ]) OR CountryCode=UPPER([TIN 1 issuedBy ])),NULL),
	[TIN 2 issuedBy ] = ISNULL((SELECT CountryCode FROM CountryList WHERE CountryName=UPPER([TIN 2 issuedBy ]) OR CountryCode=UPPER([TIN 2 issuedBy ])),NULL),
	[TIN 3 issuedBy ] = ISNULL((SELECT CountryCode FROM CountryList WHERE CountryName=UPPER([TIN 3 issuedBy ]) OR CountryCode=UPPER([TIN 3 issuedBy ])),NULL)

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
    DECLARE @Gross_amount_of_interest decimal(16, 2)
    DECLARE @Gross_amount_of_dividend decimal(16, 2)
    DECLARE @Gross_amount_of_other_income decimal(16, 2)
    DECLARE @Gross_proceeds decimal(16, 2)
    DECLARE @AcctType nvarchar(10)
    DECLARE @AcctID int
	DECLARE @PId int

  	DECLARE indiv_cursor CURSOR FOR SELECT * FROM Individual_tbl

  	OPEN indiv_cursor
	FETCH NEXT FROM indiv_cursor
	INTO @FirstName,@LastName,@AddressFree,@City,@Country,@BirthDate,
	@BirthPlace,@Jurisdiction1,@Jurisdiction2,@Jurisdiction3,@TIN1,@TIN1Ctry,@TIN2,@TIN2Ctry,@TIN3,@TIN3Ctry,@AccountNumber,
	@CurrencyCode,@AccountBalance,@Gross_amount_of_interest,@Gross_amount_of_dividend,@Gross_amount_of_other_income,@Gross_proceeds, @AcctType

    WHILE @@FETCH_STATUS = 0
    BEGIN
	    IF(SELECT AcctNumber FROM Account WHERE [AcctNumber]=@AccountNumber AND [AcctType]=@AcctType) IS NULL
	    BEGIN
		    INSERT INTO Account(AcctNumber, CurrCode, AccountBalance, AcctType) 
		    VALUES(@AccountNumber,@CurrencyCode, @AccountBalance, @AcctType)
			/*   
	        SET @AcctID = (SELECT DISTINCT [AcctID] 
	        FROM Account 
	        WHERE AcctNumber=@AccountNumber AND AcctType=@AcctType AND CurrCode = @CurrencyCode AND AccountBalance = @AccountBalance)
			*/
			SET @AcctID = (SELECT SCOPE_IDENTITY()) --for testing

		    INSERT INTO Person(FirstName, LastName, BirthDate, BirthCountry)
		    VALUES(@FirstName, @LastName, @BirthDate, @BirthPlace)

		    SET @PId = (SELECT SCOPE_IDENTITY()) --get the last identity value inserted in Person table

			INSERT INTO Address(FreeLine, City, CountryCode, P_Ent_Id)
		    VALUES(@AddressFree, @City,@Country, @PId)
		        
		    IF(@Jurisdiction1 IS NOT NULL)
		    BEGIN
			    IF (SELECT COUNT(*) FROM ResCountryCode WHERE P_Ent_id = @PId AND CountryCode = @Jurisdiction1) = 0 
				    INSERT INTO ResCountryCode(P_Ent_Id,CountryCode,isReportable)
				    VALUES(@PId,@Jurisdiction1,1)
		    END 
		    IF(@Jurisdiction2 IS NOT NULL)
		    BEGIN
			    IF (SELECT COUNT(*) FROM ResCountryCode WHERE P_Ent_id = @PId AND CountryCode = @Jurisdiction2) = 0 
				    INSERT INTO ResCountryCode(P_Ent_Id,CountryCode,isReportable)
				    VALUES(@PId,@Jurisdiction2,1)
		    END 
		    IF(@Jurisdiction3 IS NOT NULL)
	        BEGIN
		        IF (SELECT COUNT(*) FROM ResCountryCode WHERE P_Ent_id = @PId AND CountryCode = @Jurisdiction3) = 0 
			        INSERT INTO ResCountryCode(P_Ent_Id,CountryCode,isReportable)
			        VALUES(@PId,@Jurisdiction3,1)
	        END 
		    IF(@TIN1 IS NOT NULL)
			    INSERT INTO INType(Value,CountryCode,IType,P_Ent_Id)
			    VALUES(@TIN1,@TIN1Ctry,'TIN',@PId)
		    IF(@TIN2 IS NOT NULL)
			    INSERT INTO INType(Value,CountryCode,IType,P_Ent_Id)
			    VALUES(@TIN2,@TIN1Ctry,'TIN',@PId)
		    IF(@TIN3 IS NOT NULL)
			    INSERT INTO INType(Value,CountryCode,IType,P_Ent_Id)
			    VALUES(@TIN3,@TIN1Ctry,'TIN',@PId)

			--Person Payment
			IF(@Gross_amount_of_interest IS NOT NULL)
				INSERT INTO Payment(PaymentType,Amount,CurrCode,AcctID)
				VALUES('CRS502',@Gross_amount_of_interest,@CurrencyCode,@AcctID)
			IF (@Gross_amount_of_dividend IS NOT NULL)
				INSERT INTO Payment(PaymentType,Amount,CurrCode,AcctID)
				VALUES('CRS501',@Gross_amount_of_interest,@CurrencyCode,@AcctID)
			IF(@Gross_amount_of_other_income IS NOT NULL)
				INSERT INTO Payment(PaymentType,Amount,CurrCode,AcctID)
				VALUES('CRS504',@Gross_amount_of_interest,@CurrencyCode,@AcctID)
			IF(@Gross_proceeds IS NOT NULL)
				INSERT INTO Payment(PaymentType,Amount,CurrCode,AcctID)
				VALUES('CRS503',@Gross_amount_of_interest,@CurrencyCode,@AcctID)

		    INSERT INTO PersonAcctHolder(AcctID, PId)
		    VALUES(@AcctID,@PId)
		
			FETCH NEXT FROM indiv_cursor
			INTO @FirstName,@LastName,@AddressFree,@City,@Country,@BirthDate,
			@BirthPlace,@Jurisdiction1,@Jurisdiction2,@Jurisdiction3,@TIN1,@TIN1Ctry,@TIN2,@TIN2Ctry,@TIN3,@TIN3Ctry,@AccountNumber,
			@CurrencyCode,@AccountBalance,@Gross_amount_of_interest,@Gross_amount_of_dividend,@Gross_amount_of_other_income,@Gross_proceeds, @AcctType
		END
   	END

    CLOSE indiv_cursor
    DEALLOCATE indiv_cursor