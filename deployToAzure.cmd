IF NOT EXIST "Tools" (md "Tools")
IF NOT EXIST "Tools\Addins" (md "Tools\Addins")
nuget install Cake -Version 0.32.1 -ExcludeVersion -OutputDirectory "Tools"
.\Tools\Cake\Cake.exe deployToAzure.cake --target="Kudu-Sync" -verbosity=Verbose
