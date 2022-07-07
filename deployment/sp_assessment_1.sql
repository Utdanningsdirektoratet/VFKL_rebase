-- PROCEDURE: assessment.insert_assessment(integer, character varying, character varying, character varying, timestamp without time zone, character varying, character varying)

-- DROP PROCEDURE IF EXISTS assessment.insert_assessment(integer, character varying, character varying, character varying, timestamp without time zone, character varying, character varying);

CREATE OR REPLACE PROCEDURE assessment.insert_assessment(
	IN userid integer,
	IN teachingaid character varying,
	IN instanceid character varying,
	IN groupassessmentid character varying,
	IN createddatetime timestamp without time zone,
	IN usercomments character varying,
	IN teachingaidsupplier character varying)
LANGUAGE 'plpgsql'
AS $BODY$
BEGIN
IF NOT EXISTS(SELECT * FROM assessment.assessment where instance_id = instanceid) THEN
INSERT INTO assessment.assessment (user_id_fk, teachingaid,instance_id, groupassessment_id_fk, created_datetime, user_comments, teachingaid_supplier) VALUES (userid, teachingaid,instanceid,groupassessmentid, createddatetime, usercomments, teachingaidsupplier);
END IF;
END
$BODY$;
ALTER PROCEDURE assessment.insert_assessment(integer, character varying, character varying, character varying, timestamp without time zone, character varying, character varying)
    OWNER TO postgres;
