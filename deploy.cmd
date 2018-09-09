IF NOT EXIST "Tools" (md "Tools")
IF NOT EXIST "Tools\Addins" (md "Tools\Addins")
nuget install Cake -ExcludeVersion -OutputDirectory "Tools"
.\Tools\Cake\Cake.exe build.cake --target="Kudu-Sync" -verbosity=Verbose
