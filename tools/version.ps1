param([String]$ProjectName, [String]$ToolsPath, [String]$Args)

Write-Output $ProjectName
Write-Output $ToolsPath
Write-Output $Args

$newVersion = (python $ToolsPath/main.py -n $ProjectName $Args) | Out-String

Write-Output "##vso[task.setvariable variable=PackageVersion;]$newVersion"
