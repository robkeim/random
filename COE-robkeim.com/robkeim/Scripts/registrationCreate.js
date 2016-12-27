$(document).ready(function ()
{
    $("input[name='AddressButtons']").change(function ()
    {
        var shouldShow = $("input[name='AddressButtons']:checked").val() === "Other";
        $("#Address").toggle(shouldShow);
    });

    // Restore the checkbox to the previous value
    var btn = $("#AddressLine1").val();
    if (btn !== "" && btn !== "Smith" && btn !== "Phillips")
    {
        btn = "Other";
    }

    $("input[name='AddressButtons'][value='" + btn + "']").attr('checked', 'true');

    $("#Participating").change(function ()
    {
        var shouldShow = $(this).val() === "Yes";

        $("#participantInfo").toggle(shouldShow);
    });
});

function OnFormSubmit()
{
    var checkBoxValue = $("input[name='AddressButtons']:checked").val();

    if (checkBoxValue !== "Other")
    {
        $("#AddressLine1").val(checkBoxValue);
        $("#AddressLine2").val("");
    }
};