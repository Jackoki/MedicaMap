INSERT INTO Establishments
(
    CnesCode,
    TradeName,
    Cep,
    Street,
    AddressNumber,
    Neighborhood,
    Phone,
    Email,
    MunicipalityId
)
SELECT
    r.co_cnes,
    MAX(r.no_fantasia),
    MAX(r.co_cep),
    MAX(r.no_logradouro),
    MAX(r.nu_endereco),
    MAX(r.no_bairro),
    MAX(r.nu_telefone),
    MAX(r.no_email),
    m.Id
FROM BnafarRaw r
INNER JOIN Municipalities m
    ON m.IbgeCode = r.co_municipio_ibge
WHERE r.co_cnes IS NOT NULL
  AND r.co_cnes <> ''
  AND r.co_cnes <> '0'
GROUP BY
    r.co_cnes,
    m.Id;