-- phpMyAdmin SQL Dump
-- version 4.8.2
-- https://www.phpmyadmin.net/
--
-- Host: localhost
-- Generation Time: 18-Jul-2018 às 20:38
-- Versão do servidor: 8.0.11
-- PHP Version: 7.2.8

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `boa_agenda`
--

DELIMITER $$
--
-- Procedures
--
CREATE DEFINER=`root`@`localhost` PROCEDURE `pr_in_user` (IN `_nome` VARCHAR(50), IN `_sobrenome` VARCHAR(50), IN `_cep` VARCHAR(15), IN `_telefone` VARCHAR(11), IN `_celular` VARCHAR(12), IN `_numero` VARCHAR(4), IN `_email` VARCHAR(50), IN `_login` VARCHAR(8), IN `_senha` VARCHAR(8), IN `_endereco` VARCHAR(70), IN `_cpf` VARCHAR(12), IN `_rg` VARCHAR(12), IN `_tipo` INT)  begin
DECLARE EXIT HANDLER for SQLEXCEPTION
begin
	 SELECT 'erro desconhecido';
end;

	IF(!EXISTS(SELECT (1) from `usuarios` where cpf = _cpf OR rg = _rg)) THEN    
        INSERT INTO `usuarios`(      
            `nome`
            ,`sobrenome`
            ,`cep`
            ,`telefone`
            ,`celular`
            ,`numero`
            ,`email`
            ,`login`
            ,`senha`
            ,`endereco`
            ,`cpf`
            ,`rg`
        	,`tipo`) 
            VALUES (
            `_nome`
            ,`_sobrenome`
            ,`_cep`
            ,`_telefone`
            ,`_celular`
            ,`_numero`
            ,`_email`
            ,`_login`
            ,`_senha`
            ,`_endereco`
            ,`_cpf`
            ,`_rg`
            ,`_tipo`
            );
            SELECT 'Cadastro realizado com sucesso';
        ELSE 
        	select 'cpf ou rg ja cadastrados';
		end if;
end$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `pr_up_user` (IN `_id` INT, IN `_nome` VARCHAR(50), IN `_sobrenome` VARCHAR(50), IN `_cep` VARCHAR(15), IN `_telefone` VARCHAR(11), IN `_celular` VARCHAR(12), IN `_numero` VARCHAR(4), IN `_email` VARCHAR(50), IN `_login` VARCHAR(8), IN `_senha` VARCHAR(8), IN `_endereco` VARCHAR(70), IN `_cpf` VARCHAR(12), IN `_rg` VARCHAR(12), IN `_tipo` INT)  BEGIN
  DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        SELECT 'erro desconhecido';
    END;  
IF(!EXISTS(SELECT (1) FROM `usuarios` WHERE cpf = _cpf and id_usuario = _id) OR
     !EXISTS(SELECT(1) FROM `usuarios` WHERE rg = _rg and id_usuario = _id)) THEN
UPDATE
    `usuarios`
SET
    `nome` = `_nome`,
    `sobrenome` = `_sobrenome`,
    `cep` = `_cep`,
    `telefone` = `_telefone`,
    `celular` = `_celular`,
    `numero` = `_numero`,
    `email` = `_email`,
    `login` = `_login`,
    `senha` = `_senha`,
    `endereco` = `_endereco`,
    `cpf` = `_cpf`,
    `rg` = `_rg`,
    `tipo` = `_tipo`
WHERE
    `id_usuario` = `_id`;
SELECT
    'Dados atualizado com sucesso'; ELSE
SELECT
    'cpf ou rg ja cadastrados';
END IF;
END$$

DELIMITER ;

-- --------------------------------------------------------

--
-- Estrutura da tabela `consultas`
--

CREATE TABLE `consultas` (
  `id_consulta` int(11) NOT NULL,
  `hora` time DEFAULT NULL,
  `dia` date DEFAULT NULL,
  `usuario` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- --------------------------------------------------------

--
-- Estrutura da tabela `usuarios`
--

CREATE TABLE `usuarios` (
  `id_usuario` int(11) NOT NULL,
  `nome` varchar(50) DEFAULT NULL,
  `sobrenome` varchar(50) DEFAULT NULL,
  `cep` varchar(15) NOT NULL,
  `telefone` varchar(11) DEFAULT NULL,
  `celular` varchar(12) DEFAULT NULL,
  `numero` varchar(4) DEFAULT NULL,
  `email` varchar(50) NOT NULL,
  `login` varchar(8) NOT NULL,
  `senha` varchar(8) NOT NULL,
  `endereco` varchar(70) DEFAULT NULL,
  `cpf` varchar(12) DEFAULT NULL,
  `rg` varchar(12) DEFAULT NULL,
  `tipo` int(11) NOT NULL DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Extraindo dados da tabela `usuarios`
--

INSERT INTO `usuarios` (`id_usuario`, `nome`, `sobrenome`, `cep`, `telefone`, `celular`, `numero`, `email`, `login`, `senha`, `endereco`, `cpf`, `rg`, `tipo`) VALUES
(1, '', '', '', '', '', '', '', '', '', '', '', '', 0),
(2, 'lucas', 'melo', '351', '15', '15', '165', '151', '151', '15', '1651', '14561', '1565165', 0),
(3, 'lucas', '161', '', '', '', '', '', '', '23', '', '1459', '156519', 0);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `consultas`
--
ALTER TABLE `consultas`
  ADD PRIMARY KEY (`id_consulta`),
  ADD KEY `FK_user` (`usuario`);

--
-- Indexes for table `usuarios`
--
ALTER TABLE `usuarios`
  ADD PRIMARY KEY (`id_usuario`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `consultas`
--
ALTER TABLE `consultas`
  MODIFY `id_consulta` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `usuarios`
--
ALTER TABLE `usuarios`
  MODIFY `id_usuario` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- Constraints for dumped tables
--

--
-- Limitadores para a tabela `consultas`
--
ALTER TABLE `consultas`
  ADD CONSTRAINT `FK_user` FOREIGN KEY (`usuario`) REFERENCES `usuarios` (`id_usuario`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
