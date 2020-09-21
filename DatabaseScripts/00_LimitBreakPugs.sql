﻿USE [master]
GO

CREATE DATABASE [LimitBreakPugs]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'LimitBreakPugs', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\LimitBreakPugs.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'LimitBreakPugs_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\LimitBreakPugs_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [LimitBreakPugs].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [LimitBreakPugs] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [LimitBreakPugs] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [LimitBreakPugs] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [LimitBreakPugs] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [LimitBreakPugs] SET ARITHABORT OFF 
GO

ALTER DATABASE [LimitBreakPugs] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [LimitBreakPugs] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [LimitBreakPugs] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [LimitBreakPugs] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [LimitBreakPugs] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [LimitBreakPugs] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [LimitBreakPugs] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [LimitBreakPugs] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [LimitBreakPugs] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [LimitBreakPugs] SET  DISABLE_BROKER 
GO

ALTER DATABASE [LimitBreakPugs] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [LimitBreakPugs] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [LimitBreakPugs] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [LimitBreakPugs] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [LimitBreakPugs] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [LimitBreakPugs] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [LimitBreakPugs] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [LimitBreakPugs] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [LimitBreakPugs] SET  MULTI_USER 
GO

ALTER DATABASE [LimitBreakPugs] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [LimitBreakPugs] SET DB_CHAINING OFF 
GO

ALTER DATABASE [LimitBreakPugs] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [LimitBreakPugs] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [LimitBreakPugs] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [LimitBreakPugs] SET QUERY_STORE = OFF
GO

ALTER DATABASE [LimitBreakPugs] SET  READ_WRITE 
GO


