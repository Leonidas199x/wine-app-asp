function renderMap(accessToken, longitude, latitude, zoom, isoCode, mapId, url, type) {
    mapboxgl.accessToken = accessToken;

    var map = new mapboxgl.Map({
        container: 'map',
        style: 'mapbox://styles/mapbox/streets-v11',
        center: [longitude, latitude],
        zoom: zoom
    });

    map.on('load', function () {
        map.addLayer({
            id: mapId,
            source: {
                type: 'vector',
                url: 'mapbox://' + url,
            },
            'source-layer': 'country_boundaries',
            type: 'fill',
            paint: {
                'fill-color': '#d2361e',
                'fill-opacity': 0.4,
            },
        },
            'country-label'
        );

        if (type === "country") {
            map.setFilter('country-boundaries', [
                "in",
                "iso_3166_1",
                isoCode
            ]);
        } else {
            map.setFilter('country-boundaries', [
                "in",
                "iso_3166_2",
                isoCode
            ]);
        }
    });
};