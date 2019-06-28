
CREATE TABLE categoria(
 cod NUMBER(8,0) PRIMARY KEY,
 nome_cat varchar(50),
 des_cat varchar(1000)
 CONSTRAINT nome_cat NOT NULL
 );

 CREATE TABLE produto(
 cod NUMBER(10,0) PRIMARY KEY,
 cat NUMBER(8,0) ,
 preco NUMBER(8,2),
 cod_barras char(13),
 fab varchar(60),
 nome_pro varchar(50),
 des_pro varchar(1000),
 CONSTRAINT fk_cat FOREIGN KEY(cat) REFERENCES  categoria(cod)
-- CONSTRAINT nome_pro NOT NULL,
 --CONSTRAINT preco NOT NULL
 );
  CREATE TABLE clientes(
 cod  NUMBER(8,0) PRIMARY KEY,
 nome_cli varchar(50),
 sexo char(1),
 cpf char(11),
 rg varchar(9),
 dt_nas date,
 endereco varchar(200),
 email varchar(80),
 bairro varchar(50),
 cidade varchar(50),
 cep char(8),
 uf char(2),
 CONSTRAINT clientes_nn_02 check (cod is NOT NULL)
 );
 CREATE TABLE vendas(
 cod NUMBER(8,0) PRIMARY KEY,
 cod_cli NUMBER(8,0),
 cod_pro NUMBER(10,0),
 qtd NUMBER(8,0),
 preco NUMBER(10,2),
 CONSTRAINT vendas_nn_01 check (cod_pro is NOT NULL),
 CONSTRAINT vendas_nn_02 check (cod_cli is NOT NULL),
 CONSTRAINT fk_pro FOREIGN KEY(cod_pro) REFERENCES  produto(cod),
 CONSTRAINT fk_cli FOREIGN KEY(cod_cli) REFERENCES  clientes(cod)
 );

 CREATE VIEW vendasview AS
SELECT vendas.cod   AS "código" ,
  CLIENTES.NOME_CLI AS "nome do cliente",
  PRODUTO.NOME_PRO  AS "nome do produto",
  qtd               AS "quantidade",
  vendas.preco      AS "preço"
FROM vendas
INNER JOIN CLIENTES
ON(vendas.cod_cli=CLIENTES.cod)
INNER JOIN PRODUTO
ON(vendas.cod_pro=PRODUTO.cod);
create view produtoview as 
select produto.cod, NOME_PRO,DES_PRO, NOME_CAT,COD_BARRAS,FAB,preco from produto left join CATEGORIA on(produto.cat=CATEGORIA.COD);