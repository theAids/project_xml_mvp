USE [master]
GO
/****** Object:  Database [AEOIDB]    Script Date: 06/19/2017 14:56:42 ******/
CREATE DATABASE [AEOIDB] ON  PRIMARY 
( NAME = N'AEOIDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\AEOIDB.mdf' , SIZE = 10240KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'AEOIDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\AEOIDB_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [AEOIDB] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [AEOIDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [AEOIDB] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [AEOIDB] SET ANSI_NULLS OFF
GO
ALTER DATABASE [AEOIDB] SET ANSI_PADDING OFF
GO
ALTER DATABASE [AEOIDB] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [AEOIDB] SET ARITHABORT OFF
GO
ALTER DATABASE [AEOIDB] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [AEOIDB] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [AEOIDB] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [AEOIDB] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [AEOIDB] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [AEOIDB] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [AEOIDB] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [AEOIDB] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [AEOIDB] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [AEOIDB] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [AEOIDB] SET  DISABLE_BROKER
GO
ALTER DATABASE [AEOIDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [AEOIDB] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [AEOIDB] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [AEOIDB] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [AEOIDB] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [AEOIDB] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [AEOIDB] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [AEOIDB] SET  READ_WRITE
GO
ALTER DATABASE [AEOIDB] SET RECOVERY SIMPLE
GO
ALTER DATABASE [AEOIDB] SET  MULTI_USER
GO
ALTER DATABASE [AEOIDB] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [AEOIDB] SET DB_CHAINING OFF
GO
USE [AEOIDB]
GO
/****** Object:  Table [dbo].[Entity_tbl]    Script Date: 06/19/2017 14:56:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Entity_tbl](
	[Name] [nvarchar](255) NOT NULL,
	[AddressFree] [nvarchar](255) NULL,
	[City] [nvarchar](255) NOT NULL,
	[Country] [nvarchar](255) NOT NULL,
	[Jurisdiction 1] [nvarchar](255) NULL,
	[Jurisdiction 2] [nvarchar](255) NULL,
	[Jurisdiction 3] [nvarchar](255) NULL,
	[TIN 1] [nvarchar](255) NULL,
	[TIN 1 issuedBy] [nvarchar](255) NULL,
	[TIN 2] [nvarchar](255) NULL,
	[TIN 2 issuedBy] [nvarchar](255) NULL,
	[TIN 3] [nvarchar](255) NULL,
	[TIN 3 issuedBy] [nvarchar](255) NULL,
	[Account Number] [nvarchar](255) NOT NULL,
	[CRS Status] [nvarchar](255) NOT NULL,
	[Currency Code] [nvarchar](255) NOT NULL,
	[Account Balance] [decimal](16, 2) NOT NULL,
	[Gross amount of interest] [decimal](16, 2) NULL,
	[Gross amount of dividend] [decimal](16, 2) NULL,
	[Gross amount of other income] [decimal](16, 2) NULL,
	[Gross proceeds] [decimal](16, 2) NULL,
	[CP1 FirstName] [nvarchar](255) NULL,
	[CP1 LastName] [nvarchar](255) NULL,
	[CP1 AddressFree] [nvarchar](750) NULL,
	[CP1 City] [nvarchar](255) NULL,
	[CP1 Country] [nvarchar](255) NULL,
	[CP1 Date of birth] [nvarchar](255) NULL,
	[CP1 Place of birth] [nvarchar](255) NULL,
	[CP1 Jurisdiction 1] [nvarchar](255) NULL,
	[CP1 Jurisdiction 2] [nvarchar](255) NULL,
	[CP1 Jurisdiction 3] [nvarchar](255) NULL,
	[CP1 TIN 1] [nvarchar](255) NULL,
	[CP1 TIN 1 issuedBy] [nvarchar](255) NULL,
	[CP1 TIN 2] [nvarchar](255) NULL,
	[CP1 TIN 2 issuedBy] [nvarchar](255) NULL,
	[CP1 TIN 3] [nvarchar](255) NULL,
	[CP1 TIN 3 issuedBy] [nvarchar](255) NULL,
	[CP2 FirstName] [nvarchar](255) NULL,
	[CP2 LastName] [nvarchar](255) NULL,
	[CP2 AddressFree] [nvarchar](750) NULL,
	[CP2 City] [nvarchar](255) NULL,
	[CP2 Country] [nvarchar](255) NULL,
	[CP2 Date of birth] [nvarchar](255) NULL,
	[CP2 Place of birth] [nvarchar](255) NULL,
	[CP2 Jurisdiction 1] [nvarchar](255) NULL,
	[CP2 Jurisdiction 2] [nvarchar](255) NULL,
	[CP2 Jurisdiction 3] [nvarchar](255) NULL,
	[CP2 TIN 1] [nvarchar](255) NULL,
	[CP2 TIN 1 issuedBy] [nvarchar](255) NULL,
	[CP2 TIN 2] [nvarchar](255) NULL,
	[CP2 TIN 2 issuedBy] [nvarchar](255) NULL,
	[CP2 TIN 3] [nvarchar](255) NULL,
	[CP2 TIN 3 issuedBy] [nvarchar](255) NULL,
	[CP3 FirstName] [nvarchar](255) NULL,
	[CP3 LastName] [nvarchar](255) NULL,
	[CP3 AddressFree] [nvarchar](750) NULL,
	[CP3 City] [nvarchar](255) NULL,
	[CP3 Country] [nvarchar](255) NULL,
	[CP3 Date of birth] [nvarchar](255) NULL,
	[CP3 Place of birth] [nvarchar](255) NULL,
	[CP3 Jurisdiction 1] [nvarchar](255) NULL,
	[CP3 Jurisdiction 2] [nvarchar](255) NULL,
	[CP3 Jurisdiction 3] [nvarchar](255) NULL,
	[CP3 TIN 1] [nvarchar](255) NULL,
	[CP3 TIN 1 issuedBy] [nvarchar](255) NULL,
	[CP3 TIN 2] [nvarchar](255) NULL,
	[CP3 TIN 2 issuedBy] [nvarchar](255) NULL,
	[CP3 TIN 3] [nvarchar](255) NULL,
	[CP3 TIN 3 issuedBy] [nvarchar](255) NULL,
	[AcctType] [nvarchar](10) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Entity]    Script Date: 06/19/2017 14:56:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Entity](
	[EntityId] [int] IDENTITY(2001,1) NOT NULL,
	[Name] [nvarchar](120) NOT NULL,
	[NameType] [nvarchar](12) NULL,
	[AcctID] [int] NOT NULL,
 CONSTRAINT [PK_Entity] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DocSpec]    Script Date: 06/19/2017 14:56:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DocSpec](
	[DocRefId] [nvarchar](40) NOT NULL,
	[DocTypeIndic] [nvarchar](12) NOT NULL,
	[MessageRefId] [nvarchar](40) NOT NULL,
	[CorrFileSerialNumber] [nvarchar](20) NULL,
	[CorrDocRefId] [nvarchar](40) NULL,
	[CorrAccountNumber] [nvarchar](72) NULL,
	[AcctID] [int] NOT NULL,
 CONSTRAINT [PK_DocSpec] PRIMARY KEY CLUSTERED 
(
	[DocRefId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CountryList]    Script Date: 06/19/2017 14:56:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CountryList](
	[CountryName] [nvarchar](255) NULL,
	[CountryCode] [nvarchar](3) NULL,
	[NumericCode] [int] NOT NULL,
 CONSTRAINT [PK_CountryList] PRIMARY KEY CLUSTERED 
(
	[NumericCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'AFGHANISTAN', N'AF', 4)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'ALBANIA', N'AL', 8)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'ANTARCTICA', N'AQ', 10)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'ALGERIA', N'DZ', 12)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'AMERICAN SAMOA', N'AS', 16)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'ANDORRA', N'AD', 20)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'ANGOLA', N'AO', 24)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'ANTIGUA AND BARBUDA', N'AG', 28)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'AZERBAIJAN', N'AZ', 31)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'ARGENTINA', N'AR', 32)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'AUSTRALIA', N'AU', 36)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'AUSTRIA', N'AT', 40)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'BAHAMAS', N'BS', 44)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'BAHRAIN', N'BH', 48)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'BANGLADESH', N'BD', 50)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'ARMENIA', N'AM', 51)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'BARBADOS', N'BB', 52)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'BELGIUM', N'BE', 56)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'BERMUDA', N'BM', 60)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'BHUTAN', N'BT', 64)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'BOLIVIA, PLURINATIONAL STATE OF', N'BO', 68)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'BOSNIA AND HERZEGOVINA', N'BA', 70)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'BOTSWANA', N'BW', 72)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'BOUVET ISLAND', N'BV', 74)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'BRAZIL', N'BR', 76)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'BELIZE', N'BZ', 84)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'BRITISH INDIAN OCEAN TERRITORY', N'IO', 86)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'SOLOMON ISLANDS', N'SB', 90)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'VIRGIN ISLANDS (BRITISH)', N'VG', 92)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'BRUNEI DARUSSALAM', N'BN', 96)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'BULGARIA', N'BG', 100)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'MYANMAR', N'MM', 104)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'BURUNDI', N'BI', 108)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'BELARUS', N'BY', 112)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'CAMBODIA', N'KH', 116)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'CAMEROON', N'CM', 120)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'CANADA', N'CA', 124)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'CAPE VERDE', N'CV', 132)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'CAYMAN ISLANDS', N'KY', 136)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'CENTRAL AFRICAN REPUBLIC', N'CF', 140)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'SRI LANKA', N'LK', 144)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'CHAD', N'TD', 148)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'CHILE', N'CL', 152)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'CHINA', N'CN', 156)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'TAIWAN', N'TW', 158)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'CHRISTMAS ISLAND', N'CX', 162)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'COCOS (KEELING) ISLANDS', N'CC', 166)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'COLOMBIA', N'CO', 170)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'COMOROS', N'KM', 174)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'MAYOTTE', N'YT', 175)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'CONGO', N'CG', 178)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'CONGO, DEMOCRATIC REPUBLIC OF THE', N'CD', 180)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'COOK ISLANDS', N'CK', 184)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'COSTA RICA', N'CR', 188)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'CROATIA', N'HR', 191)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'CUBA', N'CU', 192)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'CYPRUS', N'CY', 196)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'CZECH REPUBLIC', N'CZ', 203)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'BENIN', N'BJ', 204)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'DENMARK', N'DK', 208)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'DOMINICA', N'DM', 212)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'DOMINICAN REPUBLIC', N'DO', 214)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'ECUADOR', N'EC', 218)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'EL SALVADOR', N'SV', 222)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'EQUATORIAL GUINEA', N'GQ', 226)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'ETHIOPIA', N'ET', 231)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'ERITREA', N'ER', 232)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'ESTONIA', N'EE', 233)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'FAROE ISLANDS', N'FO', 234)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'FALKLAND ISLANDS (MALVINAS)', N'FK', 238)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'SOUTH GEORGIA AND THE SOUTH SANDWICH ISLANDS', N'GS', 239)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'FIJI', N'FJ', 242)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'FINLAND', N'FI', 246)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'ALAND ISLANDS', N'AX', 248)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'FRANCE', N'FR', 250)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'FRENCH GUIANA', N'GF', 254)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'FRENCH POLYNESIA', N'PF', 258)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'FRENCH SOUTHERN TERRITORIES', N'TF', 260)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'DJIBOUTI', N'DJ', 262)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'GABON', N'GA', 266)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'GEORGIA', N'GE', 268)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'GAMBIA', N'GM', 270)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'WEST BANK AND GAZA', N'PS', 275)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'GERMANY', N'DE', 276)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'GHANA', N'GH', 288)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'GIBRALTAR', N'GI', 292)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'KIRIBATI', N'KI', 296)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'GREECE', N'GR', 300)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'GREENLAND', N'GL', 304)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'GRENADA', N'GD', 308)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'GUADELOUPE', N'GP', 312)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'GUAM', N'GU', 316)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'GUATEMALA', N'GT', 320)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'GUINEA', N'GN', 324)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'GUYANA', N'GY', 328)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'HAITI', N'HT', 332)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'HEARD ISLAND AND MCDONALD ISLANDS', N'HM', 334)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'HOLY SEE (VATICAN CITY STATE)', N'VA', 336)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'HONDURAS', N'HN', 340)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'HONG KONG', N'HK', 344)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'HUNGARY', N'HU', 348)
GO
print 'Processed 100 total records'
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'ICELAND', N'IS', 352)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'INDIA', N'IN', 356)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'INDONESIA', N'ID', 360)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'IRAN, ISLAMIC REPUBLIC OF', N'IR', 364)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'IRAQ', N'IQ', 368)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'IRELAND', N'IE', 372)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'ISRAEL', N'IL', 376)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'ITALY', N'IT', 380)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'COTE D''IVOIRE', N'CI', 384)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'JAMAICA', N'JM', 388)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'JAPAN', N'JP', 392)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'KAZAKHSTAN', N'KZ', 398)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'JORDAN', N'JO', 400)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'KENYA', N'KE', 404)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'KOREA, DEMOCRATIC PEOPLE''S REPUBLIC OF', N'KP', 408)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'KOREA, REPUBLIC OF', N'KR', 410)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'KUWAIT', N'KW', 414)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'KYRGYZSTAN', N'KG', 417)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'LAO PEOPLE''S DEMOCRATIC REPUBLIC', N'LA', 418)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'LEBANON', N'LB', 422)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'LESOTHO', N'LS', 426)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'LATVIA', N'LV', 428)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'LIBERIA', N'LR', 430)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'LIBYA', N'LY', 434)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'LIECHTENSTEIN', N'LI', 438)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'LITHUANIA', N'LT', 440)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'LUXEMBOURG', N'LU', 442)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'MACAO', N'MO', 446)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'MADAGASCAR', N'MG', 450)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'MALAWI', N'MW', 454)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'MALAYSIA', N'MY', 458)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'MALDIVES', N'MV', 462)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'MALI', N'ML', 466)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'MALTA', N'MT', 470)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'MARTINIQUE', N'MQ', 474)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'MAURITANIA', N'MR', 478)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'MAURITIUS', N'MU', 480)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'MEXICO', N'MX', 484)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'MONACO', N'MC', 492)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'MONGOLIA', N'MN', 496)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'MOLDOVA, REPUBLIC OF', N'MD', 498)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'MONTENEGRO', N'ME', 499)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'MONTSERRAT', N'MS', 500)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'MOROCCO', N'MA', 504)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'MOZAMBIQUE', N'MZ', 508)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'OMAN', N'OM', 512)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'NAMIBIA', N'NA', 516)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'NAURU', N'NR', 520)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'NEPAL', N'NP', 524)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'NETHERLANDS', N'NL', 528)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'CURACAO', N'CW', 531)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'ARUBA', N'AW', 533)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'SINT MAARTEN (DUTCH PART)', N'SX', 534)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'BONAIRE, SINT EUSTATIUS AND SABA', N'BQ', 535)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'NEW CALEDONIA', N'NC', 540)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'VANUATU', N'VU', 548)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'NEW ZEALAND', N'NZ', 554)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'NICARAGUA', N'NI', 558)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'NIGER', N'NE', 562)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'NIGERIA', N'NG', 566)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'NIUE', N'NU', 570)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'NORFOLK ISLAND', N'NF', 574)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'NORWAY', N'NO', 578)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'NORTHERN MARIANA ISLANDS', N'MP', 580)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'UNITED STATES MINOR OUTLYING ISLANDS', N'UM', 581)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'MICRONESIA, FEDERATED STATES OF', N'FM', 583)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'MARSHALL ISLANDS', N'MH', 584)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'PALAU', N'PW', 585)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'PAKISTAN', N'PK', 586)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'PANAMA', N'PA', 591)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'PAPUA NEW GUINEA', N'PG', 598)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'PARAGUAY', N'PY', 600)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'PERU', N'PE', 604)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'PHILIPPINES', N'PH', 608)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'PITCAIRN', N'PN', 612)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'POLAND', N'PL', 616)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'PORTUGAL', N'PT', 620)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'GUINEA-BISSAU', N'GW', 624)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'TIMOR-LESTE', N'TL', 626)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'PUERTO RICO', N'PR', 630)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'QATAR', N'QA', 634)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'REUNION', N'RE', 638)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'ROMANIA', N'RO', 642)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'RUSSIAN FEDERATION', N'RU', 643)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'RWANDA', N'RW', 646)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'SAINT BARTHÉLEMY', N'BL', 652)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'SAINT HELENA, ASCENSION AND TRISTAN DA CUNHA', N'SH', 654)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'SAINT KITTS AND NEVIS', N'KN', 659)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'ANGUILLA', N'AI', 660)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'SAINT LUCIA', N'LC', 662)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'SAINT MARTIN (FRENCH PART)', N'MF', 663)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'SAINT PIERRE AND MIQUELON', N'PM', 666)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'SAINT VINCENT AND THE GRENADINES', N'VC', 670)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'SAN MARINO', N'SM', 674)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'SAO TOME AND PRINCIPE', N'ST', 678)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'SAUDI ARABIA', N'SA', 682)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'SENEGAL', N'SN', 686)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'SERBIA', N'RS', 688)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'SEYCHELLES', N'SC', 690)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'SIERRA LEONE', N'SL', 694)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'SINGAPORE', N'SG', 702)
GO
print 'Processed 200 total records'
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'SLOVAKIA', N'SK', 703)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'VIET NAM', N'VN', 704)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'SLOVENIA', N'SI', 705)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'SOUTH AFRICA', N'ZA', 710)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'ZIMBABWE', N'ZW', 716)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'SPAIN', N'ES', 724)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'SOUTH SUDAN', N'SS', 728)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'SUDAN', N'SD', 729)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'WESTERN SAHARA', N'EH', 732)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'SURINAME', N'SR', 740)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'SWAZILAND', N'SZ', 748)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'SWEDEN', N'SE', 752)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'SWITZERLAND', N'CH', 756)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'SYRIAN ARAB REPUBLIC', N'SY', 760)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'TAJIKISTAN', N'TJ', 762)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'THAILAND', N'TH', 764)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'TOGO', N'TG', 768)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'TOKELAU', N'TK', 772)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'TONGA', N'TO', 776)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'TRINIDAD AND TOBAGO', N'TT', 780)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'UNITED ARAB EMIRATES', N'AE', 784)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'TUNISIA', N'TN', 788)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'TURKEY', N'TR', 792)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'TURKMENISTAN', N'TM', 795)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'TURKS AND CAICOS ISLANDS', N'TC', 796)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'TUVALU', N'TV', 798)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'UGANDA', N'UG', 800)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'UKRAINE', N'UA', 804)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'MACEDONIA, THE FORMER YUGOSLAV REPUBLIC OF', N'MK', 807)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'EGYPT', N'EG', 818)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'UNITED KINGDOM', N'GB', 826)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'GUERNSEY', N'GG', 831)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'JERSEY', N'JE', 832)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'ISLE OF MAN', N'IM', 833)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'TANZANIA, UNITED REPUBLIC OF', N'TZ', 834)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'UNITED STATES', N'US', 840)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'VIRGIN ISLANDS (U.S.)', N'VI', 850)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'BURKINA FASO', N'BF', 854)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'URUGUAY', N'UY', 858)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'UZBEKISTAN', N'UZ', 860)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'VENEZUELA, BOLIVARIAN REPUBLIC OF', N'VE', 862)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'WALLIS AND FUTUNA', N'WF', 876)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'SAMOA', N'WS', 882)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'YEMEN', N'YE', 887)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'ZAMBIA', N'ZM', 894)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'KOSOVO', N'XK', 901)
INSERT [dbo].[CountryList] ([CountryName], [CountryCode], [NumericCode]) VALUES (N'OTHER', N'XX', 999)
/****** Object:  Table [dbo].[ControllingPerson]    Script Date: 06/19/2017 14:56:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ControllingPerson](
	[CtrlPersonType] [nvarchar](12) NULL,
	[AcctID] [int] NOT NULL,
	[PId] [int] NOT NULL,
 CONSTRAINT [PK_ControllingPerson] PRIMARY KEY CLUSTERED 
(
	[AcctID] ASC,
	[PId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AeoiProfile]    Script Date: 06/19/2017 14:56:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AeoiProfile](
	[AeoiId] [nvarchar](12) NOT NULL,
	[FIName] [nvarchar](120) NOT NULL,
 CONSTRAINT [PK_AeoiProfile] PRIMARY KEY CLUSTERED 
(
	[AeoiId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Address]    Script Date: 06/19/2017 14:56:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Address](
	[AddrId] [int] IDENTITY(3001,1) NOT NULL,
	[CountryCode] [nvarchar](2) NOT NULL,
	[Street] [nvarchar](100) NULL,
	[BuildingIdentifier] [nvarchar](70) NULL,
	[SuiteIdentifier] [nvarchar](40) NULL,
	[FloorIdentifier] [nvarchar](20) NULL,
	[DistrictName] [nvarchar](70) NULL,
	[POB] [nvarchar](70) NULL,
	[PostCode] [nvarchar](35) NULL,
	[City] [nvarchar](70) NULL,
	[CountrySubentity] [nvarchar](70) NULL,
	[FreeLine] [nvarchar](750) NULL,
	[AddressType] [nvarchar](12) NULL,
	[P_Ent_Id] [int] NOT NULL,
 CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED 
(
	[AddrId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AcctHolderType]    Script Date: 06/19/2017 14:56:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AcctHolderType](
	[CrsCode] [nvarchar](10) NOT NULL,
	[CrsDescription] [nvarchar](255) NOT NULL
) ON [PRIMARY]
GO
INSERT [dbo].[AcctHolderType] ([CrsCode], [CrsDescription]) VALUES (N'CRS101', N'Passive Non-Financial Entity with one or more controlling person that is a Reportable Person')
INSERT [dbo].[AcctHolderType] ([CrsCode], [CrsDescription]) VALUES (N'CRS102', N'Reportable Person')
INSERT [dbo].[AcctHolderType] ([CrsCode], [CrsDescription]) VALUES (N'CRS103', N'Passive NFE that is a Reportable Person')
/****** Object:  Table [dbo].[Account]    Script Date: 06/19/2017 14:56:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[AcctID] [int] IDENTITY(1,1) NOT NULL,
	[AcctNumber] [nvarchar](72) NOT NULL,
	[AcctNumberType] [nvarchar](12) NULL,
	[CurrCode] [nvarchar](3) NOT NULL,
	[AccountBalance] [decimal](16, 2) NOT NULL,
	[isUndocumented] [bit] NULL,
	[isClosed] [bit] NULL,
	[isDormant] [bit] NULL,
	[AcctType] [nvarchar](10) NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[AcctID] ASC,
	[AcctNumber] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ResCountryCode]    Script Date: 06/19/2017 14:56:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ResCountryCode](
	[P_Ent_Id] [int] NOT NULL,
	[CountryCode] [nvarchar](2) NOT NULL,
	[AcctHolderType] [nvarchar](12) NULL,
	[isReportable] [bit] NOT NULL,
 CONSTRAINT [PK_ResCountryCode] PRIMARY KEY CLUSTERED 
(
	[P_Ent_Id] ASC,
	[CountryCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PersonAcctHolder]    Script Date: 06/19/2017 14:56:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PersonAcctHolder](
	[AcctID] [int] NOT NULL,
	[PId] [int] NOT NULL,
 CONSTRAINT [PK_PersonAcctHolder] PRIMARY KEY CLUSTERED 
(
	[AcctID] ASC,
	[PId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Person]    Script Date: 06/19/2017 14:56:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Person](
	[PId] [int] IDENTITY(1001,1) NOT NULL,
	[PrecedingTitle] [nvarchar](100) NULL,
	[Title] [nvarchar](100) NULL,
	[FirstName] [nvarchar](70) NOT NULL,
	[MiddleName] [nvarchar](120) NULL,
	[NamePrefix] [nvarchar](30) NULL,
	[LastName] [nvarchar](70) NOT NULL,
	[GenerationIdentifier] [nvarchar](100) NULL,
	[Suffix] [nvarchar](100) NULL,
	[GeneralSuffix] [nvarchar](30) NULL,
	[NameType] [nvarchar](12) NULL,
	[BirthDate] [nvarchar](12) NULL,
	[BirthCity] [nvarchar](70) NULL,
	[BirthCitySubentity] [nvarchar](70) NULL,
	[BirthCountry] [nvarchar](70) NULL,
 CONSTRAINT [PK_Person] PRIMARY KEY CLUSTERED 
(
	[PId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payment]    Script Date: 06/19/2017 14:56:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payment](
	[PaymentId] [int] IDENTITY(5001,1) NOT NULL,
	[PaymentType] [nvarchar](12) NOT NULL,
	[Amount] [decimal](16, 2) NOT NULL,
	[CurrCode] [nvarchar](3) NOT NULL,
	[AcctID] [int] NOT NULL,
 CONSTRAINT [PK_Payment] PRIMARY KEY CLUSTERED 
(
	[PaymentId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MessageSpec]    Script Date: 06/19/2017 14:56:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MessageSpec](
	[MessageRefId] [nvarchar](40) NOT NULL,
	[MessageTypeIndic] [nvarchar](12) NOT NULL,
	[ReturnYear] [nvarchar](4) NOT NULL,
	[FileSerialNumber] [nvarchar](20) NULL,
	[AttentionNote] [nvarchar](150) NULL,
	[Contact] [nvarchar](120) NULL,
	[AeoiId] [nvarchar](12) NOT NULL,
 CONSTRAINT [PK_MessageSpec] PRIMARY KEY CLUSTERED 
(
	[MessageRefId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[INType]    Script Date: 06/19/2017 14:56:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[INType](
	[INId] [int] IDENTITY(4001,1) NOT NULL,
	[Value] [nvarchar](80) NOT NULL,
	[CountryCode] [nvarchar](2) NULL,
	[IType] [nvarchar](30) NULL,
	[P_Ent_Id] [int] NOT NULL,
 CONSTRAINT [PK_INType] PRIMARY KEY CLUSTERED 
(
	[Value] ASC,
	[P_Ent_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Individual_tbl]    Script Date: 06/19/2017 14:56:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Individual_tbl](
	[FirstName] [nvarchar](255) NOT NULL,
	[LastName] [nvarchar](255) NOT NULL,
	[AddressFree] [nvarchar](750) NULL,
	[City] [nvarchar](255) NOT NULL,
	[Country] [nvarchar](255) NOT NULL,
	[Date of birth] [nvarchar](255) NULL,
	[Place of birth] [nvarchar](255) NULL,
	[Jurisdiction 1] [nvarchar](255) NULL,
	[Jurisdiction 2] [nvarchar](255) NULL,
	[Jurisdiction 3] [nvarchar](255) NULL,
	[TIN 1] [nvarchar](255) NULL,
	[TIN 1 issuedBy ] [nvarchar](255) NULL,
	[TIN 2] [nvarchar](255) NULL,
	[TIN 2 issuedBy ] [nvarchar](255) NULL,
	[TIN 3 ] [nvarchar](255) NULL,
	[TIN 3 issuedBy ] [nvarchar](255) NULL,
	[Account Number] [nvarchar](255) NOT NULL,
	[Currency Code] [nvarchar](255) NOT NULL,
	[Account Balance] [decimal](16, 2) NOT NULL,
	[Gross amount of interest] [decimal](16, 2) NULL,
	[Gross amount of dividend] [decimal](16, 2) NULL,
	[Gross amount of other income] [decimal](16, 2) NULL,
	[Gross proceeds] [decimal](16, 2) NULL,
	[AcctType] [nvarchar](10) NULL
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[GetCountryCode]    Script Date: 06/19/2017 14:56:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetCountryCode] @countryname nvarchar(255), @CountryInput nvarchar(2) OUTPUT
AS
SELECT CountryCode FROM CountryList 
WHERE CountryName=UPPER(@countryname) OR CountryCode=UPPER(@countryname)
GO
/****** Object:  Default [DF__ResCountr__isRep__1920BF5C]    Script Date: 06/19/2017 14:56:43 ******/
ALTER TABLE [dbo].[ResCountryCode] ADD  DEFAULT ((1)) FOR [isReportable]
GO
