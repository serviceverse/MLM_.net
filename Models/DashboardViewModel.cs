using System.Collections.Generic;

namespace MLM.Models
{
    public class DashboardViewModel
    {
        public string UserName { get; set; } = "Trader";
        public decimal WalletBalance { get; set; } = 0;
        public string Currency { get; set; } = "USD";
        public int AccountCount { get; set; } = 0;
        public decimal TotalDeposits { get; set; } = 0;
        public decimal TotalWithdrawals { get; set; } = 0;
        public decimal IbAvailable { get; set; } = 0;
        public decimal IbWithdrawn { get; set; } = 0;
        public int IbTotalClients { get; set; } = 0;
        public string? ReferralCode { get; set; }
        public bool ShowReferral { get; set; } = false;

        public List<Mt5AccountViewModel> Accounts { get; set; } = new List<Mt5AccountViewModel>();
        public List<IbClientViewModel> IbClients { get; set; } = new List<IbClientViewModel>();

        // Chart Data
        public List<decimal> MonthlyDeposits { get; set; } = new List<decimal>();
        public List<decimal> MonthlyWithdrawals { get; set; } = new List<decimal>();
        public List<decimal> NetMonthly { get; set; } = new List<decimal>();
    }

    public class Mt5AccountViewModel
    {
        public string LoginId { get; set; } = string.Empty;
        public string Group { get; set; } = string.Empty;
        public decimal Balance { get; set; } = 0;
        public decimal Equity { get; set; } = 0;
    }

    public class IbClientViewModel
    {
        public string Id { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string? JoinedAt { get; set; }
        public decimal TotalDeposit { get; set; } = 0;
    }
}
