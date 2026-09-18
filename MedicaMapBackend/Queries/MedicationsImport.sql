INSERT INTO Medications
(
    CatmatCode,
    Description,
    ProductType
)
SELECT
    r.co_catmat,
    MAX(r.ds_produto),
    MAX(r.tp_produto)
FROM BnafarRaw r
WHERE r.co_catmat IS NOT NULL
  AND r.co_catmat <> ''
GROUP BY
    r.co_catmat;