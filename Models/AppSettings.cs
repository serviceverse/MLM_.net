using System.ComponentModel.DataAnnotations;

namespace MLM.Models
{
    public class AppSettings
    {
        [Key]
        public int Id { get; set; }

        // Theme Settings
        [StringLength(50)]
        public string? PrimaryColor { get; set; }

        [StringLength(50)]
        public string? SecondaryColor { get; set; }

        [StringLength(50)]
        public string? TertiaryColor { get; set; }

        public bool DarkThemeEnabled { get; set; }

        // Email Settings
        [StringLength(100)]
        public string? SmtpServer { get; set; }

        public int SmtpPort { get; set; }

        [StringLength(100)]
        public string? SmtpEmail { get; set; }

        [StringLength(255)]
        public string? SmtpPassword { get; set; }

        [StringLength(100)]
        public string? SmtpFromName { get; set; }

        public bool UseSsl { get; set; }

        public int SmtpTimeout { get; set; }

        // App Settings
        public bool OtpLoginRequired { get; set; }
        
        public bool CaptchaEnabled { get; set; }
        
        public bool ReferralEnabled { get; set; }

        [StringLength(10)]
        public string? Currency { get; set; }

        [StringLength(20)]
        public string? TransactionType { get; set; }

        [StringLength(50)]
        public string? DefaultPaymentMode { get; set; }

        public int NoOfClientRequiredForIb { get; set; }

        public decimal WithdrawFee { get; set; }

        public int RequiredTradeSeconds { get; set; }

        [StringLength(255)]
        public string? Mt5Server { get; set; }

        // Image Settings (Paths)
        [StringLength(255)]
        public string? AppLogoPath { get; set; }
        
        [StringLength(255)]
        public string? AppIconPath { get; set; }

        [StringLength(255)]
        public string? FaviconPath { get; set; }
    }
}
