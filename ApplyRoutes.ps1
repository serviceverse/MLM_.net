$adminFile = "C:\Users\Kuter\Documents\.net\MLM_.net\Controllers\AdminController.cs"
$dbInitFile = "C:\Users\Kuter\Documents\.net\MLM_.net\Data\DbInitializer.cs"

$adminContent = Get-Content $adminFile -Raw
$dbContent = Get-Content $dbInitFile -Raw

$pattern = 'Route\s*=\s*"(/[^"]+)"'
$matches = [regex]::Matches($dbContent, $pattern)

$count = 0
foreach ($m in $matches) {
    $route = $m.Groups[1].Value
    if ($route -eq "/Admin/Dashboard") {
        continue
    }

    $actionName = $route -replace "/", ""
    
    $actionSig = "public IActionResult $actionName()"
    
    if ($adminContent -match [regex]::Escape($actionSig)) {
        if (-not ($adminContent -match "\[Route\(`"$route`"\)\]\s*" + [regex]::Escape($actionSig))) {
            $replacement = "[Route(`"$route`")]`r`n        $actionSig"
            $adminContent = $adminContent.Replace($actionSig, $replacement)
            $count++
        }
    } else {
        Write-Output "Action not found: $actionName for Route: $route"
    }
}

Set-Content -Path $adminFile -Value $adminContent
Write-Output "Added $count [Route] attributes to AdminController."
