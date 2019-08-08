
@if (!ViewData.ModelState.IsValid) {
    <text>
        $(document).ready(function() {
            alert(@this.ViewData["ErrorMessage"]);
});
            </text>
}
