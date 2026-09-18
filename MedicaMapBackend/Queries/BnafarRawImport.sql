LOAD DATA LOCAL INFILE 'C:/Planilha.csv'
INTO TABLE BnafarRaw
CHARACTER SET latin1
FIELDS TERMINATED BY ';'
OPTIONALLY ENCLOSED BY '"'
LINES TERMINATED BY '\r\n'
IGNORE 1 LINES
(
    sg_uf,
    co_municipio_ibge,
    no_municipio,
    co_cnes,
    no_razao_social,
    no_fantasia,
    co_cep,
    no_logradouro,
    nu_endereco,
    no_bairro,
    nu_telefone,
    nu_latitude,
    nu_longitude,
    no_email,
    dt_posicao_estoque,
    co_catmat,
    ds_produto,
    qt_estoque,
    nu_lote,
    dt_validade,
    tp_produto,
    sg_programa_saude,
    ds_programa_saude,
    sg_origem
);