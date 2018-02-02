/*
 Navicat Premium Data Transfer

 Source Server         : PostgreSQL-Local
 Source Server Type    : PostgreSQL
 Source Server Version : 100001
 Source Host           : localhost:5432
 Source Catalog        : MusicDB
 Source Schema         : public

 Target Server Type    : PostgreSQL
 Target Server Version : 100001
 File Encoding         : 65001

 Date: 02/02/2018 16:26:44
*/


-- ----------------------------
-- Sequence structure for Albums_album_id_seq
-- ----------------------------
DROP SEQUENCE IF EXISTS "public"."Albums_album_id_seq";
CREATE SEQUENCE "public"."Albums_album_id_seq" 
INCREMENT 1
MINVALUE  1
MAXVALUE 9223372036854775807
START 1
CACHE 1;

-- ----------------------------
-- Sequence structure for Artists_artist_id_seq
-- ----------------------------
DROP SEQUENCE IF EXISTS "public"."Artists_artist_id_seq";
CREATE SEQUENCE "public"."Artists_artist_id_seq" 
INCREMENT 1
MINVALUE  1
MAXVALUE 9223372036854775807
START 1
CACHE 1;

-- ----------------------------
-- Sequence structure for Featurings_featuring_id_seq
-- ----------------------------
DROP SEQUENCE IF EXISTS "public"."Featurings_featuring_id_seq";
CREATE SEQUENCE "public"."Featurings_featuring_id_seq" 
INCREMENT 1
MINVALUE  1
MAXVALUE 9223372036854775807
START 1
CACHE 1;

-- ----------------------------
-- Sequence structure for Genres_genre_id_seq
-- ----------------------------
DROP SEQUENCE IF EXISTS "public"."Genres_genre_id_seq";
CREATE SEQUENCE "public"."Genres_genre_id_seq" 
INCREMENT 1
MINVALUE  1
MAXVALUE 9223372036854775807
START 1
CACHE 1;

-- ----------------------------
-- Sequence structure for Lyrics_lyric_id_seq
-- ----------------------------
DROP SEQUENCE IF EXISTS "public"."Lyrics_lyric_id_seq";
CREATE SEQUENCE "public"."Lyrics_lyric_id_seq" 
INCREMENT 1
MINVALUE  1
MAXVALUE 9223372036854775807
START 1
CACHE 1;

-- ----------------------------
-- Sequence structure for Songs_song_id_seq
-- ----------------------------
DROP SEQUENCE IF EXISTS "public"."Songs_song_id_seq";
CREATE SEQUENCE "public"."Songs_song_id_seq" 
INCREMENT 1
MINVALUE  1
MAXVALUE 9223372036854775807
START 1
CACHE 1;

-- ----------------------------
-- Table structure for Albums
-- ----------------------------
DROP TABLE IF EXISTS "public"."Albums";
CREATE TABLE "public"."Albums" (
  "album_id" int4 NOT NULL DEFAULT nextval('"Albums_album_id_seq"'::regclass),
  "album_name" varchar COLLATE "pg_catalog"."default",
  "artist_id" int4,
  "barcode" varchar COLLATE "pg_catalog"."default",
  "country" varchar COLLATE "pg_catalog"."default",
  "is_single" bool,
  "release_year" int4
)
;

-- ----------------------------
-- Table structure for Artists
-- ----------------------------
DROP TABLE IF EXISTS "public"."Artists";
CREATE TABLE "public"."Artists" (
  "artist_id" int4 NOT NULL DEFAULT nextval('"Artists_artist_id_seq"'::regclass),
  "artist_name" varchar COLLATE "pg_catalog"."default",
  "country" varchar COLLATE "pg_catalog"."default",
  "is_group" bool,
  "real_name" varchar COLLATE "pg_catalog"."default",
  "started_year" int4
)
;

-- ----------------------------
-- Table structure for Featurings
-- ----------------------------
DROP TABLE IF EXISTS "public"."Featurings";
CREATE TABLE "public"."Featurings" (
  "featuring_id" int4 NOT NULL DEFAULT nextval('"Featurings_featuring_id_seq"'::regclass),
  "artist_id" int4,
  "song_id" int4
)
;

-- ----------------------------
-- Table structure for Genres
-- ----------------------------
DROP TABLE IF EXISTS "public"."Genres";
CREATE TABLE "public"."Genres" (
  "genre_id" int4 NOT NULL DEFAULT nextval('"Genres_genre_id_seq"'::regclass),
  "genre_name" varchar COLLATE "pg_catalog"."default"
)
;

-- ----------------------------
-- Table structure for Lyrics
-- ----------------------------
DROP TABLE IF EXISTS "public"."Lyrics";
CREATE TABLE "public"."Lyrics" (
  "lyric_id" int4 NOT NULL DEFAULT nextval('"Lyrics_lyric_id_seq"'::regclass),
  "language" varchar COLLATE "pg_catalog"."default",
  "lyrics" text COLLATE "pg_catalog"."default",
  "song_id" int4
)
;

-- ----------------------------
-- Table structure for Songs
-- ----------------------------
DROP TABLE IF EXISTS "public"."Songs";
CREATE TABLE "public"."Songs" (
  "song_id" int4 NOT NULL DEFAULT nextval('"Songs_song_id_seq"'::regclass),
  "album_id" int4,
  "artist_id" int4,
  "genre_id" int4,
  "is_featuring" bool,
  "language" varchar COLLATE "pg_catalog"."default",
  "song_name" varchar COLLATE "pg_catalog"."default"
)
;

-- ----------------------------
-- Table structure for log_table
-- ----------------------------
DROP TABLE IF EXISTS "public"."log_table";
CREATE TABLE "public"."log_table" (
  "table_name" varchar COLLATE "pg_catalog"."default",
  "operation" text COLLATE "pg_catalog"."default",
  "data_new" text COLLATE "pg_catalog"."default",
  "data_old" text COLLATE "pg_catalog"."default",
  "process_date" date,
  "id" int4 NOT NULL DEFAULT nextval('"Artists_artist_id_seq"'::regclass)
)
;

-- ----------------------------
-- Function structure for delete_log_function
-- ----------------------------
DROP FUNCTION IF EXISTS "public"."delete_log_function"();
CREATE OR REPLACE FUNCTION "public"."delete_log_function"()
  RETURNS "pg_catalog"."trigger" AS $BODY$

begin
insert into log_table(table_name,operation,data_new,data_old,process_date)
values(TG_TABLE_NAME::TEXT,TG_OP,'',ROW(OLD.*),CURRENT_TIMESTAMP);
return NEW;
end;

$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100;

-- ----------------------------
-- Function structure for insert_log_function
-- ----------------------------
DROP FUNCTION IF EXISTS "public"."insert_log_function"();
CREATE OR REPLACE FUNCTION "public"."insert_log_function"()
  RETURNS "pg_catalog"."trigger" AS $BODY$

begin
insert into log_table(table_name,operation,data_new,data_old,process_date)
values(TG_TABLE_NAME::TEXT,TG_OP,ROW(NEW.*),'',CURRENT_TIMESTAMP);
return NEW;
end;

$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100;

-- ----------------------------
-- Function structure for pg_buffercache_pages
-- ----------------------------
DROP FUNCTION IF EXISTS "public"."pg_buffercache_pages"();
CREATE OR REPLACE FUNCTION "public"."pg_buffercache_pages"()
  RETURNS SETOF "pg_catalog"."record" AS '$libdir/pg_buffercache', 'pg_buffercache_pages'
  LANGUAGE c VOLATILE
  COST 1
  ROWS 1000;

-- ----------------------------
-- Function structure for pg_stat_statements
-- ----------------------------
DROP FUNCTION IF EXISTS "public"."pg_stat_statements"(IN "showtext" bool, OUT "userid" oid, OUT "dbid" oid, OUT "queryid" int8, OUT "query" text, OUT "calls" int8, OUT "total_time" float8, OUT "min_time" float8, OUT "max_time" float8, OUT "mean_time" float8, OUT "stddev_time" float8, OUT "rows" int8, OUT "shared_blks_hit" int8, OUT "shared_blks_read" int8, OUT "shared_blks_dirtied" int8, OUT "shared_blks_written" int8, OUT "local_blks_hit" int8, OUT "local_blks_read" int8, OUT "local_blks_dirtied" int8, OUT "local_blks_written" int8, OUT "temp_blks_read" int8, OUT "temp_blks_written" int8, OUT "blk_read_time" float8, OUT "blk_write_time" float8);
CREATE OR REPLACE FUNCTION "public"."pg_stat_statements"(IN "showtext" bool, OUT "userid" oid, OUT "dbid" oid, OUT "queryid" int8, OUT "query" text, OUT "calls" int8, OUT "total_time" float8, OUT "min_time" float8, OUT "max_time" float8, OUT "mean_time" float8, OUT "stddev_time" float8, OUT "rows" int8, OUT "shared_blks_hit" int8, OUT "shared_blks_read" int8, OUT "shared_blks_dirtied" int8, OUT "shared_blks_written" int8, OUT "local_blks_hit" int8, OUT "local_blks_read" int8, OUT "local_blks_dirtied" int8, OUT "local_blks_written" int8, OUT "temp_blks_read" int8, OUT "temp_blks_written" int8, OUT "blk_read_time" float8, OUT "blk_write_time" float8)
  RETURNS SETOF "pg_catalog"."record" AS '$libdir/pg_stat_statements', 'pg_stat_statements_1_3'
  LANGUAGE c VOLATILE STRICT
  COST 1
  ROWS 1000;

-- ----------------------------
-- Function structure for pg_stat_statements_reset
-- ----------------------------
DROP FUNCTION IF EXISTS "public"."pg_stat_statements_reset"();
CREATE OR REPLACE FUNCTION "public"."pg_stat_statements_reset"()
  RETURNS "pg_catalog"."void" AS '$libdir/pg_stat_statements', 'pg_stat_statements_reset'
  LANGUAGE c VOLATILE
  COST 1;

-- ----------------------------
-- Function structure for update_log_function
-- ----------------------------
DROP FUNCTION IF EXISTS "public"."update_log_function"();
CREATE OR REPLACE FUNCTION "public"."update_log_function"()
  RETURNS "pg_catalog"."trigger" AS $BODY$

begin
insert into log_table(table_name,operation,data_new,data_old,process_date)
values(TG_TABLE_NAME::TEXT,TG_OP,ROW(NEW.*),ROW(OLD.*),CURRENT_TIMESTAMP);
return new;
end;

$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100;

-- ----------------------------
-- View structure for pg_stat_statements
-- ----------------------------
DROP VIEW IF EXISTS "public"."pg_stat_statements";
CREATE VIEW "public"."pg_stat_statements" AS  SELECT pg_stat_statements.userid,
    pg_stat_statements.dbid,
    pg_stat_statements.queryid,
    pg_stat_statements.query,
    pg_stat_statements.calls,
    pg_stat_statements.total_time,
    pg_stat_statements.min_time,
    pg_stat_statements.max_time,
    pg_stat_statements.mean_time,
    pg_stat_statements.stddev_time,
    pg_stat_statements.rows,
    pg_stat_statements.shared_blks_hit,
    pg_stat_statements.shared_blks_read,
    pg_stat_statements.shared_blks_dirtied,
    pg_stat_statements.shared_blks_written,
    pg_stat_statements.local_blks_hit,
    pg_stat_statements.local_blks_read,
    pg_stat_statements.local_blks_dirtied,
    pg_stat_statements.local_blks_written,
    pg_stat_statements.temp_blks_read,
    pg_stat_statements.temp_blks_written,
    pg_stat_statements.blk_read_time,
    pg_stat_statements.blk_write_time
   FROM pg_stat_statements(true) pg_stat_statements(userid, dbid, queryid, query, calls, total_time, min_time, max_time, mean_time, stddev_time, rows, shared_blks_hit, shared_blks_read, shared_blks_dirtied, shared_blks_written, local_blks_hit, local_blks_read, local_blks_dirtied, local_blks_written, temp_blks_read, temp_blks_written, blk_read_time, blk_write_time);

-- ----------------------------
-- View structure for pg_buffercache
-- ----------------------------
DROP VIEW IF EXISTS "public"."pg_buffercache";
CREATE VIEW "public"."pg_buffercache" AS  SELECT p.bufferid,
    p.relfilenode,
    p.reltablespace,
    p.reldatabase,
    p.relforknumber,
    p.relblocknumber,
    p.isdirty,
    p.usagecount,
    p.pinning_backends
   FROM pg_buffercache_pages() p(bufferid integer, relfilenode oid, reltablespace oid, reldatabase oid, relforknumber smallint, relblocknumber bigint, isdirty boolean, usagecount smallint, pinning_backends integer);

-- ----------------------------
-- View structure for song_info
-- ----------------------------
DROP VIEW IF EXISTS "public"."song_info";
CREATE VIEW "public"."song_info" AS  SELECT "Songs".song_id,
    "Songs".song_name,
    "Artists".artist_name,
    "Albums".album_name,
    "Lyrics".lyrics,
    "Genres".genre_name,
    "Songs".language
   FROM (((("Songs"
     JOIN "Artists" ON (("Songs".artist_id = "Artists".artist_id)))
     JOIN "Albums" ON (("Songs".album_id = "Albums".album_id)))
     JOIN "Genres" ON (("Songs".genre_id = "Genres".genre_id)))
     LEFT JOIN "Lyrics" ON (("Songs".song_id = "Lyrics".song_id)));

-- ----------------------------
-- View structure for featuring_info
-- ----------------------------
DROP VIEW IF EXISTS "public"."featuring_info";
CREATE VIEW "public"."featuring_info" AS  SELECT "Songs".song_id,
    ( SELECT "Artists_1".artist_name
           FROM "Artists" "Artists_1"
          WHERE ("Artists_1".artist_id = "Songs".artist_id)) AS sahibi,
    string_agg((( SELECT "Artists_1".artist_name
           FROM "Artists" "Artists_1"
          WHERE ("Artists_1".artist_id = "Featurings".artist_id)))::text, ', '::text) AS "düetciler",
    ( SELECT "Albums".album_name
           FROM "Albums"
          WHERE ("Albums".album_id = "Songs".album_id)) AS album_adi,
    "Songs".song_name
   FROM (("Songs"
     JOIN "Featurings" ON (("Songs".song_id = "Featurings".song_id)))
     JOIN "Artists" ON (("Featurings".artist_id = "Artists".artist_id)))
  GROUP BY "Songs".song_id;

-- ----------------------------
-- Alter sequences owned by
-- ----------------------------
ALTER SEQUENCE "public"."Albums_album_id_seq"
OWNED BY "public"."Albums"."album_id";
SELECT setval('"public"."Albums_album_id_seq"', 460, true);
ALTER SEQUENCE "public"."Artists_artist_id_seq"
OWNED BY "public"."Artists"."artist_id";
SELECT setval('"public"."Artists_artist_id_seq"', 1983, true);
ALTER SEQUENCE "public"."Featurings_featuring_id_seq"
OWNED BY "public"."Featurings"."featuring_id";
SELECT setval('"public"."Featurings_featuring_id_seq"', 364, true);
ALTER SEQUENCE "public"."Genres_genre_id_seq"
OWNED BY "public"."Genres"."genre_id";
SELECT setval('"public"."Genres_genre_id_seq"', 1916, true);
ALTER SEQUENCE "public"."Lyrics_lyric_id_seq"
OWNED BY "public"."Lyrics"."lyric_id";
SELECT setval('"public"."Lyrics_lyric_id_seq"', 217, true);
ALTER SEQUENCE "public"."Songs_song_id_seq"
OWNED BY "public"."Songs"."song_id";
SELECT setval('"public"."Songs_song_id_seq"', 863, true);

-- ----------------------------
-- Indexes structure for table Albums
-- ----------------------------
CREATE INDEX "IX_Albums_artist_id" ON "public"."Albums" USING btree (
  "artist_id" "pg_catalog"."int4_ops" ASC NULLS LAST
);

-- ----------------------------
-- Triggers structure for table Albums
-- ----------------------------
CREATE TRIGGER "album_delete_log" AFTER DELETE ON "public"."Albums"
FOR EACH ROW
EXECUTE PROCEDURE "public"."delete_log_function"();
CREATE TRIGGER "album_insert_log" AFTER INSERT ON "public"."Albums"
FOR EACH ROW
EXECUTE PROCEDURE "public"."insert_log_function"();
CREATE TRIGGER "album_update_log" AFTER UPDATE ON "public"."Albums"
FOR EACH ROW
EXECUTE PROCEDURE "public"."update_log_function"();

-- ----------------------------
-- Primary Key structure for table Albums
-- ----------------------------
ALTER TABLE "public"."Albums" ADD CONSTRAINT "PK_Albums" PRIMARY KEY ("album_id");

-- ----------------------------
-- Triggers structure for table Artists
-- ----------------------------
CREATE TRIGGER "artist_delete_log" AFTER DELETE ON "public"."Artists"
FOR EACH ROW
EXECUTE PROCEDURE "public"."delete_log_function"();
CREATE TRIGGER "artist_insert_log" AFTER INSERT ON "public"."Artists"
FOR EACH ROW
EXECUTE PROCEDURE "public"."insert_log_function"();
CREATE TRIGGER "artist_update_log" AFTER UPDATE ON "public"."Artists"
FOR EACH ROW
EXECUTE PROCEDURE "public"."update_log_function"();

-- ----------------------------
-- Primary Key structure for table Artists
-- ----------------------------
ALTER TABLE "public"."Artists" ADD CONSTRAINT "PK_Artists" PRIMARY KEY ("artist_id");

-- ----------------------------
-- Indexes structure for table Featurings
-- ----------------------------
CREATE INDEX "IX_Featurings_artist_id" ON "public"."Featurings" USING btree (
  "artist_id" "pg_catalog"."int4_ops" ASC NULLS LAST
);
CREATE INDEX "IX_Featurings_song_id" ON "public"."Featurings" USING btree (
  "song_id" "pg_catalog"."int4_ops" ASC NULLS LAST
);

-- ----------------------------
-- Triggers structure for table Featurings
-- ----------------------------
CREATE TRIGGER "featuring_delete_log" AFTER DELETE ON "public"."Featurings"
FOR EACH ROW
EXECUTE PROCEDURE "public"."delete_log_function"();
CREATE TRIGGER "featuring_insert_log" AFTER INSERT ON "public"."Featurings"
FOR EACH ROW
EXECUTE PROCEDURE "public"."insert_log_function"();
CREATE TRIGGER "featuring_update_log" AFTER UPDATE ON "public"."Featurings"
FOR EACH ROW
EXECUTE PROCEDURE "public"."update_log_function"();

-- ----------------------------
-- Primary Key structure for table Featurings
-- ----------------------------
ALTER TABLE "public"."Featurings" ADD CONSTRAINT "PK_Featurings" PRIMARY KEY ("featuring_id");

-- ----------------------------
-- Triggers structure for table Genres
-- ----------------------------
CREATE TRIGGER "genre_delete_log" AFTER DELETE ON "public"."Genres"
FOR EACH ROW
EXECUTE PROCEDURE "public"."delete_log_function"();
CREATE TRIGGER "genre_insert_log" AFTER INSERT ON "public"."Genres"
FOR EACH ROW
EXECUTE PROCEDURE "public"."insert_log_function"();
CREATE TRIGGER "genre_update_log" AFTER UPDATE ON "public"."Genres"
FOR EACH ROW
EXECUTE PROCEDURE "public"."update_log_function"();

-- ----------------------------
-- Primary Key structure for table Genres
-- ----------------------------
ALTER TABLE "public"."Genres" ADD CONSTRAINT "PK_Genres" PRIMARY KEY ("genre_id");

-- ----------------------------
-- Indexes structure for table Lyrics
-- ----------------------------
CREATE UNIQUE INDEX "IX_Lyrics_song_id" ON "public"."Lyrics" USING btree (
  "song_id" "pg_catalog"."int4_ops" ASC NULLS LAST
);

-- ----------------------------
-- Triggers structure for table Lyrics
-- ----------------------------
CREATE TRIGGER "lyric_delete_log" AFTER DELETE ON "public"."Lyrics"
FOR EACH ROW
EXECUTE PROCEDURE "public"."delete_log_function"();
CREATE TRIGGER "lyric_insert_log" AFTER INSERT ON "public"."Lyrics"
FOR EACH ROW
EXECUTE PROCEDURE "public"."insert_log_function"();
CREATE TRIGGER "lyric_update_log" AFTER UPDATE ON "public"."Lyrics"
FOR EACH ROW
EXECUTE PROCEDURE "public"."update_log_function"();

-- ----------------------------
-- Primary Key structure for table Lyrics
-- ----------------------------
ALTER TABLE "public"."Lyrics" ADD CONSTRAINT "PK_Lyrics" PRIMARY KEY ("lyric_id");

-- ----------------------------
-- Indexes structure for table Songs
-- ----------------------------
CREATE INDEX "IX_Songs_album_id" ON "public"."Songs" USING btree (
  "album_id" "pg_catalog"."int4_ops" ASC NULLS LAST
);
CREATE INDEX "IX_Songs_artist_id" ON "public"."Songs" USING btree (
  "artist_id" "pg_catalog"."int4_ops" ASC NULLS LAST
);
CREATE INDEX "IX_Songs_genre_id" ON "public"."Songs" USING btree (
  "genre_id" "pg_catalog"."int4_ops" ASC NULLS LAST
);

-- ----------------------------
-- Triggers structure for table Songs
-- ----------------------------
CREATE TRIGGER "song_delete_log" AFTER DELETE ON "public"."Songs"
FOR EACH ROW
EXECUTE PROCEDURE "public"."delete_log_function"();
CREATE TRIGGER "song_insert_log" AFTER INSERT ON "public"."Songs"
FOR EACH ROW
EXECUTE PROCEDURE "public"."insert_log_function"();
CREATE TRIGGER "song_update_log" AFTER UPDATE ON "public"."Songs"
FOR EACH ROW
EXECUTE PROCEDURE "public"."update_log_function"();

-- ----------------------------
-- Primary Key structure for table Songs
-- ----------------------------
ALTER TABLE "public"."Songs" ADD CONSTRAINT "PK_Songs" PRIMARY KEY ("song_id");

-- ----------------------------
-- Primary Key structure for table log_table
-- ----------------------------
ALTER TABLE "public"."log_table" ADD CONSTRAINT "log_table_pkey" PRIMARY KEY ("id");

-- ----------------------------
-- Foreign Keys structure for table Albums
-- ----------------------------
ALTER TABLE "public"."Albums" ADD CONSTRAINT "FK_Albums_Artists_artist_id" FOREIGN KEY ("artist_id") REFERENCES "Artists" ("artist_id") ON DELETE CASCADE ON UPDATE CASCADE;

-- ----------------------------
-- Foreign Keys structure for table Featurings
-- ----------------------------
ALTER TABLE "public"."Featurings" ADD CONSTRAINT "FK_Featurings_Artists_artist_id" FOREIGN KEY ("artist_id") REFERENCES "Artists" ("artist_id") ON DELETE CASCADE ON UPDATE CASCADE;
ALTER TABLE "public"."Featurings" ADD CONSTRAINT "FK_Featurings_Songs_song_id" FOREIGN KEY ("song_id") REFERENCES "Songs" ("song_id") ON DELETE CASCADE ON UPDATE CASCADE;

-- ----------------------------
-- Foreign Keys structure for table Lyrics
-- ----------------------------
ALTER TABLE "public"."Lyrics" ADD CONSTRAINT "FK_Lyrics_Songs_song_id" FOREIGN KEY ("song_id") REFERENCES "Songs" ("song_id") ON DELETE CASCADE ON UPDATE CASCADE;

-- ----------------------------
-- Foreign Keys structure for table Songs
-- ----------------------------
ALTER TABLE "public"."Songs" ADD CONSTRAINT "FK_Songs_Albums_album_id" FOREIGN KEY ("album_id") REFERENCES "Albums" ("album_id") ON DELETE CASCADE ON UPDATE CASCADE;
ALTER TABLE "public"."Songs" ADD CONSTRAINT "FK_Songs_Artists_artist_id" FOREIGN KEY ("artist_id") REFERENCES "Artists" ("artist_id") ON DELETE CASCADE ON UPDATE CASCADE;
ALTER TABLE "public"."Songs" ADD CONSTRAINT "FK_Songs_Genres_genre_id" FOREIGN KEY ("genre_id") REFERENCES "Genres" ("genre_id") ON DELETE CASCADE ON UPDATE CASCADE;
