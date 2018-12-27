DROP FUNCTION IF EXISTS ufn_CountTimmy
;
delimiter //
;
CREATE FUNCTION  ufn_CountTimmy (i INT) RETURNS int
  READS SQL DATA
  DETERMINISTIC
BEGIN
SELECT COUNT(*) INTO i FROM Timmy;
return i;
END//
;
delimiter ;
;