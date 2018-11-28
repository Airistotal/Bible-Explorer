[System.Reflection.Assembly]::LoadWithPartialName('Microsoft.SqlServer.SMO') | out-null

$srv = new-Object Microsoft.SqlServer.Management.Smo.Server("(local)\sqlexpress")

$dbExists = $FALSE
foreach ($currDb in $srv.databases) {
  if ($currDb.name -eq "BE") {
    Write-Host "BE already exists."
    $db = $currDb
    $dbExists = $TRUE
  }
}

if ($dbExists -eq $FALSE) {
  $db = New-Object Microsoft.SqlServer.Management.Smo.Database($srv, "BE")
  $db.Create()
}

Get-ChildItem ".\Database\tsql" -Filter *.sql | 
Foreach-Object {
  $content = (Get-Content $_.FullName) -join [Environment]::NewLine

  Write-Host $content

  $Batch = New-Object -TypeName:Collections.Specialized.StringCollection
  $Batch.AddRange($content)

  $db.ExecuteWithResults($Batch)
}