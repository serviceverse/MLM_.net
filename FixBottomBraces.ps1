$files = "Controllers\SalesController.cs", "Controllers\TransactionController.cs", "Controllers\UserController.cs"

foreach ($f in $files) {
    $c = Get-Content $f -Raw
    # Remove the trailing malformed braces
    $c = $c -replace 'public IActionResult [a-zA-Z0-9_]+\(\) \{ return View\(\); \}\s+\}\s+\}\s+\}', ""
    $c = $c -replace 'public IActionResult [A-Za-z0-9_]+\(\) \{ return View\(\); \}\s+\}\s*\}\s*\}?', ""
    $c = $c -replace '\s*\}\s*\}\s*\}?\s*$', ""

    # Re-add the proper actions
    if ($f -match "Sales") {
        $c += "`r`n        public IActionResult Add() { return View(); }`r`n    }`r`n}"
    } elseif ($f -match "Transaction") {
        $c += "`r`n        public IActionResult WalletDeposit() { return View(); }`r`n        public IActionResult WalletWithdraw() { return View(); }`r`n    }`r`n}"
    } elseif ($f -match "User") {
        $c += "`r`n        public IActionResult AddUser() { return View(); }`r`n        public IActionResult LeverageList() { return View(); }`r`n        public IActionResult PendingDocs() { return View(); }`r`n        public IActionResult Referrals() { return View(); }`r`n    }`r`n}"
    }
    Set-Content $f -Value $c
}
