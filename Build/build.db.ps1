[System.Reflection.Assembly]::LoadWithPartialName('Microsoft.SqlServer.SMO') | out-null

$srv = new-Object Microsoft.SqlServer.Management.Smo.Server("localhost")

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
  Write-Host "Running script" $_.Name
  Invoke-Sqlcmd -Inputfile $_.FullName -ServerInstance localhost -Database BE
}
