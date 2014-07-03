# ************************************************************
# Sequel Pro SQL dump
# Version 4096
#
# http://www.sequelpro.com/
# http://code.google.com/p/sequel-pro/
#
# Host: 202.113.2.233 (MySQL 5.6.16)
# Database: ooc
# Generation Time: 2014-04-18 04:44:30 +0000
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
  CONSTRAINT `fk_bill_cm` FOREIGN KEY (`cmGuid`) REFERENCES `CompositionModel` (`guid`),
  CONSTRAINT `fk_bill_task` FOREIGN KEY (`taskGuid`) REFERENCES `Task` (`guid`),
  CONSTRAINT `fk_bill_user` FOREIGN KEY (`userId`) REFERENCES `User` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

LOCK TABLES `Bill` WRITE;
/*!40000 ALTER TABLE `Bill` DISABLE KEYS */;

INSERT INTO `Bill` (`id`, `userId`, `taskGuid`, `cmGuid`, `isRefunded`, `amount`, `creation`, `modification`)
VALUES
	(1,1,NULL,NULL,1,5,'2014-04-01 14:50:54','2014-04-01 15:24:31'),
	(2,1,NULL,NULL,0,5,'2014-04-02 10:59:09',NULL);

/*!40000 ALTER TABLE `Bill` ENABLE KEYS */;
UNLOCK TABLES;


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

LOCK TABLES `Composition` WRITE;
/*!40000 ALTER TABLE `Composition` DISABLE KEYS */;

INSERT INTO `Composition` (`guid`, `authorUserId`, `title`, `isShared`, `isFinished`, `creation`, `modification`)
VALUES
	('51af35f4-2dbf-4967-b42d-bd0c0dce394a',1,'COMP-TEST',1,1,'2014-04-04 18:49:55',NULL),
	('eb83b4b0-8dd4-48cb-807b-3b5fefcd29c7',1,'multiple-test',1,1,'2014-04-11 16:02:54','2014-04-11 16:02:58');

/*!40000 ALTER TABLE `Composition` ENABLE KEYS */;
UNLOCK TABLES;


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
  CONSTRAINT `fk_link_comp` FOREIGN KEY (`compositionGuid`) REFERENCES `Composition` (`guid`),
  CONSTRAINT `fk_link_source_cm` FOREIGN KEY (`sourceCmGuid`) REFERENCES `CompositionModel` (`guid`),
  CONSTRAINT `fk_link_target_cm` FOREIGN KEY (`targetCmGuid`) REFERENCES `CompositionModel` (`guid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

LOCK TABLES `CompositionLink` WRITE;
/*!40000 ALTER TABLE `CompositionLink` DISABLE KEYS */;

INSERT INTO `CompositionLink` (`guid`, `compositionGuid`, `sourceCmGuid`, `targetCmGuid`, `sourceQuantity`, `targetQuantity`, `sourceElementSet`, `targetElementSet`, `dataOperation`, `creation`, `modification`)
VALUES
	('3bd24919-73d0-46aa-9abe-6e7c3ffcee79','eb83b4b0-8dd4-48cb-807b-3b5fefcd29c7','4c2f3302-c327-424d-9e5d-106bf7db1e38','1e3ce983-39f1-4eec-afea-a6f340677243','Leakage','Quantity','Branch:0','1',NULL,'2014-04-12 13:56:08',NULL),
	('9dfec86b-e265-4c79-88d1-0e5c6b5e5d74','eb83b4b0-8dd4-48cb-807b-3b5fefcd29c7','4c2f3302-c327-424d-9e5d-106bf7db1e38','1e3ce983-39f1-4eec-afea-a6f340677243','Flow','Quantity','Branch:0','1',NULL,'2014-04-11 16:21:13','2014-04-11 16:40:51');

/*!40000 ALTER TABLE `CompositionLink` ENABLE KEYS */;
UNLOCK TABLES;


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
  CONSTRAINT `fk_cm_comp` FOREIGN KEY (`compositionGuid`) REFERENCES `Composition` (`guid`),
  CONSTRAINT `fk_cm_model` FOREIGN KEY (`modelGuid`) REFERENCES `Model` (`guid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

LOCK TABLES `CompositionModel` WRITE;
/*!40000 ALTER TABLE `CompositionModel` DISABLE KEYS */;

INSERT INTO `CompositionModel` (`guid`, `compositionGuid`, `modelGuid`, `properties`, `creation`, `modification`)
VALUES
	('19e95294-6aaf-4cd6-937a-e22ea58cc636','51af35f4-2dbf-4967-b42d-bd0c0dce394a','794a9489-3bae-45dc-9654-27aa306462ce','<ArrayOfKeyValueOfstringstring xmlns=\"http://schemas.microsoft.com/2003/10/Serialization/Arrays\" xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\"><KeyValueOfstringstring><Key>controlFilePath</Key><Value>Compositions\\51af35f4-2dbf-4967-b42d-bd0c0dce394a\\test.swm</Value></KeyValueOfstringstring></ArrayOfKeyValueOfstringstring>','2014-04-04 18:51:19','2014-04-08 10:53:44'),
	('1e3ce983-39f1-4eec-afea-a6f340677243','eb83b4b0-8dd4-48cb-807b-3b5fefcd29c7','180512cf-b424-4e7f-9d9e-e2850874040d','<ArrayOfKeyValueOfstringstring xmlns=\"http://schemas.microsoft.com/2003/10/Serialization/Arrays\" xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\"><KeyValueOfstringstring><Key>FileName</Key><Value>rivermodel.dat</Value></KeyValueOfstringstring></ArrayOfKeyValueOfstringstring>','2014-04-11 16:18:45','2014-04-11 16:19:15'),
	('4c2f3302-c327-424d-9e5d-106bf7db1e38','eb83b4b0-8dd4-48cb-807b-3b5fefcd29c7','942f7612-b95f-6666-7777-72dd482fbc12','<ArrayOfKeyValueOfstringstring xmlns=\"http://schemas.microsoft.com/2003/10/Serialization/Arrays\" xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\">\n<KeyValueOfstringstring><Key>modelID</Key><Value>RiverModel</Value></KeyValueOfstringstring>\n<KeyValueOfstringstring><Key>simulationStart</Key><Value>1990,1,2,0,0,0</Value></KeyValueOfstringstring>\n<KeyValueOfstringstring><Key>simulationEnd</Key><Value>1990,6,1,0,0,0</Value></KeyValueOfstringstring>\n<KeyValueOfstringstring><Key>xCoordinate</Key><Value>1510, 1510,  1723, 1950, 2003, 2110, 2403, 2830, 3537, 4010, 4552, 4510, 4002, 3310, 2889, 2900, 3110, 3051, 2328, 1688, 1110</Value></KeyValueOfstringstring>\n<KeyValueOfstringstring><Key>yCoordinate</Key><Value>10510,10000, 9131, 8051, 8062, 8020, 8343, 8463, 7814, 7010, 5827, 5010, 4498, 4110, 3624, 2663, 2110, 1788, 1496, 1475, 1510</Value></KeyValueOfstringstring>\n<KeyValueOfstringstring><Key>TimeStepLength</Key><Value>3600</Value></KeyValueOfstringstring>\n<KeyValueOfstringstring><Key>riverBedLevel</Key><Value>10.0</Value></KeyValueOfstringstring>\n<KeyValueOfstringstring><Key>leakageCoefficient</Key><Value>0.01</Value></KeyValueOfstringstring>\n</ArrayOfKeyValueOfstringstring>','2014-04-11 16:03:42','2014-04-11 16:05:05');

/*!40000 ALTER TABLE `CompositionModel` ENABLE KEYS */;
UNLOCK TABLES;


# Dump of table Model
# ------------------------------------------------------------

DROP TABLE IF EXISTS `Model`;

CREATE TABLE `Model` (
  `guid` varchar(40) NOT NULL DEFAULT '',
  `name` varchar(64) NOT NULL DEFAULT '',
  `version` varchar(16) NOT NULL DEFAULT '',
  `abstract` varchar(500) NOT NULL DEFAULT '',
  `authorUserId` int(11) unsigned NOT NULL,
  `isPublic` tinyint(1) NOT NULL,
  `isApproved` tinyint(1) NOT NULL,
  `className` varchar(256) DEFAULT NULL,
  `creation` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `modification` timestamp NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`guid`),
  KEY `fk_model_author` (`authorUserId`),
  CONSTRAINT `fk_model_author` FOREIGN KEY (`authorUserId`) REFERENCES `User` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

LOCK TABLES `Model` WRITE;
/*!40000 ALTER TABLE `Model` DISABLE KEYS */;

INSERT INTO `Model` (`guid`, `name`, `version`, `abstract`, `authorUserId`, `isPublic`, `isApproved`, `className`, `creation`, `modification`)
VALUES
	('180512cf-b424-4e7f-9d9e-e2850874040d','DataLogger','0.0.1','DataLogger based on DataMonitor',1,1,1,'OOC.OpenMIComponent.DataMonitor.DataLogger','2014-04-11 16:15:54',NULL),
	('794a9489-3bae-45dc-9654-27aa306462ce','RiverNetwork','0.0.1','Test River Network',1,1,1,'RiverNetworkModel.RiverNetworkLC','2014-04-04 18:50:42','2014-04-11 15:59:18'),
	('942f7612-b95f-6666-7777-72dd482fbc12','RiverModel','0.0.1','OpenMI Example',1,1,1,'Oatc.OpenMI.Examples.ModelComponents.SpatialModels.RiverModel.RiverModelLC','2014-04-11 16:01:31',NULL);

/*!40000 ALTER TABLE `Model` ENABLE KEYS */;
UNLOCK TABLES;


# Dump of table ModelFileMapping
# ------------------------------------------------------------

DROP TABLE IF EXISTS `ModelFileMapping`;

CREATE TABLE `ModelFileMapping` (
  `modelGuid` varchar(40) NOT NULL DEFAULT '',
  `fileName` varchar(200) NOT NULL DEFAULT '',
  `relativePath` varchar(512) NOT NULL DEFAULT '',
  `isMainLibrary` tinyint(1) NOT NULL,
  `isDocument` tinyint(1) NOT NULL,
  `signature` varchar(40) NOT NULL DEFAULT '',
  `creation` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `modification` timestamp NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`modelGuid`,`fileName`),
  CONSTRAINT `fk_file_model` FOREIGN KEY (`modelGuid`) REFERENCES `Model` (`guid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

LOCK TABLES `ModelFileMapping` WRITE;
/*!40000 ALTER TABLE `ModelFileMapping` DISABLE KEYS */;

INSERT INTO `ModelFileMapping` (`modelGuid`, `fileName`, `relativePath`, `isMainLibrary`, `isDocument`, `signature`, `creation`, `modification`)
VALUES
	('180512cf-b424-4e7f-9d9e-e2850874040d','Models\\180512cf-b424-4e7f-9d9e-e2850874040d\\OOC.OpenMIComponent.DataMonitor.dll','OOC.OpenMIComponent.DataMonitor.dll',1,0,'','2014-04-11 16:16:41','2014-04-11 16:16:59'),
	('794a9489-3bae-45dc-9654-27aa306462ce','Models\\794a9489-3bae-45dc-9654-27aa306462ce\\Common.dll','Common.dll',0,0,'','2014-04-04 18:53:03','2014-04-08 10:56:19'),
	('794a9489-3bae-45dc-9654-27aa306462ce','Models\\794a9489-3bae-45dc-9654-27aa306462ce\\DATA\\Boundary.txt','DATA\\Boundary.txt',0,0,'','2014-04-04 18:53:57','2014-04-08 10:58:37'),
	('794a9489-3bae-45dc-9654-27aa306462ce','Models\\794a9489-3bae-45dc-9654-27aa306462ce\\DATA\\element1D.txt','DATA\\element1D.txt',0,0,'','2014-04-04 18:54:02','2014-04-08 10:58:39'),
	('794a9489-3bae-45dc-9654-27aa306462ce','Models\\794a9489-3bae-45dc-9654-27aa306462ce\\DATA\\node1D.txt','DATA\\node1D.txt',0,0,'','2014-04-04 18:54:08','2014-04-08 10:58:46'),
	('794a9489-3bae-45dc-9654-27aa306462ce','Models\\794a9489-3bae-45dc-9654-27aa306462ce\\DATA\\sect.txt','DATA\\sect.txt',0,0,'','2014-04-04 18:54:12','2014-04-08 10:58:45'),
	('794a9489-3bae-45dc-9654-27aa306462ce','Models\\794a9489-3bae-45dc-9654-27aa306462ce\\DATA\\test.swm','DATA\\test.swm',0,0,'','2014-04-04 18:54:17','2014-04-08 10:58:49'),
	('794a9489-3bae-45dc-9654-27aa306462ce','Models\\794a9489-3bae-45dc-9654-27aa306462ce\\DATA\\TimeSerial.txt','DATA\\TimeSerial.txt',0,0,'','2014-04-04 18:54:22','2014-04-08 10:58:52'),
	('794a9489-3bae-45dc-9654-27aa306462ce','Models\\794a9489-3bae-45dc-9654-27aa306462ce\\DrawingObject.dll','DrawingObject.dll',0,0,'','2014-04-04 18:53:08','2014-04-08 10:58:02'),
	('794a9489-3bae-45dc-9654-27aa306462ce','Models\\794a9489-3bae-45dc-9654-27aa306462ce\\HydraulicModel.dll','HydraulicModel.dll',0,0,'','2014-04-04 18:53:15','2014-04-08 10:57:56'),
	('794a9489-3bae-45dc-9654-27aa306462ce','Models\\794a9489-3bae-45dc-9654-27aa306462ce\\IHydraulicModel.dll','IHydraulicModel.dll',0,0,'','2014-04-04 18:53:21','2014-04-08 10:57:51'),
	('794a9489-3bae-45dc-9654-27aa306462ce','Models\\794a9489-3bae-45dc-9654-27aa306462ce\\MyDataBase.dll','MyDataBase.dll',0,0,'','2014-04-04 18:53:28','2014-04-08 10:57:43'),
	('794a9489-3bae-45dc-9654-27aa306462ce','Models\\794a9489-3bae-45dc-9654-27aa306462ce\\RiverNetworkModel.dll','RiverNetworkModel.dll',1,0,'','2014-04-04 18:51:48','2014-04-08 10:57:28'),
	('794a9489-3bae-45dc-9654-27aa306462ce','Models\\794a9489-3bae-45dc-9654-27aa306462ce\\S1D_SemiImplicit.dll','S1D_SemiImplicit.dll',0,0,'','2014-04-04 18:53:41','2014-04-08 10:57:11'),
	('942f7612-b95f-6666-7777-72dd482fbc12','Models\\942f7612-b95f-6666-7777-72dd482fbc12\\Oatc.OpenMI.Examples.ModelComponents.SpatialModels.RiverModel.dll','Oatc.OpenMI.Examples.ModelComponents.SpatialModels.RiverModel.dll',1,0,'','2014-04-11 16:01:50',NULL);

/*!40000 ALTER TABLE `ModelFileMapping` ENABLE KEYS */;
UNLOCK TABLES;


# Dump of table ModelProperty
# ------------------------------------------------------------

DROP TABLE IF EXISTS `ModelProperty`;

CREATE TABLE `ModelProperty` (
  `modelGuid` varchar(40) NOT NULL DEFAULT '',
  `key` varchar(64) NOT NULL DEFAULT '',
  `type` tinyint(4) NOT NULL,
  `description` varchar(256) DEFAULT NULL,
  `default` varchar(64) DEFAULT NULL,
  `additional` text,
  `creation` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `modification` timestamp NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`modelGuid`,`key`),
  CONSTRAINT `fk_property_model` FOREIGN KEY (`modelGuid`) REFERENCES `Model` (`guid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

LOCK TABLES `ModelProperty` WRITE;
/*!40000 ALTER TABLE `ModelProperty` DISABLE KEYS */;

INSERT INTO `ModelProperty` (`modelGuid`, `key`, `type`, `description`, `default`, `additional`, `creation`, `modification`)
VALUES
	('180512cf-b424-4e7f-9d9e-e2850874040d','FileName',5,NULL,NULL,NULL,'2014-04-11 16:17:51',NULL),
	('794a9489-3bae-45dc-9654-27aa306462ce','controlFilePath',4,NULL,NULL,'','2014-04-04 18:54:40','2014-04-07 17:56:20'),
	('942f7612-b95f-6666-7777-72dd482fbc12','leakageCoefficient',2,NULL,NULL,NULL,'2014-04-11 16:06:51',NULL),
	('942f7612-b95f-6666-7777-72dd482fbc12','modelID',0,NULL,NULL,NULL,'2014-04-11 16:05:23','2014-04-11 16:05:31'),
	('942f7612-b95f-6666-7777-72dd482fbc12','riverBedLevel',2,NULL,NULL,NULL,'2014-04-11 16:06:41',NULL),
	('942f7612-b95f-6666-7777-72dd482fbc12','simulationEnd',0,NULL,NULL,NULL,'2014-04-11 16:06:13',NULL),
	('942f7612-b95f-6666-7777-72dd482fbc12','simulationStart',0,NULL,NULL,NULL,'2014-04-11 16:06:07',NULL),
	('942f7612-b95f-6666-7777-72dd482fbc12','TimeStepLength',1,NULL,NULL,NULL,'2014-04-11 16:06:33',NULL),
	('942f7612-b95f-6666-7777-72dd482fbc12','xCoordinate',0,NULL,NULL,NULL,'2014-04-11 16:06:18',NULL),
	('942f7612-b95f-6666-7777-72dd482fbc12','yCoordinate',0,NULL,NULL,NULL,'2014-04-11 16:06:23',NULL);

/*!40000 ALTER TABLE `ModelProperty` ENABLE KEYS */;
UNLOCK TABLES;


# Dump of table ModelTag
# ------------------------------------------------------------

DROP TABLE IF EXISTS `ModelTag`;

CREATE TABLE `ModelTag` (
  `guid` varchar(40) NOT NULL DEFAULT '',
  `name` varchar(64) NOT NULL DEFAULT '',
  `description` varchar(200) NOT NULL DEFAULT '',
  `parentTagGuid` varchar(40) DEFAULT NULL,
  `creation` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `modification` timestamp NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`guid`),
  KEY `parentTagGuid` (`parentTagGuid`),
  CONSTRAINT `fk_tag_parent` FOREIGN KEY (`parentTagGuid`) REFERENCES `ModelTag` (`guid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;



# Dump of table ModelTagMapping
# ------------------------------------------------------------

DROP TABLE IF EXISTS `ModelTagMapping`;

CREATE TABLE `ModelTagMapping` (
  `modelGuid` varchar(40) NOT NULL DEFAULT '',
  `tagGuid` varchar(40) NOT NULL DEFAULT '',
  PRIMARY KEY (`modelGuid`,`tagGuid`),
  KEY `fk_mapping_tag` (`tagGuid`),
  CONSTRAINT `fk_mapping_model` FOREIGN KEY (`modelGuid`) REFERENCES `Model` (`guid`),
  CONSTRAINT `fk_mapping_tag` FOREIGN KEY (`tagGuid`) REFERENCES `ModelTag` (`guid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;



# Dump of table Task
# ------------------------------------------------------------

DROP TABLE IF EXISTS `Task`;

CREATE TABLE `Task` (
  `guid` varchar(40) NOT NULL DEFAULT '',
  `instanceName` varchar(64) DEFAULT '',
  `compositionGuid` varchar(40) NOT NULL DEFAULT '',
  `compositionData` longtext NOT NULL,
  `state` tinyint(4) NOT NULL,
  `modelProgress` text,
  `userId` int(11) unsigned NOT NULL,
  `triggerInvokeTime` varchar(32) NOT NULL DEFAULT '',
  `timeStarted` timestamp NULL DEFAULT NULL,
  `timeFinished` timestamp NULL DEFAULT NULL,
  `creation` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `modification` timestamp NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`guid`),
  KEY `fk_task_comp` (`compositionGuid`),
  KEY `fk_task_user` (`userId`),
  CONSTRAINT `fk_task_comp` FOREIGN KEY (`compositionGuid`) REFERENCES `Composition` (`guid`),
  CONSTRAINT `fk_task_user` FOREIGN KEY (`userId`) REFERENCES `User` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

LOCK TABLES `Task` WRITE;
/*!40000 ALTER TABLE `Task` DISABLE KEYS */;

INSERT INTO `Task` (`guid`, `instanceName`, `compositionGuid`, `compositionData`, `state`, `modelProgress`, `userId`, `triggerInvokeTime`, `timeStarted`, `timeFinished`, `creation`, `modification`)
VALUES
	('91c04c56-8d7a-48fd-ad71-3d3d0a65dfa8','9775854e-e9fd-418f-8625-7d3a2a99f463','eb83b4b0-8dd4-48cb-807b-3b5fefcd29c7','<CompositionData xmlns=\"http://schemas.datacontract.org/2004/07/OOC.Contract.Data.Common\" xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\"><Composition z:Id=\"i1\" xmlns:a=\"http://schemas.datacontract.org/2004/07/OOC.Entity\" xmlns:z=\"http://schemas.microsoft.com/2003/10/Serialization/\"><EntityKey z:Id=\"i2\" xmlns=\"http://schemas.datacontract.org/2004/07/System.Data.Objects.DataClasses\" xmlns:b=\"http://schemas.datacontract.org/2004/07/System.Data\"><b:EntityContainerName>OOCEntities</b:EntityContainerName><b:EntityKeyValues><b:EntityKeyMember><b:Key>guid</b:Key><b:Value i:type=\"c:string\" xmlns:c=\"http://www.w3.org/2001/XMLSchema\">eb83b4b0-8dd4-48cb-807b-3b5fefcd29c7</b:Value></b:EntityKeyMember></b:EntityKeyValues><b:EntitySetName>Composition</b:EntitySetName></EntityKey><a:authorUserId>1</a:authorUserId><a:creation>2014-04-11T16:02:54+08:00</a:creation><a:guid>eb83b4b0-8dd4-48cb-807b-3b5fefcd29c7</a:guid><a:isFinished>true</a:isFinished><a:isShared>true</a:isShared><a:modification>2014-04-11T16:02:58+08:00</a:modification><a:title>multiple-test</a:title></Composition><Links><CompositionLinkData><CompositionLink z:Id=\"i3\" xmlns:a=\"http://schemas.datacontract.org/2004/07/OOC.Entity\" xmlns:z=\"http://schemas.microsoft.com/2003/10/Serialization/\"><EntityKey z:Id=\"i4\" xmlns=\"http://schemas.datacontract.org/2004/07/System.Data.Objects.DataClasses\" xmlns:b=\"http://schemas.datacontract.org/2004/07/System.Data\"><b:EntityContainerName>OOCEntities</b:EntityContainerName><b:EntityKeyValues><b:EntityKeyMember><b:Key>guid</b:Key><b:Value i:type=\"c:string\" xmlns:c=\"http://www.w3.org/2001/XMLSchema\">3bd24919-73d0-46aa-9abe-6e7c3ffcee79</b:Value></b:EntityKeyMember></b:EntityKeyValues><b:EntitySetName>CompositionLink</b:EntitySetName></EntityKey><a:compositionGuid>eb83b4b0-8dd4-48cb-807b-3b5fefcd29c7</a:compositionGuid><a:creation>2014-04-12T13:56:08+08:00</a:creation><a:dataOperation i:nil=\"true\"/><a:guid>3bd24919-73d0-46aa-9abe-6e7c3ffcee79</a:guid><a:modification i:nil=\"true\"/><a:sourceCmGuid>4c2f3302-c327-424d-9e5d-106bf7db1e38</a:sourceCmGuid><a:sourceElementSet>Branch:0</a:sourceElementSet><a:sourceQuantity>Leakage</a:sourceQuantity><a:targetCmGuid>1e3ce983-39f1-4eec-afea-a6f340677243</a:targetCmGuid><a:targetElementSet>1</a:targetElementSet><a:targetQuantity>Quantity</a:targetQuantity></CompositionLink><DataOperation><Kvs i:nil=\"true\" xmlns=\"http://schemas.datacontract.org/2004/07/OOC.Contract.Data.Abstract\" xmlns:a=\"http://schemas.microsoft.com/2003/10/Serialization/Arrays\"/></DataOperation></CompositionLinkData><CompositionLinkData><CompositionLink z:Id=\"i5\" xmlns:a=\"http://schemas.datacontract.org/2004/07/OOC.Entity\" xmlns:z=\"http://schemas.microsoft.com/2003/10/Serialization/\"><EntityKey z:Id=\"i6\" xmlns=\"http://schemas.datacontract.org/2004/07/System.Data.Objects.DataClasses\" xmlns:b=\"http://schemas.datacontract.org/2004/07/System.Data\"><b:EntityContainerName>OOCEntities</b:EntityContainerName><b:EntityKeyValues><b:EntityKeyMember><b:Key>guid</b:Key><b:Value i:type=\"c:string\" xmlns:c=\"http://www.w3.org/2001/XMLSchema\">9dfec86b-e265-4c79-88d1-0e5c6b5e5d74</b:Value></b:EntityKeyMember></b:EntityKeyValues><b:EntitySetName>CompositionLink</b:EntitySetName></EntityKey><a:compositionGuid>eb83b4b0-8dd4-48cb-807b-3b5fefcd29c7</a:compositionGuid><a:creation>2014-04-11T16:21:13+08:00</a:creation><a:dataOperation i:nil=\"true\"/><a:guid>9dfec86b-e265-4c79-88d1-0e5c6b5e5d74</a:guid><a:modification>2014-04-11T16:40:51+08:00</a:modification><a:sourceCmGuid>4c2f3302-c327-424d-9e5d-106bf7db1e38</a:sourceCmGuid><a:sourceElementSet>Branch:0</a:sourceElementSet><a:sourceQuantity>Flow</a:sourceQuantity><a:targetCmGuid>1e3ce983-39f1-4eec-afea-a6f340677243</a:targetCmGuid><a:targetElementSet>1</a:targetElementSet><a:targetQuantity>Quantity</a:targetQuantity></CompositionLink><DataOperation><Kvs i:nil=\"true\" xmlns=\"http://schemas.datacontract.org/2004/07/OOC.Contract.Data.Abstract\" xmlns:a=\"http://schemas.microsoft.com/2003/10/Serialization/Arrays\"/></DataOperation></CompositionLinkData></Links><Models><CompositionModelData><CompositionModel z:Id=\"i7\" xmlns:a=\"http://schemas.datacontract.org/2004/07/OOC.Entity\" xmlns:z=\"http://schemas.microsoft.com/2003/10/Serialization/\"><EntityKey z:Id=\"i8\" xmlns=\"http://schemas.datacontract.org/2004/07/System.Data.Objects.DataClasses\" xmlns:b=\"http://schemas.datacontract.org/2004/07/System.Data\"><b:EntityContainerName>OOCEntities</b:EntityContainerName><b:EntityKeyValues><b:EntityKeyMember><b:Key>guid</b:Key><b:Value i:type=\"c:string\" xmlns:c=\"http://www.w3.org/2001/XMLSchema\">1e3ce983-39f1-4eec-afea-a6f340677243</b:Value></b:EntityKeyMember></b:EntityKeyValues><b:EntitySetName>CompositionModel</b:EntitySetName></EntityKey><a:compositionGuid>eb83b4b0-8dd4-48cb-807b-3b5fefcd29c7</a:compositionGuid><a:creation>2014-04-11T16:18:45+08:00</a:creation><a:guid>1e3ce983-39f1-4eec-afea-a6f340677243</a:guid><a:modelGuid>180512cf-b424-4e7f-9d9e-e2850874040d</a:modelGuid><a:modification>2014-04-11T16:19:15+08:00</a:modification><a:properties>&lt;ArrayOfKeyValueOfstringstring xmlns=\"http://schemas.microsoft.com/2003/10/Serialization/Arrays\" xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\"&gt;&lt;KeyValueOfstringstring&gt;&lt;Key&gt;FileName&lt;/Key&gt;&lt;Value&gt;rivermodel.dat&lt;/Value&gt;&lt;/KeyValueOfstringstring&gt;&lt;/ArrayOfKeyValueOfstringstring&gt;</a:properties></CompositionModel><Model z:Id=\"i9\" xmlns:a=\"http://schemas.datacontract.org/2004/07/OOC.Entity\" xmlns:z=\"http://schemas.microsoft.com/2003/10/Serialization/\"><EntityKey z:Id=\"i10\" xmlns=\"http://schemas.datacontract.org/2004/07/System.Data.Objects.DataClasses\" xmlns:b=\"http://schemas.datacontract.org/2004/07/System.Data\"><b:EntityContainerName>OOCEntities</b:EntityContainerName><b:EntityKeyValues><b:EntityKeyMember><b:Key>guid</b:Key><b:Value i:type=\"c:string\" xmlns:c=\"http://www.w3.org/2001/XMLSchema\">180512cf-b424-4e7f-9d9e-e2850874040d</b:Value></b:EntityKeyMember></b:EntityKeyValues><b:EntitySetName>Model</b:EntitySetName></EntityKey><a:abstract>DataLogger based on DataMonitor</a:abstract><a:authorUserId>1</a:authorUserId><a:className>OOC.OpenMIComponent.DataMonitor.DataLogger</a:className><a:creation>2014-04-11T16:15:54+08:00</a:creation><a:guid>180512cf-b424-4e7f-9d9e-e2850874040d</a:guid><a:isApproved>true</a:isApproved><a:isPublic>true</a:isPublic><a:modification i:nil=\"true\"/><a:name>DataLogger</a:name><a:version>0.0.1</a:version></Model><ModelFiles xmlns:a=\"http://schemas.datacontract.org/2004/07/OOC.Entity\"><a:ModelFileMapping z:Id=\"i11\" xmlns:z=\"http://schemas.microsoft.com/2003/10/Serialization/\"><EntityKey z:Id=\"i12\" xmlns=\"http://schemas.datacontract.org/2004/07/System.Data.Objects.DataClasses\" xmlns:b=\"http://schemas.datacontract.org/2004/07/System.Data\"><b:EntityContainerName>OOCEntities</b:EntityContainerName><b:EntityKeyValues><b:EntityKeyMember><b:Key>modelGuid</b:Key><b:Value i:type=\"c:string\" xmlns:c=\"http://www.w3.org/2001/XMLSchema\">180512cf-b424-4e7f-9d9e-e2850874040d</b:Value></b:EntityKeyMember><b:EntityKeyMember><b:Key>fileName</b:Key><b:Value i:type=\"c:string\" xmlns:c=\"http://www.w3.org/2001/XMLSchema\">Models\\180512cf-b424-4e7f-9d9e-e2850874040d\\OOC.OpenMIComponent.DataMonitor.dll</b:Value></b:EntityKeyMember></b:EntityKeyValues><b:EntitySetName>ModelFileMapping</b:EntitySetName></EntityKey><a:creation>2014-04-11T16:16:41+08:00</a:creation><a:fileName>Models\\180512cf-b424-4e7f-9d9e-e2850874040d\\OOC.OpenMIComponent.DataMonitor.dll</a:fileName><a:isDocument>false</a:isDocument><a:isMainLibrary>true</a:isMainLibrary><a:modelGuid>180512cf-b424-4e7f-9d9e-e2850874040d</a:modelGuid><a:modification>2014-04-11T16:16:59+08:00</a:modification><a:relativePath>OOC.OpenMIComponent.DataMonitor.dll</a:relativePath><a:signature/></a:ModelFileMapping></ModelFiles><ModelProperties xmlns:a=\"http://schemas.datacontract.org/2004/07/OOC.Entity\"><a:ModelProperty z:Id=\"i13\" xmlns:z=\"http://schemas.microsoft.com/2003/10/Serialization/\"><EntityKey z:Id=\"i14\" xmlns=\"http://schemas.datacontract.org/2004/07/System.Data.Objects.DataClasses\" xmlns:b=\"http://schemas.datacontract.org/2004/07/System.Data\"><b:EntityContainerName>OOCEntities</b:EntityContainerName><b:EntityKeyValues><b:EntityKeyMember><b:Key>modelGuid</b:Key><b:Value i:type=\"c:string\" xmlns:c=\"http://www.w3.org/2001/XMLSchema\">180512cf-b424-4e7f-9d9e-e2850874040d</b:Value></b:EntityKeyMember><b:EntityKeyMember><b:Key>key</b:Key><b:Value i:type=\"c:string\" xmlns:c=\"http://www.w3.org/2001/XMLSchema\">FileName</b:Value></b:EntityKeyMember></b:EntityKeyValues><b:EntitySetName>ModelProperty</b:EntitySetName></EntityKey><a:additional i:nil=\"true\"/><a:creation>2014-04-11T16:17:51+08:00</a:creation><a:default i:nil=\"true\"/><a:description i:nil=\"true\"/><a:key>FileName</a:key><a:modelGuid>180512cf-b424-4e7f-9d9e-e2850874040d</a:modelGuid><a:modification i:nil=\"true\"/><a:type>5</a:type></a:ModelProperty></ModelProperties><PropertyValues><Kvs xmlns=\"http://schemas.datacontract.org/2004/07/OOC.Contract.Data.Abstract\" xmlns:a=\"http://schemas.microsoft.com/2003/10/Serialization/Arrays\"><a:KeyValueOfstringstring><a:Key>FileName</a:Key><a:Value>rivermodel.dat</a:Value></a:KeyValueOfstringstring></Kvs></PropertyValues></CompositionModelData><CompositionModelData><CompositionModel z:Id=\"i15\" xmlns:a=\"http://schemas.datacontract.org/2004/07/OOC.Entity\" xmlns:z=\"http://schemas.microsoft.com/2003/10/Serialization/\"><EntityKey z:Id=\"i16\" xmlns=\"http://schemas.datacontract.org/2004/07/System.Data.Objects.DataClasses\" xmlns:b=\"http://schemas.datacontract.org/2004/07/System.Data\"><b:EntityContainerName>OOCEntities</b:EntityContainerName><b:EntityKeyValues><b:EntityKeyMember><b:Key>guid</b:Key><b:Value i:type=\"c:string\" xmlns:c=\"http://www.w3.org/2001/XMLSchema\">4c2f3302-c327-424d-9e5d-106bf7db1e38</b:Value></b:EntityKeyMember></b:EntityKeyValues><b:EntitySetName>CompositionModel</b:EntitySetName></EntityKey><a:compositionGuid>eb83b4b0-8dd4-48cb-807b-3b5fefcd29c7</a:compositionGuid><a:creation>2014-04-11T16:03:42+08:00</a:creation><a:guid>4c2f3302-c327-424d-9e5d-106bf7db1e38</a:guid><a:modelGuid>942f7612-b95f-6666-7777-72dd482fbc12</a:modelGuid><a:modification>2014-04-11T16:05:05+08:00</a:modification><a:properties>&lt;ArrayOfKeyValueOfstringstring xmlns=\"http://schemas.microsoft.com/2003/10/Serialization/Arrays\" xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\"&gt;\n&lt;KeyValueOfstringstring&gt;&lt;Key&gt;modelID&lt;/Key&gt;&lt;Value&gt;RiverModel&lt;/Value&gt;&lt;/KeyValueOfstringstring&gt;\n&lt;KeyValueOfstringstring&gt;&lt;Key&gt;simulationStart&lt;/Key&gt;&lt;Value&gt;1990,1,2,0,0,0&lt;/Value&gt;&lt;/KeyValueOfstringstring&gt;\n&lt;KeyValueOfstringstring&gt;&lt;Key&gt;simulationEnd&lt;/Key&gt;&lt;Value&gt;1990,6,1,0,0,0&lt;/Value&gt;&lt;/KeyValueOfstringstring&gt;\n&lt;KeyValueOfstringstring&gt;&lt;Key&gt;xCoordinate&lt;/Key&gt;&lt;Value&gt;1510, 1510,  1723, 1950, 2003, 2110, 2403, 2830, 3537, 4010, 4552, 4510, 4002, 3310, 2889, 2900, 3110, 3051, 2328, 1688, 1110&lt;/Value&gt;&lt;/KeyValueOfstringstring&gt;\n&lt;KeyValueOfstringstring&gt;&lt;Key&gt;yCoordinate&lt;/Key&gt;&lt;Value&gt;10510,10000, 9131, 8051, 8062, 8020, 8343, 8463, 7814, 7010, 5827, 5010, 4498, 4110, 3624, 2663, 2110, 1788, 1496, 1475, 1510&lt;/Value&gt;&lt;/KeyValueOfstringstring&gt;\n&lt;KeyValueOfstringstring&gt;&lt;Key&gt;TimeStepLength&lt;/Key&gt;&lt;Value&gt;3600&lt;/Value&gt;&lt;/KeyValueOfstringstring&gt;\n&lt;KeyValueOfstringstring&gt;&lt;Key&gt;riverBedLevel&lt;/Key&gt;&lt;Value&gt;10.0&lt;/Value&gt;&lt;/KeyValueOfstringstring&gt;\n&lt;KeyValueOfstringstring&gt;&lt;Key&gt;leakageCoefficient&lt;/Key&gt;&lt;Value&gt;0.01&lt;/Value&gt;&lt;/KeyValueOfstringstring&gt;\n&lt;/ArrayOfKeyValueOfstringstring&gt;</a:properties></CompositionModel><Model z:Id=\"i17\" xmlns:a=\"http://schemas.datacontract.org/2004/07/OOC.Entity\" xmlns:z=\"http://schemas.microsoft.com/2003/10/Serialization/\"><EntityKey z:Id=\"i18\" xmlns=\"http://schemas.datacontract.org/2004/07/System.Data.Objects.DataClasses\" xmlns:b=\"http://schemas.datacontract.org/2004/07/System.Data\"><b:EntityContainerName>OOCEntities</b:EntityContainerName><b:EntityKeyValues><b:EntityKeyMember><b:Key>guid</b:Key><b:Value i:type=\"c:string\" xmlns:c=\"http://www.w3.org/2001/XMLSchema\">942f7612-b95f-6666-7777-72dd482fbc12</b:Value></b:EntityKeyMember></b:EntityKeyValues><b:EntitySetName>Model</b:EntitySetName></EntityKey><a:abstract>OpenMI Example</a:abstract><a:authorUserId>1</a:authorUserId><a:className>Oatc.OpenMI.Examples.ModelComponents.SpatialModels.RiverModel.RiverModelLC</a:className><a:creation>2014-04-11T16:01:31+08:00</a:creation><a:guid>942f7612-b95f-6666-7777-72dd482fbc12</a:guid><a:isApproved>true</a:isApproved><a:isPublic>true</a:isPublic><a:modification i:nil=\"true\"/><a:name>RiverModel</a:name><a:version>0.0.1</a:version></Model><ModelFiles xmlns:a=\"http://schemas.datacontract.org/2004/07/OOC.Entity\"><a:ModelFileMapping z:Id=\"i19\" xmlns:z=\"http://schemas.microsoft.com/2003/10/Serialization/\"><EntityKey z:Id=\"i20\" xmlns=\"http://schemas.datacontract.org/2004/07/System.Data.Objects.DataClasses\" xmlns:b=\"http://schemas.datacontract.org/2004/07/System.Data\"><b:EntityContainerName>OOCEntities</b:EntityContainerName><b:EntityKeyValues><b:EntityKeyMember><b:Key>modelGuid</b:Key><b:Value i:type=\"c:string\" xmlns:c=\"http://www.w3.org/2001/XMLSchema\">942f7612-b95f-6666-7777-72dd482fbc12</b:Value></b:EntityKeyMember><b:EntityKeyMember><b:Key>fileName</b:Key><b:Value i:type=\"c:string\" xmlns:c=\"http://www.w3.org/2001/XMLSchema\">Models\\942f7612-b95f-6666-7777-72dd482fbc12\\Oatc.OpenMI.Examples.ModelComponents.SpatialModels.RiverModel.dll</b:Value></b:EntityKeyMember></b:EntityKeyValues><b:EntitySetName>ModelFileMapping</b:EntitySetName></EntityKey><a:creation>2014-04-11T16:01:50+08:00</a:creation><a:fileName>Models\\942f7612-b95f-6666-7777-72dd482fbc12\\Oatc.OpenMI.Examples.ModelComponents.SpatialModels.RiverModel.dll</a:fileName><a:isDocument>false</a:isDocument><a:isMainLibrary>true</a:isMainLibrary><a:modelGuid>942f7612-b95f-6666-7777-72dd482fbc12</a:modelGuid><a:modification i:nil=\"true\"/><a:relativePath>Oatc.OpenMI.Examples.ModelComponents.SpatialModels.RiverModel.dll</a:relativePath><a:signature/></a:ModelFileMapping></ModelFiles><ModelProperties xmlns:a=\"http://schemas.datacontract.org/2004/07/OOC.Entity\"><a:ModelProperty z:Id=\"i21\" xmlns:z=\"http://schemas.microsoft.com/2003/10/Serialization/\"><EntityKey z:Id=\"i22\" xmlns=\"http://schemas.datacontract.org/2004/07/System.Data.Objects.DataClasses\" xmlns:b=\"http://schemas.datacontract.org/2004/07/System.Data\"><b:EntityContainerName>OOCEntities</b:EntityContainerName><b:EntityKeyValues><b:EntityKeyMember><b:Key>modelGuid</b:Key><b:Value i:type=\"c:string\" xmlns:c=\"http://www.w3.org/2001/XMLSchema\">942f7612-b95f-6666-7777-72dd482fbc12</b:Value></b:EntityKeyMember><b:EntityKeyMember><b:Key>key</b:Key><b:Value i:type=\"c:string\" xmlns:c=\"http://www.w3.org/2001/XMLSchema\">leakageCoefficient</b:Value></b:EntityKeyMember></b:EntityKeyValues><b:EntitySetName>ModelProperty</b:EntitySetName></EntityKey><a:additional i:nil=\"true\"/><a:creation>2014-04-11T16:06:51+08:00</a:creation><a:default i:nil=\"true\"/><a:description i:nil=\"true\"/><a:key>leakageCoefficient</a:key><a:modelGuid>942f7612-b95f-6666-7777-72dd482fbc12</a:modelGuid><a:modification i:nil=\"true\"/><a:type>2</a:type></a:ModelProperty><a:ModelProperty z:Id=\"i23\" xmlns:z=\"http://schemas.microsoft.com/2003/10/Serialization/\"><EntityKey z:Id=\"i24\" xmlns=\"http://schemas.datacontract.org/2004/07/System.Data.Objects.DataClasses\" xmlns:b=\"http://schemas.datacontract.org/2004/07/System.Data\"><b:EntityContainerName>OOCEntities</b:EntityContainerName><b:EntityKeyValues><b:EntityKeyMember><b:Key>modelGuid</b:Key><b:Value i:type=\"c:string\" xmlns:c=\"http://www.w3.org/2001/XMLSchema\">942f7612-b95f-6666-7777-72dd482fbc12</b:Value></b:EntityKeyMember><b:EntityKeyMember><b:Key>key</b:Key><b:Value i:type=\"c:string\" xmlns:c=\"http://www.w3.org/2001/XMLSchema\">modelID</b:Value></b:EntityKeyMember></b:EntityKeyValues><b:EntitySetName>ModelProperty</b:EntitySetName></EntityKey><a:additional i:nil=\"true\"/><a:creation>2014-04-11T16:05:23+08:00</a:creation><a:default i:nil=\"true\"/><a:description i:nil=\"true\"/><a:key>modelID</a:key><a:modelGuid>942f7612-b95f-6666-7777-72dd482fbc12</a:modelGuid><a:modification>2014-04-11T16:05:31+08:00</a:modification><a:type>0</a:type></a:ModelProperty><a:ModelProperty z:Id=\"i25\" xmlns:z=\"http://schemas.microsoft.com/2003/10/Serialization/\"><EntityKey z:Id=\"i26\" xmlns=\"http://schemas.datacontract.org/2004/07/System.Data.Objects.DataClasses\" xmlns:b=\"http://schemas.datacontract.org/2004/07/System.Data\"><b:EntityContainerName>OOCEntities</b:EntityContainerName><b:EntityKeyValues><b:EntityKeyMember><b:Key>modelGuid</b:Key><b:Value i:type=\"c:string\" xmlns:c=\"http://www.w3.org/2001/XMLSchema\">942f7612-b95f-6666-7777-72dd482fbc12</b:Value></b:EntityKeyMember><b:EntityKeyMember><b:Key>key</b:Key><b:Value i:type=\"c:string\" xmlns:c=\"http://www.w3.org/2001/XMLSchema\">riverBedLevel</b:Value></b:EntityKeyMember></b:EntityKeyValues><b:EntitySetName>ModelProperty</b:EntitySetName></EntityKey><a:additional i:nil=\"true\"/><a:creation>2014-04-11T16:06:41+08:00</a:creation><a:default i:nil=\"true\"/><a:description i:nil=\"true\"/><a:key>riverBedLevel</a:key><a:modelGuid>942f7612-b95f-6666-7777-72dd482fbc12</a:modelGuid><a:modification i:nil=\"true\"/><a:type>2</a:type></a:ModelProperty><a:ModelProperty z:Id=\"i27\" xmlns:z=\"http://schemas.microsoft.com/2003/10/Serialization/\"><EntityKey z:Id=\"i28\" xmlns=\"http://schemas.datacontract.org/2004/07/System.Data.Objects.DataClasses\" xmlns:b=\"http://schemas.datacontract.org/2004/07/System.Data\"><b:EntityContainerName>OOCEntities</b:EntityContainerName><b:EntityKeyValues><b:EntityKeyMember><b:Key>modelGuid</b:Key><b:Value i:type=\"c:string\" xmlns:c=\"http://www.w3.org/2001/XMLSchema\">942f7612-b95f-6666-7777-72dd482fbc12</b:Value></b:EntityKeyMember><b:EntityKeyMember><b:Key>key</b:Key><b:Value i:type=\"c:string\" xmlns:c=\"http://www.w3.org/2001/XMLSchema\">simulationEnd</b:Value></b:EntityKeyMember></b:EntityKeyValues><b:EntitySetName>ModelProperty</b:EntitySetName></EntityKey><a:additional i:nil=\"true\"/><a:creation>2014-04-11T16:06:13+08:00</a:creation><a:default i:nil=\"true\"/><a:description i:nil=\"true\"/><a:key>simulationEnd</a:key><a:modelGuid>942f7612-b95f-6666-7777-72dd482fbc12</a:modelGuid><a:modification i:nil=\"true\"/><a:type>0</a:type></a:ModelProperty><a:ModelProperty z:Id=\"i29\" xmlns:z=\"http://schemas.microsoft.com/2003/10/Serialization/\"><EntityKey z:Id=\"i30\" xmlns=\"http://schemas.datacontract.org/2004/07/System.Data.Objects.DataClasses\" xmlns:b=\"http://schemas.datacontract.org/2004/07/System.Data\"><b:EntityContainerName>OOCEntities</b:EntityContainerName><b:EntityKeyValues><b:EntityKeyMember><b:Key>modelGuid</b:Key><b:Value i:type=\"c:string\" xmlns:c=\"http://www.w3.org/2001/XMLSchema\">942f7612-b95f-6666-7777-72dd482fbc12</b:Value></b:EntityKeyMember><b:EntityKeyMember><b:Key>key</b:Key><b:Value i:type=\"c:string\" xmlns:c=\"http://www.w3.org/2001/XMLSchema\">simulationStart</b:Value></b:EntityKeyMember></b:EntityKeyValues><b:EntitySetName>ModelProperty</b:EntitySetName></EntityKey><a:additional i:nil=\"true\"/><a:creation>2014-04-11T16:06:07+08:00</a:creation><a:default i:nil=\"true\"/><a:description i:nil=\"true\"/><a:key>simulationStart</a:key><a:modelGuid>942f7612-b95f-6666-7777-72dd482fbc12</a:modelGuid><a:modification i:nil=\"true\"/><a:type>0</a:type></a:ModelProperty><a:ModelProperty z:Id=\"i31\" xmlns:z=\"http://schemas.microsoft.com/2003/10/Serialization/\"><EntityKey z:Id=\"i32\" xmlns=\"http://schemas.datacontract.org/2004/07/System.Data.Objects.DataClasses\" xmlns:b=\"http://schemas.datacontract.org/2004/07/System.Data\"><b:EntityContainerName>OOCEntities</b:EntityContainerName><b:EntityKeyValues><b:EntityKeyMember><b:Key>modelGuid</b:Key><b:Value i:type=\"c:string\" xmlns:c=\"http://www.w3.org/2001/XMLSchema\">942f7612-b95f-6666-7777-72dd482fbc12</b:Value></b:EntityKeyMember><b:EntityKeyMember><b:Key>key</b:Key><b:Value i:type=\"c:string\" xmlns:c=\"http://www.w3.org/2001/XMLSchema\">TimeStepLength</b:Value></b:EntityKeyMember></b:EntityKeyValues><b:EntitySetName>ModelProperty</b:EntitySetName></EntityKey><a:additional i:nil=\"true\"/><a:creation>2014-04-11T16:06:33+08:00</a:creation><a:default i:nil=\"true\"/><a:description i:nil=\"true\"/><a:key>TimeStepLength</a:key><a:modelGuid>942f7612-b95f-6666-7777-72dd482fbc12</a:modelGuid><a:modification i:nil=\"true\"/><a:type>1</a:type></a:ModelProperty><a:ModelProperty z:Id=\"i33\" xmlns:z=\"http://schemas.microsoft.com/2003/10/Serialization/\"><EntityKey z:Id=\"i34\" xmlns=\"http://schemas.datacontract.org/2004/07/System.Data.Objects.DataClasses\" xmlns:b=\"http://schemas.datacontract.org/2004/07/System.Data\"><b:EntityContainerName>OOCEntities</b:EntityContainerName><b:EntityKeyValues><b:EntityKeyMember><b:Key>modelGuid</b:Key><b:Value i:type=\"c:string\" xmlns:c=\"http://www.w3.org/2001/XMLSchema\">942f7612-b95f-6666-7777-72dd482fbc12</b:Value></b:EntityKeyMember><b:EntityKeyMember><b:Key>key</b:Key><b:Value i:type=\"c:string\" xmlns:c=\"http://www.w3.org/2001/XMLSchema\">xCoordinate</b:Value></b:EntityKeyMember></b:EntityKeyValues><b:EntitySetName>ModelProperty</b:EntitySetName></EntityKey><a:additional i:nil=\"true\"/><a:creation>2014-04-11T16:06:18+08:00</a:creation><a:default i:nil=\"true\"/><a:description i:nil=\"true\"/><a:key>xCoordinate</a:key><a:modelGuid>942f7612-b95f-6666-7777-72dd482fbc12</a:modelGuid><a:modification i:nil=\"true\"/><a:type>0</a:type></a:ModelProperty><a:ModelProperty z:Id=\"i35\" xmlns:z=\"http://schemas.microsoft.com/2003/10/Serialization/\"><EntityKey z:Id=\"i36\" xmlns=\"http://schemas.datacontract.org/2004/07/System.Data.Objects.DataClasses\" xmlns:b=\"http://schemas.datacontract.org/2004/07/System.Data\"><b:EntityContainerName>OOCEntities</b:EntityContainerName><b:EntityKeyValues><b:EntityKeyMember><b:Key>modelGuid</b:Key><b:Value i:type=\"c:string\" xmlns:c=\"http://www.w3.org/2001/XMLSchema\">942f7612-b95f-6666-7777-72dd482fbc12</b:Value></b:EntityKeyMember><b:EntityKeyMember><b:Key>key</b:Key><b:Value i:type=\"c:string\" xmlns:c=\"http://www.w3.org/2001/XMLSchema\">yCoordinate</b:Value></b:EntityKeyMember></b:EntityKeyValues><b:EntitySetName>ModelProperty</b:EntitySetName></EntityKey><a:additional i:nil=\"true\"/><a:creation>2014-04-11T16:06:23+08:00</a:creation><a:default i:nil=\"true\"/><a:description i:nil=\"true\"/><a:key>yCoordinate</a:key><a:modelGuid>942f7612-b95f-6666-7777-72dd482fbc12</a:modelGuid><a:modification i:nil=\"true\"/><a:type>0</a:type></a:ModelProperty></ModelProperties><PropertyValues><Kvs xmlns=\"http://schemas.datacontract.org/2004/07/OOC.Contract.Data.Abstract\" xmlns:a=\"http://schemas.microsoft.com/2003/10/Serialization/Arrays\"><a:KeyValueOfstringstring><a:Key>modelID</a:Key><a:Value>RiverModel</a:Value></a:KeyValueOfstringstring><a:KeyValueOfstringstring><a:Key>simulationStart</a:Key><a:Value>1990,1,2,0,0,0</a:Value></a:KeyValueOfstringstring><a:KeyValueOfstringstring><a:Key>simulationEnd</a:Key><a:Value>1990,6,1,0,0,0</a:Value></a:KeyValueOfstringstring><a:KeyValueOfstringstring><a:Key>xCoordinate</a:Key><a:Value>1510, 1510,  1723, 1950, 2003, 2110, 2403, 2830, 3537, 4010, 4552, 4510, 4002, 3310, 2889, 2900, 3110, 3051, 2328, 1688, 1110</a:Value></a:KeyValueOfstringstring><a:KeyValueOfstringstring><a:Key>yCoordinate</a:Key><a:Value>10510,10000, 9131, 8051, 8062, 8020, 8343, 8463, 7814, 7010, 5827, 5010, 4498, 4110, 3624, 2663, 2110, 1788, 1496, 1475, 1510</a:Value></a:KeyValueOfstringstring><a:KeyValueOfstringstring><a:Key>TimeStepLength</a:Key><a:Value>3600</a:Value></a:KeyValueOfstringstring><a:KeyValueOfstringstring><a:Key>riverBedLevel</a:Key><a:Value>10.0</a:Value></a:KeyValueOfstringstring><a:KeyValueOfstringstring><a:Key>leakageCoefficient</a:Key><a:Value>0.01</a:Value></a:KeyValueOfstringstring></Kvs></PropertyValues></CompositionModelData></Models></CompositionData>',5,'<ArrayOfKeyValueOfstringstring xmlns=\"http://schemas.microsoft.com/2003/10/Serialization/Arrays\" xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\"><KeyValueOfstringstring><Key>4c2f3302-c327-424d-9e5d-106bf7db1e38</Key><Value>1990/5/12 5:00:00</Value></KeyValueOfstringstring></ArrayOfKeyValueOfstringstring>',1,'1990-06-01 00:00:00','2014-04-12 13:59:42','2014-04-12 14:00:02','2014-04-11 16:07:54','2014-04-12 14:00:02'),
	('acd18117-9ea8-417f-a5b8-fb1e50701dd8','2cbacb41-362b-4ff3-b9f9-281b00bcd5dd','51af35f4-2dbf-4967-b42d-bd0c0dce394a','<CompositionData xmlns=\"http://schemas.datacontract.org/2004/07/OOC.Contract.Data.Common\" xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\"><Composition z:Id=\"i1\" xmlns:a=\"http://schemas.datacontract.org/2004/07/OOC.Entity\" xmlns:z=\"http://schemas.microsoft.com/2003/10/Serialization/\"><EntityKey z:Id=\"i2\" xmlns=\"http://schemas.datacontract.org/2004/07/System.Data.Objects.DataClasses\" xmlns:b=\"http://schemas.datacontract.org/2004/07/System.Data\"><b:EntityContainerName>OOCEntities</b:EntityContainerName><b:EntityKeyValues><b:EntityKeyMember><b:Key>guid</b:Key><b:Value i:type=\"c:string\" xmlns:c=\"http://www.w3.org/2001/XMLSchema\">51af35f4-2dbf-4967-b42d-bd0c0dce394a</b:Value></b:EntityKeyMember></b:EntityKeyValues><b:EntitySetName>Composition</b:EntitySetName></EntityKey><a:authorUserId>1</a:authorUserId><a:creation>2014-04-04T18:49:55+08:00</a:creation><a:guid>51af35f4-2dbf-4967-b42d-bd0c0dce394a</a:guid><a:isFinished>true</a:isFinished><a:isShared>true</a:isShared><a:modification i:nil=\"true\"/><a:title>COMP-TEST</a:title></Composition><Links xmlns:a=\"http://schemas.datacontract.org/2004/07/OOC.Entity\"/><Models><CompositionModelData><CompositionModel z:Id=\"i3\" xmlns:a=\"http://schemas.datacontract.org/2004/07/OOC.Entity\" xmlns:z=\"http://schemas.microsoft.com/2003/10/Serialization/\"><EntityKey z:Id=\"i4\" xmlns=\"http://schemas.datacontract.org/2004/07/System.Data.Objects.DataClasses\" xmlns:b=\"http://schemas.datacontract.org/2004/07/System.Data\"><b:EntityContainerName>OOCEntities</b:EntityContainerName><b:EntityKeyValues><b:EntityKeyMember><b:Key>guid</b:Key><b:Value i:type=\"c:string\" xmlns:c=\"http://www.w3.org/2001/XMLSchema\">19e95294-6aaf-4cd6-937a-e22ea58cc636</b:Value></b:EntityKeyMember></b:EntityKeyValues><b:EntitySetName>CompositionModel</b:EntitySetName></EntityKey><a:compositionGuid>51af35f4-2dbf-4967-b42d-bd0c0dce394a</a:compositionGuid><a:creation>2014-04-04T18:51:19+08:00</a:creation><a:guid>19e95294-6aaf-4cd6-937a-e22ea58cc636</a:guid><a:modelGuid>794a9489-3bae-45dc-9654-27aa306462ce</a:modelGuid><a:modification>2014-04-08T10:53:44+08:00</a:modification><a:properties>&lt;ArrayOfKeyValueOfstringstring xmlns=\"http://schemas.microsoft.com/2003/10/Serialization/Arrays\" xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\"&gt;&lt;KeyValueOfstringstring&gt;&lt;Key&gt;controlFilePath&lt;/Key&gt;&lt;Value&gt;Compositions\\51af35f4-2dbf-4967-b42d-bd0c0dce394a\\test.swm&lt;/Value&gt;&lt;/KeyValueOfstringstring&gt;&lt;/ArrayOfKeyValueOfstringstring&gt;</a:properties></CompositionModel><Model z:Id=\"i5\" xmlns:a=\"http://schemas.datacontract.org/2004/07/OOC.Entity\" xmlns:z=\"http://schemas.microsoft.com/2003/10/Serialization/\"><EntityKey z:Id=\"i6\" xmlns=\"http://schemas.datacontract.org/2004/07/System.Data.Objects.DataClasses\" xmlns:b=\"http://schemas.datacontract.org/2004/07/System.Data\"><b:EntityContainerName>OOCEntities</b:EntityContainerName><b:EntityKeyValues><b:EntityKeyMember><b:Key>guid</b:Key><b:Value i:type=\"c:string\" xmlns:c=\"http://www.w3.org/2001/XMLSchema\">794a9489-3bae-45dc-9654-27aa306462ce</b:Value></b:EntityKeyMember></b:EntityKeyValues><b:EntitySetName>Model</b:EntitySetName></EntityKey><a:abstract>Test River Network</a:abstract><a:authorUserId>1</a:authorUserId><a:className>RiverNetworkModel.RiverNetworkLC</a:className><a:creation>2014-04-04T18:50:42+08:00</a:creation><a:guid>794a9489-3bae-45dc-9654-27aa306462ce</a:guid><a:isApproved>true</a:isApproved><a:isPublic>true</a:isPublic><a:modification i:nil=\"true\"/><a:name>MODEL-TEST</a:name><a:version>0.0.1</a:version></Model><ModelFiles xmlns:a=\"http://schemas.datacontract.org/2004/07/OOC.Entity\"><a:ModelFileMapping z:Id=\"i7\" xmlns:z=\"http://schemas.microsoft.com/2003/10/Serialization/\"><EntityKey z:Id=\"i8\" xmlns=\"http://schemas.datacontract.org/2004/07/System.Data.Objects.DataClasses\" xmlns:b=\"http://schemas.datacontract.org/2004/07/System.Data\"><b:EntityContainerName>OOCEntities</b:EntityContainerName><b:EntityKeyValues><b:EntityKeyMember><b:Key>modelGuid</b:Key><b:Value i:type=\"c:string\" xmlns:c=\"http://www.w3.org/2001/XMLSchema\">794a9489-3bae-45dc-9654-27aa306462ce</b:Value></b:EntityKeyMember><b:EntityKeyMember><b:Key>fileName</b:Key><b:Value i:type=\"c:string\" xmlns:c=\"http://www.w3.org/2001/XMLSchema\">Models\\794a9489-3bae-45dc-9654-27aa306462ce\\Common.dll</b:Value></b:EntityKeyMember></b:EntityKeyValues><b:EntitySetName>ModelFileMapping</b:EntitySetName></EntityKey><a:creation>2014-04-04T18:53:03+08:00</a:creation><a:fileName>Models\\794a9489-3bae-45dc-9654-27aa306462ce\\Common.dll</a:fileName><a:isDocument>false</a:isDocument><a:isMainLibrary>false</a:isMainLibrary><a:modelGuid>794a9489-3bae-45dc-9654-27aa306462ce</a:modelGuid><a:modification>2014-04-08T10:56:19+08:00</a:modification><a:relativePath>Common.dll</a:relativePath><a:signature/></a:ModelFileMapping><a:ModelFileMapping z:Id=\"i9\" xmlns:z=\"http://schemas.microsoft.com/2003/10/Serialization/\"><EntityKey z:Id=\"i10\" xmlns=\"http://schemas.datacontract.org/2004/07/System.Data.Objects.DataClasses\" xmlns:b=\"http://schemas.datacontract.org/2004/07/System.Data\"><b:EntityContainerName>OOCEntities</b:EntityContainerName><b:EntityKeyValues><b:EntityKeyMember><b:Key>modelGuid</b:Key><b:Value i:type=\"c:string\" xmlns:c=\"http://www.w3.org/2001/XMLSchema\">794a9489-3bae-45dc-9654-27aa306462ce</b:Value></b:EntityKeyMember><b:EntityKeyMember><b:Key>fileName</b:Key><b:Value i:type=\"c:string\" xmlns:c=\"http://www.w3.org/2001/XMLSchema\">Models\\794a9489-3bae-45dc-9654-27aa306462ce\\DATA\\Boundary.txt</b:Value></b:EntityKeyMember></b:EntityKeyValues><b:EntitySetName>ModelFileMapping</b:EntitySetName></EntityKey><a:creation>2014-04-04T18:53:57+08:00</a:creation><a:fileName>Models\\794a9489-3bae-45dc-9654-27aa306462ce\\DATA\\Boundary.txt</a:fileName><a:isDocument>false</a:isDocument><a:isMainLibrary>false</a:isMainLibrary><a:modelGuid>794a9489-3bae-45dc-9654-27aa306462ce</a:modelGuid><a:modification>2014-04-08T10:58:37+08:00</a:modification><a:relativePath>DATA\\Boundary.txt</a:relativePath><a:signature/></a:ModelFileMapping><a:ModelFileMapping z:Id=\"i11\" xmlns:z=\"http://schemas.microsoft.com/2003/10/Serialization/\"><EntityKey z:Id=\"i12\" xmlns=\"http://schemas.datacontract.org/2004/07/System.Data.Objects.DataClasses\" xmlns:b=\"http://schemas.datacontract.org/2004/07/System.Data\"><b:EntityContainerName>OOCEntities</b:EntityContainerName><b:EntityKeyValues><b:EntityKeyMember><b:Key>modelGuid</b:Key><b:Value i:type=\"c:string\" xmlns:c=\"http://www.w3.org/2001/XMLSchema\">794a9489-3bae-45dc-9654-27aa306462ce</b:Value></b:EntityKeyMember><b:EntityKeyMember><b:Key>fileName</b:Key><b:Value i:type=\"c:string\" xmlns:c=\"http://www.w3.org/2001/XMLSchema\">Models\\794a9489-3bae-45dc-9654-27aa306462ce\\DATA\\element1D.txt</b:Value></b:EntityKeyMember></b:EntityKeyValues><b:EntitySetName>ModelFileMapping</b:EntitySetName></EntityKey><a:creation>2014-04-04T18:54:02+08:00</a:creation><a:fileName>Models\\794a9489-3bae-45dc-9654-27aa306462ce\\DATA\\element1D.txt</a:fileName><a:isDocument>false</a:isDocument><a:isMainLibrary>false</a:isMainLibrary><a:modelGuid>794a9489-3bae-45dc-9654-27aa306462ce</a:modelGuid><a:modification>2014-04-08T10:58:39+08:00</a:modification><a:relativePath>DATA\\element1D.txt</a:relativePath><a:signature/></a:ModelFileMapping><a:ModelFileMapping z:Id=\"i13\" xmlns:z=\"http://schemas.microsoft.com/2003/10/Serialization/\"><EntityKey z:Id=\"i14\" xmlns=\"http://schemas.datacontract.org/2004/07/System.Data.Objects.DataClasses\" xmlns:b=\"http://schemas.datacontract.org/2004/07/System.Data\"><b:EntityContainerName>OOCEntities</b:EntityContainerName><b:EntityKeyValues><b:EntityKeyMember><b:Key>modelGuid</b:Key><b:Value i:type=\"c:string\" xmlns:c=\"http://www.w3.org/2001/XMLSchema\">794a9489-3bae-45dc-9654-27aa306462ce</b:Value></b:EntityKeyMember><b:EntityKeyMember><b:Key>fileName</b:Key><b:Value i:type=\"c:string\" xmlns:c=\"http://www.w3.org/2001/XMLSchema\">Models\\794a9489-3bae-45dc-9654-27aa306462ce\\DATA\\node1D.txt</b:Value></b:EntityKeyMember></b:EntityKeyValues><b:EntitySetName>ModelFileMapping</b:EntitySetName></EntityKey><a:creation>2014-04-04T18:54:08+08:00</a:creation><a:fileName>Models\\794a9489-3bae-45dc-9654-27aa306462ce\\DATA\\node1D.txt</a:fileName><a:isDocument>false</a:isDocument><a:isMainLibrary>false</a:isMainLibrary><a:modelGuid>794a9489-3bae-45dc-9654-27aa306462ce</a:modelGuid><a:modification>2014-04-08T10:58:46+08:00</a:modification><a:relativePath>DATA\\node1D.txt</a:relativePath><a:signature/></a:ModelFileMapping><a:ModelFileMapping z:Id=\"i15\" xmlns:z=\"http://schemas.microsoft.com/2003/10/Serialization/\"><EntityKey z:Id=\"i16\" xmlns=\"http://schemas.datacontract.org/2004/07/System.Data.Objects.DataClasses\" xmlns:b=\"http://schemas.datacontract.org/2004/07/System.Data\"><b:EntityContainerName>OOCEntities</b:EntityContainerName><b:EntityKeyValues><b:EntityKeyMember><b:Key>modelGuid</b:Key><b:Value i:type=\"c:string\" xmlns:c=\"http://www.w3.org/2001/XMLSchema\">794a9489-3bae-45dc-9654-27aa306462ce</b:Value></b:EntityKeyMember><b:EntityKeyMember><b:Key>fileName</b:Key><b:Value i:type=\"c:string\" xmlns:c=\"http://www.w3.org/2001/XMLSchema\">Models\\794a9489-3bae-45dc-9654-27aa306462ce\\DATA\\sect.txt</b:Value></b:EntityKeyMember></b:EntityKeyValues><b:EntitySetName>ModelFileMapping</b:EntitySetName></EntityKey><a:creation>2014-04-04T18:54:12+08:00</a:creation><a:fileName>Models\\794a9489-3bae-45dc-9654-27aa306462ce\\DATA\\sect.txt</a:fileName><a:isDocument>false</a:isDocument><a:isMainLibrary>false</a:isMainLibrary><a:modelGuid>794a9489-3bae-45dc-9654-27aa306462ce</a:modelGuid><a:modification>2014-04-08T10:58:45+08:00</a:modification><a:relativePath>DATA\\sect.txt</a:relativePath><a:signature/></a:ModelFileMapping><a:ModelFileMapping z:Id=\"i17\" xmlns:z=\"http://schemas.microsoft.com/2003/10/Serialization/\"><EntityKey z:Id=\"i18\" xmlns=\"http://schemas.datacontract.org/2004/07/System.Data.Objects.DataClasses\" xmlns:b=\"http://schemas.datacontract.org/2004/07/System.Data\"><b:EntityContainerName>OOCEntities</b:EntityContainerName><b:EntityKeyValues><b:EntityKeyMember><b:Key>modelGuid</b:Key><b:Value i:type=\"c:string\" xmlns:c=\"http://www.w3.org/2001/XMLSchema\">794a9489-3bae-45dc-9654-27aa306462ce</b:Value></b:EntityKeyMember><b:EntityKeyMember><b:Key>fileName</b:Key><b:Value i:type=\"c:string\" xmlns:c=\"http://www.w3.org/2001/XMLSchema\">Models\\794a9489-3bae-45dc-9654-27aa306462ce\\DATA\\test.swm</b:Value></b:EntityKeyMember></b:EntityKeyValues><b:EntitySetName>ModelFileMapping</b:EntitySetName></EntityKey><a:creation>2014-04-04T18:54:17+08:00</a:creation><a:fileName>Models\\794a9489-3bae-45dc-9654-27aa306462ce\\DATA\\test.swm</a:fileName><a:isDocument>false</a:isDocument><a:isMainLibrary>false</a:isMainLibrary><a:modelGuid>794a9489-3bae-45dc-9654-27aa306462ce</a:modelGuid><a:modification>2014-04-08T10:58:49+08:00</a:modification><a:relativePath>DATA\\test.swm</a:relativePath><a:signature/></a:ModelFileMapping><a:ModelFileMapping z:Id=\"i19\" xmlns:z=\"http://schemas.microsoft.com/2003/10/Serialization/\"><EntityKey z:Id=\"i20\" xmlns=\"http://schemas.datacontract.org/2004/07/System.Data.Objects.DataClasses\" xmlns:b=\"http://schemas.datacontract.org/2004/07/System.Data\"><b:EntityContainerName>OOCEntities</b:EntityContainerName><b:EntityKeyValues><b:EntityKeyMember><b:Key>modelGuid</b:Key><b:Value i:type=\"c:string\" xmlns:c=\"http://www.w3.org/2001/XMLSchema\">794a9489-3bae-45dc-9654-27aa306462ce</b:Value></b:EntityKeyMember><b:EntityKeyMember><b:Key>fileName</b:Key><b:Value i:type=\"c:string\" xmlns:c=\"http://www.w3.org/2001/XMLSchema\">Models\\794a9489-3bae-45dc-9654-27aa306462ce\\DATA\\TimeSerial.txt</b:Value></b:EntityKeyMember></b:EntityKeyValues><b:EntitySetName>ModelFileMapping</b:EntitySetName></EntityKey><a:creation>2014-04-04T18:54:22+08:00</a:creation><a:fileName>Models\\794a9489-3bae-45dc-9654-27aa306462ce\\DATA\\TimeSerial.txt</a:fileName><a:isDocument>false</a:isDocument><a:isMainLibrary>false</a:isMainLibrary><a:modelGuid>794a9489-3bae-45dc-9654-27aa306462ce</a:modelGuid><a:modification>2014-04-08T10:58:52+08:00</a:modification><a:relativePath>DATA\\TimeSerial.txt</a:relativePath><a:signature/></a:ModelFileMapping><a:ModelFileMapping z:Id=\"i21\" xmlns:z=\"http://schemas.microsoft.com/2003/10/Serialization/\"><EntityKey z:Id=\"i22\" xmlns=\"http://schemas.datacontract.org/2004/07/System.Data.Objects.DataClasses\" xmlns:b=\"http://schemas.datacontract.org/2004/07/System.Data\"><b:EntityContainerName>OOCEntities</b:EntityContainerName><b:EntityKeyValues><b:EntityKeyMember><b:Key>modelGuid</b:Key><b:Value i:type=\"c:string\" xmlns:c=\"http://www.w3.org/2001/XMLSchema\">794a9489-3bae-45dc-9654-27aa306462ce</b:Value></b:EntityKeyMember><b:EntityKeyMember><b:Key>fileName</b:Key><b:Value i:type=\"c:string\" xmlns:c=\"http://www.w3.org/2001/XMLSchema\">Models\\794a9489-3bae-45dc-9654-27aa306462ce\\DrawingObject.dll</b:Value></b:EntityKeyMember></b:EntityKeyValues><b:EntitySetName>ModelFileMapping</b:EntitySetName></EntityKey><a:creation>2014-04-04T18:53:08+08:00</a:creation><a:fileName>Models\\794a9489-3bae-45dc-9654-27aa306462ce\\DrawingObject.dll</a:fileName><a:isDocument>false</a:isDocument><a:isMainLibrary>false</a:isMainLibrary><a:modelGuid>794a9489-3bae-45dc-9654-27aa306462ce</a:modelGuid><a:modification>2014-04-08T10:58:02+08:00</a:modification><a:relativePath>DrawingObject.dll</a:relativePath><a:signature/></a:ModelFileMapping><a:ModelFileMapping z:Id=\"i23\" xmlns:z=\"http://schemas.microsoft.com/2003/10/Serialization/\"><EntityKey z:Id=\"i24\" xmlns=\"http://schemas.datacontract.org/2004/07/System.Data.Objects.DataClasses\" xmlns:b=\"http://schemas.datacontract.org/2004/07/System.Data\"><b:EntityContainerName>OOCEntities</b:EntityContainerName><b:EntityKeyValues><b:EntityKeyMember><b:Key>modelGuid</b:Key><b:Value i:type=\"c:string\" xmlns:c=\"http://www.w3.org/2001/XMLSchema\">794a9489-3bae-45dc-9654-27aa306462ce</b:Value></b:EntityKeyMember><b:EntityKeyMember><b:Key>fileName</b:Key><b:Value i:type=\"c:string\" xmlns:c=\"http://www.w3.org/2001/XMLSchema\">Models\\794a9489-3bae-45dc-9654-27aa306462ce\\HydraulicModel.dll</b:Value></b:EntityKeyMember></b:EntityKeyValues><b:EntitySetName>ModelFileMapping</b:EntitySetName></EntityKey><a:creation>2014-04-04T18:53:15+08:00</a:creation><a:fileName>Models\\794a9489-3bae-45dc-9654-27aa306462ce\\HydraulicModel.dll</a:fileName><a:isDocument>false</a:isDocument><a:isMainLibrary>false</a:isMainLibrary><a:modelGuid>794a9489-3bae-45dc-9654-27aa306462ce</a:modelGuid><a:modification>2014-04-08T10:57:56+08:00</a:modification><a:relativePath>HydraulicModel.dll</a:relativePath><a:signature/></a:ModelFileMapping><a:ModelFileMapping z:Id=\"i25\" xmlns:z=\"http://schemas.microsoft.com/2003/10/Serialization/\"><EntityKey z:Id=\"i26\" xmlns=\"http://schemas.datacontract.org/2004/07/System.Data.Objects.DataClasses\" xmlns:b=\"http://schemas.datacontract.org/2004/07/System.Data\"><b:EntityContainerName>OOCEntities</b:EntityContainerName><b:EntityKeyValues><b:EntityKeyMember><b:Key>modelGuid</b:Key><b:Value i:type=\"c:string\" xmlns:c=\"http://www.w3.org/2001/XMLSchema\">794a9489-3bae-45dc-9654-27aa306462ce</b:Value></b:EntityKeyMember><b:EntityKeyMember><b:Key>fileName</b:Key><b:Value i:type=\"c:string\" xmlns:c=\"http://www.w3.org/2001/XMLSchema\">Models\\794a9489-3bae-45dc-9654-27aa306462ce\\IHydraulicModel.dll</b:Value></b:EntityKeyMember></b:EntityKeyValues><b:EntitySetName>ModelFileMapping</b:EntitySetName></EntityKey><a:creation>2014-04-04T18:53:21+08:00</a:creation><a:fileName>Models\\794a9489-3bae-45dc-9654-27aa306462ce\\IHydraulicModel.dll</a:fileName><a:isDocument>false</a:isDocument><a:isMainLibrary>false</a:isMainLibrary><a:modelGuid>794a9489-3bae-45dc-9654-27aa306462ce</a:modelGuid><a:modification>2014-04-08T10:57:51+08:00</a:modification><a:relativePath>IHydraulicModel.dll</a:relativePath><a:signature/></a:ModelFileMapping><a:ModelFileMapping z:Id=\"i27\" xmlns:z=\"http://schemas.microsoft.com/2003/10/Serialization/\"><EntityKey z:Id=\"i28\" xmlns=\"http://schemas.datacontract.org/2004/07/System.Data.Objects.DataClasses\" xmlns:b=\"http://schemas.datacontract.org/2004/07/System.Data\"><b:EntityContainerName>OOCEntities</b:EntityContainerName><b:EntityKeyValues><b:EntityKeyMember><b:Key>modelGuid</b:Key><b:Value i:type=\"c:string\" xmlns:c=\"http://www.w3.org/2001/XMLSchema\">794a9489-3bae-45dc-9654-27aa306462ce</b:Value></b:EntityKeyMember><b:EntityKeyMember><b:Key>fileName</b:Key><b:Value i:type=\"c:string\" xmlns:c=\"http://www.w3.org/2001/XMLSchema\">Models\\794a9489-3bae-45dc-9654-27aa306462ce\\MyDataBase.dll</b:Value></b:EntityKeyMember></b:EntityKeyValues><b:EntitySetName>ModelFileMapping</b:EntitySetName></EntityKey><a:creation>2014-04-04T18:53:28+08:00</a:creation><a:fileName>Models\\794a9489-3bae-45dc-9654-27aa306462ce\\MyDataBase.dll</a:fileName><a:isDocument>false</a:isDocument><a:isMainLibrary>false</a:isMainLibrary><a:modelGuid>794a9489-3bae-45dc-9654-27aa306462ce</a:modelGuid><a:modification>2014-04-08T10:57:43+08:00</a:modification><a:relativePath>MyDataBase.dll</a:relativePath><a:signature/></a:ModelFileMapping><a:ModelFileMapping z:Id=\"i29\" xmlns:z=\"http://schemas.microsoft.com/2003/10/Serialization/\"><EntityKey z:Id=\"i30\" xmlns=\"http://schemas.datacontract.org/2004/07/System.Data.Objects.DataClasses\" xmlns:b=\"http://schemas.datacontract.org/2004/07/System.Data\"><b:EntityContainerName>OOCEntities</b:EntityContainerName><b:EntityKeyValues><b:EntityKeyMember><b:Key>modelGuid</b:Key><b:Value i:type=\"c:string\" xmlns:c=\"http://www.w3.org/2001/XMLSchema\">794a9489-3bae-45dc-9654-27aa306462ce</b:Value></b:EntityKeyMember><b:EntityKeyMember><b:Key>fileName</b:Key><b:Value i:type=\"c:string\" xmlns:c=\"http://www.w3.org/2001/XMLSchema\">Models\\794a9489-3bae-45dc-9654-27aa306462ce\\RiverNetworkModel.dll</b:Value></b:EntityKeyMember></b:EntityKeyValues><b:EntitySetName>ModelFileMapping</b:EntitySetName></EntityKey><a:creation>2014-04-04T18:51:48+08:00</a:creation><a:fileName>Models\\794a9489-3bae-45dc-9654-27aa306462ce\\RiverNetworkModel.dll</a:fileName><a:isDocument>false</a:isDocument><a:isMainLibrary>true</a:isMainLibrary><a:modelGuid>794a9489-3bae-45dc-9654-27aa306462ce</a:modelGuid><a:modification>2014-04-08T10:57:28+08:00</a:modification><a:relativePath>RiverNetworkModel.dll</a:relativePath><a:signature/></a:ModelFileMapping><a:ModelFileMapping z:Id=\"i31\" xmlns:z=\"http://schemas.microsoft.com/2003/10/Serialization/\"><EntityKey z:Id=\"i32\" xmlns=\"http://schemas.datacontract.org/2004/07/System.Data.Objects.DataClasses\" xmlns:b=\"http://schemas.datacontract.org/2004/07/System.Data\"><b:EntityContainerName>OOCEntities</b:EntityContainerName><b:EntityKeyValues><b:EntityKeyMember><b:Key>modelGuid</b:Key><b:Value i:type=\"c:string\" xmlns:c=\"http://www.w3.org/2001/XMLSchema\">794a9489-3bae-45dc-9654-27aa306462ce</b:Value></b:EntityKeyMember><b:EntityKeyMember><b:Key>fileName</b:Key><b:Value i:type=\"c:string\" xmlns:c=\"http://www.w3.org/2001/XMLSchema\">Models\\794a9489-3bae-45dc-9654-27aa306462ce\\S1D_SemiImplicit.dll</b:Value></b:EntityKeyMember></b:EntityKeyValues><b:EntitySetName>ModelFileMapping</b:EntitySetName></EntityKey><a:creation>2014-04-04T18:53:41+08:00</a:creation><a:fileName>Models\\794a9489-3bae-45dc-9654-27aa306462ce\\S1D_SemiImplicit.dll</a:fileName><a:isDocument>false</a:isDocument><a:isMainLibrary>false</a:isMainLibrary><a:modelGuid>794a9489-3bae-45dc-9654-27aa306462ce</a:modelGuid><a:modification>2014-04-08T10:57:11+08:00</a:modification><a:relativePath>S1D_SemiImplicit.dll</a:relativePath><a:signature/></a:ModelFileMapping></ModelFiles><ModelProperties xmlns:a=\"http://schemas.datacontract.org/2004/07/OOC.Entity\"><a:ModelProperty z:Id=\"i33\" xmlns:z=\"http://schemas.microsoft.com/2003/10/Serialization/\"><EntityKey z:Id=\"i34\" xmlns=\"http://schemas.datacontract.org/2004/07/System.Data.Objects.DataClasses\" xmlns:b=\"http://schemas.datacontract.org/2004/07/System.Data\"><b:EntityContainerName>OOCEntities</b:EntityContainerName><b:EntityKeyValues><b:EntityKeyMember><b:Key>modelGuid</b:Key><b:Value i:type=\"c:string\" xmlns:c=\"http://www.w3.org/2001/XMLSchema\">794a9489-3bae-45dc-9654-27aa306462ce</b:Value></b:EntityKeyMember><b:EntityKeyMember><b:Key>key</b:Key><b:Value i:type=\"c:string\" xmlns:c=\"http://www.w3.org/2001/XMLSchema\">controlFilePath</b:Value></b:EntityKeyMember></b:EntityKeyValues><b:EntitySetName>ModelProperty</b:EntitySetName></EntityKey><a:additional/><a:creation>2014-04-04T18:54:40+08:00</a:creation><a:default i:nil=\"true\"/><a:description i:nil=\"true\"/><a:key>controlFilePath</a:key><a:modelGuid>794a9489-3bae-45dc-9654-27aa306462ce</a:modelGuid><a:modification>2014-04-07T17:56:20+08:00</a:modification><a:type>4</a:type></a:ModelProperty></ModelProperties><PropertyValues><Kvs xmlns=\"http://schemas.datacontract.org/2004/07/OOC.Contract.Data.Abstract\" xmlns:a=\"http://schemas.microsoft.com/2003/10/Serialization/Arrays\"><a:KeyValueOfstringstring><a:Key>controlFilePath</a:Key><a:Value>Compositions\\51af35f4-2dbf-4967-b42d-bd0c0dce394a\\test.swm</a:Value></a:KeyValueOfstringstring></Kvs></PropertyValues></CompositionModelData></Models></CompositionData>',5,'<ArrayOfKeyValueOfstringstring xmlns=\"http://schemas.microsoft.com/2003/10/Serialization/Arrays\" xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\"><KeyValueOfstringstring><Key>19e95294-6aaf-4cd6-937a-e22ea58cc636</Key><Value>2002/3/27 0:00:00</Value></KeyValueOfstringstring></ArrayOfKeyValueOfstringstring>',1,'2006-04-07 00:00:00','2014-04-09 13:47:21','2014-04-09 13:48:23','2014-04-08 11:01:17','2014-04-09 13:48:23');

/*!40000 ALTER TABLE `Task` ENABLE KEYS */;
UNLOCK TABLES;


# Dump of table TaskFileMapping
# ------------------------------------------------------------

DROP TABLE IF EXISTS `TaskFileMapping`;

CREATE TABLE `TaskFileMapping` (
  `taskGuid` varchar(40) NOT NULL DEFAULT '',
  `fileName` varchar(200) NOT NULL DEFAULT '',
  `relativePath` varchar(512) NOT NULL DEFAULT '',
  `type` tinyint(4) NOT NULL,
  `isDownloadable` tinyint(1) NOT NULL,
  PRIMARY KEY (`taskGuid`,`fileName`),
  CONSTRAINT `fk_file_task` FOREIGN KEY (`taskGuid`) REFERENCES `Task` (`guid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

LOCK TABLES `TaskFileMapping` WRITE;
/*!40000 ALTER TABLE `TaskFileMapping` DISABLE KEYS */;

INSERT INTO `TaskFileMapping` (`taskGuid`, `fileName`, `relativePath`, `type`, `isDownloadable`)
VALUES
	('91c04c56-8d7a-48fd-ad71-3d3d0a65dfa8','Tasks\\91c04c56-8d7a-48fd-ad71-3d3d0a65dfa8\\Log\\runner.log','runner.log',3,1),
	('91c04c56-8d7a-48fd-ad71-3d3d0a65dfa8','Tasks\\91c04c56-8d7a-48fd-ad71-3d3d0a65dfa8\\Log\\taskManager.log','taskManager.log',3,1),
	('91c04c56-8d7a-48fd-ad71-3d3d0a65dfa8','Tasks\\91c04c56-8d7a-48fd-ad71-3d3d0a65dfa8\\Log\\workspace.log','workspace.log',3,1),
	('91c04c56-8d7a-48fd-ad71-3d3d0a65dfa8','Tasks\\91c04c56-8d7a-48fd-ad71-3d3d0a65dfa8\\Output\\rivermodel.dat','rivermodel.dat',2,1),
	('acd18117-9ea8-417f-a5b8-fb1e50701dd8','Compositions\\51af35f4-2dbf-4967-b42d-bd0c0dce394a\\test.swm','test.swm',1,0);

/*!40000 ALTER TABLE `TaskFileMapping` ENABLE KEYS */;
UNLOCK TABLES;


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
	(1,'test','123','',15,'','2014-04-01 11:24:38','2014-04-02 10:59:09');

/*!40000 ALTER TABLE `User` ENABLE KEYS */;
UNLOCK TABLES;



/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
