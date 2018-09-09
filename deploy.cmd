IF NOT EXIST "Tools" (md "Tools")
IF NOT EXIST "Tools\Addins" (md "Tools\Addins")
nuget install Cake -ExcludeVersion -OutputDirectory "Tools"
.\Tools\Cake\Cake.exe kududeploy.cake --target="Kudu-Sync" -verbosity=Verbose