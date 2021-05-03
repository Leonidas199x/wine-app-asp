function deleteDrinker(id, name) {
    if (confirm(`Are you sure you want to delete '${name}' from the list of available drinkers?`)) {
        $.ajax({
            url: `/Drinker/Delete/${id}`,
            type: 'DELETE',
            error: function (err) {
                console.log(err);
                alert(`Something went wrong, cannot delete drinker with name '${name}' and ID ${id}. Error code: ${err.status}`);
            },
            success: function () {
                window.location.href = `/Drinker/List`;
            },
            async: true,
        });
    }
}