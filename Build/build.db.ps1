[System.Reflection.Assembly]::LoadWithPartialName('Microsoft.SqlServer.SMO') | out-null

$srv = new-Object Microsoft.SqlServer.Management.Smo.Server("localhost")

$dbExists = $FALSE
foreach ($currDb in $srv.databases) {
  if ($currDb.name -eq "BB") {
    Write-Host "BB already exists."
    $db = $currDb
    $dbExists = $TRUE
  }
}

if ($dbExists -eq $FALSE) {
  $db = New-Object Microsoft.SqlServer.Management.Smo.Database($srv, "BB")
  $db.Create()
}

Get-ChildItem ".\Database\tsql" -Filter *.sql | 
Foreach-Object {
  Write-Host "Running script" $_.Name
  Invoke-Sqlcmd -Inputfile $_.FullName -ServerInstance localhost -Database BB
}
