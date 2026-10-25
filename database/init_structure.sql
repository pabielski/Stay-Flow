CREATE TABLE hotels (
    id          bigint GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    name        varchar(200) NOT NULL,
    city        varchar(100) NOT NULL,
    address     varchar(300) NOT NULL,
    phone       varchar(30),
    email       varchar(254),
    check_in_time time NOT NULL DEFAULT '15:00'
    is_active   boolean      NOT NULL DEFAULT true,
    created_at  timestamptz  NOT NULL DEFAULT now(),
    updated_at  timestamptz  NOT NULL DEFAULT now(),

    CONSTRAINT uq_hotels_name_city UNIQUE (name, city)
);
CREATE TABLE room_types (
    id          bigint GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    hotel_id       bigint       NOT NULL,
    name        varchar(200) NOT NULL,
    description    text,
    max_occupancy  smallint     NOT NULL,
    size_m2     numeric(6,2) NOT NULL,
    is_active   boolean      NOT NULL DEFAULT true,
    created_at  timestamptz  NOT NULL DEFAULT now(),
    updated_at  timestamptz  NOT NULL DEFAULT now(),

    CONSTRAINT fk_room_types_hotels FOREIGN KEY (hotel_id)
        REFERENCES hotels (id) ON DELETE RESTRICT,
    CONSTRAINT uq_room_types_hotel_name UNIQUE (hotel_id, name),
    CONSTRAINT ck_room_types_max_occupancy CHECK (max_occupancy BETWEEN 1 AND 20),
    CONSTRAINT ck_room_types_size_m2 CHECK (size_m2 > 0)
);

CREATE TABLE rooms (
    id                bigint GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    hotel_id          bigint      NOT NULL,
    room_type_id      bigint      NOT NULL,
    number            varchar(10) NOT NULL,
    floor             smallint    NOT NULL,
    cleaning_status   varchar(20) NOT NULL DEFAULT 'dirty',
    technical_status  varchar(20) NOT NULL DEFAULT 'operational',
    notes             text,
    is_active         boolean     NOT NULL DEFAULT true,
    created_at        timestamptz NOT NULL DEFAULT now(),
    updated_at        timestamptz NOT NULL DEFAULT now(),

    CONSTRAINT fk_rooms_hotels FOREIGN KEY (hotel_id)
        REFERENCES hotels (id) ON DELETE RESTRICT,
    CONSTRAINT fk_rooms_room_types FOREIGN KEY (room_type_id)
        REFERENCES room_types (id) ON DELETE RESTRICT,
    CONSTRAINT uq_rooms_hotel_number UNIQUE (hotel_id, number),
    CONSTRAINT ck_rooms_cleaning_status
        CHECK (cleaning_status IN ('dirty', 'in_progress', 'clean', 'inspected')),
    CONSTRAINT ck_rooms_technical_status
        CHECK (technical_status IN ('operational', 'needs_repair', 'out_of_order'))
);

CREATE INDEX ix_rooms_room_type_id ON rooms (room_type_id);