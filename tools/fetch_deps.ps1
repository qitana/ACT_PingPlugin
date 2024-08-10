#
# Fetch dependencies from deps.json
#

# get base path
$base = (Get-Item $PSCommandPath).Directory.Parent.FullName

# set paths
$deps_path = (Join-Path $base "deps.json")
$cache_dir_path = (Join-Path $base ".cache_deps")


# if deps.json not exists, exit with error
if (-not (Test-Path $deps_path)) {
  Write-Error "deps.json not found"
  exit 1
}

# if cache_dir_path not exists, create it
if (-not (Test-Path $cache_dir_path)) {
  New-Item -ItemType Directory -Path $cache_dir_path
}

# load deps.json
$deps = Get-Content $deps_path | ConvertFrom-Json

# load keys from deps
$keys = $deps | Get-Member -MemberType NoteProperty | Select-Object -ExpandProperty Name

foreach ($key in $keys) {
  $dep = $deps.$key
  $url = $dep.url
  $filename = $dep.filename
  $dest = $dep.dest
  $stripRootFolder = $dep.stripRootFolder
  $sha256 = $dep.sha256

  $cache_file_path = (Join-Path $cache_dir_path $filename)
  $dest_path = (Join-Path $base $dest)

  # if cache_file_path exists, check sha256 match. if not match, delete it and download it.
  if (Test-Path $cache_file_path) {
    Write-Host "Found $filename in cache"
    $hash = Get-FileHash -Path $cache_file_path -Algorithm SHA256
    if ($null -ne $sha256 -and $hash.Hash -ne $sha256) {
      Write-Host "SHA256 not match for $filename, deleting it"
      Remove-Item $cache_file_path
    }
  }

  # if cache_file_path not exists, download it
  if (-not (Test-Path $cache_file_path)) {
    Write-Host "Downloading $filename from $url"
    Invoke-WebRequest -Uri $url -OutFile $cache_file_path
  }

  # if sha256 not match, exit with error
  $hash = Get-FileHash -Path $cache_file_path -Algorithm SHA256
  if ($null -ne $sha256 -and $hash.Hash -ne $sha256) {
    Write-Error "SHA256 not match for $filename"
    exit 1
  }

  # if dest_path exists, delete it
  if (Test-Path $dest_path) {
    Write-Host "Deleting $dest_path"
    Remove-Item $dest_path -Recurse
  }

  # expand cache_file_path to dest_path
  Write-Host "Expanding $filename to $dest_path"
  if ($stripRootFolder -eq $true) {
    $tmp_path = $dest_path + "_tmp"
    Expand-Archive -Path $cache_file_path -DestinationPath $tmp_path
    Move-Item -Path (Get-ChildItem -Path $tmp_path -Directory).FullName -Destination $dest_path
    Remove-Item $tmp_path
  }
  else {
    Expand-Archive -Path $cache_file_path -DestinationPath $dest_path
  }
}
