$targetDir = "C:\Users\Kuter\Documents\.net\MLM_.net"
$controllers = Get-ChildItem -Path "$targetDir\Controllers" -Filter "*-*.cs"

foreach ($file in $controllers) {
    $oldName = $file.Name
    $newName = $oldName -replace "-", ""
    $oldBase = $oldName.Replace("Controller.cs", "")
    $newBase = $newName.Replace("Controller.cs", "")
    
    # 1. Rename the file
    $newPath = Join-Path $file.DirectoryName $newName
    Rename-Item -Path $file.FullName -NewName $newName
    
    # 2. Update the class name inside the file
    $content = Get-Content $newPath -Raw
    $oldClassName = $oldName.Replace(".cs", "")
    $newClassName = $newName.Replace(".cs", "")
    
    $content = $content.Replace("public class $oldClassName", "public class $newClassName")
    $content = $content.Replace("public $oldClassName(", "public $newClassName(")
    Set-Content -Path $newPath -Value $content
    
    # 3. Rename the View directory
    $oldViewDir = Join-Path "$targetDir\Views" $oldBase
    $newViewDir = Join-Path "$targetDir\Views" $newBase
    
    if (Test-Path $oldViewDir) {
        Rename-Item -Path $oldViewDir -NewName $newBase
    }
}
Write-Output "Fixed controllers with hyphens."
