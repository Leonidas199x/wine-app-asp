function deleteGrapeColour(id, colour) {
    if (confirm(`Are you sure you want to delete '${colour}' from the list of available colours?`)) {
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

function deleteGrape(id, name) {
    if (confirm(`Are you sure you want to delete '${name}' from the list of available grapes?`)) {
        $.ajax({
            url: `/grape/delete/${id}`,
            type: 'DELETE',
            error: function (err) {
                console.log(err);
                alert(`Something went wrong, cannot delete grape '${name}' and ID ${id}. Error code: ${err.status}`);
            },
            success: function () {
                window.location.href = `/grape/listgrapes`;
            },
            async: true,
        });
    }
}