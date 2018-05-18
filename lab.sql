drop TRIGGER set_car;
drop TRIGGER set_cat;
drop TRIGGER set_liv;
drop TRIGGER set_ven;
drop TRIGGER set_cli;
drop TRIGGER set_end;
drop TRIGGER set_usu;
drop TRIGGER set_item;
drop TRIGGER set_for;

DROP TABLE estoque;
DROP TABLE ranking;
DROP TABLE pedido;
DROP TABLE ocorencia;
DROP TABLE item_venda;
DROP TABLE vendas;
DROP TABLE cat_liv;
DROP TABLE livro;
DROP TABLE formato_produto;
DROP TABLE g_preco;
DROP TABLE end_cli;
DROP TABLE car_cli;
DROP TABLE cartao_credito;
DROP TABLE bandeira;
DROP TABLE clientes;
DROP TABLE endereco;
DROP TABLE categoria;
drop table papel_func;
drop table funcionalidade;
drop table perfil;
DROP TABLE usuarios;
DROP TABLE papel; 
-- SELECT * FROM USER_CONSTRAINTS WHERE TABLE_NAME = 'CATEGORIA';
DROP SEQUENCE car;
DROP SEQUENCE cat;
DROP SEQUENCE cli;
DROP SEQUENCE liv;
drop SEQUENCE ven;
drop SEQUENCE ende;
drop SEQUENCE usu;
drop SEQUENCE itm;
drop SEQUENCE formato;

CREATE SEQUENCE car START WITH 1;
CREATE SEQUENCE cat START WITH 1;
CREATE SEQUENCE cli START WITH 1;
CREATE SEQUENCE liv START WITH 1;
CREATE SEQUENCE itm START WITH 1;
CREATE SEQUENCE ven START WITH 1;
CREATE SEQUENCE ende START WITH 1;
CREATE SEQUENCE usu START WITH 1;
CREATE SEQUENCE formato START WITH 1;

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
password_user varchar2(80)
);


create table perfil(
id_perfil NUMBER(13) PRIMARY KEY,
id_papel int,
id_user numeric(10),
CONSTRAINT usuario FOREIGN KEY(id_user) REFERENCES usuarios(id_user),
CONSTRAINT papel FOREIGN KEY(id_papel) REFERENCES papel(id_papel)
);

CREATE TABLE categoria
(
id_cat      NUMBER(8,0)  PRIMARY KEY,   
nome_cat VARCHAR(50),
des_cat  VARCHAR(1000),
ative char,
CONSTRAINT cat_nn_010 CHECK (id_cat IS NOT NULL)
);

CREATE TABLE g_preco(
id_g_pre NUMBER(10,0) primary key,
nome_g_preco VARCHAR(30),
porcentagem NUMBER(8,5)
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
CREATE TABLE livro
(
    id_liv    NUMBER(10,0) PRIMARY KEY,
    dim      varchar(20),
    ative    char,
    id_g_pre  NUMBER(10,0),
    n_pags  int,
    isbn  CHAR(13),
    edicao  VARCHAR(60),
    cod_bar VARCHAR(13),
    edi   VARCHAR(60),
    nome_liv VARCHAR(50),
    des_liv  VARCHAR(1000),
    image BLOB,
    ext VARCHAR2(15),
    id_for NUMBER(10,0),
    CONSTRAINT fk_formato FOREIGN KEY(id_for) REFERENCES formato_produto(id_for)  on delete cascade,
    CONSTRAINT fk_g_preco FOREIGN KEY(id_g_pre) REFERENCES g_preco(id_g_pre)  on delete cascade
);

CREATE TABLE cat_liv
  (
    id_cat      NUMBER(8,0),   
    id_liv   NUMBER(10,0),
    CONSTRAINT fk_cat_lig FOREIGN KEY(id_cat) REFERENCES categoria(id_cat)  on delete cascade,
    CONSTRAINT fk_liv_lig FOREIGN KEY(id_liv) REFERENCES livro(id_liv)  on delete cascade,
    constraint PK_D primary key (id_cat, id_liv)
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
CREATE TABLE bandeira
(
    id_band      NUMBER(9) PRIMARY KEY,
    nome_band VARCHAR(50)
);

CREATE TABLE cartao_credito
(
    id_car      NUMBER(10,0) PRIMARY KEY,
    numero CHAR(16),
    ccv NUMBER(9) ,    
    nome_car VARCHAR(50),
    validade  VARCHAR(9),
    id_band NUMBER(9),
    CONSTRAINT fk_bandeira FOREIGN KEY (id_band) REFERENCES bandeira(id_band)
);

CREATE TABLE clientes
(
    id_cli      NUMBER(8,0) PRIMARY KEY,
    ative     CHAR(1),
    id_user NUMBER(10) ,    
    nome_cli VARCHAR(50),
--    senha Varchar(30),
    sexo     CHAR(1),
    cpf      CHAR(11),
    rg       VARCHAR(9),
    dt_nas   DATE,
--    email    VARCHAR(80)
    CONSTRAINT fk_usu_cli FOREIGN KEY(id_user) REFERENCES usuarios(id_user)  on delete cascade
);

CREATE TABLE car_cli
(
    id_cli      NUMBER(8,0),   
    id_car   NUMBER(10,0),
    pref  CHAR(1),
    CONSTRAINT fk_car_cli FOREIGN KEY(id_cli) REFERENCES clientes(id_cli)  on delete cascade,
    CONSTRAINT fk_cli_car FOREIGN KEY(id_car) REFERENCES cartao_credito(id_car)  on delete cascade,
    constraint PK_CC primary key (id_cli, id_car)
); 

CREATE TABLE estoque
(
    id_est      NUMBER(8,0),   
    id_liv   NUMBER(10,0),
    qtd      number(10),
    custo    number(8,2),
    data_entrada Date,
    CONSTRAINT fk_est_liv FOREIGN KEY(id_liv) REFERENCES livro(id_liv)  on delete cascade,
    constraint PK_EST primary key (id_est)
);

CREATE TABLE end_cli
(
    id_cli      NUMBER(8,0),   
    id_end   NUMBER(10,0),
    tipo_end number(1),
    CONSTRAINT fk_cli_end FOREIGN KEY(id_cli) REFERENCES clientes(id_cli)  on delete cascade,
    CONSTRAINT fk_end_cli FOREIGN KEY(id_end) REFERENCES endereco(id_end)  on delete cascade,
    constraint PK_EC primary key (id_cli, id_end)
); 
CREATE TABLE ranking
(   
    id_cli NUMBER(8,0),
    g_preco char,
    mont NUMBER(16,0),
    CONSTRAINT fk_ran_cli FOREIGN KEY(id_cli) REFERENCES clientes(id_cli)  on delete cascade,
    constraint PK_RAN primary key (id_cli,g_preco, mont)
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
    id_liv NUMBER(10,0),
    qtd     NUMBER(8,0),
    preco NUMBER(10,2),
    id_ven     NUMBER(8,0),
    CONSTRAINT fk_ven FOREIGN KEY (id_ven) REFERENCES vendas(id_ven),
   CONSTRAINT fk_pro FOREIGN KEY(id_liv) REFERENCES livro(id_liv)    
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

CREATE OR REPLACE TRIGGER set_car 
BEFORE INSERT ON cartao_credito 
FOR EACH ROW
BEGIN
  SELECT car.NEXTVAL
  INTO   :new.id_car
  FROM   dual;
END set_cat;
/

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
CREATE OR REPLACE TRIGGER set_liv 
BEFORE INSERT ON livro
FOR EACH ROW
BEGIN
  SELECT liv.NEXTVAL
  INTO   :new.id_liv
  FROM   dual;
END set_liv;
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

insert into g_preco(id_g_pre,nome_g_preco,porcentagem) values(1,'medio',0.3);
insert into g_preco(id_g_pre,nome_g_preco,porcentagem) values(2,'baixo',0.2);
insert into bandeira(id_band,nome_band) values(1,'Mastercard');
insert into bandeira(id_band,nome_band) values(2,'Visa');
insert into bandeira(id_band,nome_band) values(3,'American Express');
--insert into livro ( ative, dim, g_preco, n_pags, isbn, edicao, cod_bar, edi, nome_liv, des_liv, image, ext) values ('A','vai','C',111,'asdf','asdf');
insert into categoria (des_cat, nome_cat,ative, id_cat) values ('','Aventura','A',1);
insert into categoria (des_cat, nome_cat,ative, id_cat) values ('','Ação','A',2);
insert into categoria (des_cat, nome_cat,ative, id_cat) values ('','Comédia','A',3);
insert into categoria (des_cat, nome_cat,ative, id_cat ) values ('','Romance','A',4);
insert into categoria (des_cat, nome_cat,ative, id_cat) values ('','Terror','A',5);

insert into livro(ative,nome_liv,n_pags,id_g_pre) values ('A','A Cabana',80,1);
insert into estoque(id_est,id_liv,qtd,custo,DATA_ENTRADA) values (1,1,10,20,current_date);
insert into estoque(id_est,id_liv,qtd,custo,DATA_ENTRADA) values (1,1,10,20,current_date);
select * from cat_liv join categoria using (id_cat) where id_liv=5;
SELECT * FROM livro WHERE ative!='I' AND id_liv=5;
insert into cat_liv(id_liv,id_cat) values (1,1);
select * from cat_liv join categoria using (id_cat) join LIVRO using (id_liv) where id_liv=5;