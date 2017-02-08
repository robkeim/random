if ($args.count -ne 2)
{
	Write-Host "Usage: Copy.ps1 SOURCE DESTINATION"
	exit;
}

$src = $args[0]
$dst = $args[1]

if (Test-Path -Path $dst)
{
	Write-Host "Destination folder must not exist (robocopy overwites the contents)"
	exit
}

& robocopy "$src" "$dst" "/MIR" "/R:30" "/W:2" "/NS" "/NP" "/NC" "/NFL" "/NDL" "/MT"