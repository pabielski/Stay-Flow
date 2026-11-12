CREATE TABLE hotels (
    id             bigint GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    name           varchar(200) NOT NULL,
    city           varchar(100) NOT NULL,
    address        varchar(300) NOT NULL,
    phone          varchar(30),
    email          varchar(254),
    check_in_time  time         NOT NULL DEFAULT '15:00',
    is_active      boolean      NOT NULL DEFAULT true,
    created_at     timestamptz  NOT NULL DEFAULT now(),
    updated_at     timestamptz  NOT NULL DEFAULT now(),

    CONSTRAINT uq_hotels_name_city UNIQUE (name, city)
);

CREATE TABLE room_types (
    id             bigint GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    hotel_id       bigint       NOT NULL,
    name           varchar(200) NOT NULL,
    description    text,
    max_occupancy  smallint     NOT NULL,
    size_m2        numeric(6,2) NOT NULL,
    is_active      boolean      NOT NULL DEFAULT true,
    created_at     timestamptz  NOT NULL DEFAULT now(),
    updated_at     timestamptz  NOT NULL DEFAULT now(),

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

CREATE TABLE users (
    id             bigint GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    hotel_id       bigint,
    email          varchar(254) NOT NULL,
    password_hash  varchar(255) NOT NULL,
    first_name     varchar(100) NOT NULL,
    last_name      varchar(100) NOT NULL,
    phone          varchar(30),
    is_active      boolean      NOT NULL DEFAULT true,
    last_login_at  timestamptz,
    created_at     timestamptz  NOT NULL DEFAULT now(),
    updated_at     timestamptz  NOT NULL DEFAULT now(),

    CONSTRAINT fk_users_hotels FOREIGN KEY (hotel_id)
        REFERENCES hotels (id) ON DELETE RESTRICT,
    CONSTRAINT uq_users_email UNIQUE (email),
    CONSTRAINT ck_users_email_lowercase CHECK (email = lower(email))
);

CREATE INDEX ix_users_hotel_id ON users (hotel_id);

CREATE TABLE roles (
    id           bigint GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    name         varchar(100) NOT NULL,
    description  text,
    is_system    boolean      NOT NULL DEFAULT false,
    created_at   timestamptz  NOT NULL DEFAULT now(),
    updated_at   timestamptz  NOT NULL DEFAULT now(),

    CONSTRAINT uq_roles_name UNIQUE (name)
);

CREATE TABLE permissions (
    id           bigint GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    code         varchar(100) NOT NULL,
    description  text         NOT NULL,

    CONSTRAINT uq_permissions_code UNIQUE (code)
);

CREATE TABLE role_permissions (
    id             bigint GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    role_id        bigint      NOT NULL,
    permission_id  bigint      NOT NULL,
    created_at     timestamptz NOT NULL DEFAULT now(),

    CONSTRAINT fk_role_permissions_roles FOREIGN KEY (role_id)
        REFERENCES roles (id) ON DELETE CASCADE,
    CONSTRAINT fk_role_permissions_permissions FOREIGN KEY (permission_id)
        REFERENCES permissions (id) ON DELETE RESTRICT,
    CONSTRAINT uq_role_permissions_role_permission UNIQUE (role_id, permission_id)
);

CREATE INDEX ix_role_permissions_permission_id ON role_permissions (permission_id);

CREATE TABLE user_roles (
    id          bigint GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    user_id     bigint      NOT NULL,
    role_id     bigint      NOT NULL,
    created_at  timestamptz NOT NULL DEFAULT now(),

    CONSTRAINT fk_user_roles_users FOREIGN KEY (user_id)
        REFERENCES users (id) ON DELETE CASCADE,
    CONSTRAINT fk_user_roles_roles FOREIGN KEY (role_id)
        REFERENCES roles (id) ON DELETE RESTRICT,
    CONSTRAINT uq_user_roles_user_role UNIQUE (user_id, role_id)
);

CREATE INDEX ix_user_roles_role_id ON user_roles (role_id);

CREATE TABLE content_pages (
    id                  bigint GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    hotel_id            bigint       NOT NULL,
    page_type           varchar(20)  NOT NULL,
    slug                varchar(100) NOT NULL,
    version             integer      NOT NULL DEFAULT 1,
    title               varchar(200) NOT NULL,
    content             text         NOT NULL,
    is_published        boolean      NOT NULL DEFAULT false,
    published_at        timestamptz,
    created_by_user_id  bigint,
    created_at          timestamptz  NOT NULL DEFAULT now(),
    updated_at          timestamptz  NOT NULL DEFAULT now(),

    CONSTRAINT fk_content_pages_hotels FOREIGN KEY (hotel_id)
        REFERENCES hotels (id) ON DELETE RESTRICT,
    CONSTRAINT fk_content_pages_users FOREIGN KEY (created_by_user_id)
        REFERENCES users (id) ON DELETE RESTRICT,
    CONSTRAINT uq_content_pages_hotel_slug_version UNIQUE (hotel_id, slug, version),
    CONSTRAINT ck_content_pages_page_type
        CHECK (page_type IN ('info', 'terms', 'privacy')),
    CONSTRAINT ck_content_pages_version CHECK (version > 0)
);

CREATE INDEX ix_content_pages_created_by_user_id ON content_pages (created_by_user_id);

CREATE TABLE document_templates (
    id          bigint GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    hotel_id    bigint       NOT NULL,
    code        varchar(50)  NOT NULL,
    name        varchar(200) NOT NULL,
    content     text         NOT NULL,
    is_active   boolean      NOT NULL DEFAULT true,
    created_at  timestamptz  NOT NULL DEFAULT now(),
    updated_at  timestamptz  NOT NULL DEFAULT now(),

    CONSTRAINT fk_document_templates_hotels FOREIGN KEY (hotel_id)
        REFERENCES hotels (id) ON DELETE RESTRICT,
    CONSTRAINT uq_document_templates_hotel_code UNIQUE (hotel_id, code)
);

CREATE TABLE guests (
    id          bigint GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    first_name  varchar(100) NOT NULL,
    last_name   varchar(100) NOT NULL,
    email       varchar(254) NOT NULL,
    phone       varchar(30),
    created_at  timestamptz  NOT NULL DEFAULT now(),
    updated_at  timestamptz  NOT NULL DEFAULT now(),

    CONSTRAINT uq_guests_email UNIQUE (email),
    CONSTRAINT ck_guests_email_lowercase CHECK (email = lower(email))
);

CREATE TABLE stays (
    id                bigint GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    room_id           bigint       NOT NULL,
    check_in_date     date         NOT NULL,
    check_out_date    date         NOT NULL,
    status            varchar(20)  NOT NULL DEFAULT 'expected',
    payment_status    varchar(20)  NOT NULL DEFAULT 'unpaid',
    check_in_token    varchar(100) NOT NULL,
    room_access_code  varchar(20),
    checked_out_at    timestamptz,
    notes             text,
    created_at        timestamptz  NOT NULL DEFAULT now(),
    updated_at        timestamptz  NOT NULL DEFAULT now(),

    CONSTRAINT fk_stays_rooms FOREIGN KEY (room_id)
        REFERENCES rooms (id) ON DELETE RESTRICT,
    CONSTRAINT uq_stays_check_in_token UNIQUE (check_in_token),
    CONSTRAINT ck_stays_status
        CHECK (status IN ('expected', 'checked_in', 'checked_out', 'cancelled')),
    CONSTRAINT ck_stays_payment_status
        CHECK (payment_status IN ('unpaid', 'paid', 'not_required')),
    CONSTRAINT ck_stays_dates
        CHECK (check_out_date > check_in_date)
);

CREATE INDEX ix_stays_room_id ON stays (room_id);

CREATE TABLE stay_guests (
    id          bigint GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    stay_id     bigint      NOT NULL,
    guest_id    bigint      NOT NULL,
    is_primary  boolean     NOT NULL DEFAULT false,
    created_at  timestamptz NOT NULL DEFAULT now(),

    CONSTRAINT fk_stay_guests_stays FOREIGN KEY (stay_id)
        REFERENCES stays (id) ON DELETE CASCADE,
    CONSTRAINT fk_stay_guests_guests FOREIGN KEY (guest_id)
        REFERENCES guests (id) ON DELETE RESTRICT,
    CONSTRAINT uq_stay_guests_stay_guest UNIQUE (stay_id, guest_id)
);

CREATE INDEX ix_stay_guests_guest_id ON stay_guests (guest_id);

CREATE UNIQUE INDEX uq_stay_guests_one_primary
    ON stay_guests (stay_id, is_primary)
    WHERE is_primary;

CREATE TABLE online_check_ins (
    id                      bigint GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    stay_id                 bigint      NOT NULL,
    completed_at            timestamptz NOT NULL DEFAULT now(),
    method                  varchar(20) NOT NULL DEFAULT 'online',
    completed_by_user_id    bigint,
    terms_page_id           bigint      NOT NULL,
    estimated_arrival_time  time,
    guest_notes             text,
    staff_notes             text,
    created_at              timestamptz NOT NULL DEFAULT now(),

    CONSTRAINT fk_online_check_ins_stays FOREIGN KEY (stay_id)
        REFERENCES stays (id) ON DELETE CASCADE,
    CONSTRAINT fk_online_check_ins_users FOREIGN KEY (completed_by_user_id)
        REFERENCES users (id) ON DELETE RESTRICT,
    CONSTRAINT fk_online_check_ins_content_pages FOREIGN KEY (terms_page_id)
        REFERENCES content_pages (id) ON DELETE RESTRICT,
    CONSTRAINT uq_online_check_ins_stay UNIQUE (stay_id),
    CONSTRAINT ck_online_check_ins_method
        CHECK (method IN ('online', 'manual')),
    CONSTRAINT ck_online_check_ins_completed_by
        CHECK (
            (method = 'online' AND completed_by_user_id IS NULL)
            OR
            (method = 'manual' AND completed_by_user_id IS NOT NULL)
        )
);

CREATE INDEX ix_online_check_ins_completed_by_user_id ON online_check_ins (completed_by_user_id);
CREATE INDEX ix_online_check_ins_terms_page_id ON online_check_ins (terms_page_id);


CREATE TABLE checklist_templates (
    id          bigint GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    hotel_id    bigint       NOT NULL,
    name        varchar(200) NOT NULL,
    task_type   varchar(20)  NOT NULL,
    is_active   boolean      NOT NULL DEFAULT true,
    created_at  timestamptz  NOT NULL DEFAULT now(),
    updated_at  timestamptz  NOT NULL DEFAULT now(),

    CONSTRAINT fk_checklist_templates_hotels FOREIGN KEY (hotel_id)
        REFERENCES hotels (id) ON DELETE RESTRICT,
    CONSTRAINT uq_checklist_templates_hotel_name UNIQUE (hotel_id, name),
    CONSTRAINT ck_checklist_templates_task_type
        CHECK (task_type IN ('departure', 'stay_over'))
);

CREATE TABLE checklist_template_items (
    id           bigint GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    template_id  bigint       NOT NULL,
    position     smallint     NOT NULL,
    description  varchar(300) NOT NULL,
    is_required  boolean      NOT NULL DEFAULT true,
    created_at   timestamptz  NOT NULL DEFAULT now(),
    updated_at   timestamptz  NOT NULL DEFAULT now(),

    CONSTRAINT fk_checklist_template_items_templates FOREIGN KEY (template_id)
        REFERENCES checklist_templates (id) ON DELETE CASCADE,
    CONSTRAINT ck_checklist_template_items_position CHECK (position > 0)
);

CREATE INDEX ix_checklist_template_items_template_id ON checklist_template_items (template_id);

CREATE TABLE housekeeping_tasks (
    id                     bigint GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    room_id                bigint      NOT NULL,
    stay_id                bigint,
    checklist_template_id  bigint      NOT NULL,
    task_type              varchar(20) NOT NULL,
    status                 varchar(20) NOT NULL DEFAULT 'pending',
    scheduled_date         date        NOT NULL,
    assigned_to_user_id    bigint,
    assigned_by_user_id    bigint,
    started_at             timestamptz,
    completed_at           timestamptz,
    inspected_by_user_id   bigint,
    inspected_at           timestamptz,
    inspection_notes       text,
    notes                  text,
    created_at             timestamptz NOT NULL DEFAULT now(),
    updated_at             timestamptz NOT NULL DEFAULT now(),

    CONSTRAINT fk_housekeeping_tasks_rooms FOREIGN KEY (room_id)
        REFERENCES rooms (id) ON DELETE RESTRICT,
    CONSTRAINT fk_housekeeping_tasks_stays FOREIGN KEY (stay_id)
        REFERENCES stays (id) ON DELETE RESTRICT,
    CONSTRAINT fk_housekeeping_tasks_templates FOREIGN KEY (checklist_template_id)
        REFERENCES checklist_templates (id) ON DELETE RESTRICT,
    CONSTRAINT fk_housekeeping_tasks_assigned_to FOREIGN KEY (assigned_to_user_id)
        REFERENCES users (id) ON DELETE RESTRICT,
    CONSTRAINT fk_housekeeping_tasks_assigned_by FOREIGN KEY (assigned_by_user_id)
        REFERENCES users (id) ON DELETE RESTRICT,
    CONSTRAINT fk_housekeeping_tasks_inspected_by FOREIGN KEY (inspected_by_user_id)
        REFERENCES users (id) ON DELETE RESTRICT,
    CONSTRAINT ck_housekeeping_tasks_task_type
        CHECK (task_type IN ('departure', 'stay_over')),
    CONSTRAINT ck_housekeeping_tasks_status
        CHECK (status IN ('pending', 'assigned', 'in_progress', 'completed', 'approved', 'needs_rework')),
    CONSTRAINT ck_housekeeping_tasks_assignee
        CHECK (status = 'pending' OR assigned_to_user_id IS NOT NULL)
);

CREATE INDEX ix_housekeeping_tasks_room_id ON housekeeping_tasks (room_id);
CREATE INDEX ix_housekeeping_tasks_stay_id ON housekeeping_tasks (stay_id);
CREATE INDEX ix_housekeeping_tasks_checklist_template_id ON housekeeping_tasks (checklist_template_id);
CREATE INDEX ix_housekeeping_tasks_assigned_to_status ON housekeeping_tasks (assigned_to_user_id, status);
CREATE INDEX ix_housekeeping_tasks_assigned_by_user_id ON housekeeping_tasks (assigned_by_user_id);
CREATE INDEX ix_housekeeping_tasks_inspected_by_user_id ON housekeeping_tasks (inspected_by_user_id);

CREATE TABLE housekeeping_task_checklist_items (
    id           bigint GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    task_id      bigint       NOT NULL,
    position     smallint     NOT NULL,
    description  varchar(300) NOT NULL,
    is_required  boolean      NOT NULL,
    is_done      boolean      NOT NULL DEFAULT false,
    done_at      timestamptz,

    CONSTRAINT fk_housekeeping_task_checklist_items_tasks FOREIGN KEY (task_id)
        REFERENCES housekeeping_tasks (id) ON DELETE CASCADE,
    CONSTRAINT ck_housekeeping_task_checklist_items_done
        CHECK (is_done = (done_at IS NOT NULL))
);

CREATE INDEX ix_housekeeping_task_checklist_items_task_id ON housekeeping_task_checklist_items (task_id);


CREATE TABLE issue_categories (
    id          bigint GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    name        varchar(100) NOT NULL,
    is_active   boolean      NOT NULL DEFAULT true,
    created_at  timestamptz  NOT NULL DEFAULT now(),
    updated_at  timestamptz  NOT NULL DEFAULT now(),

    CONSTRAINT uq_issue_categories_name UNIQUE (name)
);

CREATE TABLE issues (
    id                     bigint GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    hotel_id               bigint       NOT NULL,
    room_id                bigint,
    category_id            bigint       NOT NULL,
    title                  varchar(200) NOT NULL,
    description            text         NOT NULL,
    priority               varchar(20)  NOT NULL DEFAULT 'medium',
    status                 varchar(20)  NOT NULL DEFAULT 'open',
    reported_by_user_id    bigint,
    reported_by_guest_id   bigint,
    housekeeping_task_id   bigint,
    assigned_to_user_id    bigint,
    resolution_notes       text,
    resolved_at            timestamptz,
    closed_at              timestamptz,
    created_at             timestamptz  NOT NULL DEFAULT now(),
    updated_at             timestamptz  NOT NULL DEFAULT now(),

    CONSTRAINT fk_issues_hotels FOREIGN KEY (hotel_id)
        REFERENCES hotels (id) ON DELETE RESTRICT,
    CONSTRAINT fk_issues_rooms FOREIGN KEY (room_id)
        REFERENCES rooms (id) ON DELETE RESTRICT,
    CONSTRAINT fk_issues_categories FOREIGN KEY (category_id)
        REFERENCES issue_categories (id) ON DELETE RESTRICT,
    CONSTRAINT fk_issues_reported_by_user FOREIGN KEY (reported_by_user_id)
        REFERENCES users (id) ON DELETE RESTRICT,
    CONSTRAINT fk_issues_reported_by_guest FOREIGN KEY (reported_by_guest_id)
        REFERENCES guests (id) ON DELETE RESTRICT,
    CONSTRAINT fk_issues_housekeeping_tasks FOREIGN KEY (housekeeping_task_id)
        REFERENCES housekeeping_tasks (id) ON DELETE RESTRICT,
    CONSTRAINT fk_issues_assigned_to FOREIGN KEY (assigned_to_user_id)
        REFERENCES users (id) ON DELETE RESTRICT,
    CONSTRAINT ck_issues_priority
        CHECK (priority IN ('low', 'medium', 'high', 'critical')),
    CONSTRAINT ck_issues_status
        CHECK (status IN ('open', 'assigned', 'in_progress', 'resolved', 'closed')),
    CONSTRAINT ck_issues_one_reporter
        CHECK (num_nonnulls(reported_by_user_id, reported_by_guest_id) = 1),
    CONSTRAINT ck_issues_assignee
        CHECK (status = 'open' OR assigned_to_user_id IS NOT NULL)
);

CREATE INDEX ix_issues_hotel_status ON issues (hotel_id, status);
CREATE INDEX ix_issues_room_id ON issues (room_id);
CREATE INDEX ix_issues_category_id ON issues (category_id);
CREATE INDEX ix_issues_reported_by_user_id ON issues (reported_by_user_id);
CREATE INDEX ix_issues_reported_by_guest_id ON issues (reported_by_guest_id);
CREATE INDEX ix_issues_housekeeping_task_id ON issues (housekeeping_task_id);
CREATE INDEX ix_issues_assigned_to_user_id ON issues (assigned_to_user_id);

CREATE TABLE issue_comments (
    id               bigint GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    issue_id         bigint      NOT NULL,
    author_user_id   bigint,
    author_guest_id  bigint,
    content          text        NOT NULL,
    is_internal      boolean     NOT NULL DEFAULT false,
    created_at       timestamptz NOT NULL DEFAULT now(),

    CONSTRAINT fk_issue_comments_issues FOREIGN KEY (issue_id)
        REFERENCES issues (id) ON DELETE CASCADE,
    CONSTRAINT fk_issue_comments_users FOREIGN KEY (author_user_id)
        REFERENCES users (id) ON DELETE RESTRICT,
    CONSTRAINT fk_issue_comments_guests FOREIGN KEY (author_guest_id)
        REFERENCES guests (id) ON DELETE RESTRICT,
    CONSTRAINT ck_issue_comments_one_author
        CHECK (num_nonnulls(author_user_id, author_guest_id) = 1),
    CONSTRAINT ck_issue_comments_internal_by_staff
        CHECK (NOT is_internal OR author_user_id IS NOT NULL)
);

CREATE INDEX ix_issue_comments_issue_id ON issue_comments (issue_id);
CREATE INDEX ix_issue_comments_author_user_id ON issue_comments (author_user_id);
CREATE INDEX ix_issue_comments_author_guest_id ON issue_comments (author_guest_id);


CREATE TABLE service_categories (
    id          bigint GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    hotel_id    bigint       NOT NULL,
    name        varchar(100) NOT NULL,
    is_active   boolean      NOT NULL DEFAULT true,
    created_at  timestamptz  NOT NULL DEFAULT now(),
    updated_at  timestamptz  NOT NULL DEFAULT now(),

    CONSTRAINT fk_service_categories_hotels FOREIGN KEY (hotel_id)
        REFERENCES hotels (id) ON DELETE RESTRICT,
    CONSTRAINT uq_service_categories_hotel_name UNIQUE (hotel_id, name)
);

CREATE TABLE services (
    id                bigint GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    category_id       bigint        NOT NULL,
    name              varchar(200)  NOT NULL,
    description       text,
    price             numeric(10,2) NOT NULL,
    requires_booking  boolean       NOT NULL DEFAULT false,
    duration_minutes  smallint,
    capacity          smallint,
    available_from    time,
    available_to      time,
    is_active         boolean       NOT NULL DEFAULT true,
    created_at        timestamptz   NOT NULL DEFAULT now(),
    updated_at        timestamptz   NOT NULL DEFAULT now(),

    CONSTRAINT fk_services_categories FOREIGN KEY (category_id)
        REFERENCES service_categories (id) ON DELETE RESTRICT,
    CONSTRAINT uq_services_category_name UNIQUE (category_id, name),
    CONSTRAINT ck_services_price CHECK (price >= 0),
    CONSTRAINT ck_services_booking_fields
        CHECK (NOT requires_booking OR (duration_minutes IS NOT NULL AND capacity IS NOT NULL)),
    CONSTRAINT ck_services_duration CHECK (duration_minutes > 0),
    CONSTRAINT ck_services_capacity CHECK (capacity > 0),
    CONSTRAINT ck_services_hours
        CHECK (available_from IS NULL OR available_to IS NULL OR available_to > available_from)
);

CREATE TABLE service_orders (
    id                   bigint GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    service_id           bigint        NOT NULL,
    stay_id              bigint        NOT NULL,
    quantity             smallint      NOT NULL DEFAULT 1,
    unit_price           numeric(10,2) NOT NULL,
    status               varchar(20)   NOT NULL DEFAULT 'pending',
    scheduled_start      timestamptz,
    scheduled_end        timestamptz,
    assigned_to_user_id  bigint,
    guest_notes          text,
    staff_notes          text,
    completed_at         timestamptz,
    cancelled_at         timestamptz,
    created_at           timestamptz   NOT NULL DEFAULT now(),
    updated_at           timestamptz   NOT NULL DEFAULT now(),

    CONSTRAINT fk_service_orders_services FOREIGN KEY (service_id)
        REFERENCES services (id) ON DELETE RESTRICT,
    CONSTRAINT fk_service_orders_stays FOREIGN KEY (stay_id)
        REFERENCES stays (id) ON DELETE RESTRICT,
    CONSTRAINT fk_service_orders_users FOREIGN KEY (assigned_to_user_id)
        REFERENCES users (id) ON DELETE RESTRICT,
    CONSTRAINT ck_service_orders_quantity CHECK (quantity > 0),
    CONSTRAINT ck_service_orders_unit_price CHECK (unit_price >= 0),
    CONSTRAINT ck_service_orders_status
        CHECK (status IN ('pending', 'accepted', 'in_progress', 'completed', 'cancelled', 'rejected')),
    CONSTRAINT ck_service_orders_schedule
        CHECK (
            (scheduled_start IS NULL AND scheduled_end IS NULL)
            OR
            (scheduled_start IS NOT NULL AND scheduled_end IS NOT NULL AND scheduled_end > scheduled_start)
        )
);

CREATE INDEX ix_service_orders_service_schedule ON service_orders (service_id, scheduled_start);
CREATE INDEX ix_service_orders_stay_id ON service_orders (stay_id);
CREATE INDEX ix_service_orders_assigned_to_user_id ON service_orders (assigned_to_user_id);