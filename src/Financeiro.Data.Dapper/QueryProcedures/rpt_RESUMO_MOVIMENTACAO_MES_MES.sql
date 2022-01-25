
CREATE PROCEDURE [dbo].[rpt_RESUMO_MOVIMENTACAO_MES_MES]
	@TipoMovimento int,
	@DataInicial AS VARCHAR(10),
	@DataFinal AS VARCHAR(10),
	@ContaId AS VARCHAR(100),
	@PessoaId AS VARCHAR(100),
	@CentroCustoId AS VARCHAR(100)
AS

	SET DATEFORMAT DMY;

	DECLARE @tblTipos AS TABLE(tipo VARCHAR(1))
	IF @TipoMovimento = 3 BEGIN
		INSERT INTO @tblTipos VALUES (1), (2)
	END ELSE BEGIN
		INSERT INTO @tblTipos VALUES (@TipoMovimento)
	END

	SELECT	M.Id AS MovimentoId,
			M.PessoaId AS PessoaId,
			P.Nome AS NomePessoa,
			CAST(DATEPART(YEAR, M.dataVencimento) AS varchar(4)) + 
			CAST(DATEPART(MONTH, M.dataVencimento) AS varchar(2)) AS AnoMes,
			DATEPART(YEAR, M.dataVencimento) AS Ano,
			DATEPART(MONTH, M.dataVencimento) AS Mes,
			DATENAME(MONTH, M.dataVencimento) AS NomeMes,
			LTRIM(RTRIM(UPPER(C.Nome))) AS NomeCentroCusto,
			LTRIM(RTRIM(UPPER(F.Nome))) AS NomeFornecedor,
			LTRIM(RTRIM(UPPER(M.Descricao))) AS DescricaoMovimento,
			CASE WHEN M.tipoMovimento = 1 THEN M.valorMovimento 
			     ELSE M.valorMovimento * (-1) END AS ValorMovimento,
			ISNULL(PC.ValorMensal, 0) * (-1) ValorMensal
	FROM Movimentos M
	LEFT JOIN CentroCusto C ON M.CentroCustoId = C.Id
	LEFT JOIN Fornecedores F ON M.FornecedorId = F.Id
	LEFT JOIN Pessoas P ON M.PessoaId = P.Id
	LEFT JOIN PessoaCentroCusto PC ON M.PessoaId = PC.PessoaId AND M.CentroCustoId = PC.CentroCustoId
	WHERE	M.dataVencimento >= @DataInicial 
		AND M.dataVencimento <= @DataFinal + ' 23:59:59'
		AND (M.ContaId = @ContaId OR @ContaId = '')
		AND (M.PessoaId = @PessoaId OR @PessoaId = '')
		AND (SELECT COUNT(*) FROM ItemMovimento I WHERE I.MovimentoId = M.Id) = 0
		AND M.tipoMovimento IN (SELECT tipo FROM @tblTipos)
		AND (M.CentroCustoId = @CentroCustoId OR @CentroCustoId = '')

	UNION ALL

	SELECT	II.Id AS IdMovimento,
			P.Id AS IdPessoa,
			P.Nome AS NomePessoa,
			CAST(DATEPART(YEAR, M.dataVencimento) AS varchar(4)) + 
			CAST(DATEPART(MONTH, M.dataVencimento) AS varchar(2)) AS AnoMes,
			DATEPART(YEAR, M.dataVencimento) AS Ano,
			DATEPART(MONTH, M.dataVencimento) AS Mes,
			DATENAME(MONTH, M.dataVencimento) AS NomeMes,
			LTRIM(RTRIM(UPPER(C.Nome))) AS NomeCentroCusto,
			LTRIM(RTRIM(UPPER(F.Nome))) AS NomeFornecedor,
			LTRIM(RTRIM(UPPER(II.historico))) AS DescricaoMovimento, 
			SUM(CASE WHEN M.tipoMovimento = 1 THEN II.valor 
			    ELSE II.valor * (-1) END) AS ValorMovimento,
			ISNULL(PC.ValorMensal, 0) * (-1) ValorMensal
	FROM Movimentos M
	JOIN ItemMovimento II ON M.Id = II.MovimentoId
	LEFT JOIN CentroCusto C ON II.CentroCustoId = C.Id
	LEFT JOIN Fornecedores F ON M.FornecedorId = F.Id
	LEFT JOIN Pessoas P ON II.PessoaId = P.Id
	LEFT JOIN PessoaCentroCusto PC ON II.PessoaId = PC.PessoaId AND II.CentroCustoId = PC.CentroCustoId

	WHERE	M.dataVencimento >= @DataInicial 
		AND M.dataVencimento <= @DataFinal + ' 23:59:59'
		AND (M.ContaId = @ContaId OR @ContaId = '')
		AND (II.PessoaId = @PessoaId OR @PessoaId = '')
		AND M.tipoMovimento IN (SELECT tipo FROM @tblTipos)
		AND (II.CentroCustoId = @CentroCustoId OR @CentroCustoId = '')

	GROUP BY II.Id,
			 P.Id,
			 P.Nome,
			 DATEPART(YEAR, M.dataVencimento),
			 DATEPART(MONTH, M.dataVencimento),
			 DATENAME(MONTH, M.dataVencimento),
			 LTRIM(RTRIM(UPPER(C.Nome))),
			 LTRIM(RTRIM(UPPER(F.Nome))),
			 LTRIM(RTRIM(UPPER(II.historico))),
			 ISNULL(PC.ValorMensal, 0) * (-1)