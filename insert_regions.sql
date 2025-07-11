-- Insert sample data into Regions table
-- You can run this in any SQL client connected to your database

USE NZWalksDb;

-- Insert Auckland Region
INSERT INTO Regions (Id, Name, Code, RegionImageUrl)
VALUES (
    NEWID(),
    'Auckland',
    'AKL',
    'https://example.com/auckland.jpg'
);

-- Insert Wellington Region
INSERT INTO Regions (Id, Name, Code, RegionImageUrl)
VALUES (
    NEWID(),
    'Wellington',
    'WLG',
    'https://example.com/wellington.jpg'
);

-- Insert Canterbury Region
INSERT INTO Regions (Id, Name, Code, RegionImageUrl)
VALUES (
    NEWID(),
    'Canterbury',
    'CAN',
    'https://example.com/canterbury.jpg'
);

-- Verify the data was inserted
SELECT * FROM Regions;
