USE medicamap;

CREATE TABLE States (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    IbgeCode VARCHAR(2) NOT NULL,
    Uf VARCHAR(2) NOT NULL,
    Name VARCHAR(100) NOT NULL,

    UNIQUE (IbgeCode),
    UNIQUE (Uf)
);

INSERT INTO States (IbgeCode, Uf, Name) VALUES
('11', 'RO', 'Rondônia'),
('12', 'AC', 'Acre'),
('13', 'AM', 'Amazonas'),
('14', 'RR', 'Roraima'),
('15', 'PA', 'Pará'),
('16', 'AP', 'Amapá'),
('17', 'TO', 'Tocantins'),
('21', 'MA', 'Maranhão'),
('22', 'PI', 'Piauí'),
('23', 'CE', 'Ceará'),
('24', 'RN', 'Rio Grande do Norte'),
('25', 'PB', 'Paraíba'),
('26', 'PE', 'Pernambuco'),
('27', 'AL', 'Alagoas'),
('28', 'SE', 'Sergipe'),
('29', 'BA', 'Bahia'),
('31', 'MG', 'Minas Gerais'),
('32', 'ES', 'Espírito Santo'),
('33', 'RJ', 'Rio de Janeiro'),
('35', 'SP', 'São Paulo'),
('41', 'PR', 'Paraná'),
('42', 'SC', 'Santa Catarina'),
('43', 'RS', 'Rio Grande do Sul'),
('50', 'MS', 'Mato Grosso do Sul'),
('51', 'MT', 'Mato Grosso'),
('52', 'GO', 'Goiás'),
('53', 'DF', 'Distrito Federal');


CREATE TABLE IF NOT EXISTS Municipalities (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    IbgeCode VARCHAR(20) NOT NULL,
    Name VARCHAR(150) NOT NULL,
    StateId INT NOT NULL,

    CONSTRAINT FK_Municipalities_States
        FOREIGN KEY (StateId)
        REFERENCES States(Id)
        ON DELETE RESTRICT
        ON UPDATE CASCADE
);

CREATE TABLE IF NOT EXISTS Establishments (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    CnesCode VARCHAR(20) NOT NULL,
    TradeName VARCHAR(255),
    Cep VARCHAR(20),
    Street VARCHAR(255),
    AddressNumber VARCHAR(50),
    Neighborhood VARCHAR(150),
    Phone VARCHAR(50),
    Email VARCHAR(255),
    MunicipalityId INT NOT NULL,

    CONSTRAINT FK_Establishments_Municipalities
        FOREIGN KEY (MunicipalityId)
        REFERENCES Municipalities(Id)
        ON DELETE RESTRICT
        ON UPDATE CASCADE
);

CREATE TABLE IF NOT EXISTS Medications (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    CatmatCode VARCHAR(50) NOT NULL,
    Description VARCHAR(500) NOT NULL,
    ProductType VARCHAR(10) NOT NULL
);

CREATE TABLE IF NOT EXISTS Stocks (
    Id BIGINT AUTO_INCREMENT PRIMARY KEY,
    EstablishmentId INT NOT NULL,
    MedicationId INT NOT NULL,
    StockDate DATE NOT NULL,
    Quantity DECIMAL(18,3) NOT NULL,

    CONSTRAINT FK_Stocks_Establishments
        FOREIGN KEY (EstablishmentId)
        REFERENCES Establishments(Id)
        ON DELETE RESTRICT
        ON UPDATE CASCADE,

    CONSTRAINT FK_Stocks_Medications
        FOREIGN KEY (MedicationId)
        REFERENCES Medications(Id)
        ON DELETE RESTRICT
        ON UPDATE CASCADE,

    CONSTRAINT UQ_Stocks_Establishment_Medication_Date
        UNIQUE (EstablishmentId, MedicationId, StockDate)
);