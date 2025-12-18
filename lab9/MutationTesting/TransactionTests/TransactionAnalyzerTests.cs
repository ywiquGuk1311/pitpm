using TransactionLib;

public class TransactionAnalyzerTests
{
    [Fact]
    public void RejectsUnknownTransactionType()
    {
        var tx = new Transaction
        {
            Amount = 100,
            Kind = TransactionKind.Unknown,
            Timestamp = DateTime.Now
        };

        Assert.StartsWith("Ошибка", TransactionAnalyzer.AnalyzeTransaction(tx));
    }

    [Fact]
    public void RejectsTooManyTransactionsForNonVip()
    {
        var tx = new Transaction
        {
            Amount = 100,
            Kind = TransactionKind.Deposit,
            DailyTransactionCount = 11,
            IsVipClient = false
        };

        Assert.Contains("превышено количество", TransactionAnalyzer.AnalyzeTransaction(tx));
    }

    [Fact]
    public void AllowDailyTransactionCountForNonVip()
    {
        var tx = new Transaction
        {
            Amount = 100,
            Kind = TransactionKind.Deposit,
            DailyTransactionCount = 10,
            IsVipClient = false
        };

        Assert.Equal("Транзакция допустима.", TransactionAnalyzer.AnalyzeTransaction(tx));
    }

    [Fact]
    public void RejectToTransferLargeExternal()
    {
        var tx = new Transaction
        {
            Amount = 1_000_001,
            Kind = TransactionKind.Transfer,
            IsInternal = false,
            IsVipClient = true
        };

        Assert.StartsWith("Отклонено: крупные", TransactionAnalyzer.AnalyzeTransaction(tx));
    }

    [Fact]
    public void AllowToTransferLargeExternal()
    {
        var tx = new Transaction
        {
            Amount = 1_000_000,
            Kind = TransactionKind.Transfer,
            Timestamp = DateTime.UtcNow,
            IsInternal = false,
            IsVipClient = true
        };

        Assert.Equal("Транзакция допустима.", TransactionAnalyzer.AnalyzeTransaction(tx));
    }

    [Fact]
    public void AppliesCommissionForNonVipCurrentToSaving()
    {
        var tx = new Transaction
        {
            Amount = 1000,
            Kind = TransactionKind.Transfer,
            FromAccountType = "Текущий",
            ToAccountType = "Сберегательный",
            IsInternal = false,
            IsVipClient = false
        };

        Assert.StartsWith("Комиссия", TransactionAnalyzer.AnalyzeTransaction(tx));
    }

    [Fact]
    public void AllowTransferToIternalTransferBetweenDifferenTypesToSaving()
    {
        var tx = new Transaction
        {
            Amount = 1000,
            Kind = TransactionKind.Transfer,
            FromAccountType = "Текущий",
            ToAccountType = "Сберегательный",
            IsInternal = true,
            IsVipClient = false
        };

        Assert.Equal("Транзакция допустима.", TransactionAnalyzer.AnalyzeTransaction(tx));
    }

    [Fact]
    public void RejectsDailySumExceededForNonVip()
    {
        var tx = new Transaction
        {
            Amount = 11,
            Kind = TransactionKind.Transfer,
            DailyTransactionTotal = 499_990,
            IsVipClient = false
        };

        Assert.StartsWith("Ограничение: превышена", TransactionAnalyzer.AnalyzeTransaction(tx));
    }

    [Fact]
    public void AllowRejectsDailySumExceededForNonVip()
    {
        var tx = new Transaction
        {
            Amount = 250_000,
            Kind = TransactionKind.Deposit,
            DailyTransactionTotal = 250_000,
            IsVipClient = false
        };

        Assert.Equal("Транзакция допустима.", TransactionAnalyzer.AnalyzeTransaction(tx));
    }

    [Fact]
    public void RejectsLargeAtmTransfer()
    {
        var tx = new Transaction
        {
            Amount = 100_001,
            Kind = TransactionKind.Transfer,
            Channel = "ATM",
            IsInternal = true
        };

        Assert.StartsWith("Ограничение: банкоматы", TransactionAnalyzer.AnalyzeTransaction(tx));
    }

    [Fact]
    public void AllowAtmTransfer()
    {
        var tx = new Transaction
        {
            Amount = 100_000,
            Kind = TransactionKind.Transfer,
            Channel = "ATM",
            IsInternal = true
        };

        Assert.StartsWith("Транзакция допустима.", TransactionAnalyzer.AnalyzeTransaction(tx));
    }

    [Fact]
    public void AllowableAtmTransfer()
    {
        var tx = new Transaction
        {
            Amount = 99_999,
            Kind = TransactionKind.Deposit,
            Channel = "ATM",
            IsInternal = true
        };

        Assert.Equal("Транзакция допустима.", TransactionAnalyzer.AnalyzeTransaction(tx));
    }

    [Fact]
    public void AllowsVipLargeExternalWithdrawal()
    {
        var tx = new Transaction
        {
            Amount = 500_000,
            Kind = TransactionKind.Withdrawal,
            IsInternal = false,
            IsVipClient = true,
            Channel = "Office"
        };

        Assert.Equal("Транзакция допустима.", TransactionAnalyzer.AnalyzeTransaction(tx));
    }

    [Fact]
    public void RejectsZeroAmountWithDeposit()
    {
        var tx = new Transaction
        {
            Amount = 0,
            Kind = TransactionKind.Deposit,
            IsVipClient = false,
            Channel = "Web",
        };

        Assert.StartsWith("Ошибка", TransactionAnalyzer.AnalyzeTransaction(tx));
    }

    [Fact]
    public void RejectsWithdrawalFromWeb()
    {
        var tx = new Transaction
        {
            Amount = 100,
            Kind = TransactionKind.Withdrawal,
            Channel = "Web",
        };

        Assert.StartsWith("Отклонено", TransactionAnalyzer.AnalyzeTransaction(tx));
    }

    [Fact]
    public void RejectsWithdrawaLargeAmountForNotVip()
    {
        var tx = new Transaction
        {
            Amount = 200_001,
            Kind = TransactionKind.Withdrawal,
            Channel = "Mobile",
            IsVipClient = false
        };

        Assert.StartsWith("Ограничение", TransactionAnalyzer.AnalyzeTransaction(tx));
    }

    [Fact]
    public void AllowWithdrawaIternalLargeAmountForVip()
    {
        var tx = new Transaction
        {
            Amount = 200_001,
            Kind = TransactionKind.Withdrawal,
            Channel = "Mobile",
            IsVipClient = true,
            IsInternal = true
            
        };

        Assert.Equal("Транзакция допустима.", TransactionAnalyzer.AnalyzeTransaction(tx));
    }

    [Fact]
    public void AllowWithdrawaAmountFromMobileForNotVip()
    {
        var tx = new Transaction
        {
            Amount = 200_000,
            Kind = TransactionKind.Withdrawal,
            Channel = "Mobile",
            IsVipClient = false
        };

        Assert.Equal("Транзакция допустима.", TransactionAnalyzer.AnalyzeTransaction(tx));
    }

    [Fact]
    public void RejectTransferInWeekEndBetweenDifferentTypesFromMobile()
    {
        var tx = new Transaction
        {
            Amount = 100,
            Kind = TransactionKind.Transfer,
            Channel = "Mobile",
            FromAccountType = "Текущий",
            ToAccountType = "Сберегательный",
            Timestamp = new DateTime(2025, 12, 27),
            IsVipClient = false,
            IsInternal = true
        };

        Assert.Contains("в выходные нельзя", TransactionAnalyzer.AnalyzeTransaction(tx));
    }

    [Fact]
    public void RejectTransferInWeekEndBetweenDifferentTypesFromWeb()
    {
        var tx = new Transaction
        {
            Amount = 100,
            Kind = TransactionKind.Transfer,
            Channel = "Web",
            FromAccountType = "Текущий",
            ToAccountType = "Сберегательный",
            Timestamp = new DateTime(2025, 12, 28),
            IsVipClient = false,
            IsInternal = true            
        };

        Assert.Contains("в выходные нельзя", TransactionAnalyzer.AnalyzeTransaction(tx));
    }

    [Fact]
    public void AllowTransferInWeekEndBetweenEqualTypesFromWeb()
    {
        var tx = new Transaction
        {
            Amount = 100,
            Kind = TransactionKind.Transfer,
            Channel = "Web",
            Timestamp = new DateTime(2025, 12, 28),
            IsVipClient = false,
            IsInternal = true
        };

        Assert.Equal("Транзакция допустима.", TransactionAnalyzer.AnalyzeTransaction(tx));
    }
}
