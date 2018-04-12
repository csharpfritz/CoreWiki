$CakeVersion = "0.26.1"

# Make sure tools folder exists
$PSScriptRoot = Split-Path $MyInvocation.MyCommand.Path -Parent
$ToolPath = Join-Path $PSScriptRoot "tools"
if (!(Test-Path $ToolPath)) {
    Write-Verbose "Creating tools directory..."
    New-Item -Path $ToolPath -Type directory | out-null
}

###########################################################################
# INSTALL CAKE
###########################################################################

Add-Type -AssemblyName System.IO.Compression.FileSystem
Function Unzip
{
    param([string]$zipfile, [string]$outpath)

    [System.IO.Compression.ZipFile]::ExtractToDirectory($zipfile, $outpath)
}


# Make sure Cake has been installed.
$CakePath = Join-Path $ToolPath "Cake.CoreCLR.$CakeVersion"
$CakeDllPath = Join-Path $CakePath "Cake.dll"
$CakeZipPath = Join-Path $ToolPath "Cake.zip"
if (!(Test-Path $CakeDllPath)) {
    Write-Host "Installing Cake $CakeVersion..."
     (New-Object System.Net.WebClient).DownloadFile("https://www.nuget.org/api/v2/package/Cake.CoreCLR/$CakeVersion", $CakeZipPath)
     Unzip $CakeZipPath $CakePath
     Remove-Item $CakeZipPath
}

###########################################################################
# RUN BUILD SCRIPT
###########################################################################
& dotnet "$CakeDllPath" ./build.cake --bootstrap
if ($LASTEXITCODE -eq 0)
{
    & dotnet "$CakeDllPath" ./build.cake $args
}
exit $LASTEXITCODE