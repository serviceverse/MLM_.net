$baseDir = "C:\Users\Kuter\Documents\.net\mts\mt5UI\app"
$targetDir = "C:\Users\Kuter\Documents\.net\MLM_.net"

$pages = Get-ChildItem -Path $baseDir -Filter page.tsx -Recurse

# Group by first folder inside 'app'
$groups = @{}

foreach ($page in $pages) {
    $relPath = $page.FullName.Substring($baseDir.Length + 1)
    $parts = $relPath -split '\\'
    
    $controllerPart = $parts[0]
    if ($controllerPart.StartsWith('(') -and $controllerPart.EndsWith(')')) {
        $controllerPart = $controllerPart.Substring(1, $controllerPart.Length - 2)
    }
    
    $controllerName = (Get-Culture).TextInfo.ToTitleCase($controllerPart) + "Controller"
    
    $actionNameParts = @()
    for ($i = 1; $i -lt $parts.Length - 1; $i++) {
        $subParts = $parts[$i] -split '-'
        foreach ($sp in $subParts) {
            # remove square brackets for dynamic routes like [referralId]
            $sp = $sp -replace '\[', '' -replace '\]', ''
            if ($sp.Length -gt 0) {
                $actionNameParts += (Get-Culture).TextInfo.ToTitleCase($sp)
            }
        }
    }
    
    $actionName = $actionNameParts -join ''
    if ([string]::IsNullOrWhiteSpace($actionName)) {
        $actionName = "Index"
    }

    if (-not $groups.ContainsKey($controllerName)) {
        $groups[$controllerName] = @()
    }
    
    $groups[$controllerName] += [PSCustomObject]@{
        Action = $actionName
        OriginalPath = $relPath
    }
}

foreach ($ctrl in $groups.Keys) {
    $ctrlNameOnly = $ctrl.Replace("Controller", "")
    
    # 1. Generate Controller
    $ctrlPath = Join-Path $targetDir "Controllers\$ctrl.cs"
    $actions = $groups[$ctrl] | Select-Object -ExpandProperty Action -Unique
    
    $methods = ""
    foreach ($act in $actions) {
        $methods += @"
        public IActionResult $act()
        {
            return View();
        }

"@
    }
    
    $ctrlContent = @"
using Microsoft.AspNetCore.Mvc;
using MLM.Data;

namespace MLM.Controllers
{
    public class $ctrl : Controller
    {
        private readonly AppDBContext _context;

        public $ctrl(AppDBContext context)
        {
            _context = context;
        }

$methods
    }
}
"@
    Set-Content -Path $ctrlPath -Value $ctrlContent
    
    # 2. Generate Views
    $viewDir = Join-Path $targetDir "Views\$ctrlNameOnly"
    if (-not (Test-Path $viewDir)) {
        New-Item -ItemType Directory -Path $viewDir | Out-Null
    }
    
    foreach ($item in $groups[$ctrl]) {
        $act = $item.Action
        $viewPath = Join-Path $viewDir "$act.cshtml"
        if (-not (Test-Path $viewPath)) {
            $viewContent = @"
@{
    ViewData["Title"] = "$act";
}

<div class="min-h-screen bg-primary text-[var(--text-primary)] p-8">
    <div class="max-w-6xl mx-auto space-y-8">
        <div>
            <h1 class="text-4xl font-bold text-[var(--text-primary)] mb-2">$act</h1>
            <p class="text-[var(--text-secondary)]">Migrated from: $($item.OriginalPath)</p>
        </div>
        <div class="bg-[var(--bg-secondary)] border border-[var(--border-primary)] rounded-3xl p-8">
            <p class="text-[var(--text-tertiary)]">Content for $act will go here.</p>
        </div>
    </div>
</div>
"@
            Set-Content -Path $viewPath -Value $viewContent
        }
    }
}

Write-Output "Successfully scaffolded all controllers and views."
