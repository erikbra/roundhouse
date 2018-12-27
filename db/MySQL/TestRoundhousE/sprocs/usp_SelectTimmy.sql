DROP PROCEDURE IF EXISTS usp_SelectTimmy;
;
delimiter //
;
CREATE PROCEDURE usp_SelectTimmy()
  READS SQL DATA
  DETERMINISTIC
BEGIN
  SELECT * FROM Timmy;
END//
;
delimiter ;
;