-- PROCEDURE: invitations.insert_invitation(integer, character varying, character varying, character varying, character varying, character varying, character varying, integer, character varying, character varying, character varying)

-- DROP PROCEDURE IF EXISTS invitations.insert_invitation(integer, character varying, character varying, character varying, character varying, character varying, character varying, integer, character varying, character varying, character varying);

CREATE OR REPLACE PROCEDURE invitations.insert_invitation(
	IN userid integer,
	IN gvid character varying,
	IN frist character varying,
	IN laeremiddel character varying,
	IN laereplan character varying,
	IN mottakereposter character varying,
	IN opprettetdato character varying,
	IN assessmenttypeid integer,
	IN laereplankode character varying,
	IN bortvalgtsporsmaal character varying,
	IN laeremiddelleverandor character varying)
LANGUAGE 'plpgsql'
AS $BODY$
BEGIN
IF NOT EXISTS(SELECT * FROM invitations.invitations where gv_id = gvid) THEN
INSERT INTO invitations.invitations (user_id_fk, gv_id, frist, laeremiddel, laereplan, mottaker_eposter, opprettet_dato,assessmenttype_id_fk,laereplankode,bortvalgtsporsmaal, laeremiddel_leverandor)
VALUES (userid, gvid, frist, laeremiddel, laereplan, mottakereposter,opprettetdato,assessmenttypeid,laereplankode,bortvalgtsporsmaal, laeremiddelleverandor);
END IF;
END
$BODY$;
ALTER PROCEDURE invitations.insert_invitation(integer, character varying, character varying, character varying, character varying, character varying, character varying, integer, character varying, character varying, character varying)
    OWNER TO postgres;
