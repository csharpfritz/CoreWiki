#addin nuget:?package=Cake.Kudu&version=0.8.0

if (!Kudu.IsRunningOnKudu)
{
	throw new Exception("Not building on Kudu.");
}

var deploymentPath = Kudu.Deployment.Target;
if (!DirectoryExists(deploymentPath))
{
    throw new DirectoryNotFoundException(
	string.Format(
	    "Deployment target directory not found {0}",
	    deploymentPath
	    )
	);
}


Task("Kudu-Sync")
    .IsDependentOn("Publish")
    .Does( () => {
        Information("Deploying web from {0} to {1}", publishPath, deploymentPath);
        Kudu.Sync(publishPath);
});	

#load "build.cake"