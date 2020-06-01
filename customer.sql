/*
SQLyog Ultimate v13.1.1 (64 bit)
MySQL - 10.4.8-MariaDB : Database - customer
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
  `name` text COLLATE utf8_unicode_ci NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

/*Data for the table `client` */

insert  into `client`(`id`,`name`) values 
(1,'Nguyễn Quý Kỳ'),
(2,'Nguyễn Phương Thảo'),
(3,'Đinh Dương Liễu');

/*Table structure for table `client_free` */

DROP TABLE IF EXISTS `client_free`;

CREATE TABLE `client_free` (
  `id` int(11) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

/*Data for the table `client_free` */

/*Table structure for table `cus_current` */

DROP TABLE IF EXISTS `cus_current`;

CREATE TABLE `cus_current` (
  `id` int(11) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

/*Data for the table `cus_current` */

insert  into `cus_current`(`id`) values 
(1023);

/*Table structure for table `cus_deal` */

DROP TABLE IF EXISTS `cus_deal`;

CREATE TABLE `cus_deal` (
  `id` int(11) NOT NULL,
  `client_id` int(11) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

/*Data for the table `cus_deal` */

/*Table structure for table `cus_wait` */

DROP TABLE IF EXISTS `cus_wait`;

CREATE TABLE `cus_wait` (
  `id` int(11) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

/*Data for the table `cus_wait` */

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
