using System;
using System.Linq;
using MLM.Models;
using BCrypt.Net;

namespace MLM.Data
{
    public static class DbInitializer
    {
        public static void Initialize(AppDBContext context)
        {
            // Check if Super Admin already initialized
            if (context.Organizations.Any(o => o.Code == "crm-admin"))
            {
                Console.WriteLine("âœ… Super Admin already initialized.");
                return;
            }

            Console.WriteLine("ðŸš€ Initializing Super Admin...");

            using var transaction = context.Database.BeginTransaction();

            try
            {
                var superAdminOrg = new Organization
                {
                    Name = "iman",
                    Code = "crm-admin",
                    Email = "admin@crm.com"
                };
                context.Organizations.Add(superAdminOrg);
                context.SaveChanges();

                // Create groups
                var groups = new[]
                {
                    new NavigationGroup { Name = "DashBoard", Icon = "DashBoard", Sequence = 1 },
                    new NavigationGroup { Name = "Client Dashboard", Icon = "DashBoard", Sequence = 1 }, // Renamed from duplicate Dashboard to avoid confusion or keep as is? Let's keep as "DashBoard" if that's what JS did, but JS had two "DashBoard". I'll name one Admin DashBoard and one Client DashBoard based on variables.
                    new NavigationGroup { Name = "Compliance", Icon = "ShieldCheck", Sequence = 2 },
                    new NavigationGroup { Name = "My Account", Icon = "User", Sequence = 3 },
                    new NavigationGroup { Name = "My Fund", Icon = "Wallet", Sequence = 4 },
                    new NavigationGroup { Name = "IB Programmer", Icon = "Users", Sequence = 1 },
                    new NavigationGroup { Name = "My Reports", Icon = "FileText", Sequence = 5 },
                    new NavigationGroup { Name = "My Wallet", Icon = "CreditCard", Sequence = 6 },
                    new NavigationGroup { Name = "Rewards", Icon = "Gift", Sequence = 7 },
                    new NavigationGroup { Name = "News", Icon = "Newspaper", Sequence = 8 },
                    new NavigationGroup { Name = "Help Desk", Icon = "LifeBuoy", Sequence = 9 },
                    new NavigationGroup { Name = "User Management", Icon = "Users", Sequence = 1 },
                    new NavigationGroup { Name = "Bonus", Icon = "Gift", Sequence = 2 },
                    new NavigationGroup { Name = "IB Management", Icon = "Dribbble", Sequence = 3 },
                    new NavigationGroup { Name = "Group Management", Icon = "Users", Sequence = 4 },
                    new NavigationGroup { Name = "Transaction", Icon = "FilterDollar", Sequence = 5 },
                    new NavigationGroup { Name = "Marketing", Icon = "FilterDollar", Sequence = 6 },
                    new NavigationGroup { Name = "Exchanger Management", Icon = "ArrowRightLeft", Sequence = 7 },
                    new NavigationGroup { Name = "Sales", Icon = "FilterDollar", Sequence = 8 },
                    new NavigationGroup { Name = "All Reports", Icon = "LineChart", Sequence = 9 },
                    new NavigationGroup { Name = "Settings", Icon = "Settings", Sequence = 10 },
                    new NavigationGroup { Name = "Send Email", Icon = "Email", Sequence = 11 },
                    new NavigationGroup { Name = "Notification", Icon = "Notification", Sequence = 12 },
                    new NavigationGroup { Name = "Milestone Management", Icon = "MilestoneManagement", Sequence = 13 },
                    new NavigationGroup { Name = "Rewards Management", Icon = "Gift", Sequence = 14 },
                    new NavigationGroup { Name = "IB Request", Icon = "Request", Sequence = 15 },
                    new NavigationGroup { Name = "Tickets", Icon = "LifeBuoy", Sequence = 16 }
                };

                // The JS code assigned specific variables to these groups, so I need to get them by reference.
                var adminDashboard = new NavigationGroup { Name = "DashBoard", Icon = "DashBoard", Sequence = 1 };
                var clientDashboard = new NavigationGroup { Name = "DashBoard", Icon = "DashBoard", Sequence = 1 };
                var complianceGroup = new NavigationGroup { Name = "Compliance", Icon = "ShieldCheck", Sequence = 2 };
                var myAccountGroup = new NavigationGroup { Name = "My Account", Icon = "User", Sequence = 3 };
                var myFundGroup = new NavigationGroup { Name = "My Fund", Icon = "Wallet", Sequence = 4 };
                var ibProgrammerGroup = new NavigationGroup { Name = "IB Programmer", Icon = "Users", Sequence = 1 };
                var myReportsGroup = new NavigationGroup { Name = "My Reports", Icon = "FileText", Sequence = 5 };
                var myWalletGroup = new NavigationGroup { Name = "My Wallet", Icon = "CreditCard", Sequence = 6 };
                var rewardsGroup = new NavigationGroup { Name = "Rewards", Icon = "Gift", Sequence = 7 };
                var newsGroup = new NavigationGroup { Name = "News", Icon = "Newspaper", Sequence = 8 };
                var helpDeskGroup = new NavigationGroup { Name = "Help Desk", Icon = "LifeBuoy", Sequence = 9 };
                var userManagementGroup = new NavigationGroup { Name = "User Management", Icon = "Users", Sequence = 1 };
                var bonusGroup = new NavigationGroup { Name = "Bonus", Icon = "Gift", Sequence = 2 };
                var ibManagementGroup = new NavigationGroup { Name = "IB Management", Icon = "Dribbble", Sequence = 3 };
                var groupManagementGroup = new NavigationGroup { Name = "Group Management", Icon = "Users", Sequence = 4 };
                var transactionGroup = new NavigationGroup { Name = "Transaction", Icon = "FilterDollar", Sequence = 5 };
                var marketingGroup = new NavigationGroup { Name = "Marketing", Icon = "FilterDollar", Sequence = 6 };
                var exchangerManagementGroup = new NavigationGroup { Name = "Exchanger Management", Icon = "ArrowRightLeft", Sequence = 7 };
                var salesGroup = new NavigationGroup { Name = "Sales", Icon = "FilterDollar", Sequence = 8 };
                var reportsGroup = new NavigationGroup { Name = "All Reports", Icon = "LineChart", Sequence = 9 };
                var settingsGroup = new NavigationGroup { Name = "Settings", Icon = "Settings", Sequence = 10 };
                var sendEmail = new NavigationGroup { Name = "Send Email", Icon = "Email", Sequence = 11 };
                var notification = new NavigationGroup { Name = "Notification", Icon = "Notification", Sequence = 12 };
                var milestonemanagement = new NavigationGroup { Name = "Milestone Management", Icon = "MilestoneManagement", Sequence = 13 };
                var rewardsmanagement = new NavigationGroup { Name = "Rewards Management", Icon = "Gift", Sequence = 14 };
                var ibRequest = new NavigationGroup { Name = "IB Request", Icon = "Request", Sequence = 15 };
                var adminTickets = new NavigationGroup { Name = "Tickets", Icon = "LifeBuoy", Sequence = 16 };

                var allGroups = new[] {
                    adminDashboard, clientDashboard, complianceGroup, myAccountGroup, myFundGroup, ibProgrammerGroup,
                    myReportsGroup, myWalletGroup, rewardsGroup, newsGroup, helpDeskGroup, userManagementGroup, bonusGroup,
                    ibManagementGroup, groupManagementGroup, transactionGroup, marketingGroup, exchangerManagementGroup,
                    salesGroup, reportsGroup, settingsGroup, sendEmail, notification, milestonemanagement, rewardsmanagement,
                    ibRequest, adminTickets
                };

                context.NavigationGroups.AddRange(allGroups);
                context.SaveChanges();

                // Create menu items
                var menus = new[] {
                    new NavigationItem { Name = "Document Upload", Key = "document-upload", Route = "/compliance/document-upload", GroupId = complianceGroup.Id, Sequence = 1 },
                    new NavigationItem { Name = "Bank Details", Key = "bank-details", Route = "/compliance/bank-details", GroupId = complianceGroup.Id, Sequence = 2 },
                    new NavigationItem { Name = "Open Live Account", Key = "open-live-account", Route = "/account/open-live-account", GroupId = myAccountGroup.Id, Sequence = 1 },
                    new NavigationItem { Name = "Account List", Key = "account-list", Route = "/account/account-list", GroupId = myAccountGroup.Id, Sequence = 2 },
                    new NavigationItem { Name = "Change MT5 Password", Key = "change-mt5-password", Route = "/account/change-password", GroupId = myAccountGroup.Id, Sequence = 3 },
                    new NavigationItem { Name = "Deposits", Key = "deposits", Route = "/fund/deposits", GroupId = myFundGroup.Id, Sequence = 1 },
                    new NavigationItem { Name = "Withdraw", Key = "withdraw", Route = "/fund/withdraw", GroupId = myFundGroup.Id, Sequence = 2 },
                    new NavigationItem { Name = "Internal Transfer", Key = "internal-transfer", Route = "/fund/internal-transfer", GroupId = myFundGroup.Id, Sequence = 3 },
                    new NavigationItem { Name = "External Transfer", Key = "external-transfer", Route = "/fund/external-transfer", GroupId = myFundGroup.Id, Sequence = 4 },
                    new NavigationItem { Name = "Setup Sub IB Commission", Key = "setup-sub-ib-commission", Route = "/ib/setup-sub-ib-commission", GroupId = ibProgrammerGroup.Id, Sequence = 1 },
                    new NavigationItem { Name = "IB Dashboard", Key = "ib-dashboard", Route = "/ib/dashboard", GroupId = ibProgrammerGroup.Id, Sequence = 2 },
                    new NavigationItem { Name = "My Clients", Key = "my-clients", Route = "/ib/my-clients", GroupId = ibProgrammerGroup.Id, Sequence = 3 },
                    new NavigationItem { Name = "Tree Charts", Key = "tree-charts", Route = "/ib/tree-charts", GroupId = ibProgrammerGroup.Id, Sequence = 4 },
                    new NavigationItem { Name = "My Commissions", Key = "my-commissions", Route = "/ib/my-commissions", GroupId = ibProgrammerGroup.Id, Sequence = 5 },
                    new NavigationItem { Name = "IB Withdrawn", Key = "ib-withdrawn", Route = "/ib/withdrawn", GroupId = ibProgrammerGroup.Id, Sequence = 6 },
                    new NavigationItem { Name = "USDT IB Withdrawn", Key = "usdt-ib-withdrawn", Route = "/ib/usdt-withdrawn", GroupId = ibProgrammerGroup.Id, Sequence = 7 },
                    new NavigationItem { Name = "Team Deposit Report", Key = "team-deposit-report", Route = "/ib/team-deposit-report", GroupId = ibProgrammerGroup.Id, Sequence = 8 },
                    new NavigationItem { Name = "Team Withdraw Report", Key = "team-withdraw-report", Route = "/ib/team-withdraw-report", GroupId = ibProgrammerGroup.Id, Sequence = 9 },
                    new NavigationItem { Name = "Deposit Report", Key = "deposit-report", Route = "/reports/deposit", GroupId = myReportsGroup.Id, Sequence = 1 },
                    new NavigationItem { Name = "Withdraw Report", Key = "withdraw-report", Route = "/reports/withdraw", GroupId = myReportsGroup.Id, Sequence = 2 },
                    new NavigationItem { Name = "IB Withdrawn Report", Key = "ib-withdrawn-report", Route = "/reports/ib-withdrawn", GroupId = myReportsGroup.Id, Sequence = 3 },
                    new NavigationItem { Name = "Internal Transfer Report", Key = "internal-transfer-report", Route = "/reports/internal-transfer", GroupId = myReportsGroup.Id, Sequence = 4 },
                    new NavigationItem { Name = "Deal Report", Key = "deal-report", Route = "/reports/deal", GroupId = myReportsGroup.Id, Sequence = 1 },
                    new NavigationItem { Name = "Wallet History", Key = "wallet-history", Route = "/wallet/history", GroupId = myWalletGroup.Id, Sequence = 1 },
                    new NavigationItem { Name = "MT5 To Wallet", Key = "mt5-to-wallet", Route = "/wallet/mt5-to-wallet", GroupId = myWalletGroup.Id, Sequence = 2 },
                    new NavigationItem { Name = "Wallet To MT5", Key = "wallet-to-mt5", Route = "/wallet/wallet-to-mt5", GroupId = myWalletGroup.Id, Sequence = 3 },
                    new NavigationItem { Name = "My Tickets", Key = "my-tickets", Route = "/help-desk/my-tickets", GroupId = helpDeskGroup.Id, Sequence = 1 },
                    new NavigationItem { Name = "New Tickets", Key = "new-tickets", Route = "/help-desk/new-ticket", GroupId = helpDeskGroup.Id, Sequence = 2 },
                    new NavigationItem { Name = "Rewards", Key = "rewards", Route = "/rewards", GroupId = rewardsGroup.Id, Sequence = 1 },
                    new NavigationItem { Name = "News Group", Key = "news-group", Route = "/news", GroupId = newsGroup.Id, Sequence = 2 },
                    new NavigationItem { Name = "IB Request", Key = "ib-request", Route = "/ib-request", GroupId = ibRequest.Id, Sequence = 3 },
                    new NavigationItem { Name = "Dashboard", Key = "client-dashboard", Route = "/client/dashboard", GroupId = clientDashboard.Id, Sequence = 4 },

                    // admin part 
                    new NavigationItem { Name = "User List", Key = "user-list", Route = "/user/list", GroupId = userManagementGroup.Id, Sequence = 1 },
                    new NavigationItem { Name = "Create MT5 Account", Key = "create-mt5", Route = "/user/create-mt5", GroupId = userManagementGroup.Id, Sequence = 2 },
                    new NavigationItem { Name = "MT5 User List", Key = "mt5-user-list", Route = "/user/mt5-list", GroupId = userManagementGroup.Id, Sequence = 3 },
                    new NavigationItem { Name = "Follow Up List", Key = "follow-up", Route = "/user/follow-up", GroupId = userManagementGroup.Id, Sequence = 4 },
                    new NavigationItem { Name = "User Documents List", Key = "approved-docs", Route = "/user/approved-docs", GroupId = userManagementGroup.Id, Sequence = 6 },
                    new NavigationItem { Name = "Upload User Documents", Key = "upload-docs", Route = "/user/upload-docs", GroupId = userManagementGroup.Id, Sequence = 7 },
                    new NavigationItem { Name = "Add Bank Details", Key = "add-bank", Route = "/user/add-bank", GroupId = userManagementGroup.Id, Sequence = 8 },
                    new NavigationItem { Name = "Bank Details List", Key = "bank-list", Route = "/user/bank-list", GroupId = userManagementGroup.Id, Sequence = 9 },
                    new NavigationItem { Name = "User Password List", Key = "password-list", Route = "/user/passwords", GroupId = userManagementGroup.Id, Sequence = 10 },
                    new NavigationItem { Name = "Change User Password", Key = "change-password", Route = "/user/change-password", GroupId = userManagementGroup.Id, Sequence = 11 },
                    new NavigationItem { Name = "Add Existing Client", Key = "existing-client", Route = "/user/add-existing", GroupId = userManagementGroup.Id, Sequence = 12 },
                    new NavigationItem { Name = "Change MT5 Password", Key = "change-mt5-password", Route = "/user/change-mt5-password", GroupId = userManagementGroup.Id, Sequence = 13 },
                    new NavigationItem { Name = "Update MT5 Leverage", Key = "update-mt5-leverage", Route = "/user/update-leverage", GroupId = userManagementGroup.Id, Sequence = 14 },
                    new NavigationItem { Name = "Resend Verification Mail", Key = "resend-mail", Route = "/user/resend-verification", GroupId = userManagementGroup.Id, Sequence = 15 },
                    new NavigationItem { Name = "Resend MT5 Data Mail", Key = "resend-data-mail", Route = "/user/resend-data-mail", GroupId = userManagementGroup.Id, Sequence = 16 },

                    new NavigationItem { Name = "Give Bonus", Key = "give-bonus", Route = "/bonus/give", GroupId = bonusGroup.Id, Sequence = 1 },
                    new NavigationItem { Name = "Remove Bonus", Key = "remove-bonus", Route = "/bonus/remove", GroupId = bonusGroup.Id, Sequence = 2 },
                    new NavigationItem { Name = "Bonus List", Key = "bonus-list", Route = "/bonus/list", GroupId = bonusGroup.Id, Sequence = 3 },

                    new NavigationItem { Name = "IB Users", Key = "ib-users", Route = "/ib-management/users", GroupId = ibManagementGroup.Id, Sequence = 1 },
                    new NavigationItem { Name = "IB Requests", Key = "ib-requests", Route = "/ib-management/requests", GroupId = ibManagementGroup.Id, Sequence = 2 },
                    new NavigationItem { Name = "Set IB Commission", Key = "ib-commission", Route = "/ib-management/set-commission", GroupId = ibManagementGroup.Id, Sequence = 3 },
                    new NavigationItem { Name = "Transfer Trader", Key = "transfer-trader", Route = "/ib-management/transfer-trader", GroupId = ibManagementGroup.Id, Sequence = 4 },

                    new NavigationItem { Name = "Add Group", Key = "add-group", Route = "/group/add", GroupId = groupManagementGroup.Id, Sequence = 1 },
                    new NavigationItem { Name = "Group List", Key = "group-list", Route = "/group/list", GroupId = groupManagementGroup.Id, Sequence = 2 },
                    new NavigationItem { Name = "Update MT5 Group", Key = "update-mt5-group", Route = "/group/update-mt5", GroupId = groupManagementGroup.Id, Sequence = 3 },

                    new NavigationItem { Name = "Client Deposit", Key = "client-deposit", Route = "/transaction/client-deposit", GroupId = transactionGroup.Id, Sequence = 1 },
                    new NavigationItem { Name = "Client Withdraw", Key = "client-withdraw", Route = "/transaction/client-withdraw", GroupId = transactionGroup.Id, Sequence = 3 },
                    new NavigationItem { Name = "IB Withdraw", Key = "ib-withdraw", Route = "/transaction/ib-withdraw", GroupId = transactionGroup.Id, Sequence = 4 },
                    new NavigationItem { Name = "Internal Transfer", Key = "internal-transfer", Route = "/transaction/internal-transfer", GroupId = transactionGroup.Id, Sequence = 1 },
                    new NavigationItem { Name = "External Transfer", Key = "external-transfer", Route = "/transaction/external-transfer", GroupId = transactionGroup.Id, Sequence = 1 },
                    new NavigationItem { Name = "Pending Deposit", Key = "pending-deposit", Route = "/transaction/pending-deposit", GroupId = transactionGroup.Id, Sequence = 5 },
                    new NavigationItem { Name = "Pending Withdraw", Key = "pending-withdraw", Route = "/transaction/pending-withdraw", GroupId = transactionGroup.Id, Sequence = 6 },
                    new NavigationItem { Name = "FloxyPay Auto Withdraw", Key = "floxy-auto-withdraw", Route = "/transaction/floxy-auto", GroupId = transactionGroup.Id, Sequence = 7 },
                    new NavigationItem { Name = "Pending IB Withdraw", Key = "pending-ib-withdraw", Route = "/transaction/pending-ib-withdraw", GroupId = transactionGroup.Id, Sequence = 8 },
                    new NavigationItem { Name = "FloxyPay Auto IB Withdraw", Key = "floxy-auto-ib", Route = "/transaction/floxy-auto-ib", GroupId = transactionGroup.Id, Sequence = 9 },
                    new NavigationItem { Name = "Pending Wallet Transfer", Key = "pending-wallet-transfer", Route = "/transaction/pending-wallet-transfer", GroupId = transactionGroup.Id, Sequence = 10 },
                    new NavigationItem { Name = "Pending Internal Transfer", Key = "pending-internal-transfer", Route = "/transaction/pending-internal", GroupId = transactionGroup.Id, Sequence = 11 },
                    new NavigationItem { Name = "Pending External Transfer", Key = "pending-external-transfer", Route = "/transaction/pending-external", GroupId = transactionGroup.Id, Sequence = 12 },

                    new NavigationItem { Name = "Add Marketing", Key = "add-marketing", Route = "/marketing/add", GroupId = marketingGroup.Id, Sequence = 1 },
                    new NavigationItem { Name = "Marketing List", Key = "marketing-list", Route = "/marketing/list", GroupId = marketingGroup.Id, Sequence = 2 },
                    new NavigationItem { Name = "Incentive Report", Key = "incentive-report", Route = "/marketing/incentive-report", GroupId = marketingGroup.Id, Sequence = 3 },
                    new NavigationItem { Name = "Marketing Withdraw Report", Key = "marketing-withdraw-report", Route = "/marketing/withdraw-report", GroupId = marketingGroup.Id, Sequence = 4 },

                    new NavigationItem { Name = "Exchanger List", Key = "exchanger-list", Route = "/exchanger/list", GroupId = exchangerManagementGroup.Id, Sequence = 1 },
                    new NavigationItem { Name = "Exchanger Transfer", Key = "exchanger-transfer", Route = "/exchanger/transfer", GroupId = exchangerManagementGroup.Id, Sequence = 2 },
                    new NavigationItem { Name = "Exchanger Deposit", Key = "exchanger-deposit", Route = "/exchanger/deposit", GroupId = exchangerManagementGroup.Id, Sequence = 3 },
                    new NavigationItem { Name = "Exchanger Withdraw", Key = "exchanger-withdraw", Route = "/exchanger/withdraw", GroupId = exchangerManagementGroup.Id, Sequence = 4 },
                    new NavigationItem { Name = "Exchanger IB Withdraw", Key = "exchanger-ib-withdraw", Route = "/exchanger/ib-withdraw", GroupId = exchangerManagementGroup.Id, Sequence = 5 },

                    new NavigationItem { Name = "Sales Dashboard", Key = "sales-dashboard", Route = "/sales/dashboard", GroupId = salesGroup.Id, Sequence = 1 },
                    new NavigationItem { Name = "Add Sales", Key = "add-sales", Route = "/sales/add", GroupId = salesGroup.Id, Sequence = 2 },
                    new NavigationItem { Name = "Sales List", Key = "sales-list", Route = "/sales/list", GroupId = salesGroup.Id, Sequence = 1 },
                    new NavigationItem { Name = "Lead Status", Key = "lead-status", Route = "/sales/lead-status", GroupId = salesGroup.Id, Sequence = 3 },
                    new NavigationItem { Name = "Lead Source", Key = "lead-source", Route = "/sales/lead-source", GroupId = salesGroup.Id, Sequence = 4 },
                    new NavigationItem { Name = "Lead List", Key = "lead-list", Route = "/sales/lead-list", GroupId = salesGroup.Id, Sequence = 5 },
                    new NavigationItem { Name = "Bulk Upload Leads", Key = "bulk-leads", Route = "/sales/bulk-upload", GroupId = salesGroup.Id, Sequence = 6 },

                    new NavigationItem { Name = "Deposit Report", Key = "deposit-report", Route = "/all-reports/deposit-report", GroupId = reportsGroup.Id, Sequence = 1 },
                    new NavigationItem { Name = "Withdraw Report", Key = "withdraw-report", Route = "/all-reports/withdraw-report", GroupId = reportsGroup.Id, Sequence = 2 },
                    new NavigationItem { Name = "IB Withdraw Report", Key = "ib-withdraw-report", Route = "/all-reports/ib-withdraw-report", GroupId = reportsGroup.Id, Sequence = 3 },
                    new NavigationItem { Name = "Internal Transfer Report", Key = "internal-transfer-report", Route = "/all-reports/internal-transfer-report", GroupId = reportsGroup.Id, Sequence = 4 },
                    new NavigationItem { Name = "Wallet History Report", Key = "wallet-history-report", Route = "/all-reports/wallet-history-report", GroupId = reportsGroup.Id, Sequence = 5 },
                    new NavigationItem { Name = "Position Report", Key = "position-report", Route = "/all-reports/position-report", GroupId = reportsGroup.Id, Sequence = 6 },
                    new NavigationItem { Name = "History Report", Key = "history-report", Route = "/all-reports/history", GroupId = reportsGroup.Id, Sequence = 7 },
                    new NavigationItem { Name = "Login Activity", Key = "login-activity", Route = "/all-reports/login-activity", GroupId = reportsGroup.Id, Sequence = 8 },

                    new NavigationItem { Name = "App Settings", Key = "app-settings", Route = "/settings/app-settings", GroupId = settingsGroup.Id, Sequence = 1 },
                    new NavigationItem { Name = "Role", Key = "role", Route = "/role/index", GroupId = settingsGroup.Id, Sequence = 2 },
                    new NavigationItem { Name = "Permission", Key = "menu-permission", Route = "/settings/menu-permission", GroupId = settingsGroup.Id, Sequence = 3 },
                    new NavigationItem { Name = "User Management", Key = "users-management", Route = "/settings/users-management", GroupId = settingsGroup.Id, Sequence = 4 },

                    new NavigationItem { Name = "Send Email", Key = "send-email", Route = "/send-email", GroupId = sendEmail.Id, Sequence = 1 },
                    new NavigationItem { Name = "Notifications", Key = "notifications", Route = "/notifications", GroupId = notification.Id, Sequence = 1 },
                    new NavigationItem { Name = "Milestone Management", Key = "mileStone", Route = "/milestones", GroupId = milestonemanagement.Id, Sequence = 1 },
                    new NavigationItem { Name = "Rewards Management", Key = "rewards", Route = "/rewards-management", GroupId = rewardsmanagement.Id, Sequence = 1 },
                    new NavigationItem { Name = "Dashboard", Key = "admin-dashboard", Route = "/admin/dashboard", GroupId = adminDashboard.Id, Sequence = 1 },
                    new NavigationItem { Name = "Tickets", Key = "adminTickets", Route = "/tickets", GroupId = adminTickets.Id, Sequence = 1 }
                };

                context.NavigationItems.AddRange(menus);
                context.SaveChanges();

                var superAdminRole = new Role { Name = "Super Admin", Description = "System-wide super administrator", IsAdmin = true };
                var adminRole = new Role { Name = "Admin", Description = "Default admin role", IsAdmin = true };
                var clientRole = new Role { Name = "Client", Description = "Default client role", IsAdmin = false };

                context.Roles.AddRange(superAdminRole, adminRole, clientRole);
                context.SaveChanges();

                var allMenus = context.NavigationItems.ToList();
                foreach (var menu in allMenus)
                {
                    context.NavigationActions.AddRange(
                        new NavigationAction { Name = "View", Key = "view", ItemId = menu.Id },
                        new NavigationAction { Name = "Create", Key = "create", ItemId = menu.Id },
                        new NavigationAction { Name = "Update", Key = "update", ItemId = menu.Id },
                        new NavigationAction { Name = "Delete", Key = "delete", ItemId = menu.Id }
                    );
                }
                context.SaveChanges();

                var allActions = context.NavigationActions.ToList();
                var fullAccessRoles = new[] { superAdminRole.Id, adminRole.Id };
                
                // The JS script skipped 32 menus for admin and took 32 for clients. 
                // Because ID ordering might vary depending on DB insertion order, let's just use Skip and Take like the JS script.
                var orderedMenus = context.NavigationItems.OrderBy(m => m.Id).ToList();
                var adminMenus = orderedMenus.Skip(32).ToList();
                var clientMenus = orderedMenus.Take(32).ToList();

                foreach (var roleId in fullAccessRoles)
                {
                    var menuPerms = adminMenus.Select(m => new NavigationMenuPermission { MenuId = m.Id, RoleId = roleId });
                    var actionPerms = allActions.Select(a => new NavigationActionPermission { ActionId = a.Id, RoleId = roleId });
                    
                    context.NavigationMenuPermissions.AddRange(menuPerms);
                    context.NavigationActionPermissions.AddRange(actionPerms);
                }

                var clientMenuPerms = clientMenus.Select(m => new NavigationMenuPermission { MenuId = m.Id, RoleId = clientRole.Id });
                var clientActionPerms = clientMenus.Select(m => {
                    // Note: JS logic had a bug? "data: clientMenus.map((action) => ({ actionId: action.id" -> It mapped menu to actionId, which is wrong. 
                    // To fix this logically: we should give view/create/update/delete actions for client menus to client Role.
                    var actionsForClientMenus = allActions.Where(a => clientMenus.Any(cm => cm.Id == a.ItemId));
                    return actionsForClientMenus.Select(a => new NavigationActionPermission { ActionId = a.Id, RoleId = clientRole.Id });
                }).SelectMany(x => x);

                context.NavigationMenuPermissions.AddRange(clientMenuPerms);
                context.NavigationActionPermissions.AddRange(clientActionPerms);
                context.SaveChanges();

                // Create default Super Admin user
                string password = "admin@123";
                string salt = BCrypt.Net.BCrypt.GenerateSalt(10);
                string hash = BCrypt.Net.BCrypt.HashPassword(password, salt);
                string referralCode = Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper(); // generate random code

                var superAdmin = new Users
                {
                    Username = "superadmin",
                    FullName = "Super Admin",
                    Email = "admin@crm.com",
                    ContactNo = "9999999999",
                    Hash = hash,
                    Salt = salt,
                    RoleId = superAdminRole.Id,
                    ReferralCode = referralCode,
                    OrganizationId = superAdminOrg.Id
                };

                context.Users.Add(superAdmin);
                context.SaveChanges();

                transaction.Commit();
                Console.WriteLine("âœ… Super Admin initialized successfully!");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Console.WriteLine($"âŒ Error initializing Super Admin: {ex.Message}");
                throw;
            }
        }
    }
}

