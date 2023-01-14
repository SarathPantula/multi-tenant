CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

create table if not exists tenant(
	id uuid not null constraint pk_tenant_id primary key default uuid_generate_v4(),
	name varchar(200) not null
);

create table if not exists account(
	id uuid not null constraint pk_account_id primary key default uuid_generate_v4(),
    tenant_id uuid not null,
	full_name varchar(200) not null,
	email varchar(200) not null,
    username varchar(200) not null,
    password varchar(200) not null,
    constraint fk_tenant_tenant_id foreign key(tenant_id) references tenant(id)
);

CREATE INDEX account_tenant_id_idx ON account (tenant_id);
CREATE INDEX account_email_idx ON account (email);
CREATE INDEX username ON account (username);

INSERT INTO tenant (name)
SELECT 'Tenant ' || (row_number() OVER (ORDER BY (SELECT NULL)))::integer
FROM generate_series(1, 25);

INSERT INTO account (tenant_id, full_name, email, username, password)
SELECT t.id, 'John Doe ' || t.id, 'johndoe' || t.id || '@example.com', 'johndoe' || t.id, 'password'
FROM tenant t;