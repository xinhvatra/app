/*
SQLyog Professional v13.1.1 (64 bit)
MySQL - 10.1.37-MariaDB : Database - customer
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`customer` /*!40100 DEFAULT CHARACTER SET utf8 COLLATE utf8_unicode_ci */;

USE `customer`;

/*Table structure for table `client` */

DROP TABLE IF EXISTS `client`;

CREATE TABLE `client` (
  `id` int(11) NOT NULL,
  `service_id` int(11) NOT NULL,
  `name` text COLLATE utf8_unicode_ci NOT NULL,
  `gate` int(5) NOT NULL,
  `idle` tinyint(1) NOT NULL DEFAULT '1',
  `active` tinyint(1) NOT NULL DEFAULT '1',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

/*Data for the table `client` */

insert  into `client`(`id`,`service_id`,`name`,`gate`,`idle`,`active`) values 
(1,1,'Nguyễn Quý Kỳ',1,1,1),
(2,1,'Nguyễn Phương Thảo',2,1,1),
(3,1,'Đinh Dương Liễu',3,1,1),
(4,1,'Nguyễn Thị Hà Phương',4,1,1),
(5,1,'Nguyễn Lan Hương',5,1,1),
(6,1,'Ma Thị Dung',6,1,1),
(7,2,'Vi Thị Thoa',1,1,1),
(8,2,'Vũ Quang Hải',2,1,1),
(9,2,'Ngô Huyền Trang',3,1,1);

/*Table structure for table `cus_deal` */

DROP TABLE IF EXISTS `cus_deal`;

CREATE TABLE `cus_deal` (
  `cus_id` int(11) NOT NULL,
  `client_id` int(11) NOT NULL,
  `service_id` int(11) NOT NULL,
  PRIMARY KEY (`cus_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

/*Data for the table `cus_deal` */

/*Table structure for table `cus_wait` */

DROP TABLE IF EXISTS `cus_wait`;

CREATE TABLE `cus_wait` (
  `cus_id` int(11) NOT NULL,
  `service_id` int(11) NOT NULL,
  PRIMARY KEY (`cus_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

/*Data for the table `cus_wait` */

insert  into `cus_wait`(`cus_id`,`service_id`) values 
(1002,1),
(1003,1),
(1004,1),
(1005,1),
(2004,2),
(2005,2),
(2006,2),
(2007,2),
(2008,2),
(2009,2);

/*Table structure for table `services` */

DROP TABLE IF EXISTS `services`;

CREATE TABLE `services` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` text COLLATE utf8_unicode_ci NOT NULL,
  `current_cus` int(11) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

/*Data for the table `services` */

insert  into `services`(`id`,`name`,`current_cus`) values 
(1,'Giao dịch tiền gửi',1042),
(2,'Giao dịch Thẻ, Emobile-banking',2073),
(3,'Giao dịch tiền vay',3001);

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
