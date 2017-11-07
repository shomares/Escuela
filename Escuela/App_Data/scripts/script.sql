CREATE SCHEMA  escuela ;
-- -----------------------------------------------------
-- Table `escuela`.`secRol`
go
-- -----------------------------------------------------
CREATE TABLE escuela.secRol (
  [idsecRol] INT NOT NULL IDENTITY,
  [nombre] VARCHAR(45) NULL,
  [descripcion] VARCHAR(45) NULL,
  PRIMARY KEY ([idsecRol]))
;


-- -----------------------------------------------------
-- Table `escuela`.`secUser`
-- -----------------------------------------------------
CREATE TABLE escuela.secUser (
  [idsecUser] INT NOT NULL IDENTITY,
  [cveUser] VARCHAR(45) NOT NULL,
  [email] VARCHAR(45) NOT NULL,
  [nombre] VARCHAR(45) NOT NULL,
  [apellidoPaterno] VARCHAR(45) NOT NULL,
  [apellidoMaterno] VARCHAR(45) NOT NULL,
  [activo] SMALLINT NOT NULL,
  [secRol_idsecRol] INT NOT NULL,
  PRIMARY KEY ([idsecUser])
 ,
  CONSTRAINT [fk_secUser_secRol1]
    FOREIGN KEY ([secRol_idsecRol])
    REFERENCES escuela.secRol ([idsecRol])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
;

CREATE INDEX [fk_secUser_secRol1_idx] ON escuela.secUser ([secRol_idsecRol] ASC);


-- -----------------------------------------------------
-- Table `escuela`.`Sexo`
-- -----------------------------------------------------
CREATE TABLE escuela.Sexo (
  [idSexo] INT NOT NULL IDENTITY,
  [descripcion] VARCHAR(45) NOT NULL,
  PRIMARY KEY ([idSexo]))
;


-- -----------------------------------------------------
-- Table `escuela`.`Alumno`
-- -----------------------------------------------------
CREATE TABLE escuela.Alumno (
  [boleta] VARCHAR(10) NOT NULL,
  [nombre] VARCHAR(45) NOT NULL,
  [apellidoPaterno] VARCHAR(45) NOT NULL,
  [apellidoMaterno] VARCHAR(45) NOT NULL,
  [fechaNacimiento] DATE NOT NULL,
  [Sexo_idSexo] INT NOT NULL,
  PRIMARY KEY ([boleta])
 ,
  CONSTRAINT [fk_Alumno_Sexo]
    FOREIGN KEY ([Sexo_idSexo])
    REFERENCES escuela.Sexo ([idSexo])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
;

CREATE INDEX [fk_Alumno_Sexo_idx] ON escuela.Alumno ([Sexo_idSexo] ASC);


-- -----------------------------------------------------
-- Table `escuela`.`Profesor`
-- -----------------------------------------------------
CREATE TABLE escuela.Profesor (
  [RFC] VARCHAR(20) NOT NULL,
  [nombre] VARCHAR(45) NOT NULL,
  [apellidoPaterno] VARCHAR(45) NOT NULL,
  [apellidoMaterno] VARCHAR(45) NOT NULL,
  [fechaNacimiento] DATE NOT NULL,
  [Sexo_idSexo] INT NOT NULL,
  [cedula] VARCHAR(45) NOT NULL,
  PRIMARY KEY ([RFC])
 ,
  CONSTRAINT [fk_Profesor_Sexo1]
    FOREIGN KEY ([Sexo_idSexo])
    REFERENCES escuela.Sexo ([idSexo])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
;

CREATE INDEX [fk_Profesor_Sexo1_idx] ON escuela.Profesor ([Sexo_idSexo] ASC);


-- -----------------------------------------------------
-- Table `escuela`.`Materia`
-- -----------------------------------------------------
CREATE TABLE escuela.Materia (
  [idMateria] INT NOT NULL IDENTITY,
  [nombre] VARCHAR(45) NOT NULL,
  [creditos] FLOAT NOT NULL,
  PRIMARY KEY ([idMateria]))
;


-- -----------------------------------------------------
-- Table `escuela`.`MateriaProfesor`
-- -----------------------------------------------------
CREATE TABLE escuela.MateriaProfesor (
  [idMateriaProfesor] INT NOT NULL IDENTITY,
  [periodo] VARCHAR(45) NULL,
  [Profesor_RFC] VARCHAR(20) NOT NULL,
  [Materia_idMateria] INT NOT NULL,
  PRIMARY KEY ([idMateriaProfesor])
 ,
  CONSTRAINT [fk_MateriaProfesor_Profesor1]
    FOREIGN KEY ([Profesor_RFC])
    REFERENCES escuela.Profesor ([RFC])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT [fk_MateriaProfesor_Materia1]
    FOREIGN KEY ([Materia_idMateria])
    REFERENCES escuela.Materia ([idMateria])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
;

CREATE INDEX [fk_MateriaProfesor_Profesor1_idx] ON escuela.MateriaProfesor ([Profesor_RFC] ASC);
CREATE INDEX [fk_MateriaProfesor_Materia1_idx] ON escuela.MateriaProfesor ([Materia_idMateria] ASC);


-- -----------------------------------------------------
-- Table `escuela`.`AlumnoMateria`
-- -----------------------------------------------------
CREATE TABLE escuela.AlumnoMateria (
  [idAlumnoMateria] INT NOT NULL IDENTITY,
  [MateriaProfesor_idMateriaProfesor] INT NOT NULL,
  [Alumno_boleta] VARCHAR(10) NOT NULL,
  PRIMARY KEY ([idAlumnoMateria])
 ,
  CONSTRAINT [fk_AlumnoMateria_MateriaProfesor1]
    FOREIGN KEY ([MateriaProfesor_idMateriaProfesor])
    REFERENCES escuela.MateriaProfesor ([idMateriaProfesor])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT [fk_AlumnoMateria_Alumno1]
    FOREIGN KEY ([Alumno_boleta])
    REFERENCES escuela.Alumno ([boleta])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
;

CREATE INDEX [fk_AlumnoMateria_MateriaProfesor1_idx] ON escuela.AlumnoMateria ([MateriaProfesor_idMateriaProfesor] ASC);
CREATE INDEX [fk_AlumnoMateria_Alumno1_idx] ON escuela.AlumnoMateria ([Alumno_boleta] ASC);


-- -----------------------------------------------------
-- Table `escuela`.`Calificacion`
-- -----------------------------------------------------
CREATE TABLE escuela.Calificacion (
  [idCalificacion] INT NOT NULL IDENTITY,
  [Primer] FLOAT NULL,
  [Segunda] FLOAT NULL,
  [Tercera] FLOAT NULL,
  [AlumnoMateria_idAlumnoMateria] INT NOT NULL,
  PRIMARY KEY ([idCalificacion])
 ,
  CONSTRAINT [fk_Calificacion_AlumnoMateria1]
    FOREIGN KEY ([AlumnoMateria_idAlumnoMateria])
    REFERENCES escuela.AlumnoMateria ([idAlumnoMateria])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
 ;

CREATE INDEX [fk_Calificacion_AlumnoMateria1_idx] ON escuela.Calificacion ([AlumnoMateria_idAlumnoMateria] ASC);