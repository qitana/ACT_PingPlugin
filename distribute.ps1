# ビルド
$msbuild = $null
$msbuild_exe = @(
    "C:\Program Files (x86)\Microsoft Visual Studio\2019\Enterprise\MSBuild\Current\Bin\MSBuild.exe", 
    "C:\Program Files (x86)\Microsoft Visual Studio\2019\Professional\MSBuild\Current\Bin\MSBuild.exe", 
    "C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\MSBuild.exe",
    "C:\Program Files (x86)\Microsoft Visual Studio\2019\BuildTools\MSBuild\Current\Bin\MSBuild.exe")
foreach ($m in $msbuild_exe) {
    if (Test-Path $m) {
        $msbuild = $m
        break
    }
}
if ($null -eq $msbuild) {
    Write-Output ("MSBuild.exe が見つかりませんでした。Visual Studio または Build Tools をインストールしてください。")
	pause
	return
}
& $msbuild PingPlugin.sln /nologo /v:minimal /t:Clean /p:Configuration=Release /p:Platform="Any CPU" | Write-Output
& $msbuild PingPlugin.sln /nologo /v:minimal /t:Restore /p:Configuration=Release /p:Platform="Any CPU" | Write-Output
& $msbuild PingPlugin.sln /nologo /v:minimal /t:Rebuild /p:Configuration=Release /p:Platform="Any CPU" | Write-Output


# バージョン取得
$version = [System.Diagnostics.FileVersionInfo]::GetVersionInfo(".\PingPlugin\bin\Release\PingPlugin.dll").FileVersion

# フォルダ名
$buildFolder = ".\PingPlugin\bin\Release"
$fullFolder = ".\Distribute\PingPlugin-" + $version

# フォルダが既に存在するなら消去
if ( Test-Path $fullFolder -PathType Container ) {
	Remove-Item -Recurse -Force $fullFolder
}

# フォルダ作成
New-Item -ItemType directory -Path $fullFolder

# full
xcopy /Y /R /S /EXCLUDE:full.exclude "$buildFolder\*" "$fullFolder"

cd Distribute
$folder = "PingPlugin-" + $version

# アーカイブ
& "C:\Program Files\7-Zip\7z.exe" "a" "$folder.7z" "$folder"

pause
