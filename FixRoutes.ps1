$file = "C:\Users\Kuter\Documents\.net\MLM_.net\Data\DbInitializer.cs"
$content = Get-Content $file -Raw

# We need to replace Route = "/something/something-else" 
# With Route = "/Something/SomethingElse"

# Function to convert dash-case to PascalCase
function ConvertTo-PascalCase {
    param([string]$text)
    if ([string]::IsNullOrWhiteSpace($text)) { return $text }
    $parts = $text -split '-'
    $result = ""
    foreach ($p in $parts) {
        if ($p.Length -gt 0) {
            $result += [char]::ToUpper($p[0]) + $p.Substring(1)
        }
    }
    return $result
}

# Use regex to find Route = "..."
$pattern = 'Route\s*=\s*"(/[^"]+)"'
$matches = [regex]::Matches($content, $pattern)

foreach ($m in $matches) {
    $originalRoute = $m.Groups[1].Value
    $routeParts = $originalRoute -split '/'
    
    $newRoute = ""
    for ($i = 1; $i -lt $routeParts.Length; $i++) {
        if ($i -eq 1) {
            $newRoute += "/" + (ConvertTo-PascalCase $routeParts[$i])
        } else {
            # Combine the rest of the path into the Action name, just like ScaffoldAll.ps1 did
            if ($i -eq 2) {
                $newRoute += "/" + (ConvertTo-PascalCase $routeParts[$i])
            } else {
                $newRoute += (ConvertTo-PascalCase $routeParts[$i])
            }
        }
    }
    
    # Replace in content
    $searchString = 'Route = "' + $originalRoute + '"'
    $replaceString = 'Route = "' + $newRoute + '"'
    $content = $content.Replace($searchString, $replaceString)
}

# Specific manual fixes for complex/non-matching routes from JS logic vs scaffold logic:
# ScaffoldAll grouped by first folder.
# e.g., Route = "/all-reports/deposit-report" -> Controller "AllReports", Action "DepositReport"
# New route: "/AllReports/DepositReport" - which works correctly with the loop above!

Set-Content -Path $file -Value $content
Write-Output "DbInitializer.cs routes updated successfully."
