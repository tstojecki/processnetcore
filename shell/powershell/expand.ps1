param (
    [string] $source,
    [string] $target
)
 
$zipFiles = get-childitem $source -recurse -include *.zip
 
$progress = 1
foreach ($zip in $zipFiles) {
    Write-Progress -Activity "unzipping to $($target)" -PercentComplete (($progress / ($zipfiles.Count + 1)) * 100) -CurrentOperation $zip.FullName -Status "File $($Progress) of $($zipFiles.Count)"

	Expand-Archive -Path $zip.FullName -DestinationPath $target -Force
 
    $progress++
}