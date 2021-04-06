function deleteGrapeColour(id, colour) {
    if (confirm(`Are you sure you want to delete '${colour}' from the list of available countries?`)) {
        $.ajax({
            url: `/grape/deletecolour/${id}`,
            type: 'DELETE',
            error: function (err) {
                console.log(err);
                alert(`Something went wrong, cannot delete grape colour '${colour}' and ID ${id}. Error code: ${err.status}`);
            },
            success: function () {
                window.location.href = `/grape/listcolours`;
            },
            async: true,
        });
    }
}