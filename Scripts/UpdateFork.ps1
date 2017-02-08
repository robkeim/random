# Pulls from upstream/master and syncs to origin/master

$curBranch = git branch | select -first 1
$curBranch = $curBranch.Substring(2)

git fetch -p upstream
git fetch -p

git checkout master

git merge upstream/master

git push

git checkout $curBranch