INSERT INTO Municipalities
    (IbgeCode, Name, StateId)
SELECT DISTINCT
    r.co_municipio_ibge,
    r.no_municipio,
    s.Id
FROM BnafarRaw r
INNER JOIN States s ON s.Uf = r.sg_uf;