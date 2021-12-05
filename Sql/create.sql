DO $$
BEGIN

---------------------------
-- SCHEMA                --
---------------------------
CREATE SCHEMA IF NOT EXISTS sample;

---------------------------
-- TABLES                --
---------------------------
CREATE TABLE IF NOT EXISTS sample.tenant
(
	tenant_id INTEGER PRIMARY KEY
	, name VARCHAR(255) NOT NULL
	, description VARCHAR(255) NOT NULL
    , UNIQUE(name)
);

CREATE TABLE IF NOT EXISTS sample.customer
(
	customer_id SERIAL PRIMARY KEY
	, first_name VARCHAR(255) NOT NULL
	, last_name VARCHAR(255) NOT NULL
	, tenant_name VARCHAR(255) NOT NULL DEFAULT current_setting('app.current_tenant')::VARCHAR
);

---------------------------
-- USERS                 --
---------------------------
IF NOT EXISTS (
  SELECT FROM pg_catalog.pg_roles
  WHERE  rolname = 'app_user') THEN

  CREATE ROLE app_user LOGIN PASSWORD 'app_user';
  
END IF;

---------------------------
-- RLS                   --
---------------------------
ALTER TABLE sample.customer ENABLE ROW LEVEL SECURITY;

---------------------------
-- RLS POLICIES         --
---------------------------

DROP POLICY IF EXISTS tenant_customer_isolation_policy ON sample.customer;

CREATE POLICY tenant_customer_isolation_policy ON sample.customer
    USING (tenant_name = current_setting('app.current_tenant')::VARCHAR);

--------------------------------
-- GRANTS                     --
--------------------------------
GRANT USAGE ON SCHEMA sample TO app_user;

-------------------------------------
-- GRANT TABLE                     --
-------------------------------------
GRANT SELECT ON TABLE sample.tenant TO app_user;

GRANT ALL ON SEQUENCE sample.customer_customer_id_seq TO app_user;
GRANT SELECT, UPDATE, INSERT, DELETE ON TABLE sample.customer TO app_user;


-------------------------------------
-- SAMPLE DATA                     --
-------------------------------------

INSERT INTO sample.tenant(tenant_id, name, description)
VALUES (1, '33F3857A-D8D7-449E-B71F-B5B960A6D89A', 'Tenant 1')
ON CONFLICT  DO NOTHING;

INSERT INTO sample.tenant(tenant_id, name, description)
VALUES (2, '7344384A-A2F4-4FC4-A382-315FCB421A72', 'Tenant 2')
ON CONFLICT  DO NOTHING;

END;
$$;