--
-- PostgreSQL database dump
--

-- Dumped from database version 12.20 (Debian 12.20-1.pgdg110+1)
-- Dumped by pg_dump version 12.20 (Debian 12.20-1.pgdg110+1)

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: assignments; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.assignments (
    id uuid NOT NULL,
    title character varying(200) NOT NULL,
    description text NOT NULL,
    status smallint DEFAULT 1 NOT NULL,
    created_at timestamp without time zone DEFAULT CURRENT_TIMESTAMP NOT NULL,
    updated_at timestamp without time zone,
    user_id uuid NOT NULL,
    due_at date NOT NULL
);


ALTER TABLE public.assignments OWNER TO postgres;

--
-- Name: users; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.users (
    name character varying(50) NOT NULL,
    email character varying(50) NOT NULL,
    role smallint NOT NULL,
    created_at timestamp without time zone DEFAULT now() NOT NULL,
    id uuid NOT NULL,
    updated_at timestamp without time zone,
    status smallint DEFAULT 1 NOT NULL
);


ALTER TABLE public.users OWNER TO postgres;

--
-- Name: COLUMN users.name; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public.users.name IS 'Nombre del usuario.';


--
-- Name: COLUMN users.email; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public.users.email IS 'Email del usuario.';


--
-- Name: COLUMN users.role; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public.users.role IS 'Rol del usuario.';


--
-- Name: COLUMN users.created_at; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public.users.created_at IS 'Fecha y hora de creacion del usuario.';


--
-- Name: COLUMN users.id; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public.users.id IS 'Id del usuario.';


--
-- Name: COLUMN users.updated_at; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public.users.updated_at IS 'Fecha y hora de la ultima modificacion del usuario.';


--
-- Data for Name: assignments; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.assignments (id, title, description, status, created_at, updated_at, user_id, due_at) FROM stdin;
12ee4d16-0d7d-44fe-b1bb-74e468acf787	Task de demostracion	primer task	1	2026-04-25 12:11:10.930067	\N	47ccf05d-38a9-4a00-b31f-60e0eb98fb7c	2026-05-22
cd732118-75d9-4890-9bca-3eecfb3d155e	Task de demostracion 003	task para probar listado por usuario	1	2026-04-25 22:08:05.384587	\N	827fb15f-a8bb-4645-a045-6b82187a1f24	2026-05-01
59cd6fff-4f60-409e-b018-0b8a41fad8ab	Task de demostracion-cambio de tipo de dueAt	dueAt cambio a fecha	1	2026-04-26 14:12:14.654702	\N	827fb15f-a8bb-4645-a045-6b82187a1f24	2026-05-04
468c6d12-7532-4980-bb58-3978af4afc0d	Task de demostracion 002	segundo task	1	2026-04-25 12:14:22.980429	2026-04-27 18:50:59.38264	827fb15f-a8bb-4645-a045-6b82187a1f24	2026-11-30
700abd7b-8f3b-4dde-8ce5-2e2fd52dc68b	Cambio en la interfaz	mejora en la interfaz de tareas	4	2026-04-27 13:18:42.92026	2026-04-28 16:54:06.334699	827fb15f-a8bb-4645-a045-6b82187a1f24	2026-05-12
d0c5822b-c7ba-47bb-9cad-f983babd1d4e	CAmbio de validacion de dueAt	dueAt cambio de validacion	4	2026-04-26 21:10:20.120949	2026-04-29 09:26:02.261551	827fb15f-a8bb-4645-a045-6b82187a1f24	2026-05-10
\.


--
-- Data for Name: users; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.users (name, email, role, created_at, id, updated_at, status) FROM stdin;
Carlos Admin	carlos@ejemplo.com	1	2026-04-04 04:02:20.852012	24cfa21a-570e-46ff-9749-2947d5d36b7f	\N	1
Carolina	carolina@ejemplo.com	1	2026-04-04 04:43:29.812624	827fb15f-a8bb-4645-a045-6b82187a1f24	\N	1
Demo02	demo02@ejemplo.com	1	2026-04-05 23:25:53.408872	d842cda1-a1e3-4332-88ff-7f3dcdd61939	2026-04-05 19:44:55.771494	1
pep	pepe@ejemplo.com	2	2026-04-08 16:15:39.34849	731a7efa-6572-4af5-8241-2395bd366c5e	\N	1
Fatima Updated	fatima@ejemplo.com	2	2026-04-05 23:23:19.108898	47ccf05d-38a9-4a00-b31f-60e0eb98fb7c	2026-04-17 15:08:16.939819	1
GMI YUDY POLGAR	polgar@ejemplo.com	2	2026-04-27 13:49:10.249071	7cad3f76-b704-4524-a1d9-451a3c087670	2026-04-27 13:50:40.127437	1
\.


--
-- Name: assignments assignments_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.assignments
    ADD CONSTRAINT assignments_pkey PRIMARY KEY (id);


--
-- Name: users users_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_pk PRIMARY KEY (id);


--
-- Name: idx_userid; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX idx_userid ON public.assignments USING btree (user_id);


--
-- Name: assignments fk_user_id; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.assignments
    ADD CONSTRAINT fk_user_id FOREIGN KEY (user_id) REFERENCES public.users(id);


--
-- PostgreSQL database dump complete
--

