INSERT INTO Municipalities
    (IbgeCode, Name, State)
SELECT DISTINCT
    co_municipio_ibge,
    no_municipio,
    sg_uf
FROM BnafarRaw;