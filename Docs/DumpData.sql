CREATE DATABASE  IF NOT EXISTS `zgmarket` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `zgmarket`;
-- MySQL dump 10.13  Distrib 8.0.32, for Win64 (x86_64)
--
-- Host: localhost    Database: zgmarket
-- ------------------------------------------------------
-- Server version	8.0.32

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `emp`
--

DROP TABLE IF EXISTS `emp`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `emp` (
  `id` bigint unsigned NOT NULL AUTO_INCREMENT,
  `name` varchar(255) DEFAULT NULL,
  `last_name` varchar(255) DEFAULT NULL,
  `birth_date` date DEFAULT NULL,
  `position` varchar(255) DEFAULT NULL,
  `phone` varchar(255) NOT NULL,
  `password` varchar(45) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `phone_UNIQUE` (`phone`),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=22 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `emp`
--

LOCK TABLES `emp` WRITE;
/*!40000 ALTER TABLE `emp` DISABLE KEYS */;
INSERT INTO `emp` VALUES (1,'root','root','2005-04-20','root','root','root'),(2,'admin','admin','2002-04-05','Администратор','admin','admin'),(11,'Продавец','Кассир','2000-03-03','Продавец-кассир','salesman','salesman'),(12,NULL,NULL,NULL,'Стажер','8883','pas3'),(14,'Имя','Фамилия','2000-04-04','Стажер','8884','pas4'),(15,'Тест','Тест','2027-02-20','Продавец-кассир','121212','121212'),(16,'Степан','Степанов','2001-03-20','Стажер','89274463111','pasword1'),(17,'Norm','Name','2023-03-28','Продавец-кассир','test1','pt1'),(18,'Николай','Егоров','2000-01-01','Продавец-кассир','89978888888','qwe123'),(19,'Ник','Ник','2016-03-20','Стажер','8927446341812','pas3'),(21,'Разиль','Разиль','2010-10-20','Администратор','89274463438','razil228');
/*!40000 ALTER TABLE `emp` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `n_type`
--

DROP TABLE IF EXISTS `n_type`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `n_type` (
  `id` bigint unsigned NOT NULL AUTO_INCREMENT,
  `title` varchar(255) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id` (`id`),
  UNIQUE KEY `title_UNIQUE` (`title`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `n_type`
--

LOCK TABLES `n_type` WRITE;
/*!40000 ALTER TABLE `n_type` DISABLE KEYS */;
INSERT INTO `n_type` VALUES (2,'игрушка'),(9,'канцтовары '),(8,'молочная продукция'),(7,'мясное'),(3,'одежда'),(1,'посуда');
/*!40000 ALTER TABLE `n_type` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `nom`
--

DROP TABLE IF EXISTS `nom`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `nom` (
  `id` bigint unsigned NOT NULL AUTO_INCREMENT,
  `type_id` bigint unsigned DEFAULT NULL,
  `title` varchar(255) NOT NULL,
  `shelf_life` int DEFAULT NULL,
  `price` int DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id` (`id`),
  UNIQUE KEY `title_UNIQUE` (`title`),
  KEY `type_ind` (`type_id`),
  CONSTRAINT `nom_ibfk_1` FOREIGN KEY (`type_id`) REFERENCES `n_type` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `nom`
--

LOCK TABLES `nom` WRITE;
/*!40000 ALTER TABLE `nom` DISABLE KEYS */;
INSERT INTO `nom` VALUES (1,1,'Молоко 1л',20,140),(4,8,'Творог 100г',5,160),(5,3,'Свитер шелк',0,2500),(6,2,'Трансформер',0,500),(7,7,'Треска',10,600),(8,9,'Ручка гелевая',0,6);
/*!40000 ALTER TABLE `nom` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `nom_stock`
--

DROP TABLE IF EXISTS `nom_stock`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `nom_stock` (
  `id` bigint NOT NULL AUTO_INCREMENT,
  `stock_id` bigint unsigned DEFAULT NULL,
  `nom_id` bigint unsigned DEFAULT NULL,
  `quantity` int NOT NULL,
  `depart` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `nom_ibfk_2` (`nom_id`),
  KEY `stock_idfk_idx` (`stock_id`),
  CONSTRAINT `nom_ibfk_2` FOREIGN KEY (`nom_id`) REFERENCES `nom` (`id`),
  CONSTRAINT `stock_idfk` FOREIGN KEY (`stock_id`) REFERENCES `stock` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `nom_stock`
--

LOCK TABLES `nom_stock` WRITE;
/*!40000 ALTER TABLE `nom_stock` DISABLE KEYS */;
INSERT INTO `nom_stock` VALUES (11,7,6,6,'Товары для животных'),(14,2,5,1111,'Бакалея'),(15,2,7,13,'МясоРыба'),(16,14,8,1000,'Прочее');
/*!40000 ALTER TABLE `nom_stock` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `stock`
--

DROP TABLE IF EXISTS `stock`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `stock` (
  `id` bigint unsigned NOT NULL AUTO_INCREMENT,
  `title` varchar(255) NOT NULL,
  `address` varchar(255) DEFAULT NULL,
  `description` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `idstock` (`id`),
  UNIQUE KEY `title_UNIQUE` (`title`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `stock`
--

LOCK TABLES `stock` WRITE;
/*!40000 ALTER TABLE `stock` DISABLE KEYS */;
INSERT INTO `stock` VALUES (1,'Сухой склад','проспект Чулман, 40, Набережные Челны, РТ','Относительная влажность в помещении должна составлять от 50 до 70%, температура – от +5 до +18'),(2,'Тестовый склад','улица Пушкина, дом Колотушкина','Обычный склад '),(7,'Большой склад','Бункер в центре города','100 на 100 метров'),(14,'Морозильная камера','Комсомольская Набережная ул., 34А Набережные Челны РТ','продукты хранятся при −18°С, а −24°С требуется только для режима быстрой заморозки.');
/*!40000 ALTER TABLE `stock` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `supply`
--

DROP TABLE IF EXISTS `supply`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `supply` (
  `id` bigint unsigned NOT NULL AUTO_INCREMENT,
  `nom_id` bigint unsigned DEFAULT NULL,
  `stock_id` bigint unsigned DEFAULT NULL,
  `emp_id` bigint unsigned DEFAULT NULL,
  `quantity` int NOT NULL,
  `delivery` date DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id` (`id`),
  KEY `nom_id` (`nom_id`),
  KEY `stock_id` (`stock_id`),
  KEY `emp_id` (`emp_id`),
  CONSTRAINT `supply_ibfk_1` FOREIGN KEY (`nom_id`) REFERENCES `nom` (`id`),
  CONSTRAINT `supply_ibfk_2` FOREIGN KEY (`stock_id`) REFERENCES `stock` (`id`),
  CONSTRAINT `supply_ibfk_3` FOREIGN KEY (`emp_id`) REFERENCES `emp` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=36 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `supply`
--

LOCK TABLES `supply` WRITE;
/*!40000 ALTER TABLE `supply` DISABLE KEYS */;
INSERT INTO `supply` VALUES (30,6,7,1,6,'2022-10-10'),(31,7,1,2,6,'2022-10-10'),(32,6,7,2,6,'2022-02-20'),(33,5,2,18,1111,'2023-10-10'),(34,7,2,18,13,'2023-11-11'),(35,8,14,21,1000,'2023-03-27');
/*!40000 ALTER TABLE `supply` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-03-27 19:08:22
