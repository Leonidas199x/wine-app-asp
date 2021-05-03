function deleteEntity(controller, id, name, singular, plural) {
    if (confirm(`Are you sure you want to delete '${name}' from the list of available ${plural}?`)) {
        $.ajax({
            url: `/${controller}/delete/${id}`,
            type: 'DELETE',
            error: function (err) {
                console.log(err);
                alert(`Something went wrong, cannot delete ${singular} '${name}' and ID ${id}. Error code: ${err.status}`);
            },
            success: function () {
                window.location.href = `/${controller}/list`;
            },
            async: true,
        });
    }
}