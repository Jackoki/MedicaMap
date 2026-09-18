CREATE TABLE BnafarRaw (
    Id BIGINT AUTO_INCREMENT PRIMARY KEY,

    sg_uf VARCHAR(2),
    co_municipio_ibge VARCHAR(20),
    no_municipio VARCHAR(150),

    co_cnes VARCHAR(20),
    no_razao_social VARCHAR(255),
    no_fantasia VARCHAR(255),

    co_cep VARCHAR(20),
    no_logradouro VARCHAR(255),
    nu_endereco VARCHAR(50),
    no_bairro VARCHAR(150),

    nu_telefone VARCHAR(50),

    nu_latitude VARCHAR(50),
    nu_longitude VARCHAR(50),

    no_email VARCHAR(255),

    dt_posicao_estoque VARCHAR(30),

    co_catmat VARCHAR(50),
    ds_produto TEXT,

    qt_estoque VARCHAR(50),

    nu_lote VARCHAR(100),
    dt_validade VARCHAR(30),

    tp_produto VARCHAR(10),

    sg_programa_saude VARCHAR(50),
    ds_programa_saude VARCHAR(255),

    sg_origem VARCHAR(50)
);