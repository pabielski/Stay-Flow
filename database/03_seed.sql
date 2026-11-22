-- Dane przykładowe do developmentu i prezentacji.
-- Konto administratora nie jest tu tworzone: hasło musi zahashować Identity,
-- dlatego administrator jest zakładany przez backend przy starcie aplikacji.

INSERT INTO hotels (name, city, address, phone, email, check_in_time) VALUES
    ('Hotel Pod Lipami',   'Rzeszów', 'ul. Lipowa 1',     '+48 17 123 45 67', 'recepcja@podlipami.pl',  '15:00'),
    ('Hotel Morskie Oko',  'Gdańsk',  'ul. Portowa 5',    '+48 58 765 43 21', 'kontakt@morskieoko.pl',  '14:00'),
    ('Hotel Pod Wawelem',  'Kraków',  'ul. Grodzka 12',   NULL,               'hotel@podwawelem.pl',    '16:00');

INSERT INTO room_types (hotel_id, name, description, max_occupancy, size_m2, price_per_night) VALUES
    ((SELECT id FROM hotels WHERE name = 'Hotel Pod Lipami'),  'Jednoosobowy',         'Pokój z łóżkiem pojedynczym',      1, 14.00, 180.00),
    ((SELECT id FROM hotels WHERE name = 'Hotel Pod Lipami'),  'Dwuosobowy standard',  'Łóżko małżeńskie lub dwa osobne',  2, 18.50, 260.00),
    ((SELECT id FROM hotels WHERE name = 'Hotel Pod Lipami'),  'Apartament rodzinny',  'Dwa pokoje, aneks kuchenny',       5, 42.00, 520.00),
    ((SELECT id FROM hotels WHERE name = 'Hotel Morskie Oko'), 'Dwuosobowy z widokiem na morze', NULL,                     2, 22.00, 390.00),
    ((SELECT id FROM hotels WHERE name = 'Hotel Morskie Oko'), 'Studio',               'Pokój z aneksem kuchennym',        3, 28.00, 450.00),
    ((SELECT id FROM hotels WHERE name = 'Hotel Pod Wawelem'), 'Dwuosobowy standard',  NULL,                               2, 17.00, 310.00);

INSERT INTO identity.roles (name, normalized_name, concurrency_stamp, description, is_system) VALUES
    ('Administrator', 'ADMINISTRATOR', gen_random_uuid()::text, 'Pełny dostęp do systemu', true),
    ('Manager',       'MANAGER',       gen_random_uuid()::text, 'Zarządzanie hotelem i personelem', true),
    ('Recepcja',      'RECEPCJA',      gen_random_uuid()::text, 'Obsługa pobytów i gości', true),
    ('Pokojowa',      'POKOJOWA',      gen_random_uuid()::text, 'Wykonywanie zadań sprzątania', true),
    ('Technik',       'TECHNIK',       gen_random_uuid()::text, 'Obsługa zgłoszeń usterek', true);