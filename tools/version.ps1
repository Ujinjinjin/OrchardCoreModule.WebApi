param([String]$ProjectName, [String]$ToolsPath)

$newVersion = (python $ToolsPath/main.py -n $ProjectName $args) | Out-String

Write-Output "##vso[task.setvariable variable=PackageVersion;]$newVersion"
