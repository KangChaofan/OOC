# ************************************************************
# Sequel Pro SQL dump
# Version 4096
#
# http://www.sequelpro.com/
# http://code.google.com/p/sequel-pro/
#
# Host: 202.113.2.233 (MySQL 5.6.16)
# Database: ooc
# Generation Time: 2014-04-01 04:35:03 +0000
# ************************************************************


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


# Dump of table Bill
# ------------------------------------------------------------

DROP TABLE IF EXISTS `Bill`;

CREATE TABLE `Bill` (
  `id` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `userId` int(11) unsigned NOT NULL,
  `taskGuid` varchar(40) DEFAULT NULL,
  `cmGuid` varchar(40) DEFAULT NULL,
  `isRefunded` tinyint(1) NOT NULL,
  `amount` double NOT NULL,
  `creation` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `modification` timestamp NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  KEY `fk_bill_task` (`taskGuid`),
  KEY `fk_bill_cm` (`cmGuid`),
  KEY `fk_bill_user` (`userId`),
  CONSTRAINT `fk_bill_user` FOREIGN KEY (`userId`) REFERENCES `User` (`id`),
  CONSTRAINT `fk_bill_cm` FOREIGN KEY (`cmGuid`) REFERENCES `CompositionModel` (`guid`),
  CONSTRAINT `fk_bill_task` FOREIGN KEY (`taskGuid`) REFERENCES `Task` (`guid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;



# Dump of table Composition
# ------------------------------------------------------------

DROP TABLE IF EXISTS `Composition`;

CREATE TABLE `Composition` (
  `guid` varchar(40) NOT NULL DEFAULT '',
  `authorUserId` int(11) unsigned NOT NULL,
  `title` varchar(64) NOT NULL DEFAULT '',
  `isShared` tinyint(1) NOT NULL,
  `isFinished` tinyint(1) NOT NULL,
  `creation` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `modification` timestamp NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`guid`),
  KEY `fk_comp_author` (`authorUserId`),
  CONSTRAINT `fk_comp_author` FOREIGN KEY (`authorUserId`) REFERENCES `User` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;



# Dump of table CompositionLink
# ------------------------------------------------------------

DROP TABLE IF EXISTS `CompositionLink`;

CREATE TABLE `CompositionLink` (
  `guid` varchar(40) NOT NULL DEFAULT '',
  `compositionGuid` varchar(40) NOT NULL DEFAULT '',
  `sourceCmGuid` varchar(40) NOT NULL DEFAULT '',
  `targetCmGuid` varchar(40) NOT NULL DEFAULT '',
  `sourceQuantity` varchar(64) NOT NULL DEFAULT '',
  `targetQuantity` varchar(64) NOT NULL DEFAULT '',
  `sourceElementSet` varchar(64) NOT NULL DEFAULT '',
  `targetElementSet` varchar(64) NOT NULL DEFAULT '',
  `dataOperation` text,
  `creation` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `modification` timestamp NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`guid`),
  KEY `fk_link_comp` (`compositionGuid`),
  KEY `fk_link_source_cm` (`sourceCmGuid`),
  KEY `fk_link_target_cm` (`targetCmGuid`),
  CONSTRAINT `fk_link_target_cm` FOREIGN KEY (`targetCmGuid`) REFERENCES `CompositionModel` (`guid`),
  CONSTRAINT `fk_link_comp` FOREIGN KEY (`compositionGuid`) REFERENCES `Composition` (`guid`),
  CONSTRAINT `fk_link_source_cm` FOREIGN KEY (`sourceCmGuid`) REFERENCES `CompositionModel` (`guid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;



# Dump of table CompositionModel
# ------------------------------------------------------------

DROP TABLE IF EXISTS `CompositionModel`;

CREATE TABLE `CompositionModel` (
  `guid` varchar(40) NOT NULL DEFAULT '',
  `compositionGuid` varchar(40) NOT NULL DEFAULT '',
  `modelGuid` varchar(40) NOT NULL DEFAULT '',
  `properties` text NOT NULL,
  `creation` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `modification` timestamp NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`guid`),
  KEY `fk_cm_comp` (`compositionGuid`),
  KEY `fk_cm_model` (`modelGuid`),
  CONSTRAINT `fk_cm_model` FOREIGN KEY (`modelGuid`) REFERENCES `Model` (`guid`),
  CONSTRAINT `fk_cm_comp` FOREIGN KEY (`compositionGuid`) REFERENCES `Composition` (`guid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;



# Dump of table Model
# ------------------------------------------------------------

DROP TABLE IF EXISTS `Model`;

CREATE TABLE `Model` (
  `guid` varchar(40) NOT NULL DEFAULT '',
  `name` varchar(64) NOT NULL DEFAULT '',
  `version` varchar(16) NOT NULL DEFAULT '',
  `authorUserId` int(11) unsigned NOT NULL,
  `isGranted` tinyint(1) NOT NULL,
  `className` varchar(256) DEFAULT NULL,
  `creation` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `modification` timestamp NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`guid`),
  KEY `fk_model_author` (`authorUserId`),
  CONSTRAINT `fk_model_author` FOREIGN KEY (`authorUserId`) REFERENCES `User` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;



# Dump of table ModelFileMapping
# ------------------------------------------------------------

DROP TABLE IF EXISTS `ModelFileMapping`;

CREATE TABLE `ModelFileMapping` (
  `modelGuid` varchar(40) NOT NULL DEFAULT '',
  `fileName` varchar(200) NOT NULL DEFAULT '',
  `isMainLibrary` tinyint(1) NOT NULL,
  `creation` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `modification` timestamp NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`modelGuid`,`fileName`),
  CONSTRAINT `fk_file_model` FOREIGN KEY (`modelGuid`) REFERENCES `Model` (`guid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;



# Dump of table ModelProperty
# ------------------------------------------------------------

DROP TABLE IF EXISTS `ModelProperty`;

CREATE TABLE `ModelProperty` (
  `modelGuid` varchar(40) NOT NULL DEFAULT '',
  `key` varchar(64) NOT NULL DEFAULT '',
  `type` tinyint(1) NOT NULL,
  `description` varchar(256) DEFAULT NULL,
  `default` varchar(64) DEFAULT NULL,
  `additional` text,
  PRIMARY KEY (`modelGuid`,`key`),
  CONSTRAINT `fk_property_model` FOREIGN KEY (`modelGuid`) REFERENCES `Model` (`guid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;



# Dump of table Task
# ------------------------------------------------------------

DROP TABLE IF EXISTS `Task`;

CREATE TABLE `Task` (
  `guid` varchar(40) NOT NULL DEFAULT '',
  `instanceName` varchar(64) DEFAULT '',
  `compositionGuid` varchar(40) NOT NULL DEFAULT '',
  `compositionData` text NOT NULL,
  `state` tinyint(4) NOT NULL,
  `modelProgress` text,
  `userId` int(11) unsigned NOT NULL,
  `timeStarted` timestamp NULL DEFAULT NULL,
  `timeFinished` timestamp NULL DEFAULT NULL,
  `creation` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `modification` timestamp NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`guid`),
  KEY `fk_task_comp` (`compositionGuid`),
  KEY `fk_task_user` (`userId`),
  CONSTRAINT `fk_task_user` FOREIGN KEY (`userId`) REFERENCES `User` (`id`),
  CONSTRAINT `fk_task_comp` FOREIGN KEY (`compositionGuid`) REFERENCES `Composition` (`guid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;



# Dump of table TaskFileMapping
# ------------------------------------------------------------

DROP TABLE IF EXISTS `TaskFileMapping`;

CREATE TABLE `TaskFileMapping` (
  `taskGuid` varchar(40) NOT NULL DEFAULT '',
  `fileName` varchar(200) NOT NULL DEFAULT '',
  `type` tinyint(1) NOT NULL,
  `isDownloadable` tinyint(1) NOT NULL,
  PRIMARY KEY (`taskGuid`,`fileName`),
  CONSTRAINT `fk_file_task` FOREIGN KEY (`taskGuid`) REFERENCES `Task` (`guid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;



# Dump of table User
# ------------------------------------------------------------

DROP TABLE IF EXISTS `User`;

CREATE TABLE `User` (
  `id` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `username` varchar(32) NOT NULL DEFAULT '',
  `passhash` varchar(32) NOT NULL DEFAULT '',
  `mobile` varchar(16) NOT NULL DEFAULT '',
  `balance` double NOT NULL,
  `acl` text NOT NULL,
  `creation` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `modification` timestamp NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  KEY `username` (`username`),
  KEY `mobile` (`mobile`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

LOCK TABLES `User` WRITE;
/*!40000 ALTER TABLE `User` DISABLE KEYS */;

INSERT INTO `User` (`id`, `username`, `passhash`, `mobile`, `balance`, `acl`, `creation`, `modification`)
VALUES
	(1,'test','123','',0,'','2014-04-01 11:24:38',NULL);

/*!40000 ALTER TABLE `User` ENABLE KEYS */;
UNLOCK TABLES;



/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
