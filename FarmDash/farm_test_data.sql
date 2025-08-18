-- PostgreSQL script to generate 100 random farm records
-- Assumes your table is named "farms" (adjust table name as needed)

INSERT INTO "Farms" ("FarmID", "Name", "Location", "Description", "Animal", "DeathRate", "SickRate", "State", "Created")
SELECT 
    -- FarmID: Sequential numbers starting from 1001
    1000 + generate_series,
    
    -- Name: Random farm names
    (ARRAY[
        'Sunset Valley Farm', 'Green Meadows Ranch', 'Golden Harvest Farm', 'Rolling Hills Ranch',
        'Prairie Wind Farm', 'Oak Grove Ranch', 'Riverside Farm', 'Mountain View Ranch',
        'Peaceful Acres Farm', 'Spring Creek Ranch', 'Wildflower Farm', 'Cedar Point Ranch',
        'Maple Ridge Farm', 'Thunder Valley Ranch', 'Silver Stream Farm', 'Eagle Peak Ranch',
        'Harvest Moon Farm', 'Pine Valley Ranch', 'Blue Sky Farm', 'Redwood Ranch',
        'Sunshine Acres', 'Whispering Oaks Farm', 'Crystal Lake Ranch', 'Storm Ridge Farm',
        'Morning Glory Ranch', 'Autumn Leaf Farm', 'Big Sky Ranch', 'Emerald Valley Farm'
    ])[floor(random() * 28 + 1)::int],
    
    -- Location: Random city combinations
    (ARRAY[
        'Springfield', 'Madison', 'Franklin', 'Georgetown', 'Riverside', 'Oak Hill',
        'Fairview', 'Greenville', 'Salem', 'Lancaster', 'Bristol', 'Clinton',
        'Ashland', 'Burlington', 'Chester', 'Dover', 'Newport', 'Richmond'
    ])[floor(random() * 18 + 1)::int] || ', ' ||
    (ARRAY['County', 'Valley', 'Township', 'District'])[floor(random() * 4 + 1)::int],
    
    -- Description: Random descriptions
    (ARRAY[
        'Family-owned farm specializing in sustainable agriculture practices',
        'Large commercial operation with modern facilities and equipment',
        'Organic certified farm focusing on natural farming methods',
        'Heritage breed livestock farm preserving traditional varieties',
        'Small-scale artisanal farm with direct-to-consumer sales',
        'Research and development farm testing new agricultural techniques',
        'Community-supported agriculture farm serving local families',
        'Diversified farm operation with multiple livestock species',
        'Eco-friendly farm using renewable energy and water conservation',
        'Premium quality farm supplying high-end restaurants and markets',
        'Educational farm offering tours and agricultural workshops',
        'Cooperative farm owned and operated by multiple families'
    ])[floor(random() * 12 + 1)::int],
    
    -- Animal: Random livestock types
    (ARRAY[
        'Cattle', 'Pigs', 'Chickens', 'Sheep', 'Goats', 'Turkeys', 
        'Ducks', 'Horses', 'Alpacas', 'Rabbits', 'Mixed Livestock'
    ])[floor(random() * 11 + 1)::int],
    
    -- DeathRate: Random rate between 0.5 and 15.0 per 1000 animals
    round((random() * 14.5 + 0.5)::numeric, 2),
    
    -- SickRate: Random rate between 5.0 and 80.0 per 1000 animals
    round((random() * 75 + 5)::numeric, 2),
    
    -- State: Random US states
    (ARRAY[
        'Alabama', 'Alaska', 'Arizona', 'Arkansas', 'California', 'Colorado', 'Connecticut',
        'Delaware', 'Florida', 'Georgia', 'Hawaii', 'Idaho', 'Illinois', 'Indiana', 'Iowa',
        'Kansas', 'Kentucky', 'Louisiana', 'Maine', 'Maryland', 'Massachusetts', 'Michigan',
        'Minnesota', 'Mississippi', 'Missouri', 'Montana', 'Nebraska', 'Nevada', 'New Hampshire',
        'New Jersey', 'New Mexico', 'New York', 'North Carolina', 'North Dakota', 'Ohio',
        'Oklahoma', 'Oregon', 'Pennsylvania', 'Rhode Island', 'South Carolina', 'South Dakota',
        'Tennessee', 'Texas', 'Utah', 'Vermont', 'Virginia', 'Washington', 'West Virginia',
        'Wisconsin', 'Wyoming'
    ])[floor(random() * 50 + 1)::int],
    
    -- Created: Random dates within the last 2 years
    NOW() - INTERVAL '1 day' * floor(random() * 730)
    
FROM generate_series(1, 100);

-- Optional: Update statistics after insert for better query performance
ANALYZE "Farms";
