USE [master]
GO
/****** Object:  Database [AEOIDB]    Script Date: 04/05/2017 15:27:01 ******/
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
/****** Object:  Table [dbo].[Entity_tbl]    Script Date: 04/05/2017 15:27:02 ******/
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
/****** Object:  Table [dbo].[Entity]    Script Date: 04/05/2017 15:27:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Entity](
	[EntityId] [int] IDENTITY(2001,1) NOT NULL,
	[Name] [nvarchar](120) NOT NULL,
	[NameType] [nvarchar](12) NULL,
	[AcctID] [int] NOT NULL,
	[AcctNumber] [nvarchar](72) NULL,
 CONSTRAINT [PK_Entity] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DocSpec]    Script Date: 04/05/2017 15:27:02 ******/
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
	[AcctNumber] [nvarchar](72) NOT NULL,
 CONSTRAINT [PK_DocSpec] PRIMARY KEY CLUSTERED 
(
	[DocRefId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CountryList]    Script Date: 04/05/2017 15:27:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CountryList](
	[CountryName] [nvarchar](255) NULL,
	[CountryCode] [nvarchar](3) NULL,
	[NumericCode] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ControllingPerson]    Script Date: 04/05/2017 15:27:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ControllingPerson](
	[CtrlPersonType] [nvarchar](12) NULL,
	[AcctID] [int] NOT NULL,
	[AcctNumber] [nvarchar](72) NOT NULL,
	[PId] [int] NOT NULL,
 CONSTRAINT [PK_ControllingPerson] PRIMARY KEY CLUSTERED 
(
	[AcctID] ASC,
	[AcctNumber] ASC,
	[PId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AeoiProfile]    Script Date: 04/05/2017 15:27:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AeoiProfile](
	[AeoiId] [nvarchar](12) NOT NULL,
	[FIName] [nvarchar](120) NOT NULL,
 CONSTRAINT [PK_AeoiProfile] PRIMARY KEY CLUSTERED 
(
	[AeoiId] ASC,
	[FIName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Address]    Script Date: 04/05/2017 15:27:02 ******/
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
/****** Object:  Table [dbo].[Account]    Script Date: 04/05/2017 15:27:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[AcctID] [int] NOT NULL IDENTITY(1,1),
	[AcctNumber] [nvarchar](72) NOT NULL,
	[AcctNumberType] [nvarchar](12) NULL,
	[CurrCode] [nvarchar](3) NOT NULL,
	[AccountBalance] [decimal](16, 2) NOT NULL,
	[isUndocumented] [bit] NULL,
	[isClosed] [bit] NULL,
	[isDormant] [bit] NULL,
	[AcctType] [nvarchar](10) NULL 
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ResCountryCode]    Script Date: 04/05/2017 15:27:02 ******/
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
/****** Object:  Table [dbo].[PersonAcctHolder]    Script Date: 04/05/2017 15:27:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PersonAcctHolder](
	[AcctID] [int] NOT NULL,
	[AcctNumber] [nvarchar](72) NOT NULL,
	[PId] [int] NOT NULL,
 CONSTRAINT [PK_PersonAcctHolder] PRIMARY KEY CLUSTERED 
(
	[AcctID] ASC,
	[AcctNumber] ASC,
	[PId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Person]    Script Date: 04/05/2017 15:27:02 ******/
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
/****** Object:  Table [dbo].[Payment]    Script Date: 04/05/2017 15:27:02 ******/
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
	[AcctNumber] [nvarchar](72) NOT NULL,
 CONSTRAINT [PK_Payment] PRIMARY KEY CLUSTERED 
(
	[PaymentId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MessageSpec]    Script Date: 04/05/2017 15:27:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MessageSpec](
	[MessageRefid] [nvarchar](40) NOT NULL,
	[MessageTypeIndic] [nvarchar](12) NOT NULL,
	[ReturnYear] [nvarchar](4) NOT NULL,
	[FileSerialNumber] [nvarchar](20) NULL,
	[AttentionNote] [nvarchar](150) NULL,
	[Contact] [nvarchar](120) NULL,
	[AeoiId] [nvarchar](12) NOT NULL,
 CONSTRAINT [PK_MessageSpec] PRIMARY KEY CLUSTERED 
(
	[MessageRefid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[INType]    Script Date: 04/05/2017 15:27:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[INType](
	[INId] [int] IDENTITY(4001,1) NOT NULL,
	[Value] [nvarchar](80) NOT NULL,
	[CountryCode] [nvarchar](2) NULL,
	[IType] [nvarchar](30) NULL,
	[P_Ent_Id] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Individual_tbl]    Script Date: 04/05/2017 15:27:02 ******/
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
/****** Object:  StoredProcedure [dbo].[GetCountryCode]    Script Date: 04/05/2017 15:27:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetCountryCode] @countryname nvarchar(255), @CountryInput nvarchar(2) OUTPUT
AS
SELECT CountryCode FROM CountryList 
WHERE CountryName=UPPER(@countryname) OR CountryCode=UPPER(@countryname)
GO
/****** Object:  Default [DF__ResCountr__isRep__1273C1CD]    Script Date: 04/05/2017 15:27:02 ******/
ALTER TABLE [dbo].[ResCountryCode] ADD  DEFAULT ((1)) FOR [isReportable]
GO
