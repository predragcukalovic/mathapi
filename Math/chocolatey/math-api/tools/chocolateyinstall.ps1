$ErrorActionPreference = 'Stop' # stop on all errors
$toolsDir   = "$(Split-Path -parent $MyInvocation.MyCommand.Definition)"

$packageArgs = @{
  packageName   = $env:ChocolateyPackageName
  fileType      = 'MSI' #only one of these: exe, msi, msu
  #url           = $url
  #url64bit      = $url64
  file         = Join-Path $toolsDir 'math-api.msi'

  softwareName  = 'Math*' #part or all of the Display Name as you see it in Programs and Features. It should be enough to be unique

  # MSI
  silentArgs    = "/qn /norestart ALLUSERS=1 /l*v `"$($env:TEMP)\$($packageName).$($env:chocolateyPackageVersion).MsiInstall.log`" " # ALLUSERS=1 DISABLEDESKTOPSHORTCUT=1 ADDDESKTOPICON=0 ADDSTARTMENU=0
  validExitCodes= @(0, 3010, 1641)
}

Install-ChocolateyPackage @packageArgs # https://docs.chocolatey.org/en-us/create/functions/install-chocolateypackage
