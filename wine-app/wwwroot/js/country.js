function deleteCountry(id, name) {
    if (confirm(`Are you sure you want to delete ${name} from the available countries?`)) {
        $.ajax({
            url: `/Country/Delete/${id}`,
            type: 'DELETE',
            error: function (err) {
                console.log(err);
                alert('Something went wrong...check the console for the error');
            },
            success: function () {
                window.location.href = `/Country/List`;
            },
            async: true,
        });
    }
}