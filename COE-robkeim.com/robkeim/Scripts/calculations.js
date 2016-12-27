$(document).ready(function ()
{
    $("th[class='verticalName']").each(function ()
    {
        var name = $(this).html().trim();
        name = name.split(" ")[0];

        var newName = "";
        for (var i = 0; i < name.length; i++)
        {
            newName += name.charAt(i) + "<br />";
        }

        $(this).html(newName);
        $(this).attr("style", "vertical-align: bottom");
    });
});