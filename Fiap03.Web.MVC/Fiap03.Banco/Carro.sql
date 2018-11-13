CREATE TABLE [dbo].[Carro]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [MarcaId] INT NOT NULL, 
	[Placa] CHAR(8) NOT NULL,
    [Ano] INT NULL, 
    [Esportivo] BIT NULL, 
    [Combustivel] INT NULL, 
    [Descricao] VARCHAR(150) NULL, 
    [Renavam] INT NOT NULL, 
    CONSTRAINT [FK_Carro_Documento] FOREIGN KEY ([Renavam]) REFERENCES [Documento]([Renavam]), 
    CONSTRAINT [FK_Carro_Marca] FOREIGN KEY ([MarcaId]) REFERENCES [Marca]([Id])
)
