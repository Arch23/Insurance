-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------
-- -----------------------------------------------------
-- Schema tcs
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema tcs
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `tcs` DEFAULT CHARACTER SET utf8 ;
USE `tcs` ;

-- -----------------------------------------------------
-- Table `tcs`.`pessoa`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `tcs`.`pessoa` (
  `IdPessoa` INT(11) NOT NULL,
  `Nome` VARCHAR(45) NULL DEFAULT NULL,
  `Fone` VARCHAR(45) NULL DEFAULT NULL,
  `Endereco` VARCHAR(45) NULL DEFAULT NULL,
  `Bairro` VARCHAR(45) NULL DEFAULT NULL,
  `Cep` VARCHAR(15) NULL DEFAULT NULL,
  PRIMARY KEY (`IdPessoa`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `tcs`.`cliente`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `tcs`.`cliente` (
  `IdCliente` INT(11) NOT NULL,
  `Cpf` VARCHAR(11) NOT NULL,
  `Rg` VARCHAR(45) NULL DEFAULT NULL,
  `Sexo` VARCHAR(1) NULL DEFAULT NULL,
  `DataNascimento` DATE NULL DEFAULT NULL,
  PRIMARY KEY (`IdCliente`),
  CONSTRAINT `fk_Cliente_Pessoa`
    FOREIGN KEY (`IdCliente`)
    REFERENCES `tcs`.`pessoa` (`IdPessoa`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `tcs`.`marca`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `tcs`.`marca` (
  `IdMarca` INT(11) NOT NULL,
  `Descricao` VARCHAR(45) NULL DEFAULT NULL,
  PRIMARY KEY (`IdMarca`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `tcs`.`modelo`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `tcs`.`modelo` (
  `IdModelo` INT(11) NOT NULL,
  `IdMarca` INT(11) NOT NULL,
  `Descricao` VARCHAR(45) NULL DEFAULT NULL,
  PRIMARY KEY (`IdModelo`),
  INDEX `fk_Modelo_Marca1_idx` (`IdMarca` ASC) ,
  CONSTRAINT `fk_Modelo_Marca1`
    FOREIGN KEY (`IdMarca`)
    REFERENCES `tcs`.`marca` (`IdMarca`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `tcs`.`veiculo`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `tcs`.`veiculo` (
  `IdVeiculo` INT(11) NOT NULL,
  `IdModelo` INT(11) NOT NULL,
  `Placa` VARCHAR(45) NULL DEFAULT NULL,
  `Ano` INT(11) NULL DEFAULT NULL,
  PRIMARY KEY (`IdVeiculo`),
  INDEX `fk_Veiculo_Modelo1_idx` (`IdModelo` ASC) ,
  CONSTRAINT `fk_Veiculo_Modelo1`
    FOREIGN KEY (`IdModelo`)
    REFERENCES `tcs`.`modelo` (`IdModelo`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8
COMMENT = '			';


-- -----------------------------------------------------
-- Table `tcs`.`apolice`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `tcs`.`apolice` (
  `IdApolice` INT(11) NOT NULL,
  `IdCliente` INT(11) NOT NULL,
  `IdVeiculo` INT(11) NOT NULL,
  `NumeroApolice` VARCHAR(45) NULL DEFAULT NULL,
  `DataInicio` DATE NULL DEFAULT NULL,
  `DataFim` DATE NULL DEFAULT NULL,
  `Valor` DOUBLE NULL DEFAULT NULL,
  `Franquia` DOUBLE NULL DEFAULT NULL,
  `DataContrato` DATE NULL DEFAULT NULL,
  PRIMARY KEY (`IdApolice`),
  INDEX `fk_Apolice_Cliente1_idx` (`IdCliente` ASC) ,
  INDEX `fk_Apolice_Veiculo1_idx` (`IdVeiculo` ASC) ,
  CONSTRAINT `fk_Apolice_Cliente1`
    FOREIGN KEY (`IdCliente`)
    REFERENCES `tcs`.`cliente` (`IdCliente`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Apolice_Veiculo1`
    FOREIGN KEY (`IdVeiculo`)
    REFERENCES `tcs`.`veiculo` (`IdVeiculo`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `tcs`.`usuario`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `tcs`.`usuario` (
  `login` VARCHAR(45) NOT NULL,
  `senha` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`login`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
