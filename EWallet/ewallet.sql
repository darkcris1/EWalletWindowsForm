-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Mar 18, 2024 at 05:15 PM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `ewallet`
--

-- --------------------------------------------------------

--
-- Table structure for table `transaction`
--

CREATE TABLE `transaction` (
  `id` int(11) NOT NULL,
  `type` int(13) NOT NULL,
  `amount` double NOT NULL,
  `created_at` timestamp NOT NULL DEFAULT current_timestamp(),
  `updated_at` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  `user` int(13) NOT NULL,
  `reference_number` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `transaction`
--

INSERT INTO `transaction` (`id`, `type`, `amount`, `created_at`, `updated_at`, `user`, `reference_number`) VALUES
(11, 2, 12312, '2024-03-17 16:12:02', '2024-03-17 16:32:45', 4, 'REF-123456'),
(13, 1, 123, '2024-03-17 16:33:10', '2024-03-17 16:33:10', 4, 'IqigEeoP'),
(14, 2, 123, '2024-03-17 16:33:10', '2024-03-17 16:33:10', 3, 'IqigEeoP'),
(15, 3, 100, '2024-03-17 16:43:51', '2024-03-17 16:43:51', 3, 'hCRy5DX'),
(16, 3, 100, '2024-03-17 16:43:58', '2024-03-17 16:43:58', 3, '2GXUc3M'),
(17, 3, 100, '2024-03-17 16:46:48', '2024-03-17 16:46:48', 3, 'bNzJ0U7'),
(18, 3, 100, '2024-03-17 16:47:13', '2024-03-17 16:47:13', 3, 'S5BCUif'),
(19, 1, 123, '2024-03-17 16:50:19', '2024-03-17 16:50:19', 3, '2kU1W1TO'),
(20, 2, 123, '2024-03-17 16:50:19', '2024-03-17 16:50:19', 6, '2kU1W1TO'),
(21, 3, 100, '2024-03-17 17:11:05', '2024-03-17 17:11:05', 7, 'KSPTgVt'),
(22, 3, 100, '2024-03-17 17:11:14', '2024-03-17 17:11:14', 7, 'z73NMFD'),
(23, 3, 0.1, '2024-03-18 15:44:42', '2024-03-18 15:44:42', 3, 'NHD2ETX'),
(24, 4, 0.1, '2024-03-18 15:47:50', '2024-03-18 15:47:50', 3, 'o7BGGlY'),
(25, 4, 23, '2024-03-18 15:49:03', '2024-03-18 15:49:03', 3, 'vAm9g07'),
(26, 3, 0.1, '2024-03-18 15:54:35', '2024-03-18 15:54:35', 9, 'ClNXtnI'),
(27, 3, 0.1, '2024-03-18 15:58:00', '2024-03-18 15:58:00', 9, 'IHulsv1');

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `username` varchar(30) NOT NULL,
  `password` varchar(255) NOT NULL,
  `full_name` varchar(255) NOT NULL,
  `id` int(11) NOT NULL,
  `is_admin` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`username`, `password`, `full_name`, `id`, `is_admin`) VALUES
('cris', '12345', '12345', 3, 0),
('cris2', '12345', 'Cris Fasd', 4, 0),
('cris3', '12345', 'cris fa', 5, 0),
('cris4', '12345', 'cris4 f', 6, 0),
('cris5', '12345', 'Cris F.', 7, 0),
('', '', '', 8, 0),
('123', '12345', 'asd ', 9, 0);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `transaction`
--
ALTER TABLE `transaction`
  ADD PRIMARY KEY (`id`),
  ADD KEY `user` (`user`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `transaction`
--
ALTER TABLE `transaction`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=28;

--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `transaction`
--
ALTER TABLE `transaction`
  ADD CONSTRAINT `transaction_ibfk_1` FOREIGN KEY (`user`) REFERENCES `users` (`id`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
