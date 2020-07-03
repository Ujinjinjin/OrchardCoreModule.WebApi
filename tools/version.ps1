param([String]$ProjectName, [String]$ToolsPath)

Write-Output "Starting script..."

$newVersion = (python $ToolsPath/main.py -n $ProjectName $args) | Out-String

Write-Output "New version: $newVersion"

Write-Output "##vso[task.setvariable variable=PackageVersion;]$newVersion"
