$baseDir = "C:\Users\Kuter\Documents\.net\MLM_.net"
$adminFile = "$baseDir\Controllers\AdminController.cs"

$adminContent = Get-Content $adminFile -Raw
$pattern = '(?s)\s*\[Route\("(/([^/]+)/([^"]+))"\)\]\s+public IActionResult ([^\(]+)\(\)\s*\{\s*return View\(\);\s*\}'

$matches = [regex]::Matches($adminContent, $pattern)

$groupedActions = @{}
$viewsToMove = @()

foreach ($m in $matches) {
    $fullMatch = $m.Groups[0].Value
    $fullRoute = $m.Groups[1].Value
    $controllerName = $m.Groups[2].Value
    $actionName = $m.Groups[3].Value
    $oldActionName = $m.Groups[4].Value

    if ($controllerName -eq "Admin") { continue }

    if (-not $groupedActions.ContainsKey($controllerName)) {
        $groupedActions[$controllerName] = @()
    }
    
    $groupedActions[$controllerName] += $actionName
    $viewsToMove += @{
        Old = "$baseDir\Views\Admin\$($oldActionName).cshtml"
        NewDir = "$baseDir\Views\$controllerName"
        NewPath = "$baseDir\Views\$controllerName\$($actionName).cshtml"
        OldAction = $oldActionName
        NewAction = $actionName
    }

    $adminContent = $adminContent.Replace($fullMatch, "")
}

# Update controllers
foreach ($controllerName in $groupedActions.Keys) {
    $controllerFile = "$baseDir\Controllers\$($controllerName)Controller.cs"
    if (-not (Test-Path $controllerFile)) {
        $newControllerContent = @"
using Microsoft.AspNetCore.Mvc;
using MLM.Data;

namespace MLM.Controllers
{
    public class $($controllerName)Controller : Controller
    {
        private readonly AppDBContext _context;

        public $($controllerName)Controller(AppDBContext context)
        {
            _context = context;
        }

    }
}
"@
        Set-Content -Path $controllerFile -Value $newControllerContent
    }

    $controllerContent = Get-Content $controllerFile -Raw
    $actions = $groupedActions[$controllerName]
    
    foreach ($actionName in $actions) {
        if (-not ($controllerContent -match "public IActionResult $($actionName)\(\)")) {
            $methodToAdd = @"
        public IActionResult $($actionName)()
        {
            return View();
        }
"@
            $insertPattern = '(\s*\}\s*\})'
            $controllerContent = $controllerContent -replace $insertPattern, "`r`n$methodToAdd`r`n`$1"
        }
    }

    $maxRetries = 5
    $retryDelay = 500
    for ($i = 0; $i -lt $maxRetries; $i++) {
        try {
            Set-Content -Path $controllerFile -Value $controllerContent -ErrorAction Stop
            Write-Output "Updated $($controllerName)Controller with $($actions.Count) actions"
            break
        } catch {
            if ($i -eq $maxRetries - 1) { throw }
            Start-Sleep -Milliseconds $retryDelay
        }
    }
}

# Move views
foreach ($view in $viewsToMove) {
    if (-not (Test-Path $view.NewDir)) {
        New-Item -ItemType Directory -Path $view.NewDir | Out-Null
    }

    if (Test-Path $view.Old) {
        Move-Item -Path $view.Old -Destination $view.NewPath -Force
        
        $viewContent = Get-Content $view.NewPath -Raw
        $viewContent = $viewContent -replace $view.OldAction, $view.NewAction
        Set-Content -Path $view.NewPath -Value $viewContent
    }
}

# Update AdminController.cs
Set-Content -Path $adminFile -Value $adminContent
Write-Output "Refactoring fully complete and robustly processed!"
