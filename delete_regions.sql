-- Delete all records from Regions table
-- Run this script in your SQL client

USE NZWalksDb;

-- Delete all regions
DELETE FROM Regions;

-- Verify the table is empty
SELECT COUNT(*) as RegionCount FROM Regions;

-- Optional: Reset identity if you have an identity column
-- DBCC CHECKIDENT ('Regions', RESEED, 0);
