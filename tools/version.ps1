$newVersion = (python version.py $(ProjectName)) | Out-String

Write-Output "##vso[task.setvariable variable=PackageVersion;]$newVersion"
