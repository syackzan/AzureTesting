-- USE master
-- DROP DATABASE DotNetCourseDatabase

-- CREATE DATABASE DotNetCourseDatabase
-- GO

-- USE DotNetCourseDatabase
-- GO

-- CREATE SCHEMA TutorialAppSchema
-- GO

-- CREATE TABLE TutorialAppSchema.Computer(
-- 	ComputerId INT IDENTITY(1,1) PRIMARY KEY,
-- 	Motherboard NVARCHAR(50),
-- 	CPUCores INT,
-- 	HasWifi BIT,
-- 	HasLTE BIT,
-- 	ReleaseDate DATE,
-- 	Price DECIMAL(18,4),
-- 	VideoCard NVARCHAR(50)
-- );

USE DotNetCourseDatabase
GO

--Removes Elements from a table
--TRUNCATE TABLE TutorialAppSchema.Computer

SELECT * FROM TutorialAppSchema.Computer

-- SELECT * FROM TutorialAppSchema.Computer WHERE VideoCard = 'Robel-O''Hara'

-- INSERT INTO TutorialAppSchema.Computer (
--                 MotherBoard,
--                 CPUCores,
--                 HasWifi,
--                 HasLTE,
--                 ReleaseDate,
--                 Price,
--                 VideoCard
--             ) VALUES ( 'L620','16','True','False','9/29/2023 9:30:12 AM','1000.76','ASDFASFE12')
