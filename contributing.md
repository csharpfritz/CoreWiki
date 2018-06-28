# Contributing

When contributing to this repository, please first discuss the change you wish to make via issue with the owners and maintainers of this repository before making a change. 

Please note we have a code of conduct, please follow it in all your interactions with the project.

## Pull Request Process

So you want to submit some changes to the project, awsome! The CoreWiki project welcomes new contributors. This section will guide you through the contribution process.

### Step 1: Fork

Fork CoreWiki [on GitHub](https://github.com/csharpfritz/CoreWiki) and checkout your local copy

```
> git clone git@github.com:your-github-username/CoreWiki.git
> cd CoreWiki
> git remote add upstream git://github.com/csharpfritz/CoreWiki.git
```

### Step 2: Branch

Create a feature branch for your code and start hacking away.

```
> git checkout -b my-new-feature -t upstream/dev
```

### Step 3: Commit

**Writing good commit messages is important.** A commit message should describe what changed, why, and reference issues closed (if any).  Follow these guidelines when writing one:

1. The first line should be around 50 characters or less and contain a short description of the change.
2. Keep the second line blank.
3. Wrap all other lines at 72 columns.
4. Include `Closes #N` where *N* is the issue number the commit fixes, if any.

The first line must be meaningful as it's what people see when they run `git shortlog` or `git log --oneline`.

### Step 4: Stay Up To Date

Periodically, you'll want to pull in the latest committed changes. Merge commits and clean-up commits from merges lowers the signal-to-noise ratio of your Pull Request. Rebase helps keep the commits on the branch about just your feature.

```
> git fetch upstream
> git pull --rebase
```

**note**: If a conflict comes up during a rebase, because of the way rebase works your changes will be on the *remote* side of the conflict while the pulled-in commits are the *local* side. This is probably opposite of how you are thinking about it but makes sense since rebase essentially stashes your branch commits, pulls in the new commits from the source, and finally re-applies your commits.  So your commits are now the "new" ones, hence the *remote* side.

### Step 5: Push

You're almost done!  Push the entire branch to your clone

```
> git push origin my-new-feature
```

Go to https://github.com/your-github-username/CoreWiki.git, press *Pull Request*, and fill out the form.

Congratulations!

### Other Items of Note

1. Ensure any install or build dependencies are removed before the end of the layer when doing a build.
2. Update the README.md with details of changes to the interface, this includes new environment variables, exposed ports, useful file locations.
3. Please submit all pull-requests to the dev branch unless you are working on a formal project, in which case you should submit the pull-request to that project-specific branch.
4. Project maintainers may merge the Pull Request.

## Code of Conduct

### Our Pledge

In the interest of fostering an open and welcoming environment, we as
contributors and maintainers pledge to making participation in our project and
our community a harassment-free experience for everyone, regardless of age, body
size, disability, ethnicity, gender identity and expression, level of experience,
nationality, personal appearance, race, religion, or sexual identity and
orientation.

### Our Standards

Examples of behavior that contributes to creating a positive environment
include:

* Using welcoming and inclusive language
* Being respectful of differing viewpoints and experiences
* Gracefully accepting constructive criticism
* Focusing on what is best for the community
* Showing empathy towards other community members

Examples of unacceptable behavior by participants include:

* The use of sexualized language or imagery and unwelcome sexual attention or
advances
* Trolling, insulting/derogatory comments, and personal or political attacks
* Public or private harassment
* Publishing others' private information, such as a physical or electronic
  address, without explicit permission
* Other conduct which could reasonably be considered inappropriate in a
  professional setting

### Our Responsibilities

Project maintainers are responsible for clarifying the standards of acceptable
behavior and are expected to take appropriate and fair corrective action in
response to any instances of unacceptable behavior.

Project maintainers have the right and responsibility to remove, edit, or
reject comments, commits, code, wiki edits, issues, and other contributions
that are not aligned to this Code of Conduct, or to ban temporarily or
permanently any contributor for other behaviors that they deem inappropriate,
threatening, offensive, or harmful.

### Scope

This Code of Conduct applies both within project spaces and in public spaces
when an individual is representing the project or its community. Examples of
representing a project or community include using an official project e-mail
address, posting via an official social media account, or acting as an appointed
representative at an online or offline event. Representation of a project may be
further defined and clarified by project maintainers.

### Enforcement

Instances of abusive, harassing, or otherwise unacceptable behavior may be
reported by contacting the project team at [INSERT EMAIL ADDRESS]. All
complaints will be reviewed and investigated and will result in a response that
is deemed necessary and appropriate to the circumstances. The project team is
obligated to maintain confidentiality with regard to the reporter of an incident.
Further details of specific enforcement policies may be posted separately.

Project maintainers who do not follow or enforce the Code of Conduct in good
faith may face temporary or permanent repercussions as determined by other
members of the project's leadership.

### Attribution

This Code of Conduct is adapted from the [Contributor Covenant][homepage], version 1.4,
available at [http://contributor-covenant.org/version/1/4][version]

[homepage]: http://contributor-covenant.org
[version]: http://contributor-covenant.org/version/1/4/
