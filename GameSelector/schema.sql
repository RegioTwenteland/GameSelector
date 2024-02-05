BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS "played_games" (
	"id"	INTEGER NOT NULL,
	"player"	INTEGER NOT NULL,
	"game"	INTEGER NOT NULL,
	"start_time"	INTEGER NOT NULL,
	"end_time"	INTEGER,
	FOREIGN KEY("player") REFERENCES "groups"("id"),
	FOREIGN KEY("game") REFERENCES "games"("id"),
	PRIMARY KEY("id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "groups" (
	"id"	INTEGER,
	"card_id"	TEXT,
	"scouting_name"	TEXT NOT NULL,
	"group_name"	TEXT NOT NULL,
	"is_admin"	INTEGER NOT NULL DEFAULT 0,
	"remarks"	TEXT NOT NULL,
	PRIMARY KEY("id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "games" (
	"id"	INTEGER,
	"code"	TEXT NOT NULL,
	"description"	TEXT,
	"explanation"	TEXT,
	"active"	INTEGER NOT NULL DEFAULT 1,
	"color"	TEXT,
	"priority"	INTEGER NOT NULL DEFAULT 0,
	"occupied_by"	INTEGER,
	"start_time"	INTEGER,
	"remarks"	TEXT NOT NULL,
	"timeout"	INTEGER NOT NULL DEFAULT 0,
	FOREIGN KEY("occupied_by") REFERENCES "groups"("id"),
	PRIMARY KEY("id" AUTOINCREMENT)
);
COMMIT;
