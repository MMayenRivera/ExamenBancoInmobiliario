CREATE TABLE Cliente(
		NumeroCliente int PRIMARY KEY IDENTITY,
		ApellidoPaterno VARCHAR(30),
		ApellidoMaterno VARCHAR(30),
		Nombre VARCHAR(30),
		FechaNacimiento DATE,
		RFC VARCHAR(20)
)


CREATE TABLE Credito(
		NumeroCredito int PRIMARY KEY,
		ImporteCredito decimal,
		Plazo int,
		TasaAnual decimal,
		NumeroCliente int REFERENCES Cliente(NumeroCliente)
)


CREATE TABLE Amortizacion(
		NumeroPago int PRIMARY KEY IDENTITY,
		NumeroCredito INT REFERENCES Credito(NumeroCredito),
		PagoMensual decimal,
		CapitalAmortizado decimal,
		InteresPeriodo decimal,
		SaldoInsoluto decimal
)



CREATE PROCEDURE ClienteGetAll
		AS

			SELECT Cliente.NumeroCliente,Nombre,ApellidoPaterno ,ApellidoMaterno,FechaNacimiento,RFC,amortizacion.NumeroCredito,
			Amortizacion.CapitalAmortizado,Amortizacion.InteresPeriodo,Amortizacion.NumeroPago,Amortizacion.PagoMensual,
			Amortizacion.SaldoInsoluto 
			FROM Cliente
			INNER JOIN Credito ON Cliente.NumeroCliente = Credito.NumeroCliente
			INNER JOIN Amortizacion ON Credito.NumeroCredito = Credito.NumeroCredito



CREATE PROCEDURE ClienteAdd 'Juan','Perez','Martinez','1999/08/22',1,30000,12,0.05,2561,29888,112,113,'LOMJ990420'
		@Nombre VARCHAR (30),
		@ApellidoPaterno VARCHAR(30),
		@ApellidoMaterno VARCHAR(30),
		@FechaNacimiento VARCHAR(15),
		@NumeroCredito INT,
		@ImporteCredito decimal,
		@Plazo int,
		@TasaAnual decimal,
		@PagoMensual decimal,
		@CapitalAmortizado decimal,
		@InteresPeriodo decimal,
		@SaldoInsoluto decimal,
		@RFC VARCHAR(20)
		AS

		BEGIN TRANSACTION
				
				BEGIN TRY

					INSERT INTO Cliente 
						VALUES (@ApellidoPaterno,@ApellidoMaterno,@Nombre,CONVERT(VARCHAR,@FechaNacimiento),@RFC)


					INSERT INTO Credito
						VALUES (@NumeroCredito,@ImporteCredito,@Plazo,@TasaAnual,@@IDENTITY)

					
					INSERT INTO Amortizacion
						VALUES (@NumeroCredito,@PagoMensual,@CapitalAmortizado,@InteresPeriodo,@SaldoInsoluto)

						COMMIT

				END TRY

				BEGIN CATCH

						ROLLBACK 
						SELECT ERROR_MESSAGE()

				END CATCH
				

CREATE PROCEDURE ClienteDelete
		@NumeroCliente INT,
		@NumeroCredito INT
		AS
		    DELETE FROM Amortizacion
			WHERE NumeroCredito = @NumeroCredito

			DELETE FROM Credito 
			WHERE NumeroCredito = @NumeroCredito

			DELETE FROM Cliente
			WHERE NumeroCliente = @NumeroCliente



				 
CREATE PROCEDURE ClienteGetById 
		@NumeroCredito int
		AS

			SELECT NumeroPago,Amortizacion.NumeroCredito AS 'Numero Credito',PagoMensual,CapitalAmortizado,InteresPeriodo,SaldoInsoluto,
			ImporteCredito,Plazo,TasaAnual,Cliente.NumeroCliente,ApellidoPaterno,ApellidoMaterno,Nombre,FechaNacimiento,RFC
			FROM Amortizacion
			INNER JOIN Credito ON Amortizacion.NumeroCredito = Credito.NumeroCredito
			INNER JOIN Cliente ON Credito.NumeroCliente = Cliente.NumeroCliente
			WHERE Amortizacion.NumeroCredito = @NumeroCredito
		

		