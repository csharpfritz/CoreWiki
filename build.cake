///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var npmPath = IsRunningOnWindows()
                ? Context.Tools.Resolve("npm.cmd")
                : Context.Tools.Resolve("npm");

//////////////////////////////////////////////////////////////////////
// PARAMETERS
//////////////////////////////////////////////////////////////////////

var project = "CoreWiki";
var solution = $"./{project}.sln";
var publishPath = MakeAbsolute(Directory("./output"));

//////////////////////////////////////////////////////////////////////
// HELPERS
//////////////////////////////////////////////////////////////////////

Action<FilePath, DirectoryPath, ProcessArgumentBuilder> Cmd => (path, workingPath, args) => {
    var result = StartProcess(
        path,
        new ProcessSettings {
            Arguments = args,
            WorkingDirectory = workingPath
        });

    if(0 != result)
    {
        throw new Exception($"Failed to execute tool {path.GetFilename()} ({result})");
    }
};

///////////////////////////////////////////////////////////////////////////////
// TASKS
///////////////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does( () => {
        CleanDirectories($"./{project}/obj/**/*.*");
        CleanDirectories($"./{project}/bin/{configuration}/**/*.*");
});

Task("Clean-Publish")
    .IsDependentOn("Clean")
    .Does( () => {
        CleanDirectory(publishPath);
});

Task("NPM-Install")
    .IsDependentOn("Clean")
    .Does( ()=> {
    Cmd(npmPath,
        $"./{project}",
        new ProcessArgumentBuilder()
            .Append("install")
    );
});

Task("Restore")
    .IsDependentOn("NPM-Install")
    .IsDependentOn("Clean")
    .Does( () => {
    DotNetCoreRestore(solution);
});

Task("Build")
    .IsDependentOn("Restore")
    .Does( () => {
    DotNetCoreBuild(solution,
        new DotNetCoreBuildSettings {
            NoRestore = true,
            Configuration = configuration
        });
});

Task("Publish")
    .IsDependentOn("Restore")
    .IsDependentOn("Clean-Publish")
    .Does( () => {
    DotNetCorePublish(solution,
        new DotNetCorePublishSettings {
            NoRestore = true,
            Configuration = configuration,
            OutputDirectory = publishPath
        });
});

Task("Default")
    .IsDependentOn("Build");

RunTarget(target);