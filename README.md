CREATE TABLE Camere (
    Numero INT PRIMARY KEY,
    Descrizione NVARCHAR(100),
    Tipologia NVARCHAR(50) CHECK (Tipologia IN ('Singola', 'Doppia'))
);

CREATE TABLE Prenotazioni (
    ID INT PRIMARY KEY IDENTITY,
    CodiceFiscale NVARCHAR(16) NOT NULL,
    Cognome NVARCHAR(50) NOT NULL,
    Nome NVARCHAR(50) NOT NULL,
    Citta NVARCHAR(100),
    Provincia NVARCHAR(2),
    Email NVARCHAR(100),
    Telefono INT,
    Cellulare INT NOT NULL,
    NumeroCamera INT REFERENCES Camere(Numero),
    DataPrenotazione DATE,
    Anno INT,
    PeriodoInizio DATE,
    PeriodoFine DATE,
    Caparra DECIMAL(10, 2),
    Tariffa DECIMAL(10, 2),
    TipoSoggiorno NVARCHAR(MAX)
);

CREATE TABLE ServiziAggiuntivi (
    ID INT PRIMARY KEY IDENTITY,
    PrenotazioneID INT REFERENCES Prenotazioni(ID),
    Descrizione NVARCHAR(100),
    Quantita INT,
    Prezzo DECIMAL(10, 2),
    DataServizio DATE
);

CREATE TABLE Admin (
    ID INT PRIMARY KEY IDENTITY,
    Username NVARCHAR(50) UNIQUE,
    Password NVARCHAR(100) NOT NULL
);
