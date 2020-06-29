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
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `service_id` int(11) NOT NULL,
  `name` text COLLATE utf8_unicode_ci NOT NULL,
  `gate` int(5) NOT NULL,
  `idle` tinyint(1) NOT NULL DEFAULT '0',
  `active` tinyint(1) NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

/*Data for the table `client` */

insert  into `client`(`id`,`service_id`,`name`,`gate`,`idle`,`active`) values 
(1,1,'Nguyễn Quý Kỳ',1,0,0),
(2,1,'Nguyễn Phương Thảo',2,0,0),
(3,1,'Đinh Dương Liễu',3,0,0),
(4,1,'Nguyễn Thị Hà Phương',4,0,0),
(5,1,'Nguyễn Lan Hương',5,0,0),
(6,1,'Ma Thị Dung',6,0,0),
(7,2,'Vi Thị Thoa',7,0,0),
(8,2,'Vũ Quang Hải',8,0,0),
(9,2,'Ngô Huyền Trang',9,0,0);

/*Table structure for table `cus_deal` */

DROP TABLE IF EXISTS `cus_deal`;

CREATE TABLE `cus_deal` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `cus_id` int(11) NOT NULL,
  `client_id` int(11) NOT NULL,
  `service_id` int(11) NOT NULL,
  `gate` int(11) NOT NULL,
  `rate` int(11) NOT NULL,
  `time` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=167 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

/*Data for the table `cus_deal` */

insert  into `cus_deal`(`id`,`cus_id`,`client_id`,`service_id`,`gate`,`rate`,`time`) values 
(1,1061,6,1,6,1,'2020-06-05 10:40:00'),
(2,1062,6,1,6,1,'2020-06-05 10:41:04'),
(3,2004,8,2,2,1,'2020-06-05 10:40:36'),
(4,2005,8,2,2,1,'2020-06-23 10:06:16'),
(5,2006,8,2,2,1,'2020-06-23 10:08:31'),
(6,2007,8,2,2,1,'2020-06-23 10:11:43'),
(7,2008,8,2,2,1,'2020-06-23 10:14:14'),
(8,2009,8,2,2,1,'2020-06-23 10:19:51'),
(9,2075,8,2,2,1,'2020-06-23 10:25:53'),
(10,2076,8,2,2,1,'2020-06-23 10:26:02'),
(11,2077,8,2,2,1,'2020-06-23 10:39:41'),
(12,2078,8,2,2,1,'2020-06-23 10:40:53'),
(13,2079,8,2,2,1,'2020-06-24 10:00:31'),
(14,2080,8,2,2,1,'2020-06-24 10:19:08'),
(15,2081,8,2,2,1,'2020-06-24 10:20:32'),
(16,2082,8,2,2,1,'2020-06-24 10:21:44'),
(17,2083,8,2,2,1,'2020-06-24 10:25:55'),
(18,2084,8,2,2,1,'2020-06-24 10:30:39'),
(19,2085,8,2,2,1,'2020-06-24 10:34:27'),
(20,2086,8,2,2,1,'2020-06-24 10:34:36'),
(21,2087,8,2,2,1,'2020-06-24 10:36:40'),
(22,2088,8,2,2,1,'2020-06-24 10:36:45'),
(23,2089,8,2,2,1,'2020-06-24 10:38:27'),
(24,2090,8,2,2,1,'2020-06-24 10:40:46'),
(25,2091,8,2,2,1,'2020-06-24 10:41:05'),
(26,2092,8,2,2,1,'2020-06-24 10:41:30'),
(27,2093,8,2,2,1,'2020-06-24 10:41:35'),
(28,2094,8,2,2,1,'2020-06-24 10:42:38'),
(29,2095,8,2,2,1,'2020-06-24 10:43:45'),
(30,2096,8,2,2,1,'2020-06-24 10:44:21'),
(31,2097,8,2,2,1,'2020-06-24 10:44:45'),
(32,2098,8,2,2,1,'2020-06-24 10:50:07'),
(33,2099,8,2,2,1,'2020-06-24 10:50:54'),
(34,2100,8,2,2,1,'2020-06-24 13:29:56'),
(35,2101,8,2,2,1,'2020-06-24 13:33:17'),
(36,2102,8,2,2,1,'2020-06-24 13:33:48'),
(37,2103,8,2,2,1,'2020-06-24 13:34:25'),
(38,2104,8,2,2,1,'2020-06-24 13:37:07'),
(39,2105,8,2,2,1,'2020-06-24 13:37:16'),
(40,2106,8,2,2,1,'2020-06-24 13:39:03'),
(41,2107,8,2,2,1,'2020-06-24 13:43:42'),
(42,2108,8,2,2,1,'2020-06-24 13:57:03'),
(43,2109,8,2,2,1,'2020-06-24 13:57:11'),
(44,2110,8,2,2,1,'2020-06-24 17:15:32'),
(45,2111,8,2,2,1,'2020-06-24 17:15:53'),
(46,2112,8,2,2,1,'2020-06-24 17:16:30'),
(47,1063,1,1,1,1,'2020-06-24 17:19:04'),
(48,1064,1,1,1,1,'2020-06-24 17:19:15'),
(49,1065,1,1,1,1,'2020-06-24 17:19:39'),
(50,1066,1,1,1,1,'2020-06-24 17:20:29'),
(51,2113,8,2,2,1,'2020-06-24 17:39:00'),
(52,1067,5,1,5,1,'2020-06-24 17:39:02'),
(53,1068,1,1,1,1,'2020-06-24 17:39:03'),
(54,2114,8,2,2,1,'2020-06-24 17:39:37'),
(55,1069,5,1,5,1,'2020-06-24 17:42:28'),
(56,2115,8,2,2,1,'2020-06-24 17:42:49'),
(57,1070,1,1,1,1,'2020-06-24 17:44:05'),
(58,1071,1,1,1,1,'2020-06-24 17:46:20'),
(59,2116,8,2,2,1,'2020-06-24 17:52:11'),
(60,2117,8,2,2,1,'2020-06-25 07:39:10'),
(61,2118,8,2,2,1,'2020-06-25 07:42:04'),
(62,2119,8,2,2,1,'2020-06-25 07:54:27'),
(63,2120,8,2,2,1,'2020-06-25 08:00:33'),
(64,2121,8,2,2,1,'2020-06-25 08:01:16'),
(65,2122,8,2,2,1,'2020-06-25 08:02:11'),
(66,2123,8,2,2,1,'2020-06-25 08:07:15'),
(67,2124,8,2,2,1,'2020-06-25 08:08:07'),
(68,1072,1,1,1,1,'2020-06-25 08:09:43'),
(69,2125,8,2,2,1,'2020-06-25 08:09:49'),
(70,1073,2,1,2,1,'2020-06-25 08:10:07'),
(71,1074,1,1,1,1,'2020-06-25 08:12:37'),
(72,1075,3,1,3,1,'2020-06-25 08:13:26'),
(73,1076,2,1,2,1,'2020-06-25 08:13:50'),
(74,1077,3,1,3,1,'2020-06-25 08:18:43'),
(75,1078,2,1,2,1,'2020-06-25 08:18:43'),
(76,1079,4,1,4,1,'2020-06-25 08:18:44'),
(77,1080,1,1,1,1,'2020-06-25 08:18:45'),
(78,1081,4,1,4,1,'2020-06-25 08:45:23'),
(79,1082,3,1,3,1,'2020-06-25 08:45:25'),
(80,1083,2,1,2,1,'2020-06-25 08:45:26'),
(81,1084,1,1,1,1,'2020-06-25 08:45:27'),
(82,1085,4,1,4,1,'2020-06-25 08:46:45'),
(83,1086,1,1,1,1,'2020-06-25 08:47:44'),
(84,2126,8,2,2,1,'2020-06-25 08:49:45'),
(85,2127,8,2,12,1,'2020-06-25 09:00:49'),
(86,2128,8,2,12,1,'2020-06-25 09:03:27'),
(87,2129,8,2,12,1,'2020-06-25 09:06:09'),
(88,2130,8,2,12,1,'2020-06-25 09:06:50'),
(89,2131,8,2,12,1,'2020-06-25 09:08:07'),
(90,2132,8,2,12,1,'2020-06-25 09:08:37'),
(91,2133,8,2,12,1,'2020-06-25 09:09:19'),
(92,1087,1,1,1,1,'2020-06-25 09:13:08'),
(93,1088,1,1,1,1,'2020-06-25 09:13:44'),
(94,1089,1,1,1,1,'2020-06-25 09:14:43'),
(95,1090,1,1,1,1,'2020-06-25 09:16:00'),
(96,1091,1,1,1,1,'2020-06-25 09:17:07'),
(97,1092,1,1,1,1,'2020-06-25 09:17:23'),
(98,1093,1,1,1,1,'2020-06-25 09:18:29'),
(99,1094,1,1,1,1,'2020-06-25 09:35:45'),
(100,1095,1,1,1,1,'2020-06-25 09:37:04'),
(101,1096,1,1,1,1,'2020-06-25 09:37:38'),
(102,1097,1,1,1,1,'2020-06-25 09:38:51'),
(103,1098,1,1,1,1,'2020-06-25 09:48:31'),
(104,1099,1,1,1,1,'2020-06-25 09:48:51'),
(105,1100,6,1,6,1,'2020-06-25 09:49:27'),
(106,1101,6,1,6,1,'2020-06-25 09:58:26'),
(107,1102,6,1,6,1,'2020-06-25 09:59:34'),
(108,1103,6,1,6,1,'2020-06-25 10:03:43'),
(109,1104,6,1,6,1,'2020-06-25 10:04:03'),
(110,1105,6,1,6,1,'2020-06-25 10:04:20'),
(111,1106,6,1,6,1,'2020-06-25 10:05:18'),
(112,1107,6,1,6,1,'2020-06-25 10:08:39'),
(113,1108,6,1,6,1,'2020-06-25 14:11:47'),
(114,1109,6,1,6,1,'2020-06-25 14:12:57'),
(115,1110,6,1,6,1,'2020-06-25 14:13:08'),
(116,1111,6,1,6,1,'2020-06-25 14:14:16'),
(117,1112,6,1,6,1,'2020-06-25 14:15:11'),
(118,1113,6,1,6,1,'2020-06-25 14:16:24'),
(119,1114,1,1,1,1,'2020-06-25 14:16:58'),
(120,1115,6,1,6,1,'2020-06-25 14:16:59'),
(121,1116,6,1,6,1,'2020-06-25 14:17:40'),
(122,1117,1,1,1,1,'2020-06-25 14:17:40'),
(123,1118,3,1,3,1,'2020-06-25 14:20:07'),
(124,1119,2,1,2,1,'2020-06-25 14:20:07'),
(125,1120,1,1,1,1,'2020-06-25 14:20:09'),
(126,1121,3,1,3,1,'2020-06-25 14:20:18'),
(127,1122,1,1,1,1,'2020-06-25 14:20:40'),
(128,1123,2,1,2,1,'2020-06-25 14:20:41'),
(129,1124,3,1,3,1,'2020-06-25 14:20:41'),
(130,1125,1,1,1,1,'2020-06-25 15:07:14'),
(131,1126,2,1,2,1,'2020-06-25 15:07:47'),
(132,1127,2,1,2,1,'2020-06-25 15:08:14'),
(133,1128,1,1,1,1,'2020-06-25 15:08:55'),
(134,1129,2,1,2,1,'2020-06-25 15:09:04'),
(135,2134,8,2,8,1,'2020-06-25 15:45:13'),
(136,1130,2,1,2,1,'2020-06-25 15:45:21'),
(137,1131,1,1,1,1,'2020-06-25 15:45:24'),
(138,1132,5,1,5,1,'2020-06-25 15:48:25'),
(139,1133,5,1,5,1,'2020-06-25 15:50:48'),
(140,1134,5,1,5,1,'2020-06-25 15:50:53'),
(141,1135,5,1,5,1,'2020-06-25 15:51:18'),
(142,1136,5,1,5,1,'2020-06-25 15:51:23'),
(143,1137,5,1,5,1,'2020-06-25 15:53:05'),
(144,1138,1,1,1,1,'2020-06-25 15:55:25'),
(145,1139,2,1,2,1,'2020-06-25 15:55:41'),
(146,1140,1,1,1,1,'2020-06-25 15:59:46'),
(147,2156,8,2,8,1,'2020-06-25 16:02:05'),
(148,1141,2,1,2,1,'2020-06-25 16:02:21'),
(149,1142,1,1,1,1,'2020-06-25 16:02:22'),
(150,1143,1,1,1,1,'2020-06-25 16:02:32'),
(151,1144,2,1,2,1,'2020-06-25 16:02:35'),
(152,1145,3,1,3,1,'2020-06-25 16:03:57'),
(153,1146,2,1,2,1,'2020-06-25 16:04:00'),
(154,1147,6,1,6,1,'2020-06-25 16:04:03'),
(155,1148,1,1,1,1,'2020-06-25 16:05:30'),
(156,1150,3,1,3,1,'2020-06-29 10:44:41'),
(157,1149,3,1,3,1,'2020-06-29 14:36:04'),
(158,1151,3,1,3,1,'2020-06-29 14:37:31'),
(159,1152,3,1,3,1,'2020-06-29 15:43:18'),
(160,1153,3,1,3,1,'2020-06-29 15:48:30'),
(161,1154,3,1,3,1,'2020-06-29 16:01:16'),
(162,1155,3,1,3,1,'2020-06-29 16:03:50'),
(163,1156,3,1,3,1,'2020-06-29 16:24:21'),
(164,1157,3,1,3,1,'2020-06-29 16:24:56'),
(165,1158,3,1,3,1,'2020-06-29 16:26:04'),
(166,1159,3,1,3,1,'2020-06-29 16:32:11');

/*Table structure for table `cus_rating` */

DROP TABLE IF EXISTS `cus_rating`;

CREATE TABLE `cus_rating` (
  `id` int(2) NOT NULL,
  `value` text COLLATE utf8_unicode_ci NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

/*Data for the table `cus_rating` */

insert  into `cus_rating`(`id`,`value`) values 
(1,'Tốt'),
(2,'Bình thường'),
(3,'Không tốt');

/*Table structure for table `cus_wait` */

DROP TABLE IF EXISTS `cus_wait`;

CREATE TABLE `cus_wait` (
  `cus_id` int(11) NOT NULL,
  `service_id` int(11) NOT NULL,
  `force_service_id` smallint(3) NOT NULL DEFAULT '0',
  `priority` smallint(2) NOT NULL DEFAULT '0',
  PRIMARY KEY (`cus_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

/*Data for the table `cus_wait` */

insert  into `cus_wait`(`cus_id`,`service_id`,`force_service_id`,`priority`) values 
(1160,1,0,0),
(1161,1,0,0),
(1162,1,0,0),
(2157,2,0,0),
(2158,2,0,1),
(2159,2,0,0),
(2160,2,0,0),
(2161,2,0,0),
(2162,2,0,0);

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
(1,'Giao dịch tiền gửi',1162),
(2,'Giao dịch Thẻ, Emobile-banking',2162),
(3,'Giao dịch tiền vay',3001);

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
