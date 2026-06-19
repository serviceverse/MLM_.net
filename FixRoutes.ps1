$path = "d:\dotNet\MLM\MLM\Data\DbInitializer.cs"
$content = Get-Content $path -Raw

$evaluator = [System.Text.RegularExpressions.MatchEvaluator] {
    param($m)
    $val = $m.Groups[1].Value
    $segments = $val -split '/'
    $newSegments = foreach($seg in $segments) {
        if ([string]::IsNullOrEmpty($seg)) { continue }
        $subparts = $seg -split '-'
        $newSub = foreach($s in $subparts) {
            if ([string]::IsNullOrEmpty($s)) { continue }
            $s.Substring(0,1).ToUpper() + $s.Substring(1)
        }
        $newSub -join ''
    }
    $res = '/' + ($newSegments -join '/')
    return 'Route = "' + $res + '"'
}

$newContent = [System.Text.RegularExpressions.Regex]::Replace($content, 'Route = "(/[^"]+)"', $evaluator)
[IO.File]::WriteAllText($path, $newContent)
