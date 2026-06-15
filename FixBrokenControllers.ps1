$baseDir = "C:\Users\Kuter\Documents\.net\MLM_.net\Controllers"
$controllers = "AllReportsController.cs", "BonusController.cs", "ExchangerController.cs", "GroupController.cs", "IbManagementController.cs", "MarketingController.cs", "SalesController.cs", "TransactionController.cs"

foreach ($c in $controllers) {
    $file = "$baseDir\$c"
    if (Test-Path $file) {
        $content = Get-Content $file -Raw
        $pattern = 'public IActionResult ([A-Za-z0-9_]+)\('
        $matches = [regex]::Matches($content, $pattern)
        
        $actions = @()
        foreach ($m in $matches) {
            $actionName = $m.Groups[1].Value
            if ($actions -notcontains $actionName) {
                $actions += $actionName
            }
        }
        
        $className = $c.Replace(".cs", "")
        $newContent = @"
using Microsoft.AspNetCore.Mvc;
using MLM.Data;

namespace MLM.Controllers
{
    public class $className : Controller
    {
        private readonly AppDBContext _context;

        public $className(AppDBContext context)
        {
            _context = context;
        }
"@

        foreach ($action in $actions) {
            $newContent += @"

        public IActionResult $action()
        {
            return View();
        }
"@
        }

        $newContent += @"
    }
}
"@
        Set-Content -Path $file -Value $newContent
        Write-Output "Cleaned and rebuilt $c with $($actions.Count) actions."
    }
}

# Now append missing actions to UserController.cs
$userFile = "$baseDir\UserController.cs"
$userContent = Get-Content $userFile -Raw

$missingActions = "AddBank", "AddExisting", "ApprovedDocs", "BankList", "ChangeMt5Password", "ChangePassword", "CreateMt5", "FollowUp", "List", "Mt5List", "Passwords", "ResendDataMail", "ResendVerification", "UpdateLeverage", "UploadDocs"

$methodsToAdd = ""
foreach ($action in $missingActions) {
    if (-not ($userContent -match "public IActionResult $action\(")) {
        $methodsToAdd += @"
        public IActionResult $action()
        {
            return View();
        }
"@
    }
}

if ($methodsToAdd -ne "") {
    $insertPattern = '(\s*\}\s*\})'
    $userContent = $userContent -replace $insertPattern, "`r`n$methodsToAdd`r`n`$1"
    Set-Content -Path $userFile -Value $userContent
    Write-Output "Appended missing actions to UserController.cs"
}
