drop TRIGGER set_cat;
drop TRIGGER set_pro;
drop TRIGGER set_ven;
drop TRIGGER set_cli;
drop TRIGGER set_end;
drop TRIGGER set_usu;
drop TRIGGER set_item;
drop TRIGGER set_for;



DROP TABLE pedido;
DROP TABLE ocorencia;
drop table papel_func;
drop table funcionalidade;
drop table perfil;
DROP TABLE usuarios;
DROP TABLE papel; 
DROP TABLE item_venda;
DROP TABLE vendas;
DROP TABLE formato_produto;
DROP TABLE produto;
DROP TABLE categoria;
DROP TABLE clientes;
DROP TABLE endereco;

DROP SEQUENCE cat;
DROP SEQUENCE cli;
DROP SEQUENCE pro;
drop SEQUENCE ven;
drop SEQUENCE ende;
drop SEQUENCE usu;
drop SEQUENCE itm;
drop SEQUENCE formato;

CREATE SEQUENCE cat START WITH 1;
CREATE SEQUENCE cli START WITH 1;
CREATE SEQUENCE pro START WITH 1;
CREATE SEQUENCE itm START WITH 1;
CREATE SEQUENCE ven START WITH 1;
CREATE SEQUENCE ende START WITH 1;
CREATE SEQUENCE usu START WITH 1;
CREATE SEQUENCE formato START WITH 1;


CREATE TABLE categoria
  (
    id_cat      NUMBER(8,0)  PRIMARY KEY,   
    nome_cat VARCHAR(50),
    des_cat  VARCHAR(1000),
    CONSTRAINT cat_nn_010 CHECK (id_cat IS NOT NULL)
    
  );

CREATE TABLE produto
  (
    id_pro        NUMBER(10,0) PRIMARY KEY ,
    id_cat        NUMBER(8,0) ,
    preco      NUMBER(8,2),
    cod_barras CHAR(13),
    fab        VARCHAR(60),
    nome_pro   VARCHAR(50),
    des_pro    VARCHAR(1000),
    image BLOB,
    ext VARCHAR2(15),
    CONSTRAINT fk_cat FOREIGN KEY(id_cat) REFERENCES categoria(id_cat)  on delete cascade
  );

CREATE TABLE  formato_produto 
(
   id_for        NUMBER(10,0) PRIMARY KEY ,
   cod_formato   NUMBER(4),
   dimensoes     varchar2(30),
   peso          varchar2(15),
   comprimento   decimal,
   altura        decimal,
   largura       decimal,
   diametro      decimal
);
CREATE TABLE endereco
(
    id_end NUMBER(10) primary key,
    numero VARCHAR2(4),
    logradouro VARCHAR2(200),
    bairro   VARCHAR2(50),
    cidade   VARCHAR2(50),
    complemento VARCHAR2(35),
    cep      CHAR(8),
    uf       CHAR(2) 
);

CREATE TABLE clientes
(
    id_cli      NUMBER(8,0) PRIMARY KEY,
    id_user NUMBER(10) ,    
    nome_cli VARCHAR(50),
    sexo     CHAR(1),
    cpf      CHAR(11),
    rg       VARCHAR(9),
    dt_nas   DATE,
    email    VARCHAR(80),
    id_end NUMBER(10),
    CONSTRAINT fk_end FOREIGN KEY (id_end) REFERENCES endereco(id_end)on delete cascade,
    CONSTRAINT clientes_nn_02 CHECK (id_cli IS NOT NULL)
);

 
  
CREATE TABLE vendas
(
    id_ven     NUMBER(8,0) PRIMARY KEY,
    id_cli NUMBER(8,0),
    preco   NUMBER(10,2),
    id_end NUMBER(10),
    CONSTRAINT fk_end2 FOREIGN KEY (id_end) REFERENCES endereco(id_end),
    CONSTRAINT vendas_nn_02 CHECK (id_ven IS NOT NULL),
    CONSTRAINT fk_cli FOREIGN KEY(id_cli) REFERENCES clientes(id_cli)
);

CREATE TABLE item_venda
(
    id_item     NUMBER(8,0) PRIMARY KEY,
    id_pro NUMBER(10,0),
    qtd     NUMBER(8,0),
    preco NUMBER(10,2),
    id_ven     NUMBER(8,0),
    CONSTRAINT fk_ven FOREIGN KEY (id_ven) REFERENCES vendas(id_ven),
    CONSTRAINT fk_pro FOREIGN KEY(id_pro) REFERENCES produto(id_pro)    
); 

create table papel
(
id_papel int PRIMARY key,
descricao varchar2(30)
);

create table funcionalidade
(
id_func int PRIMARY key,
descricao varchar2(30)
);

create table papel_func
(
id_func int ,
id_papel int,
CONSTRAINT funcionalidade FOREIGN KEY(id_func) REFERENCES funcionalidade(id_func)on delete cascade,
CONSTRAINT fk_papel FOREIGN KEY(id_papel) REFERENCES papel(id_papel)on delete cascade,
CONSTRAINT pk_papel_func PRIMARY KEY(id_papel,id_func)
);

create table usuarios
(
id_user number(10) PRIMARY KEY, 
login varchar2(100) unique,
password_user varchar2(20)
);


create table perfil(
id_perfil NUMBER(13) PRIMARY KEY,
id_papel int,
id_user numeric(10),
CONSTRAINT usuario FOREIGN KEY(id_user) REFERENCES usuarios(id_user),
CONSTRAINT papel FOREIGN KEY(id_papel) REFERENCES papel(id_papel)
);

CREATE TABLE ocorencia
(
  id_oco      NUMBER(8,0)  PRIMARY KEY,   
  tipo_oco VARCHAR(50),
  des_oco  VARCHAR(1000),
  CONSTRAINT tipo_nn_01 CHECK (tipo_oco IS NOT NULL)
  
);
  
create table pedido(
id_ped NUMBER(13) PRIMARY KEY,
status VARCHAR2(70),
id_oco  NUMBER(8,0),
id_ven NUMBER(8,0),
CONSTRAINT cod_ocu FOREIGN KEY(id_oco) REFERENCES ocorencia(id_oco),
CONSTRAINT id_venda FOREIGN KEY(id_ven) REFERENCES vendas(id_ven)
);

CREATE OR REPLACE TRIGGER set_cat 
BEFORE INSERT ON categoria 
FOR EACH ROW
BEGIN
  SELECT cat.NEXTVAL
  INTO   :new.id_cat
  FROM   dual;
END set_cat;
/
CREATE OR REPLACE TRIGGER set_cli 
BEFORE INSERT ON Clientes
FOR EACH ROW
BEGIN
  SELECT CLI.NEXTVAL
  INTO   :new.id_cli
  FROM   dual;
END set_cli;
/
CREATE OR REPLACE TRIGGER set_ven 
BEFORE INSERT ON vendas
FOR EACH ROW
BEGIN
  SELECT VEN.NEXTVAL
  INTO   :new.id_ven
  FROM   dual;
END set_ven;
/
CREATE OR REPLACE TRIGGER set_pro 
BEFORE INSERT ON produto
FOR EACH ROW
BEGIN
  SELECT pro.NEXTVAL
  INTO   :new.id_pro
  FROM   dual;
END set_pro;
/
CREATE OR REPLACE TRIGGER set_end 
BEFORE INSERT ON endereco
FOR EACH ROW
BEGIN
  SELECT ende.NEXTVAL
  INTO   :new.id_end
  FROM   dual;
END;
/
CREATE OR REPLACE TRIGGER set_usu 
BEFORE INSERT ON usuarios
FOR EACH ROW
BEGIN
  SELECT usu.NEXTVAL
  INTO   :new.id_user
  FROM   dual;
END;
/
CREATE OR REPLACE TRIGGER set_item 
BEFORE INSERT ON item_venda
FOR EACH ROW
BEGIN
  SELECT itm.NEXTVAL
  INTO   :new.id_item
  FROM   dual;
END;
/
CREATE OR REPLACE TRIGGER set_for 
BEFORE INSERT ON formato_produto
FOR EACH ROW
BEGIN
  SELECT formato.NEXTVAL
  INTO   :new.id_for
  FROM   dual;
END;
/