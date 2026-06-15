namespace MLM.Models
{
    public enum DepositMode
    {
        Card,
        Usdt,
        Bank,
        Cash
    }

    public enum DepositType
    {
        Wallet,
        Account
    }

    public enum WithdrawalMode
    {
        Usdt,
        Bank,
        Cash
    }

    public enum DepositStatus
    {
        Pending,
        Approved,
        Rejected
    }

    public enum WithdrawalStatus
    {
        Pending,
        Approved,
        Rejected,
        Failed
    }

    public enum IbStatus
    {
        Pending,
        Approved,
        Rejected
    }
}
