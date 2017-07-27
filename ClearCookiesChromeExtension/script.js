// NOTE: Inside of this function there is no access to console.log so it
// unfortunately can't be leveraged for debugging
function functionToExecute() {
	// Clear local storage
	localStorage.clear();
	
	// Clear session storage
	sessionStorage.clear();
	
	// Clear cookies
	document.cookie.split(";").forEach(function(c) { document.cookie = c.replace(/^ +/, "").replace(/=.*/, "=;expires=" + new Date().toUTCString() + ";path=/"); });
};

chrome.browserAction.onClicked.addListener(function() {
	chrome.tabs.executeScript( null, { code: "(" + functionToExecute.toString() + ")()" });
});