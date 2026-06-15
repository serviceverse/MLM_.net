$baseDir = "C:\Users\Kuter\Documents\.net\MLM_.net\Controllers"
$controllers = Get-ChildItem -Path $baseDir -Filter "*Controller.cs"

foreach ($file in $controllers) {
    if ($file.Name -eq "AdminController.cs" -or $file.Name -eq "AuthController.cs") {
        continue
    }

    $content = Get-Content $file.FullName -Raw
    
    # Extract all action names correctly
    $pattern = 'public\s+(?:async\s+Task<IActionResult>|IActionResult)\s+([A-Za-z0-9_]+)\('
    $matches = [regex]::Matches($content, $pattern)
    
    $actions = @()
    foreach ($m in $matches) {
        $actionName = $m.Groups[1].Value
        if ($actions -notcontains $actionName) {
            $actions += $actionName
        }
    }

    $className = $file.Name.Replace(".cs", "")

    # Rebuild the file from scratch
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

    Set-Content -Path $file.FullName -Value $newContent
    Write-Output "Cleaned and rebuilt $($file.Name) with $($actions.Count) actions."
}
