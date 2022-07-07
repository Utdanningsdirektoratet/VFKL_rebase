ALTER TABLE IF EXISTS invitations.invitations
    ADD COLUMN assessmenttype_id_fk integer,
    ADD COLUMN laereplankode varchar,
    ADD COLUMN bortvalgtsporsmaal varchar,

ALTER TABLE IF EXISTS invitations.invitations
    ADD CONSTRAINT fk_assessmenttype FOREIGN KEY(assessmenttype_id_fk) REFERENCES invitations.assessmenttype(assessmenttype_id)