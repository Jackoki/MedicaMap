INSERT INTO Stocks
(
    EstablishmentId,
    MedicationId,
    StockDate,
    Quantity
)
SELECT
    e.Id,
    m.Id,
    STR_TO_DATE(r.dt_posicao_estoque, '%Y/%m/%d'),
    SUM(
        CAST(
            NULLIF(r.qt_estoque, '')
            AS DECIMAL(18,3)
        )
    ) AS Quantity
FROM BnafarRaw r

INNER JOIN Establishments e
    ON e.CnesCode = r.co_cnes

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