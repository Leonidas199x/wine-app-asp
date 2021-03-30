function deleteCountry(id, name) {
    if (confirm(`Are you sure you want to delete '${name}' from the list of available countries?`)) {
        $.ajax({
            url: `/Country/Delete/${id}`,
            type: 'DELETE',
            error: function (err) {
                console.log(err);
                alert(`Something went wrong, cannot delete country with name '${name}' and ID ${id}. Error code: ${err.status}`);
            },
            success: function () {
                window.location.href = `/Country/List`;
            },
            async: true,
        });
    }
}