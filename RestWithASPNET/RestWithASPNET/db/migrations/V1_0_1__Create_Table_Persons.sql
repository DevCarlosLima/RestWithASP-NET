﻿USE REST_WITH_ASP_NET;

CREATE TABLE `persons` (
	`Id` INT(10) AUTO_INCREMENT PRIMARY KEY,
	`FirstName` VARCHAR(50) NULL DEFAULT NULL,
	`LastName` VARCHAR(50) NULL DEFAULT NULL,
	`Address` VARCHAR(50) NULL DEFAULT NULL,
	`Gender` VARCHAR(50) NULL DEFAULT NULL
)
ENGINE=InnoDB
;