BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS "games" (
	"id"	INTEGER NOT NULL UNIQUE,
	"code"	TEXT NOT NULL,
	"description"	TEXT,
	"explanation"	TEXT,
	"color"	TEXT,
	"priority"	INTEGER NOT NULL DEFAULT 0,
	"occupied_by"	INTEGER,
	"start_time"	INTEGER,
	FOREIGN KEY("occupied_by") REFERENCES "groups"("id"),
	PRIMARY KEY("id" AUTOINCREMENT)
);
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
	PRIMARY KEY("id" AUTOINCREMENT)
);
COMMIT;
