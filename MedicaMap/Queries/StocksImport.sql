INSERT INTO Stocks
(
    EstablishmentId,
    MedicationId,
    StockDate,
    Quantity
)
SELECT
    e.Id AS EstablishmentId,
    m.Id AS MedicationId,
    STR_TO_DATE(r.dt_posicao_estoque, '%Y/%m/%d') AS StockDate,
    SUM(
        CAST(
            NULLIF(r.qt_estoque, '') AS DECIMAL(18,3)
        )
    ) AS Quantity
FROM BnafarRaw r

INNER JOIN Municipalities mun
    ON mun.IbgeCode = r.co_municipio_ibge

INNER JOIN Establishments e
    ON e.CnesCode = r.co_cnes
    AND e.MunicipalityId = mun.Id

INNER JOIN Medications m
    ON m.CatmatCode = r.co_catmat

WHERE r.co_cnes IS NOT NULL
  AND r.co_cnes <> ''
  AND r.co_cnes <> '0'

  AND r.co_catmat IS NOT NULL
  AND r.co_catmat <> ''

  AND r.dt_posicao_estoque IS NOT NULL
  AND r.dt_posicao_estoque <> ''

GROUP BY
    e.Id,
    m.Id,
    STR_TO_DATE(r.dt_posicao_estoque, '%Y/%m/%d');


ALTER TABLE Stocks
ADD CONSTRAINT UQ_Stocks_Establishment_Medication_Date
UNIQUE (EstablishmentId, MedicationId, StockDate);