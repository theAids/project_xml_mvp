UPDATE Entity_tbl SET
	
	--Convert country name to country code
	/* Entity */
	[Jurisdiction 1] = ISNULL((SELECT CountryCode FROM CountryList WHERE CountryName=UPPER([Jurisdiction 1]) OR CountryCode=UPPER([Jurisdiction 1])),NULL),
	[Jurisdiction 2] = ISNULL((SELECT CountryCode FROM CountryList WHERE CountryName=UPPER([Jurisdiction 2]) OR CountryCode=UPPER([Jurisdiction 2])),NULL),
	[Jurisdiction 3] = ISNULL((SELECT CountryCode FROM CountryList WHERE CountryName=UPPER([Jurisdiction 3]) OR CountryCode=UPPER([Jurisdiction 3])),NULL),
	[Country] = ISNULL((SELECT CountryCode FROM CountryList WHERE CountryName=UPPER([Country]) OR CountryCode=UPPER([Country])),NULL),
	[TIN 1 issuedBy] = ISNULL((SELECT CountryCode FROM CountryList WHERE CountryName=UPPER([TIN 1 issuedBy]) OR CountryCode=UPPER([TIN 1 issuedBy])),NULL),
	[TIN 2 issuedBy] = ISNULL((SELECT CountryCode FROM CountryList WHERE CountryName=UPPER([TIN 2 issuedBy]) OR CountryCode=UPPER([TIN 2 issuedBy])),NULL),
	[TIN 3 issuedBy] = ISNULL((SELECT CountryCode FROM CountryList WHERE CountryName=UPPER([TIN 3 issuedBy]) OR CountryCode=UPPER([TIN 3 issuedBy])),NULL),
	
	/* Controlling Person 1 */
	[CP1 Jurisdiction 1] = ISNULL((SELECT CountryCode FROM CountryList WHERE CountryName=UPPER([CP1 Jurisdiction 1]) OR CountryCode=UPPER([CP1 Jurisdiction 1])),NULL),
	[CP1 Jurisdiction 2] = ISNULL((SELECT CountryCode FROM CountryList WHERE CountryName=UPPER([CP1 Jurisdiction 2]) OR CountryCode=UPPER([CP1 Jurisdiction 2])),NULL),
	[CP1 Jurisdiction 3] = ISNULL((SELECT CountryCode FROM CountryList WHERE CountryName=UPPER([CP1 Jurisdiction 3]) OR CountryCode =UPPER([CP1 Jurisdiction 3])),NULL),
	[CP1 Country] = ISNULL((SELECT CountryCode FROM CountryList WHERE CountryName=UPPER([CP1 Country]) OR CountryCode =UPPER([CP1 Country])),NULL),
	[CP1 Place of birth] = ISNULL((SELECT CountryCode FROM CountryList WHERE CountryName=UPPER([CP1 Place of birth]) OR CountryCode =UPPER([CP1 Place of birth])),NULL),
	[CP1 TIN 1 issuedBy] = ISNULL((SELECT CountryCode FROM CountryList WHERE CountryName=UPPER([CP1 TIN 1 issuedBy]) OR CountryCode =UPPER([CP1 TIN 1 issuedBy])),NULL),
	[CP1 TIN 2 issuedBy] = ISNULL((SELECT CountryCode FROM CountryList WHERE CountryName=UPPER([CP1 TIN 2 issuedBy]) OR CountryCode =UPPER([CP1 TIN 2 issuedBy])),NULL),
	[CP1 TIN 3 issuedBy] = ISNULL((SELECT CountryCode FROM CountryList WHERE CountryName=UPPER([CP1 TIN 3 issuedBy]) OR CountryCode =UPPER([CP1 TIN 3 issuedBy])),NULL),
	
	/* Controlling Person 2 */
	[CP2 Jurisdiction 1] = ISNULL((SELECT CountryCode FROM CountryList WHERE CountryName=UPPER([CP2 Jurisdiction 1]) OR CountryCode=UPPER([CP2 Jurisdiction 1])),NULL),
	[CP2 Jurisdiction 2] = ISNULL((SELECT CountryCode FROM CountryList WHERE CountryName=UPPER([CP2 Jurisdiction 2]) OR CountryCode=UPPER([CP2 Jurisdiction 2])),NULL),
	[CP2 Jurisdiction 3] = ISNULL((SELECT CountryCode FROM CountryList WHERE CountryName=UPPER([CP2 Jurisdiction 3]) OR CountryCode=UPPER([CP2 Jurisdiction 3])),NULL),
	[CP2 Country] = ISNULL((SELECT CountryCode FROM CountryList WHERE CountryName=UPPER([CP2 Country]) OR CountryCode=UPPER([CP2 Country])),NULL),
	[CP2 Place of birth] = ISNULL((SELECT CountryCode FROM CountryList WHERE CountryName=UPPER([CP2 Place of birth]) OR CountryCode=UPPER([CP2 Place of birth])),NULL),
	[CP2 TIN 1 issuedBy] = ISNULL((SELECT CountryCode FROM CountryList WHERE CountryName=UPPER([CP2 TIN 1 issuedBy]) OR CountryCode=UPPER([CP2 TIN 1 issuedBy])),NULL),
	[CP2 TIN 2 issuedBy] = ISNULL((SELECT CountryCode FROM CountryList WHERE CountryName=UPPER([CP2 TIN 2 issuedBy]) OR CountryCode=UPPER([CP2 TIN 2 issuedBy])),NULL),
	[CP2 TIN 3 issuedBy] = ISNULL((SELECT CountryCode FROM CountryList WHERE CountryName=UPPER([CP2 TIN 3 issuedBy]) OR CountryCode=UPPER([CP2 TIN 3 issuedBy])),NULL),
	
	/* Controlling Person 3 */
	[CP3 Jurisdiction 1] = ISNULL((SELECT CountryCode FROM CountryList WHERE CountryName=UPPER([CP3 Jurisdiction 1]) OR CountryCode=UPPER([CP3 Jurisdiction 1])),NULL),
	[CP3 Jurisdiction 2] = ISNULL((SELECT CountryCode FROM CountryList WHERE CountryName=UPPER([CP3 Jurisdiction 2]) OR CountryCode=UPPER([CP3 Jurisdiction 2])),NULL),
	[CP3 Jurisdiction 3] = ISNULL((SELECT CountryCode FROM CountryList WHERE CountryName=UPPER([CP3 Jurisdiction 3]) OR CountryCode=UPPER([CP3 Jurisdiction 3])),NULL),
	[CP3 Country] = ISNULL((SELECT CountryCode FROM CountryList WHERE CountryName=UPPER([CP3 Country]) OR CountryCode=UPPER([CP3 Country])),NULL),
	[CP3 Place of birth] = ISNULL((SELECT CountryCode FROM CountryList WHERE CountryName=UPPER([CP3 Place of birth]) OR CountryCode=UPPER([CP3 Place of birth])),NULL),
	[CP3 TIN 1 issuedBy] = ISNULL((SELECT CountryCode FROM CountryList WHERE CountryName=UPPER([CP3 TIN 1 issuedBy]) OR CountryCode=UPPER([CP3 TIN 1 issuedBy])),NULL),
	[CP3 TIN 2 issuedBy] = ISNULL((SELECT CountryCode FROM CountryList WHERE CountryName=UPPER([CP3 TIN 2 issuedBy]) OR CountryCode=UPPER([CP3 TIN 2 issuedBy])),NULL),
	[CP3 TIN 3 issuedBy] = ISNULL((SELECT CountryCode FROM CountryList WHERE CountryName=UPPER([CP3 TIN 3 issuedBy]) OR CountryCode=UPPER([CP3 TIN 3 issuedBy])),NULL)

	--CRS Status Conversion for AcctHolderType

    --Cursor for Entity Account holder
    /* Entity */
    DECLARE @Name nvarchar(255)
    DECLARE @AddressFree nvarchar(255)
    DECLARE @City nvarchar(255) 
    DECLARE @Country nvarchar(255) 
    DECLARE @Jurisdiction1 nvarchar(255) 
    DECLARE @Jurisdiction2 nvarchar(255) 
    DECLARE @Jurisdiction3 nvarchar(255) 
    DECLARE @TIN1 nvarchar(255) 
    DECLARE @TIN1issuedBy nvarchar(255) 
    DECLARE @TIN2 nvarchar(255) 
    DECLARE @TIN2issuedBy nvarchar(255) 
    DECLARE @TIN3 nvarchar(255) 
    DECLARE @TIN3issuedBy nvarchar(255) 
    DECLARE @AccountNumber nvarchar(255)  
    DECLARE @CRSStatus nvarchar(255)  
    DECLARE @CurrencyCode nvarchar(255)  
    DECLARE @AccountBalance decimal(16, 2)  
    DECLARE @Gross_amount_of_interest decimal(16, 2) 
    DECLARE @Gross_amount_of_dividend decimal(16, 2) 
    DECLARE @Gross_amount_of_other_income decimal(16, 2) 
    DECLARE @Gross_proceeds decimal(16, 2)

	/* Controlling Person 1 */
	DECLARE @CP1FirstName nvarchar(255) 
	DECLARE @CP1LastName nvarchar(255) 
	DECLARE @CP1AddressFree nvarchar(750)
	DECLARE @CP1City nvarchar(255) 
	DECLARE @CP1Country nvarchar(255) 
	DECLARE @CP1BirthDate nvarchar(255)
	DECLARE @CP1BirthPlace nvarchar(255)
	DECLARE @CP1Jurisdiction1 nvarchar(255)
	DECLARE @CP1Jurisdiction2 nvarchar(255)
	DECLARE @CP1Jurisdiction3 nvarchar(255)
	DECLARE @CP1TIN1 nvarchar(255)
	DECLARE @CP1TIN1Ctry nvarchar(255)
	DECLARE @CP1TIN2 nvarchar(255)
	DECLARE @CP1TIN2Ctry  nvarchar(255)
	DECLARE @CP1TIN3  nvarchar(255)
	DECLARE @CP1TIN3Ctry nvarchar(255)

	/* Controlling Person 2 */
	DECLARE @CP2FirstName nvarchar(255) 
	DECLARE @CP2LastName nvarchar(255) 
	DECLARE @CP2AddressFree nvarchar(750)
	DECLARE @CP2City nvarchar(255) 
	DECLARE @CP2Country nvarchar(255) 
	DECLARE @CP2BirthDate nvarchar(255)
	DECLARE @CP2BirthPlace nvarchar(255)
	DECLARE @CP2Jurisdiction1 nvarchar(255)
	DECLARE @CP2Jurisdiction2 nvarchar(255)
	DECLARE @CP2Jurisdiction3 nvarchar(255)
	DECLARE @CP2TIN1 nvarchar(255)
	DECLARE @CP2TIN1Ctry nvarchar(255)
	DECLARE @CP2TIN2 nvarchar(255)
	DECLARE @CP2TIN2Ctry  nvarchar(255)
	DECLARE @CP2TIN3  nvarchar(255)
	DECLARE @CP2TIN3Ctry nvarchar(255)

	/* Controlling Person 3 */
	DECLARE @CP3FirstName nvarchar(255) 
	DECLARE @CP3LastName nvarchar(255) 
	DECLARE @CP3AddressFree nvarchar(750)
	DECLARE @CP3City nvarchar(255) 
	DECLARE @CP3Country nvarchar(255) 
	DECLARE @CP3BirthDate nvarchar(255)
	DECLARE @CP3BirthPlace nvarchar(255)
	DECLARE @CP3Jurisdiction1 nvarchar(255)
	DECLARE @CP3Jurisdiction2 nvarchar(255)
	DECLARE @CP3Jurisdiction3 nvarchar(255)
	DECLARE @CP3TIN1 nvarchar(255)
	DECLARE @CP3TIN1Ctry nvarchar(255)
	DECLARE @CP3TIN2 nvarchar(255)
	DECLARE @CP3TIN2Ctry  nvarchar(255)
	DECLARE @CP3TIN3  nvarchar(255)
	DECLARE @CP3TIN3Ctry nvarchar(255)

    DECLARE @AcctType nvarchar(10)
    DECLARE @AcctID int
	DECLARE @EntityId int
	DECLARE @PId int

	DECLARE entity_cursor CURSOR FOR SELECT * FROM Entity_tbl

    OPEN entity_cursor
    FETCH NEXT FROM entity_cursor
    INTO
	/* Entity */
	@Name, @AddressFree, @City, @Country, @Jurisdiction1, @Jurisdiction2, @Jurisdiction3,
	@TIN1, @TIN1issuedBy, @TIN2, @TIN2issuedBy, @TIN3, @TIN3issuedBy, @AccountNumber,   
	@CRSStatus, @CurrencyCode, @AccountBalance, @Gross_amount_of_interest, @Gross_amount_of_dividend,  
	@Gross_amount_of_other_income, @Gross_proceeds,
	/* Controlling Person 1 */
	@CP1FirstName, @CP1LastName, @CP1AddressFree, @CP1City, @CP1Country, @CP1BirthDate, @CP1BirthPlace, 
	@CP1Jurisdiction1, @CP1Jurisdiction2, @CP1Jurisdiction3, @CP1TIN1, @CP1TIN1Ctry, @CP1TIN2, 
	@CP1TIN2Ctry, @CP1TIN3, @CP1TIN3Ctry, 
	/* Controlling Person 2 */
	@CP2FirstName, @CP2LastName, @CP2AddressFree, @CP2City, @CP2Country, @CP2BirthDate, @CP2BirthPlace, 
	@CP2Jurisdiction1, @CP2Jurisdiction2, @CP2Jurisdiction3, @CP2TIN1, @CP2TIN1Ctry, @CP2TIN2, 
	@CP2TIN2Ctry, @CP2TIN3, @CP2TIN3Ctry,
	/* Controlling Person 3 */
	@CP3FirstName, @CP3LastName, @CP3AddressFree, @CP3City, @CP3Country, @CP3BirthDate, @CP3BirthPlace, 
	@CP3Jurisdiction1, @CP3Jurisdiction2, @CP3Jurisdiction3, @CP3TIN1, @CP3TIN1Ctry, @CP3TIN2, 
	@CP3TIN2Ctry, @CP3TIN3, @CP3TIN3Ctry,
	
	@AcctType

	WHILE @@FETCH_STATUS = 0
    BEGIN
		/* Entity */
		IF(SELECT AcctNumber FROM Account WHERE [AcctNumber]=@AccountNumber AND [AcctType]=@AcctType) IS NULL
		BEGIN
		  	--Account Info
		  	INSERT INTO Account(AcctNumber, CurrCode, AccountBalance, AcctType) 
		  	VALUES(@AccountNumber,@CurrencyCode, @AccountBalance, @AcctType)
			
			/*
	   		SET @AcctID = (SELECT DISTINCT [AcctID] 
	          				FROM Account WHERE AcctNumber=@AccountNumber AND AcctType=@AcctType AND CurrCode = @CurrencyCode AND AccountBalance = @AccountBalance)
		  	*/
		  	SET @AcctID = (SELECT SCOPE_IDENTITY()) --for testing

		  	--Entity Account Holder Info
		  	INSERT INTO Entity(Name, AcctID)
		  	VALUES(@Name, @AcctID)
		  	
		  	--get last identity value inserted
		  	SET @EntityId = (SELECT SCOPE_IDENTITY()) 
		  	
		  	--Entity Address
		 	INSERT INTO Address(FreeLine, City, CountryCode, P_Ent_Id)
		  	VALUES(@AddressFree, @City,@Country, @EntityId)
		  	--Entity ResCountry
	      	IF(@Jurisdiction1 IS NOT NULL)
	      	BEGIN
		    	IF (SELECT COUNT(*) FROM ResCountryCode WHERE P_Ent_id = @EntityId AND CountryCode = @Jurisdiction1) = 0 
			    	INSERT INTO ResCountryCode(P_Ent_Id,CountryCode,isReportable)
			      	VALUES(@EntityId,@Jurisdiction1,1)
	      	END
	      	IF(@Jurisdiction2 IS NOT NULL)
	      	BEGIN
		    	IF (SELECT COUNT(*) FROM ResCountryCode WHERE P_Ent_id = @EntityId AND CountryCode = @Jurisdiction2) = 0 
			    	INSERT INTO ResCountryCode(P_Ent_Id,CountryCode,isReportable)
			    	VALUES(@EntityId,@Jurisdiction2,1)
	      	END
	      	IF(@Jurisdiction3 IS NOT NULL)
	     	BEGIN
		  	IF (SELECT COUNT(*) FROM ResCountryCode WHERE P_Ent_id = @EntityId AND CountryCode = @Jurisdiction3) = 0 
				INSERT INTO ResCountryCode(P_Ent_Id,CountryCode,isReportable)
			  	VALUES(@EntityId,@Jurisdiction3,1)
	     	 END
		  	
		  	--Entity TIN
		  	IF(@TIN1 IS NOT NULL)
			  	INSERT INTO INType(Value,CountryCode,IType,P_Ent_Id)
			  	VALUES(@TIN1,@TIN1issuedBy,'TIN',@EntityId)
		  	IF(@TIN2 IS NOT NULL)
			  	INSERT INTO INType(Value,CountryCode,IType,P_Ent_Id)
			  	VALUES(@TIN2,@TIN2issuedBy,'TIN',@EntityId)
		  	IF(@TIN3 IS NOT NULL)
			  	INSERT INTO INType(Value,CountryCode,IType,P_Ent_Id)
			  	VALUES(@TIN3,@TIN3issuedBy,'TIN',@EntityId)
			
			--For New XML testing
			/* Controlling Person 1 */
			--CP1 Individual AcctHolder and Account Mapping
			IF (@CP1LastName IS NOT NULL)
			BEGIN
				IF((SELECT COUNT(*) FROM dbo.Person WHERE LastName=@CP1LastName AND FirstName=@CP1FirstName)= 0)
				BEGIN
					--CP1 Individual Account Holder Info
					INSERT INTO Person(FirstName, LastName, BirthDate, BirthCountry)
					VALUES(@CP1FirstName, @CP1LastName, @CP1BirthDate, @CP1BirthPlace)
					SET @PId = (SELECT SCOPE_IDENTITY())
				END
				ELSE
					SET @PId = (SELECT PId FROM Person WHERE LastName=@CP1LastName AND FirstName=@CP1FirstName)

	            IF((SELECT COUNT(*) FROM dbo.ControllingPerson WHERE AcctID=@AcctID AND PId=@PId) = 0)
					INSERT INTO ControllingPerson(AcctID,PId)
					VALUES(@AcctID,@PId)
				
				--CP1 Individual Address
				INSERT INTO Address(FreeLine, City, CountryCode, P_Ent_Id)
				VALUES(@CP1AddressFree, @CP1City,@CP1Country, @PId)
				
				--CP1 Individual ResCountry
				IF(@CP1Jurisdiction1 IS NOT NULL)
	                IF (SELECT COUNT(*) FROM ResCountryCode WHERE P_Ent_id = @PId AND CountryCode = @CP1Jurisdiction1) = 0 
						INSERT INTO ResCountryCode(P_Ent_Id,CountryCode,isReportable)
						VALUES(@PId,@CP1Jurisdiction1,1)
				IF(@CP1Jurisdiction2 IS NOT NULL)
	                IF (SELECT COUNT(*) FROM ResCountryCode WHERE P_Ent_id = @PId AND CountryCode = @CP1Jurisdiction2) = 0 
						INSERT INTO ResCountryCode(P_Ent_Id,CountryCode,isReportable)
						VALUES(@PId,@CP1Jurisdiction2,1)
				IF(@CP1Jurisdiction3 IS NOT NULL)
	                IF (SELECT COUNT(*) FROM ResCountryCode WHERE P_Ent_id = @PId AND CountryCode = @CP1Jurisdiction3) = 0 
						INSERT INTO ResCountryCode(P_Ent_Id,CountryCode,isReportable)
						VALUES(@PId,@CP1Jurisdiction3,1)
					
				--CP1 Individual TIN
				IF(@CP1TIN1 IS NOT NULL)
					INSERT INTO INType(Value,CountryCode,IType,P_Ent_Id)
					VALUES(@CP1TIN1,@CP1TIN1Ctry,'TIN',@PId)
				IF(@CP1TIN2 IS NOT NULL)
					INSERT INTO INType(Value,CountryCode,IType,P_Ent_Id)
					VALUES(@CP1TIN2,@CP1TIN1Ctry,'TIN',@PId)
				IF(@CP1TIN3 IS NOT NULL)
					INSERT INTO INType(Value,CountryCode,IType,P_Ent_Id)
					VALUES(@CP1TIN3,@CP1TIN1Ctry,'TIN',@PId)
			END
				
			/* Controlling Person 2 */	
			--CP2 Individual AcctHolder and Account Mapping
			IF (@CP2LastName IS NOT NULL)
			BEGIN
				IF((SELECT COUNT(*) FROM dbo.Person WHERE LastName=@CP2LastName AND FirstName=@CP2FirstName) = 0)
				BEGIN
					--CP2 Individual Account Holder Info
					INSERT INTO Person(FirstName, LastName, BirthDate, BirthCountry)
					VALUES(@CP2FirstName, @CP2LastName, @CP2BirthDate, @CP2BirthPlace)
					
					SET @PId = (SELECT SCOPE_IDENTITY())
				END
				ELSE
					SET @PId = (SELECT PId FROM Person WHERE LastName=@CP2LastName AND FirstName=@CP2FirstName)
						                            
	            IF((SELECT COUNT(*) FROM dbo.ControllingPerson WHERE AcctID=@AcctID AND PId=@PId) = 0)
					INSERT INTO ControllingPerson(AcctID,PId)
					VALUES(@AcctID,@PId)
					
				--CP2 Individual Address
				INSERT INTO Address(FreeLine, City, CountryCode, P_Ent_Id)
				VALUES(@CP2AddressFree, @CP2City,@CP2Country, @PId)
				--CP2 Individual ResCountry
				IF(@CP2Jurisdiction1 IS NOT NULL)
	            	IF (SELECT COUNT(*) FROM ResCountryCode WHERE P_Ent_id = @PId AND CountryCode = @CP2Jurisdiction1) = 0 
						INSERT INTO ResCountryCode(P_Ent_Id,CountryCode,isReportable)
						VALUES(@PId,@CP2Jurisdiction1,1)
				IF(@CP2Jurisdiction2 IS NOT NULL)
	                IF (SELECT COUNT(*) FROM ResCountryCode WHERE P_Ent_id = @PId AND CountryCode = @CP2Jurisdiction2) = 0 
						INSERT INTO ResCountryCode(P_Ent_Id,CountryCode,isReportable)
						VALUES(@PId,@CP2Jurisdiction2,1)
				IF(@CP2Jurisdiction3 IS NOT NULL)
	                IF (SELECT COUNT(*) FROM ResCountryCode WHERE P_Ent_id = @PId AND CountryCode = @CP2Jurisdiction3) = 0 
						INSERT INTO ResCountryCode(P_Ent_Id,CountryCode,isReportable)
						VALUES(@PId,@CP2Jurisdiction3,1)
					
				--CP2 Individual TIN
				IF(@CP2TIN1 IS NOT NULL)
					INSERT INTO INType(Value,CountryCode,IType,P_Ent_Id)
					VALUES(@CP2TIN1,@CP2TIN1Ctry,'TIN',@PId)
				IF(@CP2TIN2 IS NOT NULL)
					INSERT INTO INType(Value,CountryCode,IType,P_Ent_Id)
					VALUES(@CP2TIN2,@CP2TIN1Ctry,'TIN',@PId)
				IF(@CP2TIN3 IS NOT NULL)
					INSERT INTO INType(Value,CountryCode,IType,P_Ent_Id)
					VALUES(@CP2TIN3,@CP2TIN1Ctry,'TIN',@PId)
			END
				
			/* Controlling Person 3 */	
			--CP3 Individual AcctHolder and Account Mapping
			--CP3 Individual Account Holder Info
			IF (@CP3LastName IS NOT NULL)
			BEGIN
				IF((SELECT COUNT(*) FROM dbo.Person WHERE LastName=@CP3LastName AND FirstName=@CP3FirstName) = 0)
				BEGIN
					--CP3 Individual Account Holder Info
					INSERT INTO Person(FirstName, LastName, BirthDate, BirthCountry)
					VALUES(@CP3FirstName, @CP3LastName, @CP3BirthDate, @CP3BirthPlace)
							                                    
						SET @PId = (SELECT SCOPE_IDENTITY())
				END
				ELSE
					SET @PId = (SELECT PId FROM Person WHERE LastName=@CP3LastName AND FirstName=@CP3FirstName)

	            IF((SELECT COUNT(*) FROM dbo.ControllingPerson WHERE AcctID=@AcctID AND PId=@PId) = 0)
					INSERT INTO ControllingPerson(AcctID,PId)
					VALUES(@AcctID,@PId)
					
				--CP3 Individual Address
				INSERT INTO Address(FreeLine, City, CountryCode, P_Ent_Id)
				VALUES(@CP3AddressFree, @CP3City,@CP3Country, @PId)
					
				--CP3 Individual ResCountry
				IF(@CP3Jurisdiction1 IS NOT NULL)
	                IF (SELECT COUNT(*) FROM ResCountryCode WHERE P_Ent_id = @PId AND CountryCode = @CP3Jurisdiction1) = 0 
						INSERT INTO ResCountryCode(P_Ent_Id,CountryCode,isReportable)
						VALUES(@PId,@CP3Jurisdiction1,1)
				IF(@CP3Jurisdiction2 IS NOT NULL)
	                IF (SELECT COUNT(*) FROM ResCountryCode WHERE P_Ent_id = @PId AND CountryCode = @CP3Jurisdiction2) = 0 
						INSERT INTO ResCountryCode(P_Ent_Id,CountryCode,isReportable)
						VALUES(@PId,@CP3Jurisdiction2,1)
				IF(@CP3Jurisdiction3 IS NOT NULL)
	                    IF (SELECT COUNT(*) FROM ResCountryCode WHERE P_Ent_id = @PId AND CountryCode = @CP3Jurisdiction3) = 0 
						INSERT INTO ResCountryCode(P_Ent_Id,CountryCode,isReportable)
						VALUES(@PId,@CP3Jurisdiction3,1)
					
				--CP3 Individual TIN
				IF(@CP3TIN1 IS NOT NULL)
					INSERT INTO INType(Value,CountryCode,IType,P_Ent_Id)
					VALUES(@CP3TIN1,@CP3TIN1Ctry,'TIN',@PId)
				IF(@CP3TIN2 IS NOT NULL)
					INSERT INTO INType(Value,CountryCode,IType,P_Ent_Id)
					VALUES(@CP3TIN2,@CP3TIN1Ctry,'TIN',@PId)
				IF(@CP3TIN3 IS NOT NULL)
					INSERT INTO INType(Value,CountryCode,IType,P_Ent_Id)
					VALUES(@CP3TIN3,@CP3TIN1Ctry,'TIN',@PId)
			END
		
		    FETCH NEXT FROM entity_cursor
		    INTO @Name, @AddressFree, @City, @Country, @Jurisdiction1, @Jurisdiction2, @Jurisdiction3,
		    @TIN1, @TIN1issuedBy, @TIN2, @TIN2issuedBy, @TIN3, @TIN3issuedBy, @AccountNumber,   
		    @CRSStatus, @CurrencyCode, @AccountBalance, @Gross_amount_of_interest, @Gross_amount_of_dividend,  
		    @Gross_amount_of_other_income, @Gross_proceeds,
			
			@CP1FirstName, @CP1LastName, @CP1AddressFree, @CP1City, @CP1Country, @CP1BirthDate, @CP1BirthPlace,
		    @CP1Jurisdiction1, @CP1Jurisdiction2, @CP1Jurisdiction3, @CP1TIN1, @CP1TIN1Ctry, @CP1TIN2,
		    @CP1TIN2Ctry, @CP1TIN3, @CP1TIN3Ctry, 
		    
		    @CP2FirstName, @CP2LastName, @CP2AddressFree, @CP2City, @CP2Country, @CP2BirthDate, @CP2BirthPlace, 
		    @CP2Jurisdiction1, @CP2Jurisdiction2, @CP2Jurisdiction3, @CP2TIN1, @CP2TIN1Ctry, @CP2TIN2,
		    @CP2TIN2Ctry, @CP2TIN3, @CP2TIN3Ctry,
		    
		    @CP3FirstName, @CP3LastName, @CP3AddressFree, @CP3City, @CP3Country, @CP3BirthDate, @CP3BirthPlace, 
		    @CP3Jurisdiction1, @CP3Jurisdiction2, @CP3Jurisdiction3, @CP3TIN1, @CP3TIN1Ctry, @CP3TIN2, 
		    @CP3TIN2Ctry, @CP3TIN3, @CP3TIN3Ctry, @AcctType
	    END
    END

    CLOSE entity_cursor
    DEALLOCATE entity_cursor