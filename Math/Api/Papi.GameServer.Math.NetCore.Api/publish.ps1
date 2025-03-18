echo "Publishing Math Core API"

rmdir /S /Q publish
mkdir publish

dotnet publish -p:TargetFramework=net6.0 -c Release -o publish

$sourceDirectory = "..\Papi.GameServer.Math.Api\Data"
$destinationDirectory = ".\publish\Data"

Copy-Item -Path $sourceDirectory\* -Destination $destinationDirectory -Recurse -Force

$sourceDirectory = "..\Papi.GameServer.Math.Api\DataExt"
$destinationDirectory = ".\publish\DataExt"

Copy-Item -Path $sourceDirectory\* -Destination $destinationDirectory -Recurse -Force

echo "Publish completed"

pause
